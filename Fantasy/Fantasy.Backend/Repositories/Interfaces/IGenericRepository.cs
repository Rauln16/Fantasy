﻿using Fantasy.Shared.DTOs;
using Fantasy.Shared.Entities;
using Fantasy.Shared.Responses;

namespace Fantasy.Backend.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<ActionResponse<T>> GetAsync(int id);

    Task<ActionResponse<IEnumerable<T>>> GetAsync();

    Task<ActionResponse<T>> AddAsync(T entity);

    Task<ActionResponse<T>> DeleteAsync(int id);

    Task<ActionResponse<T>> UpdateAsync(T entity);

    Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO paginationDTO);

    Task<ActionResponse<int>> GetTotatlRecordsAsync();
}