using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Models
{
    public interface IIntexWinterRepository
    {
        IQueryable<Burialmain> Burialmains { get; }
    }
}
