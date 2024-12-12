import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { IUser } from '../interfaces/IUser';
import { IRegisterUser } from '../interfaces/IRegisterUser';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  #http = inject(HttpClient);
  #api = signal(environment.Api);

  login(user: IUser): Observable<{ token: string }> {
    return this.#http.post<{ token: string }>(`${this.#api()}/Auth/login`, user);
  }

  register(user: IRegisterUser): Observable<IUser> {
    return this.#http.post<IUser>(`${this.#api()}/Auth/register`, user);
  }
}
