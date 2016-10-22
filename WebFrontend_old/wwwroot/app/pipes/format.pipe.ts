import {Pipe, PipeTransform} from 'angular2/core';

@Pipe({
    name: 'format',
    pure: true
})
export class FormatPipe implements PipeTransform {

    transform(value: number, args: string[]): any {
        return value.toFixed(2);
    }
}