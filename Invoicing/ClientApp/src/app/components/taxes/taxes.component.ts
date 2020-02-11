import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { IgxMonthPickerComponent } from "igniteui-angular";

@Component({
  selector: 'app-taxes',
  templateUrl: './taxes.component.html',
  styleUrls: ['./taxes.component.css']
})
export class TaxesComponent implements OnInit{

  public date: Date = new Date();
  public locale: "pl";
  public numericFormatOptions = {
    month: "long",
    year: "numeric"
  };
  year: number;
  month: number;

  constructor() { }

  ngOnInit() {
  }

  changed(e) {
    this.year = this.date.getFullYear();
    this.month = this.date.getMonth();
  }
}
