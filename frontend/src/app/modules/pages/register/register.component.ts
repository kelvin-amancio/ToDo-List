import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { IRegisterUser } from '../../interfaces/IRegisterUser';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  #authService = inject(AuthService);
  #router = inject(Router);
  registerUser = signal<IRegisterUser | null>(null);

  registerForm = new FormGroup({
    userName: new FormControl('', [Validators.required, Validators.minLength(3)]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),
    confirmPassword: new FormControl('', [Validators.required, Validators.minLength(6)])
  }
  );

  passwordsMatch() {
    return this.registerForm.get('password')?.value === this.registerForm.get('confirmPassword')?.value;
  }

  submitRegister() {
    if (this.registerForm.valid) {
      this.registerUser.set({
        userName: this.registerForm.value.userName!,
        password: this.registerForm.value.password!,
        confirmPassword: this.registerForm.value.confirmPassword!
      });

      this.#authService.register(this.registerUser()!).subscribe(
        () => { },
        (error: any) => { this.modalError(error); },
        () => { this.modalSuccess() },
      )
    }
  }

  modalSuccess() {
    Swal.fire({
      position: "center",
      icon: "success",
      title: `Usuário ${this.registerForm.value.userName}, criado com sucesso!`,
      timer: 1800,
      showConfirmButton: false,
    });
    this.#router.navigate(['/login']);
  }

  modalError(error: any) {
    Swal.fire({
      position: "center",
      icon: "error",
      title: error.message,
      showConfirmButton: false,
      timer: 1800
    });
  }
}
