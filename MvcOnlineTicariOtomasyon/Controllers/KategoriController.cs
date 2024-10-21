using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();  //context sınıfından c isminde bir nesne türetildi.
        public ActionResult Index()
        {
            var degerler = c.Kategoris.ToList();  //var değişkeni tüm değişkenleri kapsadığı için seçildi. değişkene tolist metotu ile kategorileri getir komutu verildi.
            return View(degerler);  // tolistle oluşturulen değişken view ile çalıştırıldı.
        }

        [HttpGet]
        public ActionResult KategoriEkle() //aynı method 2 defa yazıldı. çünkü hem sayfa yüklerken hemde veri gönderirken çalıştırılacak.
        {
            return View();  // burada boş bir sayfa çalışacak
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k) //Kategori sınıfından k türetildi.
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");   //kategori eklendi. kaydedildi. sonrasında beni bir aksiyona yönlendir dedik.
        }

        public ActionResult KategoriSil(int id)
        { 
            var ktg=c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id) 
        { 
        var kategori=c.Kategoris.Find(id);
            return View("KategoriGetir",kategori);
        }

        public ActionResult KategoriGuncelle(Kategori k) 
        {
            var ktgr=c.Kategoris.Find(k.KategoriID);  //ktgr olarak değişken oluşturuldu ve kategoriid hafızaya alındı.
            ktgr.KategoriAd=k.KategoriAd;   //Kategoris sınıfındaki kategoriad a sağdaki k.kategoriad değeri atandı
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}