import { Routes } from '@angular/router';
import { LoginComponent } from './modules/pages/login/login.component';
import { RegisterComponent } from './modules/pages/register/register.component';
import { canActiveGuard } from './guard/can-active.guard';
import { ListComponent } from './modules/pages/list/list.component';

export const routes: Routes = [
        {
                path: '',
                component: LoginComponent
        },
        {
                path: 'login',
                component: LoginComponent
        },
        {
                path: 'register',
                component: RegisterComponent
        },
        {
                path: 'list',
                component: ListComponent,
                canActivate: [canActiveGuard]
        },
        {
                path: '**',
                component: LoginComponent
        }
];
