using System.Linq;

namespace IntexWinter.Models
{
    public interface IIntexWinterRepository
    {
        IQueryable<Burialmain> Burialmains { get; }
        Burialmain GetById(long id);
        void Edit(Burialmain burial);
        void Add_Burial(Burialmain burial);
        void Delete(Burialmain burial);
        void SaveChanges();
    }
}
