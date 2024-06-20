using Microsoft.AspNetCore.Mvc;
using SatisYonetim.Models;
using SatisYonetim.Repositories;

namespace SatisYonetim.Controllers
{
    public class DurumController : Controller
    {
        private readonly DurumRepository _durumRepository;
        public DurumController(DurumRepository durumRepository)
        {
            _durumRepository = durumRepository;
        }
        public IActionResult Index()
        {
            var durumlar = _durumRepository.GetAllDurum();
            return View(durumlar);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Durum durum)
        {
            _durumRepository.Create(durum);
            return RedirectToAction("Index", "Teklif");
        }
        public IActionResult Update(int Id)
        {
            var durum = _durumRepository.GetDurumById(Id);
            if (durum == null)
            {
                return Content("Böyle bir sayfa bulunamadı...");
            }
            return View(durum);
        }
        [HttpPost]
        public IActionResult Update(Durum durum)
        {
            _durumRepository.Update(durum);
            return RedirectToAction("Index", "Teklif");
        }
        public IActionResult Delete(int Id)
        {
            var durum = _durumRepository.GetDurumById(Id);
            if (durum == null)
            {
                return Content("Böyle bir sayfa bulunamadı");
            }
            _durumRepository.Delete(durum);
            return RedirectToAction("Index", "Teklif");
        }
    }
}
