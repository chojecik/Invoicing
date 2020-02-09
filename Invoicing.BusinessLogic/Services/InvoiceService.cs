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

        public void Add(Invoice entity)
        {
            if(entity != null)
            {
                _context.Invoices.Add(entity);
                _context.SaveChanges();
            }
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
                .Include(x => x.Attachment)
                .AsNoTracking();
        }

        public Invoice GetById(int id)
        {
            if(id > 0)
            {
                return _context.Invoices
                    .AsNoTracking()
                    .Include(x => x.Attachment)
                    .FirstOrDefault(x => x.Id == id);
            }
            return null;
        }

        public void Update(Invoice entity)
        {
            if(entity != null)
            {
                _context.Invoices.Update(entity);
                _context.SaveChanges();
            }
        }
    }
}
