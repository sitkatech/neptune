import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'neptuneModelingResultSigFig'
})
export class NeptuneModelingResultSigFigPipe implements PipeTransform {

  transform(value: number, prec: number): number {
    if (!value) {
      return 0;
    }

    return parseFloat(value.toPrecision(prec));
  }

}
