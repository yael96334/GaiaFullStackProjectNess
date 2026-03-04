import { Component, inject, OnInit, signal } from '@angular/core';
import { CalculatorService } from 'src/services/calculator.service';
import { Operation, CalculationRequest, CalculationHistoryItem } from 'src/models/calculator.models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  fieldA = '';
  fieldB = '';

  selectedOperation = signal<Operation | null>(null);
  result = signal<number | null>(null);
  operationHistory = signal<CalculationHistoryItem[]>([]);
  monthlyCount = signal<number>(0);
  operations = signal<Operation[]>([]);

  private calculatorService = inject(CalculatorService);

  ngOnInit(): void {
    this.calculatorService.getOperations().subscribe({
      next: (ops) => this.operations.set(ops),
      error: (err) => console.error('Failed to fetch operations', err)
    });
  }

  calculate(): void {
    const op = this.selectedOperation();
    if (!op) return; 

    const request: CalculationRequest = {
      fieldA: this.fieldA,
      fieldB: this.fieldB,
      operationType: op.code,
      symbol:op.symbol
    };

    this.calculatorService.calculate(request).subscribe({
      next: (res) => {
        this.result.set(res.result.result);
        this.getHistory(res.result.lastId);
        
      },
      error: (err) => console.error('Calculation failed', err)
    });
  }
getHistory(lastId:number){
   const op = this.selectedOperation();
    if (!op) return;

    this.calculatorService.getHistory( lastId
    ).subscribe({
      next: (res) => {
        debugger
        this.operationHistory.set(res.lastThree);
        this.monthlyCount.set(res.monthlyCount);
      },
      error: (err) => console.error('Failed to fetch history', err)
    });
  
}
  onOperationChange(op: Operation | null): void {
    this.selectedOperation.set(op);
  }
   isFormValid(): boolean {
    return this.fieldA.trim() !== '' && this.fieldB.trim() !== '' && this.selectedOperation() !== null;
  }
}