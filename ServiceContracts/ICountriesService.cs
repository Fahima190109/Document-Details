using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface ICountriesService
    {

    CountryResponse AddCountry(CountryAddRequest?
        CountryAddRequest);

    List<CountryResponse> GetAllCountries();

    dynamic GetCountryById(Guid? countryID);

    }
}
