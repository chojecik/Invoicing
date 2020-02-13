import { Pipe, PipeTransform } from '@angular/core';
import { Contractor } from '../models/contractor';

@Pipe({
  name: 'displayContractor'
})
export class DisplayContractorPipe implements PipeTransform {

  transform(contractor: Contractor, ...args: any[]): string {
    var displayValue = contractor.name + "  " + ",ul." + contractor.street + " " + contractor.houseNumber;

    if (contractor.localNumber == null) {
      displayValue = displayValue + ", " + contractor.zipCode + " " + contractor.city;
    }
    else {
      displayValue = displayValue + "/" + contractor.localNumber + ", " + contractor.zipCode + " " + contractor.city;
    }
    return displayValue;
  }

}
