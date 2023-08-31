using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Data.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        public IQueryable<T> Items { get; }

        public T Get(int id);
        public Task<T> GetAsync(int id, CancellationToken Cancel = default);

        public T Add(T item);
        public Task<T> AddAsync(T item, CancellationToken Cancel = default);

        public void Update(T item);
        public Task UpdateAsync(T item, CancellationToken Cancel = default);

        public void Remove(int id);
        public Task RemoveAsync(int id, CancellationToken Cancel = default);
    }
}
