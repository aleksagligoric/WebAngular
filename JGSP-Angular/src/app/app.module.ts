import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router'
import { HttpService } from './services/http.service';
import { from } from 'rxjs';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AuthHttpService } from './services/auth.service';
import { PrvaKomponentaComponent } from 'src/app/prva-komponenta/prva-komponenta.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule } from '@angular/material'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainNavComponent } from './main-nav/main-nav.component';
import {RedVoznjeComponent} from '../app/red-voznje/red-voznje.component'
import { RedVoznjeHttpService } from './services/redvoznje.service';
import { AdminRedVoznjeControlComponent } from './admin-red-voznje-control/admin-red-voznje-control.component';

const routes: Routes = [
  {path: "", component: HomeComponent},
  {path: "home", component: HomeComponent},
  {path: "login", component: LoginComponent},
  {path: "registracija", component: RegistracijaComponent},
  {path: "redvoznje",component:RedVoznjeComponent},
  {path:"adminredvoznjecontrol",component:AdminRedVoznjeControlComponent},
  {path: "**", redirectTo: "home"}
]

@NgModule({
  declarations: [
    AppComponent,
    PrvaKomponentaComponent,
    HomeComponent,
    LoginComponent,
    RegistracijaComponent,
    MainNavComponent,
    RedVoznjeComponent,
    AdminRedVoznjeControlComponent,
 
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule
  ],

  providers: [HttpService, {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}, AuthHttpService,RedVoznjeHttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
