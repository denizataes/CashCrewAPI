using System;
using Entities.Models;
using Repositories.EFCore.Extensions;
using System.Linq.Dynamic.Core;

namespace Repositories.Extensions
{
	public static class UserRepositoryExtensions
	{
		public static IQueryable<User> Search(this IQueryable<User> users, string searchTerm)
		{
			if (string.IsNullOrWhiteSpace(searchTerm))
				return users;

			var lowerCaseTerm = searchTerm.Trim().ToLower();

			return users
				.Where(b => b.FirstName.ToLower().Contains(searchTerm) ||
                b.LastName.ToLower().Contains(searchTerm));
		}

        public static IQueryable<User> Sort(this IQueryable<User> books,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(b => b.ID);

            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<User>(orderByQueryString);

            if (orderQuery is null)
                return books.OrderBy(b => b.ID);

            return books.OrderBy(orderQuery);
        }
    }
}

