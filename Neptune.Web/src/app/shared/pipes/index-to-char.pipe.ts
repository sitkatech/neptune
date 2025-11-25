import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "indexToChar",
})
export class IndexToCharPipe implements PipeTransform {
    transform(index: number): string {
        return String.fromCharCode(65 + index); // 65 is the ASCII code for 'A'
    }
}
