import { Component } from '@angular/core';

@Component({
  selector: 'app-forgot-page',
  templateUrl: './forgot-page.component.html',
  styleUrls: ['./forgot-page.component.css']
})
export class ForgotPageComponent {
  step = false;

  resetPassword(step: boolean) {
    this.step = step;
    return this.step = true;
  }
}
