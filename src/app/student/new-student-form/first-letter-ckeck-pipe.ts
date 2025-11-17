import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'firstLetterCheck',
  standalone: true
})
export class FirstLetterCheckPipe implements PipeTransform {

  transform(value: string, letter: string): boolean {
    return value[0] === letter;
  }
}
