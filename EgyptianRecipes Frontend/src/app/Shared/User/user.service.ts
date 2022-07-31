import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders}from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { map, tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }
  UserAuthentication(username , password)
  {
    var data = {
      UserName:username,
      Password:password
    }; 
    var reqHeader = new HttpHeaders({'Content-Type' : 'application/json; charset=utf-8',
    'Accept'       : 'application/json','Authorization' :'Bearer ' + localStorage.getItem("userToken") });
    return this.http.post(environment.apiURL+'User/Login',data ,{headers : reqHeader})
  }

  roleMatch(allowedRoles): boolean {
    var isMatch = false;
    var payLoad = JSON.parse(window.atob(localStorage.getItem('userToken').split('.')[1]));
    var userRole = payLoad.Roles;
    allowedRoles.forEach(element => {
      if (userRole == element) {
        isMatch = true;
        return false;
      }
    });
    return isMatch;
  }
  reqHeader = new HttpHeaders(
    {
      'Content-Type' : 'application/json; charset=utf-8',
      'Accept'       : 'application/json', 
      'Authorization' :'Bearer ' + localStorage.getItem("userToken")
    });
  getUser(id :number){
    return this.http.get(environment.apiURL+"User/Get/"+id,{headers : this.reqHeader})
  }


  /////////////////////////////
  ////// For Validation ///////

  getUsers() {
    return this.http.get<any[]>(environment.apiURL+`User/Get`,{headers : this.reqHeader}).pipe(
      map(users => {
        const newUsers = [];
        for (let user of users) {
          const email = user.Email;
          const userName = user.UserName;
          newUsers.push({ email: email, userName: userName });
        }
        return newUsers;
      }),
      tap(users => console.log(users))
    );
  }


  getUserByEmail(email: string) {
    return this.http.get<any[]>(environment.apiURL+`User/Get?Email=${email}`,{headers : this.reqHeader});
  }

  getUserByUsername(userName: string) {
    return this.http.get<any[]>(environment.apiURL+`User/Get?UserName=${userName}`,{headers : this.reqHeader});



    // or using HttpParams

    // return this.http.get<any[]>(this.url, {
    //   params: new HttpParams().set('username', uName)
    // });
  }
 
}
