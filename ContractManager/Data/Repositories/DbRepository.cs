﻿using ContractManager.Data.Interfaces;
using ContractManager.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Data.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly ContractManagerDBContext _db;
        private readonly DbSet<T> _set;

        public bool AutoSaveChanges { get; set; } = true;
        public DbRepository(ContractManagerDBContext db)
        {
            _db = db;
            _set = db.Set<T>();
        }
        public virtual IQueryable<T> Items => _set;

        public T Add(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Added;

            if (AutoSaveChanges)
                _db.SaveChanges();

            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Added;

            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }

        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default) => await Items
           .SingleOrDefaultAsync(item => item.Id == id, Cancel)
           .ConfigureAwait(false);

        public void Remove(int id)
        {
            var item = _set.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };

            _db.Remove(item);

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            _db.Remove(new T { Id = id });

            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public void Update(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;

            if (AutoSaveChanges)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
    }
}
