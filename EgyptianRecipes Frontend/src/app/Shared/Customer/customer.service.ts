import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  reqHeader = new HttpHeaders(
    {
      'Content-Type' : 'application/json; charset=utf-8',
      'Accept'       : 'application/json', 
      'Authorization' :'Bearer ' + localStorage.getItem("userToken")
    });
  constructor(private http :HttpClient) { }
  GetAllCustomers(pageIndex:number,pageSize:number){
    return this.http.get(environment.apiURL+`Customer/Get?pageIndex=${pageIndex}&pageSize=${pageSize}`,{headers : this.reqHeader});
  }
  GetCustomerDetails(id :number){
    return this.http.get(environment.apiURL+"Customer/Get/"+id,{headers : this.reqHeader})
  }
  DeleteCustomer(id :number){
    return this.http.delete(environment.apiURL+"Customer/Delete/"+id,{headers : this.reqHeader})
  }
  AddCustomer(data :any){
    return this.http.post(environment.apiURL+"Customer/Post" , data,{headers : this.reqHeader})
  }
  UpdateCustomer(ID,Data){
    return this.http.put(environment.apiURL+"Customer/Put/"+ID , Data,{headers : this.reqHeader})
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
