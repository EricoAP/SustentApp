import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loginUrl = 'https://localhost:44306/api/';

  constructor(private http: HttpClient) { }
  
  login(credentials: { email: string; password: string }): Observable<any> {
    return this.http.post<any>(this.loginUrl + 'users/login', credentials);
  }
  
  register(request : any): Observable<any> {
    return this.http.post<any>(this.loginUrl + 'users', request);
  }
  
  update(credentials: { email: string; password: string }): Observable<any> {
    return this.http.put<any>(this.loginUrl + 'users', credentials);
  }

  forgotEmail(credentials: { email: string; }): Observable<any> {
    return this.http.post<any>(this.loginUrl, credentials);
  }

  resetPassword(credentials: { token: string; email: string; password: string }): Observable<any> {
    return this.http.post<any>(this.loginUrl, credentials);
  }
  
  isAuthenticated(): boolean {
    return !!sessionStorage.getItem('User');
    // return !!localStorage.getItem('Serasa');
  }

  getUser() : Observable<any> {
    const userId = sessionStorage.getItem('UserId');
    return this.http.get<any>(this.loginUrl + 'users/' + userId);
  }
}
