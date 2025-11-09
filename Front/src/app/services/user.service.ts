import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { CreateRequestDTO } from '../models/create-request-dto.model';
import { CreateRequestResponse } from '../models/create-request-response.model';
import { GetRequestListDTO } from '../models/get-request-list-dto.model';
import { GetRequestListResponse } from '../models/get-request-list-response.model';
import { GetRequestDTO } from '../models/get-request-dto.model';
import { GetRequestResponse } from '../models/get-request-response.model';
import { API_CONFIG } from '../config/api.config';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = `${API_CONFIG.baseUrl}/user`;

  constructor(private http: HttpClient) { }

  createRequest(dto: CreateRequestDTO): Observable<CreateRequestResponse> {
    return this.http.post<CreateRequestResponse>(`${this.apiUrl}/CreateRequest`, dto)
      .pipe(
        catchError(error => {
          console.error('Ошибка при создании заявки:', error);
          return throwError(() => new Error('Не удалось создать заявку. Попробуйте позже.'));
        })
      );
  }

  getRequestList(dto: GetRequestListDTO): Observable<GetRequestListResponse> {
    const params = new HttpParams().set('studentId', dto.studentId.toString());
    return this.http.get<GetRequestListResponse>(`${this.apiUrl}/GetRequestList`, { params })
      .pipe(
        catchError(error => {
          console.error('Ошибка при получении списка заявок:', error);
          return throwError(() => new Error('Не удалось загрузить список заявок. Попробуйте позже.'));
        })
      );
  }

  getRequest(dto: GetRequestDTO): Observable<GetRequestResponse> {
    const params = new HttpParams().set('requestID', dto.requestID);
    return this.http.get<GetRequestResponse>(`${this.apiUrl}/GetRequest`, { params })
      .pipe(
        catchError(error => {
          console.error('Ошибка при получении заявки:', error);
          return throwError(() => new Error('Не удалось загрузить информацию о заявке. Попробуйте позже.'));
        })
      );
  }
}

