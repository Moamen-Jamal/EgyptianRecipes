import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  reqHeader = new HttpHeaders(
    {
      'Content-Type' : 'application/json; charset=utf-8',
      'Accept'       : 'application/json', 
      'Authorization' :'Bearer ' + localStorage.getItem("userToken")
    });

  constructor(private http :HttpClient) { }
  GetAllAdmines(pageIndex:number,pageSize:number){
    return this.http.get(environment.apiURL+`Admin/Get?pageIndex=${pageIndex}&pageSize=${pageSize}`,{headers : this.reqHeader});
  }
  GetAdminDetails(id :number){
    return this.http.get(environment.apiURL+"Admin/Get/"+id,{headers : this.reqHeader})
  }
  DeleteAdmin(id :number){
    return this.http.delete(environment.apiURL+"Admin/Delete/"+id,{headers : this.reqHeader})
  }
  AddAdmin(data :any){
    return this.http.post(environment.apiURL+"Admin/Post" , data,{headers : this.reqHeader})
  }
  UpdateAdmin(ID,Data){
    return this.http.put(environment.apiURL+"Admin/Put/"+ID , Data,{headers : this.reqHeader})
  }
  GetAdminDashboard(){
    return this.http.get(environment.apiURL+"Admin/GetDashboardDetails/",{headers : this.reqHeader})
  }

  private setHeadersWithImage(): HttpHeaders {
		let headersConfig = {
			'Accept': 'application/json',
			// 'Content-Type' : 'multipart/form-data'
		};
		return new HttpHeaders(headersConfig);
  }
  
  upload( body: Object) {
		return this.http.post(`${environment.apiURL}File/Upload`, body, { headers: this.setHeadersWithImage() });
	}
}
