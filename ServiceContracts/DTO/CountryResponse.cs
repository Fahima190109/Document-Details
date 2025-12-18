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
    }
    public static class CountryExtensions
    {
        public static CountryResponse TocountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryID = country.CountryId,
                CountryName = country.CountryName,
            };
        }
    }

    
}
