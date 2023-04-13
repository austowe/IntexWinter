using System.Linq;

namespace IntexWinter.Models
{
    public interface IIntexWinterRepository
    {
        IQueryable<Burialmain> Burialmains { get; }
        IQueryable<BurialmainTextile> BurialmainTextiles { get; }
        IQueryable<Textile> Textiles { get; }
        IQueryable<ColorTextile> ColorTextiles { get; }
        Burialmain GetById(long id);
        Burialmain Get_Last_Burial();
        void Edit(Burialmain burial);
        void Add_Burial(Burialmain burial);
        void Delete(Burialmain burial);
        void SaveChanges();
    }
}
