using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using Xunit.Abstractions;

namespace CRUDTests
{
    public class PersonServiceTest
    {
        private readonly IPersonService
           _personsService;
        private readonly ICountriesService _countriesService;
        private readonly ITestOutputHelper _outputHelper;
        public PersonServiceTest(ITestOutputHelper outputHelper)
        {
            _personsService = new PersonsService();
            _countriesService = new CountriesService(false);
            _outputHelper = outputHelper;
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
                PersonName = "Person Name..",
                Email = "person@djd.com",
                Address = "sample address",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-02-01"),
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
        public void GetPersonById_withId()
        {
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
            Assert.Equal(person_respons_from_add, person_response_from_get);
        }
        #endregion

        #region GetAllPerson
        [Fact]
        public void GetAllPerson_EmptyList()
        {
            List<PersonResponse> person_from_get = _personsService.GetAllPerson();
            Assert.Empty(person_from_get);

        }
        [Fact]
        public void GetPersonfrom_AddFewPerson()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest()
            {
                CountryName = "India"
            };
            CountryResponse response1 = _countriesService.AddCountry(countryAddRequest);
            CountryResponse response2 = _countriesService.AddCountry(countryAddRequest2);
            CountryResponse response3 = _countriesService.GetCountryById(response2.CountryID);
            _outputHelper.WriteLine(response1.CountryID.ToString());
            PersonAddRequest? request1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "Smith@djd.com",
                Address = "smithaddress",
                CountryID = response1.CountryID,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsLetters = true,
            };
            PersonAddRequest? request2 = new PersonAddRequest()
            {
                PersonName = "Smi",
                Email = "Smi@djd.com",
                Address = "smiaddress",
                CountryID = response2.CountryID,
                Gender = GenderOptions.Female,
                DateOfBirth = DateTime.Parse("2001-05-06"),
                ReceiveNewsLetters = true,
            };
            PersonAddRequest? request3 = new PersonAddRequest()
            {
                PersonName = "S3mi",
                Email = "S3mi@djd.com",
                Address = "s3miaddress",
                CountryID = response2.CountryID,
                Gender = GenderOptions.Female,
                DateOfBirth = DateTime.Parse("2000-05-06"),
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
            request1,request2, request3
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            foreach (PersonAddRequest request in person_requests)
            {
                PersonResponse persons_response = _personsService.AddPerson(request);
                person_response_list_from_add.Add(persons_response);
            }

            //print person_response_list_from_add
            foreach (PersonResponse personResponse_from_add in
                person_response_list_from_add)
            {
                _outputHelper.WriteLine(personResponse_from_add.ToString());
            }

            List<PersonResponse> person_response_from_get =
                _personsService.GetAllPerson();
            // akhane amar prb
            foreach (PersonResponse person_response_list in person_response_list_from_add)
            {
                Assert.Contains(person_response_list, person_response_from_get);
            }
        }
        #endregion

        #region GetfilterPerson

        [Fact]
        public void GetFilterPerson_StringEmptySearchTest()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest()
            {
                CountryName = "India"
            };
            CountryResponse response1 = _countriesService.AddCountry(countryAddRequest);
            CountryResponse response2 = _countriesService.AddCountry(countryAddRequest2);
            CountryResponse response3 = _countriesService.GetCountryById(response2.CountryID);
            _outputHelper.WriteLine(response1.CountryID.ToString());
            PersonAddRequest? request1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "Smith@djd.com",
                Address = "smithaddress",
                CountryID = response1.CountryID,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsLetters = true,
            };
            PersonAddRequest? request2 = new PersonAddRequest()
            {
                PersonName = "Smi",
                Email = "Smi@djd.com",
                Address = "smiaddress",
                CountryID = response2.CountryID,
                Gender = GenderOptions.Female,
                DateOfBirth = DateTime.Parse("2001-05-06"),
                ReceiveNewsLetters = true,
            };
            PersonAddRequest? request3 = new PersonAddRequest()
            {
                PersonName = "S3mi",
                Email = "S3mi@djd.com",
                Address = "s3miaddress",
                CountryID = response2.CountryID,
                Gender = GenderOptions.Female,
                DateOfBirth = DateTime.Parse("2000-05-06"),
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
            request1,request2, request3
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            foreach (PersonAddRequest request in person_requests)
            {
                PersonResponse persons_response = _personsService.AddPerson(request);
                person_response_list_from_add.Add(persons_response);
            }

            //print person_response_list_from_add
            _outputHelper.WriteLine("Actual:");
            foreach (PersonResponse personResponse_from_add in
                person_response_list_from_add)
            {
                _outputHelper.WriteLine(personResponse_from_add.ToString());
            }
            //why amra akhane person.method use korsi
            List<PersonResponse> person_response_from_search =
                _personsService.GetFilterPersons(nameof(Person.PersonName), "");

            //print person_response_list_from_search
            _outputHelper.WriteLine("Accpected:");
            foreach (PersonResponse Person_response_from_search in
                person_response_from_search)
            {
                _outputHelper.WriteLine(Person_response_from_search.ToString());
            }
            // akhane amar prb
            foreach (PersonResponse person_response_list in person_response_list_from_add)
            {
                Assert.Contains(person_response_list, person_response_from_search);
            }
        }


        //not empty string

        [Fact]
        public void GetFilterPerson_StringSearchTest()
        {
            CountryAddRequest? countryAddRequest = new CountryAddRequest()
            {
                CountryName = "US"
            };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest()
            {
                CountryName = "In"
            };
            CountryResponse response1 = _countriesService.AddCountry(countryAddRequest);
            CountryResponse response2 = _countriesService.AddCountry(countryAddRequest2);
            CountryResponse response3 = _countriesService.GetCountryById(response2.CountryID);
            _outputHelper.WriteLine(response1.CountryID.ToString());
            PersonAddRequest? request1 = new PersonAddRequest()
            {
                PersonName = "Smith",
                Email = "Smith@djd.com",
                Address = "smithaddress",
                CountryID = response1.CountryID,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsLetters = true,
            };
            PersonAddRequest? request2 = new PersonAddRequest()
            {
                PersonName = "Smi",
                Email = "Smi@djd.com",
                Address = "smiaddress",
                CountryID = response2.CountryID,
                Gender = GenderOptions.Female,
                DateOfBirth = DateTime.Parse("2001-05-06"),
                ReceiveNewsLetters = true,
            };
            PersonAddRequest? request3 = new PersonAddRequest()
            {
                PersonName = "S3mi",
                Email = "S3mi@djd.com",
                Address = "s3miaddress",
                CountryID = response2.CountryID,
                Gender = GenderOptions.Female,
                DateOfBirth = DateTime.Parse("2000-05-06"),
                ReceiveNewsLetters = true,
            };

            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
            request1,request2, request3
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            foreach (PersonAddRequest request in person_requests)
            {
                PersonResponse persons_response = _personsService.AddPerson(request);
                person_response_list_from_add.Add(persons_response);
            }

            //print person_response_list_from_add
            _outputHelper.WriteLine("Actual:");
            foreach (PersonResponse personResponse_from_add in
                person_response_list_from_add)
            {
                _outputHelper.WriteLine(personResponse_from_add.ToString());
            }
            //why amra akhane person.method use korsi
            List<PersonResponse> person_response_from_search =
                _personsService.GetFilterPersons(nameof(Person.PersonName), "sm");

            //print person_response_list_from_search
            _outputHelper.WriteLine("Accpected:");
            foreach (PersonResponse Person_response_from_search in
                person_response_from_search)
            {
                _outputHelper.WriteLine(Person_response_from_search.ToString());
            }
            // akhane amar prb
            foreach (PersonResponse person_response_list in person_response_list_from_add)
            {
                if (person_response_list.PersonName != null)
                {
                    if (person_response_list.PersonName.Contains("sm",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        Assert.Contains(person_response_list, person_response_from_search);
                    }
                }

            }
        }


        #endregion
        #region GetfilterPersonSorted
        [Fact]
        public void GetSortedPersons()
        {

            CountryAddRequest? countryAddRequest = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest? countryAddRequest2 = new CountryAddRequest()
            {
                CountryName = "India"
            };
            CountryResponse response1 = _countriesService.AddCountry(countryAddRequest);
            CountryResponse response2 = _countriesService.AddCountry(countryAddRequest2);

            PersonAddRequest? request1 = new PersonAddRequest()
            {
                PersonName = "Sa",
                Email = "Smith@djd.com",
                Address = "smithaddress",
                CountryID = response1.CountryID,
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2002-05-06"),
                ReceiveNewsLetters = true,
            };
            PersonAddRequest? request2 = new PersonAddRequest()
            {
                PersonName = "Sc",
                Email = "Smi@djd.com",
                Address = "smiaddress",
                CountryID = response2.CountryID,
                Gender = GenderOptions.Female,
                DateOfBirth = DateTime.Parse("2001-05-06"),
                ReceiveNewsLetters = true,
            };
            PersonAddRequest? request3 = new PersonAddRequest()
            {
                PersonName = "Sbmi",
                Email = "S3mi@djd.com",
                Address = "s3miaddress",
                CountryID = response2.CountryID,
                Gender = GenderOptions.Female,
                DateOfBirth = DateTime.Parse("2000-05-06"),
                ReceiveNewsLetters = true,
            };
            CountryResponse response3 = _countriesService.GetCountryById(request3.CountryID);
            _outputHelper.WriteLine(response3.CountryID.ToString());
            List<PersonAddRequest> person_requests = new List<PersonAddRequest>()
            {
            request1,request2, request3
            };
            List<PersonResponse> person_response_list_from_add = new List<PersonResponse>();
            foreach (PersonAddRequest request in person_requests)
            {
                PersonResponse persons_response = _personsService.AddPerson(request);
                person_response_list_from_add.Add(persons_response);
            }


            List<PersonResponse> allpersons = _personsService.GetAllPerson();
            //why amra akhane person.method use korsi
            List<PersonResponse> person_response_from_sort =
                _personsService.GetSortedPersons(allpersons,
                nameof(Person.PersonName), SortOrderOptions.DESC);

            //print person_response_list_from_search
            _outputHelper.WriteLine("Expected:");
            foreach (PersonResponse Person_response_from_sort in
                person_response_from_sort)
            {
                _outputHelper.WriteLine(Person_response_from_sort.ToString());
            }
            person_response_list_from_add =
             person_response_list_from_add.OrderByDescending(x => x.PersonName).ToList();
            //print person_response_list_from_add
            _outputHelper.WriteLine("Actual:");
            foreach (PersonResponse personResponse_from_add in
                person_response_list_from_add)
            {
                _outputHelper.WriteLine(personResponse_from_add.ToString());
            }


            //}
            //Assert
            for (int i = 0; i < person_response_list_from_add.Count; i++)
            {
                Assert.Equal(person_response_list_from_add[i],
                    person_response_from_sort[i]);
            }

        }
        #endregion
        #region UpdatePerson
        [Fact]
        public void UpdatePerson_NullPerson()
        {
            PersonUpdateRequest? personUpdateRequest = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            });
        }
        //When personID is invalid,it should throw ArgumentException
        [Fact]
        public void UpdatePerson_InvalidPersonId()
        {
            PersonUpdateRequest? personUpdateRequest = new
                PersonUpdateRequest()
            { PersonId = Guid.NewGuid() };
            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            });
        }
        //When person full details
        [Fact]
        public void UpdatePerson_InvalidPersonName()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse countryResponse_from_Add =
                _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John",
                CountryID = countryResponse_from_Add.CountryID,
                Address = "ABC road",
                DateOfBirth = DateTime.Parse("2000-01-05"),
                Email = "abc@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            };
            PersonResponse personResponse_from_add =
                _personsService.AddPerson(personAddRequest);
            PersonUpdateRequest personUpdateRequest =
               personResponse_from_add.TopersonUpdateRequest();
            personUpdateRequest.PersonName = null;

            Assert.Throws<ArgumentException>(() =>
            {
                _personsService.UpdatePerson(personUpdateRequest);
            });
        }

        //When personName is Null
        [Fact]
        public void UpdatePerson_PersonFullDetails()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse countryResponse_from_Add =
                _countriesService.AddCountry(countryAddRequest);
            _outputHelper.WriteLine("Actual:");

            _outputHelper.WriteLine(countryResponse_from_Add.CountryID.ToString());
            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John",
                CountryID = countryResponse_from_Add.CountryID,
                Address = "ABC road",
                DateOfBirth = DateTime.Parse("2000-01-05"),
                Email = "abc@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            };
            PersonResponse personResponse_from_add =
                _personsService.AddPerson(personAddRequest);
            //_outputHelper.WriteLine("Expected:");

            //_outputHelper.WriteLine(personResponse_from_add.Country.ToString());
            PersonUpdateRequest personUpdateRequest =
               personResponse_from_add.TopersonUpdateRequest();
            personUpdateRequest.PersonName = "William";
            personUpdateRequest.Email = "William@example.com";

            PersonResponse person_response_from_update =
                _personsService.UpdatePerson(personUpdateRequest);

            PersonResponse personResponse_from_get =
                _personsService.GetPersonById(person_response_from_update.PersonId);

            Assert.Equal(person_response_from_update, personResponse_from_get);
        }
        #endregion
        #region DeletePerson
        [Fact]
        public void DeletePerson_validPersonID()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse countryResponse_from_Add =
                _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John",
                CountryID = countryResponse_from_Add.CountryID,
                Address = "ABC road",
                DateOfBirth = DateTime.Parse("2000-01-05"),
                Email = "abc@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            };
            PersonResponse personResponse_from_add =
                _personsService.AddPerson(personAddRequest);

            bool isDeleted =
                _personsService.DeletePerson(personResponse_from_add.PersonId);
            Assert.True(isDeleted);
        }

        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            CountryAddRequest countryAddRequest = new CountryAddRequest()
            {
                CountryName = "UK"
            };
            CountryResponse countryResponse_from_Add =
                _countriesService.AddCountry(countryAddRequest);

            PersonAddRequest personAddRequest = new PersonAddRequest()
            {
                PersonName = "John",
                CountryID = countryResponse_from_Add.CountryID,
                Address = "ABC road",
                DateOfBirth = DateTime.Parse("2000-01-05"),
                Email = "abc@gmail.com",
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = true,
            };
            PersonResponse personResponse_from_add =
                _personsService.AddPerson(personAddRequest);

            bool isDeleted =
                _personsService.DeletePerson(Guid.NewGuid());
            Assert.False(isDeleted);
        }
        #endregion

    }
}
