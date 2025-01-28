using Fantasy.Shared.DTOs;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Responses;

namespace Fantasy.Backend.Repositories.Interfaces;

public interface ICountriesRepository
{
    Task<ActionResponse<Country>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<Country>>> GetAsync();

    Task<IEnumerable<Country>> GetComboAsync();

    Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO paginationDTO);

    Task<ActionResponse<int>> GetTotatlRecordsAsync(PaginationDTO pagination);
}