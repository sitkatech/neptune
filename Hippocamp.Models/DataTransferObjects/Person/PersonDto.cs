﻿namespace Hippocamp.Models.DataTransferObjects
{
    public partial class PersonDto
    {
        public string FullName => $"{FirstName} {LastName}";
    }
}