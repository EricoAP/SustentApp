import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-reset-form',
  templateUrl: './reset-form.component.html',
  styleUrls: ['./reset-form.component.css']
})
export class ResetFormComponent {
  resetPasswordForm: FormGroup;

  constructor(private router: Router, private fb: FormBuilder, private authService: AuthService) {
    this.resetPasswordForm = this.fb.group({
      token: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }
  
  sendReset() {
    if (this.resetPasswordForm.valid) {
      this.authService.resetPassword(this.resetPasswordForm.value).subscribe(
        response => {
          this.router.navigate(['/login']);
        },
        error => {
          console.error('Erro ao redefinir senha', error);
        }
      );
    }
  }
}
