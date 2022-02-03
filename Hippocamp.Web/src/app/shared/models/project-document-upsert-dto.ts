export class ProjectDocumentUpsertDto {
	ProjectID : number
	DisplayName : string
	DocumentDescription : string
    FileResource : File
	
	constructor(obj?: any) {
        Object.assign(this, obj);
    }
}