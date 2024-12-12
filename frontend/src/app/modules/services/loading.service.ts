import { inject, Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  spinner = inject(NgxSpinnerService);

  show() {
    this.spinner.show();
  }

  hide() {
    this.spinner.hide();
  }
}
