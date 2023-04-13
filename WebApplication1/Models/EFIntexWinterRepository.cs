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
        public IQueryable<BurialmainTextile> BurialmainTextiles => context.BurialmainTextile;
        public IQueryable<Textile> Textiles => context.Textile;
        public IQueryable<ColorTextile> ColorTextiles => context.ColorTextile;
        public Burialmain GetById(long id)
        {
            return context.Burialmain.SingleOrDefault(b => b.Id == id);
        }
        public void Edit(Burialmain burial)
        {
            context.Burialmain.Update(burial);
            context.SaveChanges();
        }

        public void Add_Burial(Burialmain burial)
        {
            context.Burialmain.Add(burial);
            context.SaveChanges();
        }
        public Burialmain Get_Last_Burial()
        {
            return context.Burialmain.OrderByDescending(b => b.Id).FirstOrDefault();
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
