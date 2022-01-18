using System;

namespace Hippocamp.Models.DataTransferObjects.Person
{
    public class PersonCreateDto: PersonUpsertDto
    {
        public string LoginName { get; set; }
        public Guid UserGuid { get; set; }
    }
}