using immunization.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace immunization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly ImmunizationContext _immunizationContext;
        public ProviderController(
            ImmunizationContext immunizationContext)
        {
            _immunizationContext = immunizationContext;
        }


        [HttpPost]
        public async Task<ActionResult<Provider>> post(Provider provider)
        {
            try
            {
                Provider providerData  = new Provider();

                providerData.Id = Guid.NewGuid();
                providerData.CreationTime = DateTime.Now;
                providerData.FirstName = provider.FirstName;
                providerData.LastName = provider.LastName;
                providerData.Address = provider.Address;
                providerData.LicenseNumber = provider.LicenseNumber;


                await _immunizationContext.Providers.AddAsync(providerData);
                await _immunizationContext.SaveChangesAsync();
                return Ok(providerData);

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> Get(Guid id)
        {
            try
            {
                var result = _immunizationContext.Providers.Where(a => a.Id == id).FirstOrDefault();
                if (result == null)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Provider>> Put(Guid id, Provider provider)
        {
            try
            {
                var result = _immunizationContext.Providers.Where(a => a.Id == id).FirstOrDefault();
                if (result == null)
                {
                    return NoContent();
                }

                result.FirstName = provider.FirstName;
                result.LastName = provider.LastName;
                result.Address = provider.Address;
                result.LicenseNumber = provider.LicenseNumber;

                _immunizationContext.Providers.Update(result);
                await _immunizationContext.SaveChangesAsync();

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Provider>> GetQuery([FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] long? licenseNumber)
        {
            try
            {
                List<Provider> result = new List<Provider>();
                if (firstName != null)
                {
                    result = _immunizationContext.Providers.Where(a => a.FirstName == firstName).ToList();
                }
                else if (lastName != null)
                {
                    result = _immunizationContext.Providers.Where(a => a.LastName == lastName).ToList();
                }
                else if (licenseNumber != null)
                {
                    result = _immunizationContext.Providers.Where(a => a.LicenseNumber == licenseNumber).ToList();
                }
                if (result.Count == 0)
                {
                    return NoContent();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Provider>> Delete(Guid id)
        {
            try
            {
                var provider = new Provider()
                {
                    Id = id
                };
                _immunizationContext.Providers.Remove(provider);
                await _immunizationContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }




    }
}
