import React, { Component } from 'react'
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';

class EditInspection extends Component {
    state = {
        opened: true
    }
    constructor(props) {
        super(props);
    }
    render() {
        return (
            <Dialog open={this.state.opened} onClose={this.props.handleClose} >
                <DialogTitle >Subscribe</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        To subscribe to this website, please enter your email address here. We will send updates occasionally.
                </DialogContentText>
                    <TextField autoFocus id="date" value={this.props.rowData.date} type="text" fullWidth />
                    <TextField autoFocus id="customer" value={this.props.rowData.customer} type="text" fullWidth />
                    <TextField autoFocus id="adress" value={this.props.rowData.adress} type="text" fullWidth />
                    <TextField autoFocus id="observation" value={this.props.rowData.observation} type="text" fullWidth />
                    <TextField autoFocus id="statusId" value={this.props.rowData.statusId} type="text" fullWidth />
                    <TextField autoFocus id="inspectorId" value={this.props.rowData.inspectorId} type="text" fullWidth />
                </DialogContent>
                <DialogActions>
                    <Button onClick={this.props.handleClose} color="primary">
                        {this.props.isNew ? 'Add' : 'Update'}
                    </Button>
                    <Button onClick={this.props.handleClose} color="primary">
                        Cancel
                    </Button>
                </DialogActions>
            </Dialog>
        )
    }
}

export default EditInspection;