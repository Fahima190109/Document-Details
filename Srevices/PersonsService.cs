using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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

            response. Country =_countriesService.GetCountryById
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
           return _persons.Select(temp => temp.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonById(Guid? personId)
        {
            if (personId == null) 
                return null;
            Person person = _persons.FirstOrDefault(temp => temp.PersonId == personId);
            if(person == null)
                return null;
            return person.ToPersonResponse();

        }

        public List<PersonResponse> GetFilterPersons(string searchBy, string? searchString)
        {
            List<PersonResponse> allPerson = GetAllPerson();
            List<PersonResponse> matchingPerson = allPerson;
            if(string.IsNullOrEmpty(searchString) || String.IsNullOrEmpty(searchString))
                return matchingPerson;

            switch (searchBy) {
                case nameof(Person.PersonName):
                    matchingPerson = allPerson.Where(temp => (!string.IsNullOrEmpty
                    (temp.PersonName)?temp.PersonName.Contains(searchString,
                    StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.Email):
                    matchingPerson = allPerson.Where(temp => (!string.IsNullOrEmpty
                    (temp.Email) ? temp.Email.Contains(searchString,
                    StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.DateOfBirth):
                    matchingPerson = allPerson.Where(temp => 
                    (temp.DateOfBirth != null) ?
                    temp.DateOfBirth.Value.ToString("dd MMM yyyy").Contains(searchString,
                    StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.Gender):
                    matchingPerson = allPerson.Where(temp => (!string.IsNullOrEmpty
                    (temp.Gender) ? temp.Gender.Contains(searchString,
                    StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.CountryID):
                    matchingPerson = allPerson.Where(temp => (!string.IsNullOrEmpty
                    (temp.Country) ? temp.Country.Contains(searchString,
                    StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                case nameof(Person.Address):
                    matchingPerson = allPerson.Where(temp => (!string.IsNullOrEmpty
                    (temp.Address) ? temp.Address.Contains(searchString,
                    StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;
                default: matchingPerson = allPerson; break;


                  
            }
            return matchingPerson;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse>
            allPersons, string sortedBy, SortOrderOptions sortOrder)
        {
            if (string.IsNullOrEmpty(sortedBy))
            { 
                return allPersons;
            }
            List<PersonResponse> SortedPersons = (sortedBy, sortOrder)
            switch
            {
                (nameof(PersonResponse.PersonName), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.PersonName, StringComparer
                .OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.PersonName), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer
                .OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.PersonName, StringComparer
                .OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Email), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer
                .OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),
                (nameof(PersonResponse.DateOfBirth), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),

                (nameof(PersonResponse.Age), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),
                (nameof(PersonResponse.Age), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Gender, StringComparer
                .OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Gender), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.Gender, StringComparer
                .OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Country, StringComparer
                .OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Country), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.Country, StringComparer
                .OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrderOptions.ASC)
                => allPersons.OrderBy(temp => temp.Address, StringComparer
                .OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Address), SortOrderOptions.DESC)
                => allPersons.OrderByDescending(temp => temp.Address, StringComparer
                .OrdinalIgnoreCase).ToList(),

                //(nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.ASC)
                //=> allPersons.OrderBy(temp => temp.ReceiveNewsLetters, StringComparer
                //.OrdinalIgnoreCase).ToList(),
                //(nameof(PersonResponse.ReceiveNewsLetters), SortOrderOptions.DESC)
                //=> allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters, StringComparer
                //.OrdinalIgnoreCase).ToList(),

                _ => allPersons
            };
            return SortedPersons;

        }

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
            if(personUpdateRequest == null)
                throw new ArgumentNullException(nameof(personUpdateRequest));
            //Validation
            ValidationHelper.ModelValidation(personUpdateRequest);
            //get matching person object to update 
            Person? matchingPerson = _persons.FirstOrDefault(temp => temp.PersonId
            == personUpdateRequest.PersonId);
            if(matchingPerson == null)
            {
                throw new ArgumentException("Given person id doesn't exists");
            }
            //update all details
            matchingPerson.PersonName = personUpdateRequest.PersonName;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryID = personUpdateRequest.CountryID;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;
           return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? personId)
        {
            if(personId == null)    
            throw new ArgumentNullException(nameof(personId));

            Person? person = _persons.FirstOrDefault(temp => temp.PersonId == personId);
            if (person == null) return false;
            _persons.RemoveAll(temp => temp.PersonId == personId);
            return true;
        }
    }
}
