var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HcComponent } from './hc.component';
import { MenuComponent } from './menu.component';
import { RoomComponent } from './room.component';
import { HomeComponent } from './home.component';
import { ConfigService } from './config.service';
import { NavigationService } from './navigation.service';
import { HubService } from './hub.service';
import { FormatPipe } from './format.pipe';
import { HcRoutingModule } from './hc-routing.module';
let HcModule = class HcModule {
};
HcModule = __decorate([
    NgModule({
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
        bootstrap: [HcComponent]
    })
], HcModule);
export { HcModule };
//# sourceMappingURL=hc.module.js.map