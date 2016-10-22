import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { MenuComponent } from './menu.component';
import { RoomComponent } from './room.component';

import { ConfigService } from './config.service';
import { NavigationService } from './navigation.service';
import { HubService } from './hub.service';

import { FormatPipe } from './format.pipe';

import { AppRoutingModule } from './app-routing.module';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        AppRoutingModule
    ],
    declarations: [
        AppComponent,
        MenuComponent,
        FormatPipe,
        RoomComponent
    ],
    providers: [
        NavigationService,
        ConfigService,
        HubService
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }