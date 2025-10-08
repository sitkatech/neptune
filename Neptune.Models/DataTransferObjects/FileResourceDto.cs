﻿namespace Neptune.Models.DataTransferObjects;

public class FileResourceDto
{
    public int FileResourceID { get; set; }
    public FileResourceMimeTypeSimpleDto FileResourceMimeType { get; set; }
    public string OriginalBaseFilename { get; set; }
    public string OriginalFileExtension { get; set; }
    public string OriginalFilename { get; set; }
    public Guid FileResourceGUID { get; set; }
    public PersonDisplayDto CreatePerson { get; set; }
    public DateTime CreateDate { get; set; }
    public long ContentLength { get; set; }
}