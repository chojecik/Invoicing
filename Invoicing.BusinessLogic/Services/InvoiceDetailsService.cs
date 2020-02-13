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
    public class InvoiceDetailsService : IInvoiceDetailsService
    {
        private readonly DataContext _context;

        public InvoiceDetailsService(DataContext context)
        {
            _context = context;
        }

        public void Add(InvoiceDetails entity)
        {
            if(entity != null)
            {
                _context.Add(entity);
                _context.SaveChanges();
            }
        }

        public void Delete(InvoiceDetails entity)
        {
            if(entity != null)
            {
                _context.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<InvoiceDetails> Find(Expression<Func<InvoiceDetails, bool>> predicate)
        {
            return _context.InvoiceDetails.Where(predicate);
        }

        public IEnumerable<InvoiceDetails> GetAll()
        {
            return _context.InvoiceDetails
                .AsNoTracking()
                .ToList();
        }

        public InvoiceDetails GetById(int id)
        {
            if(id > 0)
            {
                return _context.InvoiceDetails
                    .AsNoTracking()
                    .FirstOrDefault(details => details.Id == id);
            }
            return null;
        }

        public void Update(InvoiceDetails entity)
        {
            if(entity != null)
            {
                _context.Update(entity);
                _context.SaveChanges();
            }
        }
    }
}
