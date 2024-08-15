import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { ForgotPageComponent } from '../../forgot-page/forgot-page.component';

@Component({
  selector: 'app-forgot-form',
  templateUrl: './forgot-form.component.html',
  styleUrls: ['./forgot-form.component.css']
})
export class ForgotFormComponent {

  forgotPasswordForm: FormGroup;

  constructor(private router: Router, private fb: FormBuilder, private authService: AuthService, private ForgotPageComponent : ForgotPageComponent) {
    this.forgotPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }
  
  sendEmail() {
    if (this.forgotPasswordForm.valid) {
      this.authService.forgotEmail(this.forgotPasswordForm.value).subscribe(
        response => {
          this.ForgotPageComponent.resetPassword(true)
        },
        error => {
          console.error('Erro ao enviar email', error);
        }
      );
    }
  }
}
