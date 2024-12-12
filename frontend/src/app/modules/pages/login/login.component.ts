import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { IUser } from '../../interfaces/IUser';
import Swal from 'sweetalert2';
import { ELocalStorage } from '../../enum/ELocalStorage';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  #authService = inject(AuthService);
  #router = inject(Router);
  user = signal<IUser | null>(null);

  loginForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  })


  submitLogin() {
    if (this.loginForm.valid) {
      this.user.set({ userName: this.loginForm.value.userName!, password: this.loginForm.value.password! });
      this.#authService.login(this.user()!).subscribe(
        (result: { token: string }) => { this.storeToken(result.token); },
        (error: any) => { this.modalError(error) },
        () => { this.modalSuccess() },
      )
    }
  }

  storeToken(token: string) {
    localStorage.setItem(ELocalStorage.AUTH_TOKEN, token);
  }

  modalSuccess() {
    Swal.fire({
      position: "center",
      icon: "success",
      title: `Seja bem vindo ${this.loginForm.value.userName}!`,
      timer: 1800,
      showConfirmButton: false,
    });
    this.#router.navigate(['/list']);
  }

  modalError(error: any) {
    Swal.fire({
      position: "center",
      icon: "error",
      title: error.status == 404 ? "Usuário ou senha incorreta!" : error.message,
      showConfirmButton: false,
      timer: 1800
    });
  }
}
