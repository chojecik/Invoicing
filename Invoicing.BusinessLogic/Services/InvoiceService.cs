using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Core;
using Invoicing.Core.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Invoicing.BusinessLogic.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly DataContext _context;
        public InvoiceService(DataContext context)
        {
            _context = context;
        }

        public Invoice Add(Invoice entity)
        {
            if(entity != null)
            {
                _context.Invoices.Add(entity);
                _context.SaveChanges();
                //TODO return 
            }
            return null;
        }

        public void Delete(Invoice entity)
        {
            if (entity != null)
            {
                _context.Invoices.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Invoice> Find(Expression<Func<Invoice, bool>> predicate)
        {
            return _context.Invoices
                .AsNoTracking()
                .Where(predicate);
        }

        public IEnumerable<Invoice> GetAll()
        {
            return _context.Invoices
                .AsNoTracking();
        }

        public Invoice GetById(int id)
        {
            if(id > 0)
            {
                return _context.Invoices
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == id);
            }
            return null;
        }

        public Invoice Update(Invoice entity)
        {
            if(entity != null)
            {
                _context.Invoices.Update(entity);
                _context.SaveChanges();
                //TODO return
            }
            return null;
        }
    }
}
