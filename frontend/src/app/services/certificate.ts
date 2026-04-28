import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RootCertificate {
  id?: string;
  commonName: string;
  organization: string;
  country: string;
  createdAt?: string;
  expiresAt: string;
  pemData: string;
}

export interface UserCertificate {
  id?: string;
  commonName: string;
  email: string;
  rootCertificateId: string;
  csrData: string;
  pemData: string;
  signedAt?: string;
  expiresAt: string;
}

export interface PagedResult<T> {
  data: T[];
  total: number;
  page: number;
  pageSize: number;
}

@Injectable({
  providedIn: 'root'
})
export class CertificateService {
  private baseUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) {}

  getRootCertificates(page: number, pageSize: number): Observable<PagedResult<RootCertificate>> {
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);
    return this.http.get<PagedResult<RootCertificate>>(`${this.baseUrl}/rootcertificates`, { params });
  }

  createRootCertificate(cert: RootCertificate): Observable<RootCertificate> {
    return this.http.post<RootCertificate>(`${this.baseUrl}/rootcertificates`, cert);
  }

  deleteRootCertificate(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/rootcertificates/${id}`);
  }

  getUserCertificates(page: number, pageSize: number): Observable<PagedResult<UserCertificate>> {
    const params = new HttpParams()
      .set('page', page)
      .set('pageSize', pageSize);
    return this.http.get<PagedResult<UserCertificate>>(`${this.baseUrl}/usercertificates`, { params });
  }

  createUserCertificate(cert: UserCertificate): Observable<UserCertificate> {
    return this.http.post<UserCertificate>(`${this.baseUrl}/usercertificates`, cert);
  }

  deleteUserCertificate(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/usercertificates/${id}`);
  }
}