import React, { Component } from 'react'
import EditInspection from './EditInspection'
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import MaterialTable, {MTableToolbar} from 'material-table'

class InspectionTable extends Component {

    constructor(props) {
        super(props);
    }

    state = {
        openInspection: false,
        isNew: false,
        rowData: {}
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
             .catch(error => console.log('Error:', error));

        var inspector = { 34: 'İstanbul', 63: 'Şanlıurfa' };
		var inspectionStatus = { 34: 'İstanbul', 63: 'Şanlıurfa' };

        this.setState({
            columns: [
                { title: 'Date', field: 'date' },
                { title: 'Customer', field: 'customer' },
                { title: 'Adress', field: 'adress' },
				{ title: 'Observation', field: 'observation' },
				{ title: 'Status', field: 'statusId', lookup: inspectionStatus },
                { title: 'Inspector', field: 'inspectorId', lookup: inspector },
            ]
        });
    }
	
	toggleEdition(isNew, rowData){	
		 this.setState({ openInspection: !this.state.openInspection, rowData: rowData, isNew: isNew }); 
	}	
	
	defaultInspection(){
		return {
			date: '1', customer: 'Baran1', adress: '313', observation: 'observation', statusId: 1, inspectorId: 2 
		};
	}	
	
	deleteInspection(toDelete)
	{
		alert('Delete')
	}
	
	handleChange()
	{
		alert('changed')
	}
	
    render() {
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
								<Select style="{}" onChange={this.handleChange} displayEmpty>
								  <MenuItem value="">
									<em>None</em>
								  </MenuItem>
								  <MenuItem value="1">Ten</MenuItem>
								  <MenuItem value="1">Twenty</MenuItem>
								  <MenuItem value="1">Thirty</MenuItem>
								</Select>								
							  </div>
							)
                }}			 
                 actions={[
                 {
                     icon: 'add',
                     tooltip: 'Add Inspection',
                     isFreeAction: true,
                     onClick: (event) => { this.toggleEdition(true, this.defaultInspection()); }
                 },
                 {
                     icon: 'save',
                     tooltip: 'Save inspection',
                     onClick: (event, rowData) => { this.toggleEdition(false, rowData); }
                 },
                 {
                     icon: 'delete',
                     tooltip: 'Delete inspection',
                     onClick: (event, rowData) => { if (window.confirm('Are you sure you wish to delete this item?')) this.deleteInspection(rowData) } 
                 }]}>
                </MaterialTable>
                {this.state.openInspection && <EditInspection rowData={this.state.rowData} isNew={this.state.isNew} />}
            </div>
        );
    }
}

export default InspectionTable;