import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { HcComponent } from './hc.component';
import { MenuComponent } from './menu.component';
import { RoomComponent } from './room.component';
import { HomeComponent } from './home.component'

import { ConfigService } from './config.service';
import { NavigationService } from './navigation.service';
import { HubService } from './hub.service';

import { FormatPipe } from './format.pipe';

import { HcRoutingModule } from './hc-routing.module';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HcRoutingModule
    ],
    declarations: [
        HcComponent,
        MenuComponent,
        FormatPipe,
        RoomComponent,
        HomeComponent
    ],
    providers: [
        NavigationService,
        ConfigService,
        HubService
    ],
    bootstrap: [ HcComponent ]
})
export class HcModule { }