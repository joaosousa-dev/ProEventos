import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'DateTimeFormat'
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {

    DATE_FMT = 'dd/MM/yyyy';
    DATE_TIME_FMT = `${this.DATE_FMT} hh:mm`;

  transform(value: any, args?: any): any {
    return super.transform(value,this.DATE_TIME_FMT);
  }

}
