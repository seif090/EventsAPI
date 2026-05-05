import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
    <div class="card shadow-sm">
      <div class="card-body">
        <h2 class="mb-3">Login</h2>
        <form [formGroup]="form" (ngSubmit)="submit()">
          <div class="mb-3">
            <label class="form-label">Email</label>
            <input class="form-control" formControlName="email" type="email" />
          </div>
          <div class="mb-3">
            <label class="form-label">Password</label>
            <input class="form-control" formControlName="password" type="password" />
          </div>
          <button class="btn btn-primary" [disabled]="form.invalid">Login</button>
        </form>
      </div>
    </div>
  `
})
export class LoginComponent {
  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
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

    this.authService.login(this.form.getRawValue()).subscribe({
      next: () => this.router.navigateByUrl('/'),
      error: () => {
        this.form.setErrors({ invalid: true });
      }
    });
  }
}
