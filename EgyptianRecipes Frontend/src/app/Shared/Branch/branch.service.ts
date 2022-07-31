import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class BranchService {
  reqHeader = new HttpHeaders(
    {
      'Content-Type' : 'application/json; charset=utf-8',
      'Accept'       : 'application/json', 
      'Authorization' :'Bearer ' + localStorage.getItem("userToken")
    });
  constructor(private http :HttpClient) { }
  
  GetAllBranchs(pageIndex:number,pageSize:number){
    return this.http.get(environment.apiURL+`Branch/Get?pageIndex=${pageIndex}&pageSize=${pageSize}`,{headers : this.reqHeader});
  }
  GetBookedBranches(CustomerID,pageIndex, pageSize){
    return this.http.get(environment.apiURL+`Branch/GetBookedBranches?CustomerID=${CustomerID}&pageIndex=${pageIndex}&pageSize=${pageSize}`,{headers : this.reqHeader});
  }
  GetBranches(){
    return this.http.get(environment.apiURL+"Branch/GetAll",{headers : this.reqHeader})
  }
  getAllBySearch  
  (pageIndex:number,pageSize:number ,title =""){
    return this.http.get
    (environment.apiURL+`Branch/SearchWithFilter?pageIndex=${pageIndex}&pageSize=${pageSize}&title=${title}`
    ,{headers : this.reqHeader});
  }
  GetBranchDetails(id :number){
    return this.http.get(environment.apiURL+"Branch/Get/"+id,{headers : this.reqHeader})
  }
  DeleteBranch(id :number){
    return this.http.delete(environment.apiURL+"Branch/Delete/"+id,{headers : this.reqHeader}).toPromise();
  }
  CancelBookedBranch(branchID, customerID){
    return this.http.delete(environment.apiURL+`Branch/CancelBookedBranch?branchID=${branchID}&customerID=${customerID}`,{headers : this.reqHeader}).toPromise();
  }
  BookBranch(branchID, customerID){
    return this.http.get(environment.apiURL+`Branch/BookBranch?branchID=${branchID}&customerID=${customerID}`,{headers : this.reqHeader})
  }
  AddBranch(data :any){
    return this.http.post(environment.apiURL+"Branch/Post" , data,{headers : this.reqHeader})
  }
  UpdateBranch(ID,Data){
    return this.http.put(environment.apiURL+"Branch/Put/"+ID , Data,{headers : this.reqHeader})
  }
}
