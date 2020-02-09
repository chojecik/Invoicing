import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';
import { PasswordValidatorDirective } from './directives/password-validator.directive';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { CostInvoicesComponent } from './components/invoices/cost-invoices/cost-invoices.component';
import { AddInvoiceComponent } from './components/invoices/add-invoice/add-invoice.component';
import { EditInvoiceComponent } from './components/invoices/edit-invoice/edit-invoice.component';
import { ReactiveFormsModule } from '@angular/forms';
import { InvoiceTypePipe } from './pipes/invoice-type.pipe';
import { UploadComponent } from './components/upload/upload.component';
import { CustomHttpInterceptor } from './middlewares/custom-http-interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DatePipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    LoginComponent,
    RegisterComponent,
    PasswordValidatorDirective,
    HomeComponent,
    NavbarComponent,
    CostInvoicesComponent,
    AddInvoiceComponent,
    EditInvoiceComponent,
    InvoiceTypePipe,
    UploadComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'welcome', pathMatch: 'full' },
      { path: 'welcome', component: WelcomeComponent },
      { path: 'cost-invoices', component: CostInvoicesComponent },
      { path: 'cost-invoices/add', component: AddInvoiceComponent },
      { path: 'cost-invoices/edit/:id', component: EditInvoiceComponent },
      { path: 'home', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: '**', redirectTo: 'welcome' },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: CustomHttpInterceptor, multi: true },
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
