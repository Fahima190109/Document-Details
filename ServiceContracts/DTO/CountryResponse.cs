using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace ServiceContracts.DTO
{
    public class CountryResponse
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            if(obj.GetType() != typeof(CountryResponse)) return false;
            CountryResponse country_to_compare = obj as CountryResponse;
            return CountryID == country_to_compare.CountryID && 
                CountryName == country_to_compare.CountryName;
        }
    }
    public static class CountryExtensions
    {
        public static CountryResponse TocountryResponse(this Country country)
        {
            //var a = Math.Abs(5);
            return new CountryResponse()
            {
                CountryID = country.CountryId,
                CountryName = country.CountryName,
            };
        }
    }

    
}
