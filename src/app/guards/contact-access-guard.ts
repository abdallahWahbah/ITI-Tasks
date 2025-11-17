import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const contactAccessGuard: CanActivateFn = (route, state) => {
  let token = localStorage.getItem("token");
  if(token) return true;
  else{
    let router = inject(Router);
    router.navigate(['/login']);
    return false;
  }
};
