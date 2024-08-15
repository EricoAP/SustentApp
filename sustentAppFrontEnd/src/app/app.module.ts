import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';

//PAGES
import { LoginPageComponent } from './modules/login/login-page/login-page.component';
import { LoginFormComponent } from './modules/login/components/login-form/login-form.component';
import { RegisterPageComponent } from './modules/register/register-page/register-page.component';
import { RegisterFormComponent } from './modules/register/components/register-form/register-form.component';
import { ProfilePageComponent } from './modules/profile/profile-page/profile-page.component';
import { ForgotPageComponent } from './modules/forgotPassword/forgot-page/forgot-page.component';
import { ForgotFormComponent } from './modules/forgotPassword/components/forgot-form/forgot-form.component';
import { ProfileInfosFormComponent } from './modules/profile/components/profile-infos-form/profile-infos-form.component';
import { InfosContactFormComponent } from './modules/profile/components/infos-contact-form/infos-contact-form.component';
import { PointsPageComponent } from './modules/collectPoints/collectPointPages/points-page/points-page.component';
import { RequestPageComponent } from './modules/collectPoints/collectPointPages/request-page/request-page.component';
import { RecentsPageComponent } from './modules/collectPoints/collectPointPages/recents-page/recents-page.component';
import { OptionsPageComponent } from './modules/collectPoints/collectPointPages/options-page/options-page.component';
import { MediaPageComponent } from './modules/collectPoints/collectPointPages/media-page/media-page.component';
import { ResetFormComponent } from './modules/forgotPassword/components/reset-form/reset-form.component';
import { HeaderComponent } from './shared/header/header.component';
import { AuthenticationInterceptor } from './core/interceptors/authentication.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    LoginFormComponent,
    RegisterPageComponent,
    RegisterFormComponent,
    ProfilePageComponent,
    ForgotPageComponent,
    ForgotFormComponent,
    ProfileInfosFormComponent,
    InfosContactFormComponent,
    PointsPageComponent,
    RequestPageComponent,
    RecentsPageComponent,
    OptionsPageComponent,
    MediaPageComponent,
    ResetFormComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
