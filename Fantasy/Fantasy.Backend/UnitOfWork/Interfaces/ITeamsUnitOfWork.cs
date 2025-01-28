using Fantasy.Shared.DTOs;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Responses;

namespace Fantasy.Backend.UnitOfWork.Interfaces;

public interface ITeamsUnitOfWork
{
    Task<IEnumerable<Team>> GetComboAsync(int countryId);

    Task<ActionResponse<Team>> AddAsync(TeamDTO teamDTO);

    Task<ActionResponse<Team>> UpdateAsync(TeamDTO teamDTO);

    Task<ActionResponse<Team>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Team>>> GetAsync();

    Task<ActionResponse<int>> GetTotatlRecordsAsync(PaginationDTO pagination);

    Task<ActionResponse<IEnumerable<Team>>> GetAsync(PaginationDTO paginationDTO);
}