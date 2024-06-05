using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage;
using Rezervace_Ples.Models.Objects;
using Rezervace_Ples.Models.Services;
using Rezervace_Ples.Models.TableObjects;
using System.Data;

namespace Rezervace_Ples.Controllers
{
    public class RezervaceController : Controller
    {
        private StulService stolyService;
        private LidiService lidiService;
        private int isAdmin;

        public RezervaceController()
        {
            stolyService = new StulService();
            lidiService = new LidiService();
        }

        [HttpGet]
        public FileResult ExportPeopleInExcel()
        {
            var people = lidiService.GetCompleteList();
            var fileName = "list.xlsx";
            return GenerateExcel(fileName, people);
        }

        private FileResult GenerateExcel(string fileName, IEnumerable<Lidi> lidi)
        {
            DataTable dataTable = new DataTable("Lidi");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("Name"),
                new DataColumn("SurName"),
                new DataColumn("Stul"),
            });

            foreach (var clovek in lidi)
            {
                dataTable.Rows.Add(clovek.ID_Lidi, clovek.Name, clovek.Surname, clovek.ID_Stul);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

        public IActionResult Prizemi()
        {
            NastavitViewBagData("Prizemi");
            return View();
        }
       
        public IActionResult Balkon()
        {
            NastavitViewBagData("Balkon");
            return View();
        }

        public IActionResult StulDetail(int IDStolu)
        {
            Stul vybranyStul = stolyService.GetStul(IDStolu);

            var lidiList = lidiService.GetList(IDStolu);
            this.ViewBag.lidi = lidiList;

            
            this.ViewBag.stul = vybranyStul;

            int volnaMista = lidiService.GetVolnaMista(vybranyStul.ID_Stul);
            vybranyStul.ZbyvajiciMista = volnaMista;

            this.ViewBag.id = IDStolu;

            this.ViewBag.Text = sklonovaniSlovaMisto(volnaMista);
            this.ViewBag.TextVolne = sklonovaniSlovaVolne(volnaMista);

            return View();
        }

        [HttpGet]
        public IActionResult Create(int IDStolu)
        {
            int maxPocet = lidiService.GetVolnaMista(IDStolu);
            if (maxPocet == 0)
            {
                return RedirectToAction("StulDetail", new { IDStolu = IDStolu });
            }
            ViewBag.id = IDStolu;
            ViewBag.MaxPocet = maxPocet;

            var model = new KolikratNovyClovekModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(int IDStolu, KolikratNovyClovekModel zaznam)
        {
            ViewBag.id = IDStolu;
            if (ModelState.IsValid)
            {
                lidiService.Add(zaznam, IDStolu);
                return RedirectToAction("StulDetail", new { IDStolu = IDStolu });
            }

            return View(zaznam);
        }

        [HttpGet]
        public IActionResult Update(int id, int IDStolu)        
        {
            ViewBag.IDStolu = IDStolu;
            ViewBag.id = id;
            return View(lidiService.GetClovek(id));
        }

        [HttpPost]
        public IActionResult Update(Lidi clovek, int id, int IDStolu)
        {
            ViewBag.IDStolu = IDStolu;
            ViewBag.id = id;
            if (ModelState.IsValid)
            {
                lidiService.Update(clovek, id);
                return RedirectToAction("StulDetail", new { IDStolu = IDStolu });
            }
            return View(clovek);
        }

        [HttpGet]
        public IActionResult Delete(int id, int IDStolu)
        {
            lidiService.Delete(lidiService.GetClovek(id));
            return RedirectToAction("StulDetail", new { IDStolu = IDStolu });
        }

        [HttpGet]
        public IActionResult Zrusit(int IDStolu)
        {
            return RedirectToAction("StulDetail", new { IDStolu = IDStolu });
        }

        private void NastavitViewBagData(string akce)
        {
            isAdmin = (int)HttpContext.Session.GetInt32("Admin");
            var stolyList = akce == "Balkon" ? stolyService.StolyVrchniList() : stolyService.StolySpodniList();
            ViewBag.listStoly = stolyList;

            int celkovaZbyvajiciMista = 0;
            int celkovaMista = 0;

            foreach (var item in stolyList)
            {
                int volnaMista = lidiService.GetVolnaMista(item.ID_Stul);
                item.ZbyvajiciMista = volnaMista;

                celkovaZbyvajiciMista += item.ZbyvajiciMista;
                celkovaMista += item.PocetMist;
            }

            ViewBag.mistCelkem = celkovaMista;
            ViewBag.volnychMistCelkem = celkovaZbyvajiciMista;
            ViewBag.Admin = isAdmin;

            ViewBag.Text = sklonovaniSlovaMisto(celkovaZbyvajiciMista);
            ViewBag.TextVolne = sklonovaniSlovaVolne(celkovaZbyvajiciMista);
        }

        public string sklonovaniSlovaMisto(int cislo)
        {
            if (cislo == 1)
            {
                return $"místo";
            }
            else if (cislo >= 2 && cislo <= 4)
            {
                return $"místa";
            }
            else
            {
                return $"míst";
            }
        }

        public string sklonovaniSlovaVolne(int cislo)
        {
            if (cislo == 1)
            {
                return $"volné";
            }
            else if (cislo >= 2 && cislo <= 4)
            {
                return $"volná";
            }
            else
            {
                return $"volných";
            }
        }
    }
}