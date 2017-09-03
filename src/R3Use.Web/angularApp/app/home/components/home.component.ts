import { AssignmentService } from './../../core/services/assignment-data.service';
import { Assignment } from './../../models/assignment';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'home-component',
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    @ViewChild('childModal') public childModal: ModalDirective;


    public message: string;
    public assignments: Assignment[] = [];
    public assignment: Assignment = new Assignment();

    public selectedAssignment: Assignment = new Assignment();

    
    constructor(private dataService: AssignmentService) {
        this.message = 'Assignments from the ASP.NET Core API';
    }

    ngOnInit() {
        this.getAllAssignments();
    }

    public addAssignment() {
        this.dataService
            .Add(this.assignment)
            .subscribe(() => {
                this.getAllAssignments();
                this.assignment = new Assignment();
            }, (error) => {
                console.log(error);
            });
    }

    public deleteAssignment(assignment: Assignment) {
        this.dataService
            .Delete(assignment.id)
            .subscribe(() => {
                this.getAllAssignments();
            }, (error) => {
                console.log(error);
            });
    }

    private getAllAssignments() {
        this.dataService
            .GetAll()
            .subscribe(
            data => this.assignments = data,
            error => console.log(error),
            () => console.log('Get all complete')
            );
    }

    public updateAssignment(assignment: Assignment) {
        console.log('Updating', assignment.id);
    }

    private onUpdated() {
        this.getAllAssignments();
    }

    public showEdit(assignment: Assignment) {

        this.selectedAssignment = { ...assignment };
        this.childModal.show();
    }
    public saveAndHide() {
        this.dataService
            .Update(this.selectedAssignment.id, this.selectedAssignment)
            .subscribe(() => {
                this.getAllAssignments();
            }, (error) => {
                console.log(error);
            });

        this.childModal.hide();
        
    }

    public hideChildModal(): void {


        this.childModal.hide();
    }
}
