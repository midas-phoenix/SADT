using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SA_TEST.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SA_TEST.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext db;

        public HomeController(AppDBContext appDB)
        {
            db = appDB;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PlateReport()
        {
            ViewData["Message"] = "Plate Numbers Generated";

            List<PlateNumbers> plates = new List<PlateNumbers>();
            var dbplates = db.PlateNumber;
            foreach (var item in dbplates)
            {
                plates.Add(new PlateNumbers { DateCreated = item.DateCreated, platenumber = item.platenumber, LocalGovt = item.LGACode, Owner = item.Owner });
            }

            return View(plates);
        }

        public IActionResult GeneratePlate()
        {
            ViewData["Message"] = "Generate Plate Number";
            var platecount = db.PlateNumber.Count();
            List<SelectListItem> localgovt = new List<SelectListItem>() {
                new SelectListItem(text: "Ikeja", value: "KJA"),
                new SelectListItem(text: "Alimosho", value: "ALI"),
                new SelectListItem(text: "Badagry", value: "BDG"),
                new SelectListItem(text: "Apapa", value: "APP"),
                new SelectListItem(text: "Ikorodu", value: "IKD"),
                new SelectListItem(text: "Mushin", value: "MUS"),
                new SelectListItem(text: "Festac", value: "FST")
            };

            ViewBag.LocalGovt = localgovt;
            ViewBag.Gplates = new List<string>();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneratePlate(NewNumberPlates numberPlates)
        {
            Random _random = new Random();
            List<TblPlateNumber> plates = new List<TblPlateNumber>();
            for (int i = 1; i <= numberPlates.Count; i++)
            {
                var lp = db.PlateNumber.Where(x=>x.LGACode==numberPlates.LocalGovt).LastOrDefault();
                int plint = lp == null ? 0 : lp.platenumberint;
                string lch;

                if (plint != 999)
                {
                    lch= lp == null ? "AA" : lp.platenumber.Substring(6, 2);
                }
                else
                {
                    plint = 0;
                    char ls = char.Parse(lp.platenumber.Substring(6, 1));
                    char rs = char.Parse(lp.platenumber.Substring(7, 1));
                    ls = rs == 'Z' ? (char)(ls + 1) : ls;
                    rs = rs == 'Z' ? 'A' : (char)(rs + 1);
                    lch = ls.ToString() + rs.ToString();
                }

                string number = (plint + 1).ToString("000");
                //char char1 = (char)('a' + _random.Next(26));
                //char char2 = (char)('a' + _random.Next(0, 26));

                //string rr = char1.ToString() + char2.ToString();

                string plate = numberPlates.LocalGovt + number + lch.ToUpper();
                //plates.Add(plate);

                await db.PlateNumber.AddAsync(new TblPlateNumber
                {
                    DateCreated = DateTime.Now,
                    LGACode = numberPlates.LocalGovt,
                    platenumberint = plint + 1,
                    platenumber = plate,
                    Owner = numberPlates.Owner
                });
                db.SaveChanges();
                //plates.Add(new TblPlateNumber
                //{
                //    DateCreated = DateTime.Now,
                //    LGACode = numberPlates.LocalGovt,
                //    platenumberint = plint + 1,
                ////    platenumber = plate,
                //    Owner = numberPlates.Owner
                //});
            }
            //await db.PlateNumber.AddRangeAsync(plates);
            //await db.SaveChangesAsync();

            ViewBag.Gplates = plates;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
