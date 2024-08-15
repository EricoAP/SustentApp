import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.css']
})
export class RegisterFormComponent {
  userForm: FormGroup;
  adressForm: FormGroup;

  constructor(private router: Router, private fb: FormBuilder, private authService: AuthService) {
    this.adressForm = this.fb.group({
      street: ['', [Validators.required]],
      number: ['', [Validators.required]],
      complement: ['', [Validators.required]],
      neighborhood: ['', [Validators.required]],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]],
      zipCode: ['', [Validators.required]]
    });

    this.userForm = this.fb.group({
      name: ['', [Validators.required]],
      document: ['', [Validators.required]],
      address: this.adressForm,
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required]],
      password: ['', [Validators.required]],
      role: ['User']
    });

  }

  registerUser() {
    console.log(this.userForm)
    if (this.userForm.valid) {
      console.log(this.userForm)
      this.authService.register(this.userForm.value).subscribe(
        response => {
          this.router.navigate(['/login']);
        },
        error => {
          console.error('Erro ao fazer login', error);
        }
      );
    }
  }

}
