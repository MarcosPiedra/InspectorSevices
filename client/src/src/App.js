import React, { Component } from 'react'
import NavBar from './NavBar'
import InspectionTable from './InspectionTable'

class App extends Component {
  render() {
    return (
      <div>
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap" />
		<link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
        <NavBar />
		<InspectionTable />
      </div>
    )
  }
}
export default App