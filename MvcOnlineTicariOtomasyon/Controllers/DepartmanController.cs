using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult DepartmanEkle() //aynı method 2 defa yazıldı. çünkü hem sayfa yüklerken hemde veri gönderirken çalıştırılacak.
        {
            return View();  // burada boş bir sayfa çalışacak
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman d) //Departman sınıfından d türetildi.
        {
            d.Durum = true;
            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");   //departman eklendi. kaydedildi. sonrasında beni bir aksiyona yönlendir dedik.
        }

        public ActionResult DepartmanSil(int id)
        {
            var dep = c.Departmans.Find(id);
            dep.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var departman = c.Departmans.Find(id);
            return View("DepartmanGetir", departman);
        }

        public ActionResult DepartmanGuncelle(Departman d)
        {
            var dep = c.Departmans.Find(d.DepartmanId);  //dep olarak değişken oluşturuldu ve departmanid hafızaya alındı.
            dep.DepartmanAd = d.DepartmanAd;   //Departmans sınıfındaki departmanad a sağdaki d.departmanad değeri atandı
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();  //Aynı departmana ait personeli getiren sorgu kodu.
            var dpt = c.Departmans.Where(x => x.DepartmanId == id).Select(y => y.DepartmanAd).FirstOrDefault(); //tolist kullanılmamasının sebebi bir tablo değil tek bir değer getirilecek olması
            ViewBag.d = dpt; // yukarıdaki sorguyu viewbag.d altına aktardık.
            return View(degerler);

        }

        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler=c.SatisHarekets.Where(x=>x.Personelid == id).ToList();
            var per=c.Personels.Where(x=>x.PersonelId==id).Select(y=>y.PersonelAd +" "+ y.PersonelSoyad).FirstOrDefault();
            ViewBag.dpers = per;
            return View(degerler);

        }
    }
}