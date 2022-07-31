import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../Shared/User/user.service';

@Injectable({
  providedIn: 'root'
})

export class AuthenticationGuard implements CanActivate {
  constructor(private router : Router ,private service :UserService){}
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
      if (localStorage.getItem('userToken') !=null){
      
        let roles = next.data['permittedRoles'] as Array<string>;
        if(roles){
          if(this.service.roleMatch(roles)) return true;
          else{
            this.router.navigate(['/forbidden']);
            return false;
          }
        }
        return true;
      }
      else
      {
      this.router.navigate(['/Login']);
      return false;
   }
  }
}

