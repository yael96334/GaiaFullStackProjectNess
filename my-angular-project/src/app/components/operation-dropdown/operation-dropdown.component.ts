import { Component, Input, signal } from '@angular/core';
import { Operation } from 'src/models/calculator.models';

@Component({
  selector: 'app-operation-dropdown',
  template: `
    <p-dropdown
      [options]="operations()"
      [ngModel]="selected()"
      optionLabel="code"
      placeholder="Select operation"
      (onChange)="onSelect($event.value)">
    </p-dropdown>
  `
})
export class OperationDropdownComponent {
  @Input() operations = signal<Operation[]>([]);
  @Input() selected = signal<Operation | null>(null);


  onSelect(op: Operation | null): void {
    this.selected.set(op);
  }
}