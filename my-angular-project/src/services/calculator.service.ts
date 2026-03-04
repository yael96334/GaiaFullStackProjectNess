import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Operation, CalculationRequest, CalculationResult,CalculationResponse } from '../models/calculator.models';

@Injectable({
  providedIn: 'root'
})
export class CalculatorService {
  private apiUrl = 'https://localhost:44390/api/Calculator';

  constructor(private http: HttpClient) { }

  getOperations(): Observable<Operation[]> {
    return this.http.get<Operation[]>(`${this.apiUrl}/operations`);
  }

  calculate(request: CalculationRequest): Observable<CalculationResponse> {
    return this.http.post<CalculationResponse>(`${this.apiUrl}/calculate`, request);
  }

  getHistory(id: number): Observable<CalculationResult> {
    return this.http.get<CalculationResult>(`${this.apiUrl}/history/${id}`);
  }
}