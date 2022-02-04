export class ProjectDocumentUpdateDto {
	ProjectID : number
	DisplayName : string
	DocumentDescription : string
	
	constructor(obj?: any) {
        Object.assign(this, obj);
    }
}