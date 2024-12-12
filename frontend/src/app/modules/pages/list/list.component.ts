import { CommonModule, NgFor } from '@angular/common';
import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, inject, OnInit, signal, ViewChild } from '@angular/core';
import { ListItemComponent } from "../../components/list-item/list-item.component";
import { TodoService } from '../../services/todo.service';
import { ITaskItem } from '../../interfaces/ITaskItem';
import { ITaskItemPaged } from '../../interfaces/ITaskItemPaged';
import { FormsModule } from '@angular/forms';
import Swal from 'sweetalert2';
import { ModalComponent } from "../../components/modal/modal.component";
import { Router } from '@angular/router';
import { ListItemHeaderComponent } from "../../components/list-item-header/list-item-header.component";

@Component({
  selector: 'app-list',
  standalone: true,
  imports: [CommonModule, ListItemComponent, FormsModule, ModalComponent, ListItemHeaderComponent],
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class ListComponent implements AfterViewInit {
  @ViewChild('inputTitleValue', { static: false }) inputTitleValue!: ElementRef;
  @ViewChild('inputDescriptionValue', { static: false }) inputDescriptionValue!: ElementRef;
  cdr = inject(ChangeDetectorRef);

  #router = inject(Router);
  #todoService = inject(TodoService);
  taskItems = signal<ITaskItemPaged | null>(null);
  getTaskItems = this.taskItems.asReadonly();
  isInputVisible = signal(true);
  inputData = signal({ title: '', description: '', completed: false });
  taskItem = signal<ITaskItem | null>(null);
  taskItemUpdateOrAdd = signal<ITaskItem | null>(null);
  isModalVisible: boolean = false;

  ngOnInit(): void {
    this.getAll({ pageNumber: 1, pageSize: 5 });
  }

  ngAfterViewInit(): void {
    this.setFocusInput();
  }

  getAll(page: { pageNumber?: number, pageSize?: number }) {
    this.#todoService.getAll(page.pageNumber!, page.pageSize!).subscribe(
      (taskItems: ITaskItemPaged) => {
        this.taskItems.set(taskItems);
      },
      (error: any) => {
        if (error.status === 404) {
          this.taskItems.set(null);
          this.openModal({ title: '', description: '', completed: false })
        }
      }
    )
  }

  addItem(task: ITaskItem) {
    if (task.title.length > 0) {
      this.isInputVisible.set(false);
      this.setFocusInput();
    }

    if (task.title.length > 0 && task.description.length > 0) {
      this.taskItem.set({
        title: task.title, description: task.description, completed: false
      });

      this.#todoService.add(this.taskItem()!).subscribe(
        () => { },
        (error: any) => { this.modalError(error) },
        () => {
          this.modalSuccess('Tarefa criada com sucesso!');
          this.resetDataAndVisible();
        },
      )
    }
  }

  updateOrAddTaskItem(task: ITaskItem) {
    if (!task.id)
      this.addItem(task);
    else
      this.updateTaskItem(task);
  }

  updateTaskItem(task: ITaskItem) {
    this.#todoService.update(this.taskItemUpdateOrAdd()!).subscribe(
      () => { },
      (error: any) => { this.modalError(error) },
      () => {
        this.modalSuccess('Tarefa atualizada com sucesso!');
      },
    )
  }

  updateConfirmTaskItem(task: ITaskItem) {
    this.taskItemUpdateOrAdd.set({ id: task.id, title: task.title, description: task.description, completed: true });
    this.#todoService.update(this.taskItemUpdateOrAdd()!).subscribe(
      () => { },
      (error: any) => { this.modalError(error) },
      () => {
        this.modalSuccess('Tarefa realizada!');
      },
    )
  }

  deleteTaskItem(id: string) {
    this.#todoService.remove(id).subscribe(
      () => { },
      (error: any) => { this.modalError(error) },
      () => {
        this.modalSuccess('Tarefa excluída com sucesso!');
      },
    )
  }

  openModal(item: ITaskItem) {
    this.taskItemUpdateOrAdd.set({ id: item.id, title: item.title, description: item.description, completed: false });
    this.isModalVisible = true;
  }

  onModalClose() {
    this.isModalVisible = false;
  }

  resetDataAndVisible() {
    this.inputData.set({ title: '', description: '', completed: false });
    this.isInputVisible.set(true);
    this.setFocusInput();
  }

  setFocusInput() {
    this.cdr.detectChanges();

    if (this.isInputVisible() && this.inputTitleValue)
      this.inputTitleValue.nativeElement.focus();

    else if (!this.isInputVisible() && this.inputDescriptionValue)
      this.inputDescriptionValue.nativeElement.focus();

  }

  logout() {
    localStorage.removeItem('authToken');
    this.#router.navigate(['/login']);
  }

  modalSuccess(message: string) {
    Swal.fire({
      position: "center",
      icon: "success",
      title: message,
      timer: 1800,
      showConfirmButton: false,
    });
    this.getAll({ pageNumber: 1, pageSize: 5 });
  }

  modalDelete(id: string) {
    Swal.fire({
      title: "Tem certeza que Deseja excluir?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Excluir",
      cancelButtonText: "Cancelar"
    }).then((result) => {
      if (result.isConfirmed)
        this.deleteTaskItem(id);
    });
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
