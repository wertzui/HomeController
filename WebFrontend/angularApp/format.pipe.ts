import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'format',
    pure: true
})
export class FormatPipe implements PipeTransform {

    transform(value: number): string {
        return value.toFixed(2);
    }
}
