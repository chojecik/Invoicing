using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Core;
using Invoicing.Core.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Invoicing.BusinessLogic.Services
{
    public class ContractorService : IContractorService
    {
        private readonly DataContext _context;

        public ContractorService(DataContext context)
        {
            _context = context;
        }

        public void Add(Contractor entity)
        {
            if(entity != null)
            {
                _context.Contractors.Add(entity);
                _context.SaveChanges();
            }
        }

        public void Delete(Contractor entity)
        {
            if(entity != null)
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Contractor> Find(Expression<Func<Contractor, bool>> predicate)
        {
            return _context.Contractors.Where(predicate).ToList();
        }

        public IEnumerable<Contractor> GetAll()
        {
            return _context.Contractors
                .AsNoTracking()
                .ToList();
        }

        public Contractor GetById(int id)
        {
            if(id > 0)
            {
                return _context.Contractors
                    .AsNoTracking()
                    .FirstOrDefault(contractor => contractor.Id == id);
            }

            return null;
        }

        public void Update(Contractor entity)
        {
            if(entity != null)
            {
                _context.Update(entity);
                _context.SaveChanges();
            }
        }
    }
}
