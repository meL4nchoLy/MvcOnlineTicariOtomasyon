using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c= new Context();
        public ActionResult Index()
        {
            var degerler=c.Personels.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle() 
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()  // liste türünde deger1 adında bir değişken oluşturuldu.
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,  // Text alanı bize göstereceği alan bu sebeple kategori adı 
                                               Value = x.DepartmanId.ToString() //Value alanı arkada okuyacağı alan olacağı için id alanı
                                           }).ToList();

            ViewBag.dgr1 = deger1;   //view tarafından değer taşımak için viewbag kullanılıyor dgr1 adlı veriyi deger1 den atadık
            return View(); 
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}