import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { ITaskItem } from '../interfaces/ITaskItem';
import { ITaskItemPaged } from '../interfaces/ITaskItemPaged';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  #http = inject(HttpClient);
  #api = signal(environment.Api);

  getAll(pagenumber: number, pageSize: number): Observable<ITaskItemPaged> {
    return this.#http.get<ITaskItemPaged>(`${this.#api()}/TaskItem?pageNumber=${pagenumber}&pageSize=${pageSize}`);
  }

  add(taskItem: ITaskItem): Observable<ITaskItem> {
    return this.#http.post<ITaskItem>(`${this.#api()}/TaskItem`, taskItem);
  }

  update(taskItem: ITaskItem): Observable<ITaskItem> {
    return this.#http.put<ITaskItem>(`${this.#api()}/TaskItem`, taskItem);
  }

  remove(id: string): Observable<ITaskItem> {
    return this.#http.delete<ITaskItem>(`${this.#api()}/TaskItem/${id}`);
  }
}
