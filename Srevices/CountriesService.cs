using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private field
        private readonly List<Country> _countries;
        public CountriesService()
        {
            _countries = new List<Country>();
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

        public dynamic GetCountryById(Guid? countryID)
        {
            if (countryID == null) return null;
            
            Country? country_response_from_list = _countries.FirstOrDefault(temp => 
            temp.CountryId == countryID);

            if (country_response_from_list == null) return null;
            return country_response_from_list.TocountryResponse();
        }

        
    }
}
