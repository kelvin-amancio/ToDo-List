import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const canActiveGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  if (localStorage.getItem('authToken'))
    return true;

  router.navigate(['/login']);
  return false;
};
