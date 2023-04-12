using System.Linq;

namespace IntexWinter.Models
{
    public interface IIntexWinterRepository
    {
        IQueryable<Burialmain> Burialmains { get; }
        Burialmain GetById(long id);
        void Update(Burialmain burial);
        void Add(Burialmain burial);
        void Delete(Burialmain burial);
        void SaveChanges();
    }
}
