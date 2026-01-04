using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Country { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(PersonResponse)) return false;

            PersonResponse person_to_compare = obj as PersonResponse;
            return PersonId == person_to_compare.PersonId &&
            PersonName == person_to_compare.PersonName &&
            Email == person_to_compare.Email &&
            DateOfBirth == person_to_compare.DateOfBirth &&
            Gender == person_to_compare.Gender &&
            CountryID == person_to_compare.CountryID &&
            Country == person_to_compare.Country &&
            Address == person_to_compare.Address &&
            ReceiveNewsLetters == person_to_compare.ReceiveNewsLetters;

        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            //var a = Math.Abs(5);
            return new PersonResponse()
            {
                PersonId = person.PersonId,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                CountryID = person.CountryID,
                Country = person.Country,
                Address = person.Address,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = (person.DateOfBirth != null) ?
                Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays /
                365.25) : null
            };
        }
    }

       

}
