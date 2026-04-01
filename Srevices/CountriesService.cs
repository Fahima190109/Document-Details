using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private field
        private  readonly List<Country> _countries;
        public CountriesService(bool initialize = true)
        {
            _countries = new List<Country>();
            if (initialize) {
                //{82089C75-57E8-4DA5-8746-C17A122C7FE1}
                //{519A1E0C-4D2C-4BD0-A433-A425B4A80D22}
                //{4128C480-6C8C-4D29-96D0-64E3EBD02DA4}
                //{901BC4A0-3ED8-4FFA-A86F-F10E2DA71A50}
                //{F14AEB9C-EEC8-4B81-B684-4206846EFF8C}
                _countries .AddRange(new List<Country>()
                {
                     new Country()
                {
                  CountryId = Guid.Parse("82089C75-57E8-4DA5-8746-C17A122C7FE1"),
                  CountryName = "USA"
                },
                new Country()
                {
                    CountryId = Guid.Parse("519A1E0C-4D2C-4BD0-A433-A425B4A80D22"),
                    CountryName = "Canada"
                },
                new Country()
                {
                    CountryId = Guid.Parse("4128C480-6C8C-4D29-96D0-64E3EBD02DA4"),
                    CountryName = "London"
                },
                new Country()
                {
                    CountryId = Guid.Parse("901BC4A0-3ED8-4FFA-A86F-F10E2DA71A50"),
                    CountryName = "America"
                },
                new Country()
                {
                    CountryId = Guid.Parse("F14AEB9C-EEC8-4B81-B684-4206846EFF8C"),
                    CountryName = "UK"
                },
                });
               
            }
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            //validation: countryAddRequest parameter can't be null
            if(countryAddRequest  == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            //validation: countryAddRequest parameter can't be null 
            if (countryAddRequest.CountryName == null) { 
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }
            //validation: countryName can't be duplicate
            if (_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).
                Count() > 0)
            {
                throw new ArgumentException("Given country name already exists");
            }
            //cONVERT object from countryAdd requiest
            Country country = countryAddRequest.ToCountry();
            //generate countryId
            country.CountryId = Guid.NewGuid();
            _countries.Add(country);
            return country.TocountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.TocountryResponse
            ()).ToList();
        }

        public CountryResponse GetCountryById(Guid? countryID)
        {
            var firstcountry = _countries.FirstOrDefault();
            var countrylist = _countries.ToList();

            if (countryID == null) return null;
            
            Country? country_response_from_list = _countries.FirstOrDefault(temp => 
            temp.CountryId == countryID);

            if (country_response_from_list == null) return null;
            return country_response_from_list.TocountryResponse();
        }

        
    }
}
