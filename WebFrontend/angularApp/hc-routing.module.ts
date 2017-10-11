import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HcComponent } from './hc.component';
import { RoomComponent } from './room.component';
import { HomeComponent } from './home.component'

const routes: Routes = [
    { path: 'room/:name', component: RoomComponent },
    { path: '', component: HomeComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class HcRoutingModule { }