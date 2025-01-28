using Fantasy.Backend.Data;
using Fantasy.Backend.Helpers;
using Fantasy.Backend.Repositories.Interfaces;
using Fantasy.Shared.DTOs;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Repositories.Implementations;

public class CountryRepository : GenericRepository<Country>, ICountriesRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<Country>> GetAsync(int id)
    {
        var country = await _context.Countries
            .Include(x => x.Teams)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (country == null)
        {
            return new ActionResponse<Country>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<Country>
        {
            WasSuccess = true,
            Result = country
        };
    }

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
    {
        var countries = await _context.Countries
            .Include(x => x.Teams)
            .OrderBy(x => x.Name)
            .ToListAsync();
        return new ActionResponse<IEnumerable<Country>>
        {
            WasSuccess = true,
            Result = countries
        };
    }

    public async Task<IEnumerable<Country>> GetComboAsync()
    {
        return await _context.Countries
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO paginationDTO)
    {
        var queryable = _context.Countries
            .Include(x => x.Teams)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(paginationDTO.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(paginationDTO.Filter.ToLower()));
        }
        return new ActionResponse<IEnumerable<Country>>
        {
            WasSuccess = true,
            Result = await queryable
                .OrderBy(x => x.Name)
                .Paginate(paginationDTO)
                .ToListAsync()
        };
    }

    public async Task<ActionResponse<int>> GetTotatlRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Countries.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count,
        };
    }
}