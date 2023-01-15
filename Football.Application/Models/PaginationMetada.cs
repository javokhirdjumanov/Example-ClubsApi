using Microsoft.Identity.Client.Extensibility;

namespace Football.Application.Models;
internal class PaginationMetada
{
	public int TotalCount { get; set; }
	public int CurrentPage{ get; set; }
	public int PageCount { get; set; }

	public PaginationMetada(int totalCount, int currentPage, int pageSize)
	{
		TotalCount = totalCount;
		CurrentPage = currentPage;
		PageCount = (int)Math.Ceiling(1.0 * (TotalCount / pageSize));
	}

	public bool HasNext
	{
		get
		{
			return CurrentPage < PageCount;
		}
	}
	public bool HasPrevious
	{
		get
		{
			return CurrentPage > 1 && CurrentPage <= PageCount + 1;
		}
	}
}
