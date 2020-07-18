import React, { Component } from "react";
import axios from "axios";
import Results from "./results";

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      data: null,
    };

    this.handleClick = this.handleClick.bind(this);
  }

  handleClick() {
    let term = document.getElementById("txtTerm").value;
    axios.get(`https://localhost:44359/api/search?term=${term}`).then((res) => {
      this.setState({ data: res.data });
    });

    // let tempData = {
    //   Items: [
    //     {
    //       Title: "yaniv1",
    //       CreatedDate: "2020-07-18T13:10:32.3691259+03:00",
    //       DataProvider: 1,
    //     },
    //     {
    //       Title: "yaniv2",
    //       CreatedDate: "2020-07-17T13:10:32.3691259+03:00",
    //       DataProvider: 1,
    //     },
    //   ],
    // };
  }

  render() {
    const renderResults = () => {
      if (this.state.data) {
        return <Results data={this.state.data}></Results>;
      }
    };

    return (
      <div>
        <div>
          <span>Enter Search Term</span>
          <input id="txtTerm" type="textbox"></input>
          <span>
            <button onClick={this.handleClick}>Search</button>
          </span>
        </div>
        {renderResults()}
      </div>
    );
  }
}
export default App;
