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

    handleClose = () => {
        this.setState({ opened: false })
    };

    render() {
        return (
            <Dialog open={this.state.opened} onClose={this.handleClose} >
                <DialogTitle >Subscribe</DialogTitle>
                <DialogContent>
                <DialogContentText>
                        To subscribe to this website, please enter your email address here. We will send updates occasionally.
                </DialogContentText>
                    <TextField autoFocus margin="dense" id="name" label={this.props.rowData.name} type="email" fullWidth />
					<TextField autoFocus margin="dense" id="name" label={this.props.rowData.name} type="email" fullWidth />
					<TextField autoFocus margin="dense" id="name" label={this.props.rowData.name} type="email" fullWidth />
					<TextField autoFocus margin="dense" id="name" label={this.props.rowData.name} type="email" fullWidth />
					<TextField autoFocus margin="dense" id="name" label={this.props.rowData.name} type="email" fullWidth />
					<TextField autoFocus margin="dense" id="name" label={this.props.rowData.name} type="email" fullWidth />
                </DialogContent>
                <DialogActions>
                    <Button onClick={this.handleClose} color="primary">
                        {this.props.isNew ? 'Add' : 'Update'} 
                    </Button>
                    <Button onClick={this.handleClose} color="primary">
                        Cancel
                    </Button>
                </DialogActions>
            </Dialog>
        )
    }
}

export default EditInspection;