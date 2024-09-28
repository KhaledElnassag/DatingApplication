import { CanActivateFn, CanDeactivateFn } from '@angular/router';
import { MemberEditComponent } from '../Component/member-edit/member-edit.component';

export const linkGuardGuard:  CanDeactivateFn<MemberEditComponent> =
(component:MemberEditComponent):boolean => {
 if(component.form?.dirty)
   return confirm('Are you sure you want to continue? Any unsaved changes will be lost')
 return true;
};