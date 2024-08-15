import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './modules/login/login-page/login-page.component';
import { RegisterPageComponent } from './modules/register/register-page/register-page.component';
import { ProfilePageComponent } from './modules/profile/profile-page/profile-page.component';
import { ForgotPageComponent } from './modules/forgotPassword/forgot-page/forgot-page.component';
import { PointsPageComponent } from './modules/collectPoints/collectPointPages/points-page/points-page.component';
import { RequestPageComponent } from './modules/collectPoints/collectPointPages/request-page/request-page.component';
import { RecentsPageComponent } from './modules/collectPoints/collectPointPages/recents-page/recents-page.component';
import { OptionsPageComponent } from './modules/collectPoints/collectPointPages/options-page/options-page.component';
import { MediaPageComponent } from './modules/collectPoints/collectPointPages/media-page/media-page.component';
import { AuthGuard } from './core/guards/auth.guard';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent},
  { path: 'forgot-password', component: ForgotPageComponent},
  { path: 'register', component: RegisterPageComponent},
  { path: 'profile', component: ProfilePageComponent, canActivate: [AuthGuard]},
  { path: 'collectPoints', component: PointsPageComponent, canActivate: [AuthGuard]},
  { path: 'requestCollect', component: RequestPageComponent, canActivate: [AuthGuard]},
  { path: 'recentsCollect', component: RecentsPageComponent, canActivate: [AuthGuard]},
  { path: 'optionsCollect', component: OptionsPageComponent, canActivate: [AuthGuard]},
  { path: 'mediaCollect', component: MediaPageComponent, canActivate: [AuthGuard]},
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
