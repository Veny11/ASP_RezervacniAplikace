using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using Rezervace_Ples.Models.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezervace_Ples.Models.Services;

public class LidiService
{
    private MyContext _context = new MyContext();

    public Lidi GetClovek(int id)
    {
        Lidi vybranyClovek = _context.Lide.Find(id);
        return vybranyClovek;
    }

    public List<Lidi> GetList(int cisloStolu)
    {
        return _context.Lide.Where(clovek => clovek.ID_Stul == cisloStolu).ToList();
    }
    public List<Lidi> GetCompleteList()
    {
        return _context.Lide.ToList();
    }

    public void Update(Lidi clovek, int id)
    {
        var query = from a in _context.Lide
                    where id == a.ID_Lidi
                    select a;

        Lidi al = query.FirstOrDefault();

        al.Name = clovek.Name;
        al.Surname = clovek.Surname;

        _context.SaveChanges();
    }


    public void Add(KolikratNovyClovekModel kolikrat, int idStolu)
    {
        for (int i = 1; i <= kolikrat.KolikratValue; i++)
        {
            var newPerson = new Lidi
            {
                ID_Stul = idStolu,
                Name = kolikrat.Jmeno,
                Surname = kolikrat.Prijmeni,
            };

            _context.Add(newPerson);
        }
        _context.SaveChanges();
    }

    public void Delete(Lidi clovek)
    {
        var query = from a in _context.Lide
                    where clovek.ID_Lidi == a.ID_Lidi
                    select a;

        Lidi al = query.FirstOrDefault();

        _context.Entry(al).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public int GetVolnaMista(int cisloStolu)
    {
        List<Lidi> al = _context.Lide.Where(clovek => clovek.ID_Stul == cisloStolu).ToList();

        int pocetMist = 0;

        pocetMist = _context.Stoly.Where(stul => stul.ID_Stul == cisloStolu).First().PocetMist;

        return pocetMist - al.Count();
    }
}
