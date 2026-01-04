using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceContracts.Enums;

namespace CRUDTests
{
    public class PersonServiceTest
    {
        private readonly IPersonService
           _personsService;
        private readonly ICountriesService _countriesService;
        public PersonServiceTest()
        {
            _personsService = new PersonsService();
            _countriesService = new CountriesService();
        }

        #region AddPerson
        // When CountryAddRequest is null
        [Fact]
        public void AddPerson_NullPerson()
        {
            PersonAddRequest request = null;
            Assert.Throws<ArgumentNullException>(()
                => _personsService.AddPerson(request));
        }
        //When countryname is null
        [Fact]
        public void AddPerson_NullPersonName()
        {
            PersonAddRequest? request = new PersonAddRequest
            {
                PersonName = null
            };
            Assert.Throws<ArgumentException>(()
                => _personsService.AddPerson(request));
        }

        

        //When countryname is duplicate 
        // [Fact]
        //public void AddCountry_duplicateCountry()
        //{
        //    CountryAddRequest? request1 = new CountryAddRequest
        //    {
        //        CountryName = "Bangladesh"
        //    };
        //    CountryAddRequest? request2 = new CountryAddRequest
        //    {
        //        CountryName = "Bangladesh"
        //    };
        //    Assert.Throws<ArgumentException>(()
        //        =>
        //    {
        //        _countriesService.AddCountry(request1);
        //        _countriesService.AddCountry(request2);
        //    });
        //}
        //have a whole property of country
        [Fact]
        //Arrange
       
        public void AddPeople_ProperPeopleDetails()
        {
            PersonAddRequest? request = new PersonAddRequest()
            {
               PersonName = "Person Name..", Email = "person@djd.com",
               Address = "sample address", CountryID = Guid.NewGuid(),
               Gender =GenderOptions.Male,DateOfBirth = DateTime.Parse("2000-02-01"),
               ReceiveNewsLetters = true,
            };

            //Act
           PersonResponse response = _personsService.AddPerson(request);
            List<PersonResponse> Persons_from_GetAllCountries = _personsService.GetAllPerson();
                
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, Persons_from_GetAllCountries);
        }
        #endregion
        #region GetpersonBypersonId
        [Fact]
        public void GetPersonByPersonId()
        {
            Guid? personID = null;
            PersonResponse? person_respons_from_get = _personsService.GetPersonById(personID);
            Assert.Null(person_respons_from_get);
        }
        [Fact]
        public void GetPersonById_withId() {
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "Canada"
            };
           CountryResponse countryRespons = _countriesService.AddCountry(countryAddRequest);
            PersonAddRequest? request = new PersonAddRequest()
            {
                PersonName = "Person Name..",
                Email = "person@djd.com",
                Address = "sample address",
                CountryID = countryRespons.CountryID,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true,
            };
            PersonResponse person_respons_from_add = 
                _personsService.AddPerson(request);
            PersonResponse? person_response_from_get = _personsService.
                GetPersonById(person_respons_from_add.PersonId);
            Assert.Equal(person_respons_from_add, person_respons_from_add);
        }
        #endregion
    }
}
