import { AssignmentService } from './../../core/services/assignment-data.service';
import { Assignment } from './../../models/assignment';
import { Component, OnInit, EventEmitter, Input, Output, ViewChild, ElementRef } from '@angular/core';
import { ModalModule } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-edit-assignment-component',
    templateUrl: 'edit.assignment.component.html'
})

export class EditassignmentComponent implements OnInit {

    @Input()
    assignment: Assignment;

    @Output()
    updated = new EventEmitter();

    editAssignment: Assignment;

    @ViewChild('staticModal') modal: any;

    constructor(private dataService: AssignmentService) {
        this.editAssignment = new Assignment();

         // this.editAssignment.id = this.assignment.id;
        // this.editAssignment.name = this.assignment.name;
    }

    ngOnInit() {


    }

    private Save() {

        this.updated.emit();
    }

    public Show() {
            this.modal.show();
    }

    // public addAssignment() {
    //    this.dataService
    //        .Add(this.assignment)
    //        .subscribe(() => {
    //            this.getAllAssignments();
    //            this.assignment = new Assignment();
    //        }, (error) => {
    //            console.log(error);
    //        });
    // }

    // public deleteAssignment(assignment: Assignment) {
    //    this.dataService
    //        .Delete(assignment.id)
    //        .subscribe(() => {
    //            this.getAllAssignments();
    //        }, (error) => {
    //            console.log(error);
    //        });
    // }

    // private getAllAssignments() {
    //    this.dataService
    //        .GetAll()
    //        .subscribe(
    //        data => this.assignments = data,
    //        error => console.log(error),
    //        () => console.log('Get all complete')
    //        );
    // }

    // public updateAssignment(assignment: Assignment) {
    //    console.log('Updating', assignment.id);
    // }
}

