import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'thousandSeparator'
})
export class ThousandSeparatorPipe implements PipeTransform {
  transform(value: number): string {
    const formattedValue = String(value).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1 ');
    return formattedValue;
  }
}