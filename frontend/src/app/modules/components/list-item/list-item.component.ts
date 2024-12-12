import { NgClass, NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, Input, Output, signal } from '@angular/core';
import { ITaskItemPaged } from '../../interfaces/ITaskItemPaged';
import { ITaskItem } from '../../interfaces/ITaskItem';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-list-item',
  standalone: true,
  imports: [NgFor, NgClass, NgIf, FormsModule],
  templateUrl: './list-item.component.html',
  styleUrl: './list-item.component.scss'
})
export class ListItemComponent {
  @Input() taskItems: ITaskItemPaged | null = null;
  @Input() isVisible: boolean = false;
  @Output() isVisibleChange = new EventEmitter<boolean>();
  @Output() taskItem = new EventEmitter<ITaskItem>();
  @Output() taskItemDelete = new EventEmitter<string>();
  @Output() taskItemUpdateCompleted = new EventEmitter<ITaskItem>();
  @Output() taskItemPageChange = new EventEmitter<{pageSize?:number, pageNumber?:number}>();

  openModalUpdate(item: ITaskItem) {
    this.isVisible = true;
    this.isVisibleChange.emit(this.isVisible);
    this.taskItem.emit(item);
  }

  updateCompleted(item: ITaskItem) {
    this.taskItemUpdateCompleted.emit(item);
  }

  openModalDelete(id: string) {
    this.taskItemDelete.emit(id);
  }

  onPageChange(page: number): void {
    if (page >= 1 && page <= this.taskItems?.totalPages!)
      this.taskItemPageChange.emit({pageNumber:page, pageSize: this.taskItems?.pageSize});
  }

  onPageSizeChange(pageSize: number): void {
    if(pageSize >= 1)
       this.taskItemPageChange.emit({pageNumber: this.taskItems?.currentPage, pageSize:pageSize});
  }

}
