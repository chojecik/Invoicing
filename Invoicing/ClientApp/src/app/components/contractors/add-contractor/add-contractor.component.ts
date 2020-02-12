import { Component, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Contractor } from '../../../models/contractor';
import { ContractorsService } from '../../../services/contractors.service';
import { EventEmitter } from '@angular/core';

@Component({
  selector: 'add-contractor',
  templateUrl: './add-contractor.component.html',
  styleUrls: ['./add-contractor.component.css']
})
export class AddContractorComponent implements OnInit {
  contractorForm: FormGroup;
  contractor: Contractor = new Contractor();
  @Output() public onContractorAddingFinished = new EventEmitter();

  constructor(private formBuilder: FormBuilder, private contractorsService: ContractorsService) { }

  ngOnInit() {
    this.contractorForm = this.formBuilder.group({
      name: ['', Validators.required],
      street: ['', Validators.required],
      houseNumber: ['', Validators.required],
      localNumber: [Number],
      zipCode: ['', Validators.required],
      city: ['', Validators.required],
      nip: ['', Validators.required],
      bankAccount: [''],
    })
  }

  addContractor() {
    this.contractorsService.addContractor(this.contractorForm.value).subscribe(
      event => {
        this.onContractorAddingFinished.emit(event.id.toString());
      },
      err => {
        console.log(err);
      });
  }

  closeForm(event) {
    this.onContractorAddingFinished.emit("close");
  }
}
