import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NgxElectronModule } from 'ngx-electron';
import { ProfilerUiComponent } from './profiler-ui/profiler-ui.component';
import {AgGridModule} from "ag-grid-angular";
import {ProfilerDataService} from "./services/profiler-data.service";

@NgModule({
  declarations: [
    AppComponent,
    ProfilerUiComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgxElectronModule,
    AgGridModule.withComponents([]),
    RouterModule.forRoot([
      { path: '', component: ProfilerUiComponent, pathMatch: 'full' },
    ])
  ],
  providers: [
    ProfilerDataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
