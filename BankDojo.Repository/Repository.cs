using BankDojo.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDojo.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private BankDojoDBContext _context;
        private DbSet<TEntity> _dbSet;

        public Repository(BankDojoDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public void Add(TEntity data) => _dbSet.Add(data);

        public void Delete(int id)
        {
            var data = _dbSet.Find(id);
            if (data != null)
                _dbSet.Remove(data);
        }

        public IEnumerable<TEntity> Get() => _dbSet.ToList();

        public TEntity? Get(int id) => _dbSet.Find(id);

        public void Save() => _context.SaveChanges();

        public void Update(TEntity data)
        {
            _dbSet.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
        }
    }
}
