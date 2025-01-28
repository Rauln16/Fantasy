using Fantasy.Backend.Data;
using Fantasy.Backend.Helpers;
using Fantasy.Backend.Repositories.Interfaces;
using Fantasy.Shared.DTOs;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Repositories.Implementations;

public class TeamsRepository : GenericRepository<Team>, ITeamsRepository
{
    private readonly DataContext _context;

    public TeamsRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ActionResponse<IEnumerable<Team>>> GetAsync()
    {
        var teams = await _context.Teams
            .Include(x => x.Country)
            .OrderBy(x => x.Name)
            .ToListAsync();
        return new ActionResponse<IEnumerable<Team>>()
        {
            WasSuccess = true,
            Result = teams
        };
    }

    public override async Task<ActionResponse<Team>> GetAsync(int id)
    {
        var team = await _context.Teams
            .Include(x => x.Country)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (team == null)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }

        return new ActionResponse<Team>
        {
            WasSuccess = true,
            Result = team
        };
    }

    public async Task<ActionResponse<Team>> AddAsync(TeamDTO teamDTO)
    {
        var country = await _context.Countries.FindAsync(teamDTO.CountryId);
        if (country == null)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        var team = new Team
        {
            CountryId = country.Id,
            Name = teamDTO.Name,
        };

        _context.Add(team);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Team>
            {
                WasSuccess = true,
                Result = team
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception ex)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<IEnumerable<Team>> GetComboAsync(int countryId)
    {
        return await _context.Teams
            .Where(x => x.CountryId == countryId)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<ActionResponse<Team>> UpdateAsync(TeamDTO teamDTO)
    {
        var currentTeam = await _context.Teams.FindAsync(teamDTO.Id);
        if (currentTeam == null)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR005"
            };
        }

        var country = await _context.Countries.FindAsync(teamDTO.CountryId);
        if (country == null)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR004"
            };
        }

        currentTeam.Country = teamDTO.Country;
        currentTeam.CountryId = teamDTO.CountryId;
        currentTeam.Name = teamDTO.Name;

        _context.Update(currentTeam);
        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponse<Team>
            {
                WasSuccess = true,
                Result = currentTeam
            };
        }
        catch (DbUpdateException)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = "ERR003"
            };
        }
        catch (Exception ex)
        {
            return new ActionResponse<Team>
            {
                WasSuccess = false,
                Message = ex.Message
            };
        }
    }

    public override async Task<ActionResponse<IEnumerable<Team>>> GetAsync(PaginationDTO paginationDTO)
    {
        var queryable = _context.Teams
            .Include(x => x.Country)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(paginationDTO.Filter))
        {
            queryable = queryable.Where(x => x.Country!.Name.ToLower().Contains(paginationDTO.Filter.ToLower()));
        }

        return new ActionResponse<IEnumerable<Team>>
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
        var queryable = _context.Teams.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Country!.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }
        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count,
        };
    }
}