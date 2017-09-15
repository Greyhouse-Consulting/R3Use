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
import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
var EditassignmentComponent = (function () {
    function EditassignmentComponent(dataService) {
        this.dataService = dataService;
        this.updated = new EventEmitter();
        this.editAssignment = new Assignment();
    }
    EditassignmentComponent.prototype.ngOnInit = function () {
    };
    EditassignmentComponent.prototype.Save = function () {
        this.updated.emit();
    };
    EditassignmentComponent.prototype.Show = function () {
        this.modal.show();
    };
    return EditassignmentComponent;
}());
__decorate([
    Input(),
    __metadata("design:type", Assignment)
], EditassignmentComponent.prototype, "assignment", void 0);
__decorate([
    Output(),
    __metadata("design:type", Object)
], EditassignmentComponent.prototype, "updated", void 0);
__decorate([
    ViewChild('staticModal'),
    __metadata("design:type", Object)
], EditassignmentComponent.prototype, "modal", void 0);
EditassignmentComponent = __decorate([
    Component({
        selector: 'app-edit-assignment-component',
        templateUrl: 'edit.assignment.component.html'
    }),
    __metadata("design:paramtypes", [AssignmentService])
], EditassignmentComponent);
export { EditassignmentComponent };
//# sourceMappingURL=edit.assignment.component.js.map