using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace BoltTest.Controllers
{
    public abstract class DataProvider : IDataProvider
    {
        
        protected abstract string BaseUrl { get; }
        protected abstract List<Item> ParseResult(string html);
        protected abstract ProviderType ProviderType { get; }

        protected const string _ExceptionBaseMessage = "Failed getting search result from";
        protected const int _NumberOfResults = 5;
        private List<Item> _results;

        public List<Item> Search(string term)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{BaseUrl}{term}");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    _results = ParseResult(reader.ReadToEnd());
                }

                Save();

                return _results;
            }
            catch (Exception ex)
            {
                throw new Exception($"{_ExceptionBaseMessage} {Enum.GetName(typeof(ProviderType), ProviderType)}", ex);
            }
        }
        public static IDataProvider GetSearchProvider(ProviderType dataProvider)
        {
            switch (dataProvider)
            {
                case ProviderType.Google:
                    return new GoogleDataProvider();
                case ProviderType.Bing:
                    return new BingDataProvider();
            }
            return null;
        }

        private void Save()
        {
            try
            {
                using (var conn = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={AppDomain.CurrentDomain.BaseDirectory}App_Data\\Database1.mdf;Integrated Security=True"))
                {
                    using (var cmd = new SqlCommand("InsertSearchResults", conn))
                    {
                        conn.Open();
                        foreach (var item in _results)
                        {
                            cmd.Parameters.Add(new SqlParameter("@Title", item.Title));
                            cmd.Parameters.Add(new SqlParameter("@provider", Enum.GetName(typeof(ProviderType), item.DataProvider)));
                            cmd.Parameters.Add(new SqlParameter("@EnteredDate", DateTime.Now));

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();

                            cmd.Parameters.Clear();
                        }

                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed inserting data to DB", ex);
            }        
        }
    }
}
