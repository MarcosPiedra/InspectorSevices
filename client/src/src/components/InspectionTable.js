import '../App.css';
import React, { Component } from 'react'
import EditInspection from './EditInspection'
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import MaterialTable, { MTableToolbar } from 'material-table'

class InspectionTable extends Component {
    constructor(props) {
        super(props);
        this.handleClose = this.handleClose.bind(this);
    }
    state = {
        openInspection: false,
        isNew: false,
        rowData: {},
        inspectors: {},
        inspectionStatus: {},
    }
    componentDidMount() {
        fetch('https://dog.ceo/api/breed/husky/images')
            .then(res => res.json())
            .then(res => this.setState({
                data:
                    [
                        { date: '1', customer: 'Baran1', adress: '313', observation: 'observation', statusId: 1, inspectorId: 2 },
                        { date: '2', customer: 'Baran2', adress: '323', observation: 'observation', statusId: 1, inspectorId: 2 },
                        { date: '3', customer: 'Baran3', adress: '333', observation: 'observation', statusId: 1, inspectorId: 2 },
                    ]
            }))
            .catch(error => console.log('Error retrieving inspection data:', error));

        fetch('https://dog.ceo/api/breed/husky/images')
            .then(res => res.json())
            .then(res => this.setState({
                inspectors:
                {
                    1: "Inspector 1",
                    2: "Inspector 2"
                }
            }))
            .catch(error => console.log('Error retrieving inspector data:', error));

        fetch('https://dog.ceo/api/breed/husky/images')
            .then(res => res.json())
            .then(res => this.setState({
                inspectionStatus:
                {
                    1: "Status 1",
                    2: "Status 2"
                }
            }))
            .catch(error => console.log('Error retrieving status data:', error));

        this.setState({
            columns: [
                { title: 'Date', field: 'date' },
                { title: 'Customer', field: 'customer' },
                { title: 'Adress', field: 'adress' },
                { title: 'Observation', field: 'observation' },
                { title: 'Status', field: 'statusId', lookup: { 1: "Status 1", 2: "Status 2" } },
                { title: 'Inspector', field: 'inspectorId', lookup: { 1: "Inspector 1", 2: "Inspector 2" } },
            ]
        });
    }

    showEdition(isNew, rowData) {
        this.setState({ openInspection: true });
        this.setState({ rowData: rowData, isNew: isNew });
    }

    defaultInspection() {
        return {
            date: '1', customer: 'default', adress: 'default', observation: 'default', statusId: 1, inspectorId: 2
        };
    }

    deleteInspection(toDelete) {
        alert('Delete')
    }

    handleChange() {
        alert('Changed')
    }
    handleClose() {
        alert(this.state.openInspection)
        this.setState({ openInspection: false });
    }
    render() {
        var inspectorOptions = [];
        Object.entries(this.state.inspectors).forEach(function ([key, value]) {
            inspectorOptions.push({ value: key, label: value })
        })
        return (
            <div>
                <MaterialTable
                    title="Inspections Table"
                    columns={this.state.columns}
                    data={this.state.data}
                    components={{
                        Toolbar: props => (
                            <div>
                                <MTableToolbar {...props} />
                                <Select onChange={this.handleChange}
                                    labelKey='label'
                                    valueKey='value'
                                    displayEmpty>
                                    {inspectorOptions.map((i) => <option key={i.value} value={i.value}>{i.label}</option>)}
                                </Select>
                            </div>
                        )
                    }}
                    actions={[
                        {
                            icon: 'add',
                            tooltip: 'Add Inspection',
                            isFreeAction: true,
                            onClick: (event) => { this.showEdition(true, this.defaultInspection()); }
                        },
                        {
                            icon: 'save',
                            tooltip: 'Save inspection',
                            onClick: (event, rowData) => { this.showEdition(false, rowData); }
                        },
                        {
                            icon: 'delete',
                            tooltip: 'Delete inspection',
                            onClick: (event, rowData) => { if (window.confirm('Are you sure you wish to delete this item?')) this.deleteInspection(rowData) }
                        }]}>
                </MaterialTable>
                {this.state.openInspection && <EditInspection rowData={this.state.rowData} isNew={this.state.isNew} handleClose={this.handleClose} />}
            </div>
        );
    }
}

export default InspectionTable;