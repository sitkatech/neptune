/**
 * Neptune.API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { PersonDto } from '././person-dto';
import { FileResourceMimeTypeDto } from '././file-resource-mime-type-dto';

export class FileResourceDto { 
    FileResourceID?: number;
    FileResourceMimeType?: FileResourceMimeTypeDto;
    OriginalBaseFilename?: string;
    OriginalFileExtension?: string;
    FileResourceGUID?: string;
    CreatePerson?: PersonDto;
    CreateDate?: string;
    InBlobStorage?: boolean;
    ContentLength?: number;
    constructor(obj?: any) {
        Object.assign(this, obj);
    }
}