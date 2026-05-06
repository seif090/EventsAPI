import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="row justify-content-center">
      <div class="col-md-7">
        <div class="card shadow-sm">
          <div class="card-body">
            <h2 class="mb-3">Register</h2>
            <p class="text-muted">Create an account to start booking.</p>
            <form [formGroup]="form" (ngSubmit)="submit()">
              <div class="row">
                <div class="col-md-6 mb-3">
                  <label class="form-label">First name</label>
                  <input class="form-control" formControlName="firstName" />
                </div>
                <div class="col-md-6 mb-3">
                  <label class="form-label">Last name</label>
                  <input class="form-control" formControlName="lastName" />
                </div>
              </div>
              <div class="mb-3">
                <label class="form-label">Email</label>
                <input class="form-control" formControlName="email" type="email" />
              </div>
              <div class="mb-3">
                <label class="form-label">Password</label>
                <input class="form-control" formControlName="password" type="password" />
              </div>
              <div class="d-grid">
                <button class="btn btn-primary" [disabled]="form.invalid">Register</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  `
})
export class RegisterComponent {
  form = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    role: ['Client']
  });

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {}

  submit() {
    if (this.form.invalid) {
      return;
    }

    this.authService.register(this.form.getRawValue()).subscribe({
      next: () => this.router.navigateByUrl('/'),
      error: () => {
        this.form.setErrors({ invalid: true });
      }
    });
  }
}
