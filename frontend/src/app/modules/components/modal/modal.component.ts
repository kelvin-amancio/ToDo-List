import { NgIf } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ITaskItem } from '../../interfaces/ITaskItem';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [NgIf, FormsModule],
  templateUrl: './modal.component.html',
  styleUrl: './modal.component.scss'
})
export class ModalComponent {
  @Input() taskItemUpdateOrAdd: ITaskItem | null = null;
  @Input() isVisible: boolean = false;
  @Output() isVisibleChange = new EventEmitter<boolean>();
  @Output() closed = new EventEmitter<void>();
  @Output() outputUpdateOrAddTaskItem = new EventEmitter<ITaskItem>();

  updateTaskItem() {
    this.outputUpdateOrAddTaskItem.emit(this.taskItemUpdateOrAdd!);
    this.close();
  }

  close() {
    this.isVisible = false;
    this.isVisibleChange.emit(this.isVisible);
    this.closed.emit();
  }
}
