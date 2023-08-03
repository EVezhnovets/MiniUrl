using Microsoft.AspNetCore.Mvc;
using MiniUrl.ApplicationCore.Entities;
using MiniUrl.ApplicationCore.Interfaces;
using MiniUrl.Web.Models;

namespace MiniUrl.Web.Controllers
{
    public class MainController : Controller
    {
        private readonly IAppLogger<MainController> _logger;
        private readonly IRepository<MiniUrlItem> _miniUrlRepository;

        public MainController(IRepository<MiniUrlItem> miniUrlRepository, IAppLogger<MainController> logger)
        {
            _miniUrlRepository = miniUrlRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _miniUrlRepository.GetAllAsync();
            var miniUrlIndexVM = new MiniUrlIndexVM
            {
                MiniUrls = result,
                MiniUrlItem = new MiniUrlItem()
            };
            return View(miniUrlIndexVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MiniUrlIndexVM miniUrlIndexVM)
        {
            var miniUrlTodb = new MiniUrlItem
            {
                Id = Guid.NewGuid(),
                Url = miniUrlIndexVM.MiniUrlItem!.Url,
                Domain = "miniurl.com",
                Alias = miniUrlIndexVM.MiniUrlItem.Alias,
                Tags = miniUrlIndexVM.MiniUrlItem.Tags,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(180)
            };
            _ = await _miniUrlRepository.AddAsync(miniUrlTodb);
            return RedirectToAction("Index");
        }
    }
}