import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../Services/account.service';

export const authGuard: CanActivateFn = (route, state) => {
  debugger;
  const accountService=inject(AccountService);
  const router=inject(Router);
   const userExist=!!accountService.isExist();
   if(!userExist){//in case that user remove token from local storage
   router.navigate(['/']);
   }
  return userExist;
};
