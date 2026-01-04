using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PersonsService : IPersonService
    {
        private readonly List<Person> _persons;
        private readonly ICountriesService _countriesService;
        public PersonsService() {
            _persons = new List<Person>();
            _countriesService = new CountriesService();
        }

        private PersonResponse ConvertPersonToPersonResponse(Person person)
        {
            PersonResponse response = person.ToPersonResponse();
          response.Country = _countriesService.GetCountryById
                (person.CountryID)?. CountryName;
            return response;
        }
        public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
        {
            //validation: countryAddRequest parameter can't be null
            if (personAddRequest == null)
            {
                throw new ArgumentNullException(nameof(personAddRequest));
            }
            //validation: countryAddRequest parameter can't be null 
            //if (personAddRequest.PersonName == null)
            //{
            //    throw new ArgumentException(nameof(personAddRequest.PersonName));
            //}
            //Model validations
            ValidationHelper.ModelValidation(personAddRequest);
            //cONVERT object from countryAdd requiest
           Person person = personAddRequest.ToPerson();
            //generate countryId
            person.PersonId = Guid.NewGuid();
            _persons.Add(person);
            return ConvertPersonToPersonResponse(person);
        }

        public List<PersonResponse> GetAllPerson()
        {
            throw new NotImplementedException();
        }

        public PersonResponse? GetPersonById(Guid? personId)
        {
            throw new NotImplementedException();
        }

      
    }
}
