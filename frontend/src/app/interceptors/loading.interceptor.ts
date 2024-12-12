import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { LoadingService } from '../modules/services/loading.service';
import { finalize, shareReplay } from 'rxjs';
import { Router } from '@angular/router';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingService = inject(LoadingService);
  const router = inject(Router);
  const token = localStorage.getItem('authToken');
  const currentUrl = router.url;
  loadingService.show();

  if (!token && currentUrl !== '/login' && currentUrl !== '/register')
    router.navigate(['/login']);

  const authReq = req.clone({ setHeaders: { Authorization: `Bearer ${token}` } });

  return next(authReq).pipe(
    shareReplay(),
    finalize(() => {
      loadingService.hide();
    })
  );
};
