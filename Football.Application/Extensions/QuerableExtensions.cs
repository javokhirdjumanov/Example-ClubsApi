using Football.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Football.Application.Extensions;
public static class QuerableExtensions
{
    private static int maxPageSize = 100;
    private static string pagiantionKey = "X-Pagination";

    public static IQueryable<T> ToPagesList<T>(
        this IQueryable<T> source,
        HttpContext context,
        int pageSize,
        int pageIndex)
    {
        if(pageSize <= 0 && pageIndex <= 0)
        {
            throw new ValidationException("Page size or index should be greater than zero (0)");
        }
                
        if(pageSize > maxPageSize) 
        {
            throw new ValidationException($"Page size should be less than {maxPageSize}");
        }

        var paginationMetadata = new PaginationMetada(
            totalCount: source.Count(),
            currentPage: pageIndex,
            pageSize: pageSize);

        context.Response.Headers[pagiantionKey] = JsonSerializer.Serialize(paginationMetadata);

        return source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
    }
}
