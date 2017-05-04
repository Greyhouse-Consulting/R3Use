var __assign = (this && this.__assign) || Object.assign || function(t) {
    for (var s, i = 1, n = arguments.length; i < n; i++) {
        s = arguments[i];
        for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
            t[p] = s[p];
    }
    return t;
};
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { AssignmentService } from './../../core/services/assignment-data.service';
import { Assignment } from './../../models/assignment';
import { Component, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
var HomeComponent = (function () {
    function HomeComponent(dataService) {
        this.dataService = dataService;
        this.assignments = [];
        this.assignment = new Assignment();
        this.selectedAssignment = new Assignment();
        this.message = 'Assignments from the ASP.NET Core API';
    }
    HomeComponent.prototype.ngOnInit = function () {
        this.getAllAssignments();
    };
    HomeComponent.prototype.addAssignment = function () {
        var _this = this;
        this.dataService
            .Add(this.assignment)
            .subscribe(function () {
            _this.getAllAssignments();
            _this.assignment = new Assignment();
        }, function (error) {
            console.log(error);
        });
    };
    HomeComponent.prototype.deleteAssignment = function (assignment) {
        var _this = this;
        this.dataService
            .Delete(assignment.id)
            .subscribe(function () {
            _this.getAllAssignments();
        }, function (error) {
            console.log(error);
        });
    };
    HomeComponent.prototype.getAllAssignments = function () {
        var _this = this;
        this.dataService
            .GetAll()
            .subscribe(function (data) { return _this.assignments = data; }, function (error) { return console.log(error); }, function () { return console.log('Get all complete'); });
    };
    HomeComponent.prototype.updateAssignment = function (assignment) {
        console.log('Updating', assignment.id);
    };
    HomeComponent.prototype.onUpdated = function () {
        this.getAllAssignments();
    };
    HomeComponent.prototype.showEdit = function (assignment) {
        this.selectedAssignment = __assign({}, assignment);
        this.childModal.show();
    };
    HomeComponent.prototype.hideChildModal = function () {
        var _this = this;
        this.dataService
            .Update(this.selectedAssignment.id, this.selectedAssignment)
            .subscribe(function () {
            _this.getAllAssignments();
        }, function (error) {
            console.log(error);
        });
        this.childModal.hide();
    };
    return HomeComponent;
}());
__decorate([
    ViewChild('childModal'),
    __metadata("design:type", ModalDirective)
], HomeComponent.prototype, "childModal", void 0);
HomeComponent = __decorate([
    Component({
        selector: 'home-component',
        templateUrl: 'home.component.html'
    }),
    __metadata("design:paramtypes", [AssignmentService])
], HomeComponent);
export { HomeComponent };
//# sourceMappingURL=home.component.js.map