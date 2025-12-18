using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService
            _countriesServic;
        private CountriesService _countriesService;

        //Constructor
        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();

        }
        #region AddCountry
        // When CountryAddRequest is null
        [Fact]
        public void AddCountry_NullCountry()
        {
            CountryAddRequest request = null;
            Assert.Throws<ArgumentNullException>(()
                => _countriesService.AddCountry(request));
        }
        //When countryname is null
        [Fact]
        public void AddCountry_NullCountryName()
        {
            CountryAddRequest? request = new CountryAddRequest
            {
                CountryName = null
            };
            Assert.Throws<ArgumentException>(()
                => _countriesService.AddCountry(request));
        }
        //When countryname is duplicate 
        [Fact]
        public void AddCountry_duplicateCountry()
        {
            CountryAddRequest? request1 = new CountryAddRequest
            {
                CountryName = "Bangladesh"
            };
            CountryAddRequest? request2 = new CountryAddRequest
            {
                CountryName = "Bangladesh"
            };
            Assert.Throws<ArgumentException>(()
                =>
            {
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }
        //have a whole property of country
        [Fact]
        //Arrange
        public void AddCountry_ProperCountryDetails()
        {
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "America"
            };

            //Act
            CountryResponse response = _countriesService.AddCountry(request);
            Assert.True(response.CountryID != Guid.Empty);
        }
        #endregion

        #region GetAllCountries
        [Fact]
        //the list of countries should be empty blyl default(before adding any countries)
        public void GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse>
                actual_country_response_list = _countriesService.GetAllCountries();
            //Assert
            Assert.Empty(actual_country_response_list);
        }
        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
        //Arrange
        List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
        {
            new CountryAddRequest(){CountryName = "USA"},
            new CountryAddRequest(){CountryName = "UK"}
        };
        //Act
        List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
        foreach (CountryAddRequest country_request in country_request_list)
        {
            countries_list_from_add_country.Add(_countriesService.AddCountry(country_request));
        }
        List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();
        //read each element from countries_list_from_add_country
        foreach (CountryResponse expected_country in countries_list_from_add_country)
        {
            Assert.Contains(expected_country, actualCountryResponseList);
        }
        }
   
        #endregion

    }

}
