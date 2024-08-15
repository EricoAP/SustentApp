import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';


@Component({
  selector: 'app-infos-contact-form',
  templateUrl: './infos-contact-form.component.html',
  styleUrls: ['./infos-contact-form.component.css']
})
export class InfosContactFormComponent implements OnInit {
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
    });

  }
  ngOnInit(): void {
    this.populate();
  }

  populate() {
    this.authService.getUser().subscribe(
      (res) => {
        const address = res.address;
        res.address = null;
        this.userForm.patchValue(res);
        this.adressForm.patchValue(address);
        console.log(this.userForm.value)
      }
    )
  }

  updateUser() {
    if (this.userForm.valid) {
      this.authService.update(this.userForm.value).subscribe(
        response => {
          this.router.navigate(['/collectPoints']);
        },
        error => {
          console.error('Erro ao fazer login', error);
        }
      );
    }
  }

}
