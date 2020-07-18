import React, { Component } from "react";

class Results extends Component {
  constructor(props) {
    super(props);

    console.log(this.props.data);
  }

  parseDataProvider(dataProviderId) {
    switch (dataProviderId) {
      case 1:
        return "google";
      case 2:
        return "bing";
    }
  }

  render() {
    return (
      <div>
        <ul>
          {this.props.data.Items.map((item) => (
            <li>
              <div>Title:{item.Title}</div>
              <div>{this.parseDataProvider(item.DataProvider)}</div>
            </li>
          ))}
        </ul>
      </div>
    );
  }
}

export default Results;
