import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoadingComponent } from "./modules/components/loading/loading.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoadingComponent],
  template: '<router-outlet/><app-loading/>',
})
export class AppComponent {
  title = 'todo';
}
