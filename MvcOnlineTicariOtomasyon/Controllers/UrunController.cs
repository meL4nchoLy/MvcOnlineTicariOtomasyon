using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c= new Context();
        public ActionResult Index()
        {
            var urunler = c.Uruns.Where(x=>x.Durum==true).ToList();
            return View(urunler);
        }
        [HttpGet]
        public ActionResult YeniUrun() 
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()  // liste türünde deger1 adında bir değişken oluşturuldu.
                                           select new SelectListItem
                                           {
                                               Text=x.KategoriAd,  // Text alanı bize göstereceği alan bu sebeple kategori adı 
                                               Value=x.KategoriID.ToString() //Value alanı arkada okuyacağı alan olacağı için id alanı
                                           }).ToList();

            ViewBag.dgr1=deger1;   //view tarafından değer taşımak için viewbag kullanılıyor dgr1 adlı veriyi deger1 den atadık
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            
                var deger=c.Uruns.Find(id);
                deger.Durum = false;
                c.SaveChanges();
                return RedirectToAction("Index");
            }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()  // liste türünde deger1 adında bir değişken oluşturuldu.
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,  // Text alanı bize göstereceği alan bu sebeple kategori adı 
                                               Value = x.KategoriID.ToString() //Value alanı arkada okuyacağı alan olacağı için id alanı
                                           }).ToList();

            ViewBag.dgr1 = deger1;   //view tarafından değer taşımak için viewbag kullanılıyor dgr1 adlı veriyi deger1 den atadık
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }

        public ActionResult UrunGuncelle(Urun u)
        {
            var Urn = c.Uruns.Find(u.UrunId);  
            Urn.UrunAd = u.UrunAd;   
            Urn.AlisFiyat = u.AlisFiyat;
            Urn.SatisFiyat=u.SatisFiyat;
            Urn.Kategoriid=u.Kategoriid;
            Urn.Marka=u.Marka;
            Urn.Stok=u.Stok;
            Urn.UrunGorsel=u.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}