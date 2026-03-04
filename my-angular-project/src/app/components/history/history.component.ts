import { Component, Input } from '@angular/core';
import { CalculationHistoryItem } from 'src/models/calculator.models';

@Component({
  selector: 'app-history',
  template: `
  <div *ngIf="history.length>0">
    <h3>Action History:</h3>
    <div *ngFor="let item of history">
      {{ item.fieldA }}  {{ item.symbol }} {{ item.fieldB }} = {{ item.result }}
    </div>
    <p>Monthly Count: {{ monthlyCount }}</p>
    </div>
  `
})
export class HistoryComponent {
  @Input() history: CalculationHistoryItem[] = [];
  @Input() monthlyCount: number = 0;
}