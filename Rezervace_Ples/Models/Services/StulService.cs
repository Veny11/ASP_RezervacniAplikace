using Rezervace_Ples.Models.DBConnection;
using Rezervace_Ples.Models.TableObjects;

namespace Rezervace_Ples.Models.Services
{
    public class StulService
    {
        private MyContext _context = new MyContext();

        public List<Stul> StolySpodniList()
        {
            return _context.Stoly.Where(stul => stul.Podlazi == true).ToList();
        }

        public List<Stul> StolyVrchniList()
        {
            return _context.Stoly.Where(stul => stul.Podlazi == false).ToList();
        }

        public Stul GetStul(int cisloStolu)
        {
            Stul vybranyStul = _context.Stoly.Find(cisloStolu);
            return vybranyStul;
        }

        public int GetMaxPocetMist(int cisloStolu)
        {
            int celkovyPocetMist = _context.Stoly.FirstOrDefault(stul => stul.ID_Stul == cisloStolu)?.PocetMist ?? 0;
            return celkovyPocetMist;
        }
    }
}
