using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Models
{
    public class EFIntexWinterRepository : IIntexWinterRepository
    {
        private intexContext context { get; set; }

        public EFIntexWinterRepository(intexContext temp)
        {
            context = temp;
        }

        public IQueryable<Burialmain> Burialmains => context.Burialmain;
        public Burialmain GetById(long id)
        {
            return context.Burialmain.SingleOrDefault(b => b.Id == id);
        }
        public void Update(Burialmain burial)
        {
            context.Entry(burial).State = EntityState.Modified;
        }

        public void Add(Burialmain burial)
        {
            context.Burialmain.Add(burial);
        }

        public void Delete(Burialmain burial)
        {
            context.Burialmain.Remove(burial);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}
