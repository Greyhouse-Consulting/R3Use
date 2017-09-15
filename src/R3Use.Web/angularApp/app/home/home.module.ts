import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { HomeRoutes } from './home.routes';
import { HomeComponent } from './components/home.component';
import { EditassignmentComponent } from './components/edit.assignment.component';

import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        HttpModule,
        HomeRoutes,
        ModalModule.forRoot(),
        ModalModule
    ],

    declarations: [
        HomeComponent,
        EditassignmentComponent
    ],

    exports: [
        HomeComponent,
        EditassignmentComponent
    ]
})

export class HomeModule { }
