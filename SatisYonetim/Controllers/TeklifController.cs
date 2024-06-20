using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SatisYonetim.Models;
using SatisYonetim.Repositories;

namespace SatisYonetim.Controllers
{
    public class TeklifController : Controller
    {
        private readonly TeklifRepository _teklifRepository;
        private readonly DurumRepository _durumRepository;
        public TeklifController(TeklifRepository teklifRepository, DurumRepository durumRepository)
        {
            _teklifRepository = teklifRepository;
            _durumRepository = durumRepository;
        }
        public IActionResult Index()
        {

            var teklifler = _teklifRepository.GetAllTeklif();
            var durumlar = _durumRepository.GetAllDurum();


            var modelList = new Tuple<List<Durum>, List<DurumViewModel>>(durumlar, teklifler);

            return View(modelList);
        }
        public IActionResult Delete(int Id)
        {
            var teklif = _teklifRepository.GetTeklifById(Id);
            if (teklif == null)
            {
                return Content("böyle bir sayfa bulunamadı...");
            }
            _teklifRepository.Delete(teklif);
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var durumListesi = _durumRepository.GetAllDurum();
            var durumSelectList = new SelectList(durumListesi, "DurumId", "DurumAdi");
            ViewBag.DurumListesi = durumSelectList;

            return View();
        }
        [HttpPost]
        public IActionResult Create(DurumViewModel drm)
        {
            _teklifRepository.Create(drm);
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var teklif = _teklifRepository.GetTeklifById(id);

            var durumListesi = _durumRepository.GetAllDurum();
            var durumSelectList = new SelectList(durumListesi, "DurumId", "DurumAdi");
            ViewBag.DurumListesi = durumSelectList;
            return View(teklif);
        }

        [HttpPost]
        public IActionResult Update(DurumViewModel model)
        {

            _teklifRepository.Update(model);
            return RedirectToAction("Index");

        }

    }
}
