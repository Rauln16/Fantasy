using Fantasy.Backend.Data;
using Fantasy.Backend.UnitOfWork.Interfaces;
using Fantasy.Shared.DTOs;
using Fantasy.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>
    {
        private readonly ICountriesUnitOfWork _countriesUnitOfWork;

        public CountriesController(IGenericUnitOfWork<Country> unitOfWork, ICountriesUnitOfWork countriesUnitOfWork) : base(unitOfWork)
        {
            _countriesUnitOfWork = countriesUnitOfWork;
        }

        [HttpGet("paginated")]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var response = await _countriesUnitOfWork.GetAsync(pagination);
            if (response.WasSuccess)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("totalRecordsPaginated")]
        public async Task<IActionResult> GetTotalRecordsAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _countriesUnitOfWork.GetTotatlRecordsAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync()
        {
            var response = await _countriesUnitOfWork.GetAsync();
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id)
        {
            var response = await _countriesUnitOfWork.GetAsync(id);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return NotFound(response.Message);
        }

        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _countriesUnitOfWork.GetComboAsync());
        }
    }
}