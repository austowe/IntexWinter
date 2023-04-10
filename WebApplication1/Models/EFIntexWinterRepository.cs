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
    }
}
