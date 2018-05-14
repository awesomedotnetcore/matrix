import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { PositioningService } from 'ngx-bootstrap/positioning';
import { ComponentLoaderFactory } from 'ngx-bootstrap/component-loader/component-loader.factory';
import { BsModalService, ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { MetadataService } from './services/metadata.service';
import { ApplicationService } from './services/application.service';
import { LogService } from './services/log.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    ModalModule.forRoot()
  ],
  providers: [PositioningService, ComponentLoaderFactory, BsModalService, MetadataService, ApplicationService, LogService],
  bootstrap: [AppComponent]
})
export class AppModule { }
