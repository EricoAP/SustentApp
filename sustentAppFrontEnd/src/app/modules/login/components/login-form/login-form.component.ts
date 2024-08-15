import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {

  loginForm: FormGroup;

  constructor(private router: Router, private fb: FormBuilder, private authService: AuthService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  sendLogin() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe(
        response => {
          console.log(response);
          sessionStorage.setItem('UserId', response.id);
          sessionStorage.setItem('Token', response.token);
          // sessionStorage.setItem('jwtToken', response.Token);
          // window.location.reload();
          this.router.navigate(['/collectPoints']);
        },
        error => {
          console.error('Erro ao fazer login', error);
        }
      );
    }
  }
}
