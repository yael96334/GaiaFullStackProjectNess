import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { CalculatorService } from 'src/services/calculator.service';
import { OperationDropdownComponent } from './components/operation-dropdown/operation-dropdown.component';
import { HistoryComponent } from './components/history/history.component';

@NgModule({
  declarations: [
    AppComponent,
    HistoryComponent,
    OperationDropdownComponent
  ],
  imports: [
    BrowserModule,
    ButtonModule,
    InputTextModule,
    DropdownModule,
    FormsModule,
    NoopAnimationsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [CalculatorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
