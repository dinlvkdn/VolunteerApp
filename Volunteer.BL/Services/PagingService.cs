using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Services;

public class PagingService
{
    private readonly DbContext context;

    public PagingService(VolunteerDBContext context)
    {
        this.context = context;
    }

    public async Task<PagedPesponse<List<T>>> ApplyPagination<T>(PaginationFilter filter, CancellationToken cancellationToken) where T : BaseEntity
    {
        IQueryable<T> query = context.Set<T>();

        query = filter.SortColumn switch
        {
            "Id" when filter.SortDirection == "asc" => query
                .OrderBy(p => p.Id),
            "Id" => query.OrderByDescending(p => p.Id),
            _ => query
        };

        var countRecords = await query
            .CountAsync(cancellationToken);

        query = query
            .Skip(filter.PageNumber * filter.PageSize)
            .Take(filter.PageSize);

        var result = await query
            .ToListAsync(cancellationToken);

        return new PagedPesponse<List<T>>(
            result,
            countRecords,
            filter.PageNumber, filter.PageSize);
    }
}