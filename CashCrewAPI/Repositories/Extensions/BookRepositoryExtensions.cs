using System;
using Entities.Models;

namespace Repositories.Extensions
{
	public static class BookRepositoryExtensions
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
	}
}

