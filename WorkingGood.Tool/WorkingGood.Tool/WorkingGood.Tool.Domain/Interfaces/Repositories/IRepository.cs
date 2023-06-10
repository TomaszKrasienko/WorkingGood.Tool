using System;
using WorkingGood.Tool.Domain.Models.DataModels;

namespace WorkingGood.Tool.Domain.Interfaces.Repositories
{
	public interface IRepository<T> where T : BaseEntity 
	{
		Task<List<T>> GetAsync();
		Task AddAsync(T entity);
		Task EditAsync(T entity);
		Task DeleteAsync(Guid id);
	}
}

