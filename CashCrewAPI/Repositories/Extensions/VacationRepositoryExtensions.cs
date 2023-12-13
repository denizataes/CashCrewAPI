using System;
using Entities.Models;
using Repositories.EFCore.Extensions;
using System.Linq.Dynamic.Core;

namespace Repositories.Extensions
{
	public static class VacationRepositoryExtensions
	{
		public static IQueryable<Vacation> Search(this IQueryable<Vacation> vacations, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
				return vacations;

			var lowerCaseTerm = searchTerm.Trim().ToLower();

			return vacations
                .Where(b => b.Title.ToLower().Contains(searchTerm) ||
                b.Description.ToLower().Contains(searchTerm));
		}

        public static IQueryable<Vacation> Sort(this IQueryable<Vacation> vacations,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return vacations.OrderBy(b => b.ID);

            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Vacation>(orderByQueryString);

            if (orderQuery is null)
                return vacations.OrderBy(b => b.ID);

            return vacations.OrderBy(orderQuery);
        }
    }
}

