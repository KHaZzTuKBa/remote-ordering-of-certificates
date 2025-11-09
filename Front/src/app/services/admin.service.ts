import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { AdminGetRequestListDTO } from '../models/admin-get-request-list-dto.model';
import { AdminGetRequestListResponse } from '../models/admin-get-request-list-response.model';
import { AdminGetRequestDTO } from '../models/admin-get-request-dto.model';
import { AdminGetRequestResponse } from '../models/admin-get-request-response.model';
import { API_CONFIG } from '../config/api.config';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private apiUrl = `${API_CONFIG.baseUrl}/admin/admin`;

  constructor(private http: HttpClient) { }

  getRequestList(dto: AdminGetRequestListDTO): Observable<AdminGetRequestListResponse> {
    const params = new HttpParams().set('studentId', dto.studentId.toString());
    return this.http.get<AdminGetRequestListResponse>(`${this.apiUrl}/GetRequestList`, { params })
      .pipe(
        catchError(error => {
          console.error('Ошибка при получении списка заявок:', error);
          return throwError(() => new Error('Не удалось загрузить список заявок. Попробуйте позже.'));
        })
      );
  }

  getRequest(dto: AdminGetRequestDTO): Observable<AdminGetRequestResponse> {
    const params = new HttpParams().set('requestID', dto.requestID);
    return this.http.get<AdminGetRequestResponse>(`${this.apiUrl}/GetRequest`, { params })
      .pipe(
        catchError(error => {
          console.error('Ошибка при получении заявки:', error);
          return throwError(() => new Error('Не удалось загрузить информацию о заявке. Попробуйте позже.'));
        })
      );
  }
}

