using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using R3Use.Core;
using R3Use.Core.Repository.Contracts;

namespace R3Use.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProspectRepository _prospectRepository;
        private readonly IAdapter _adapter;

        public HomeController(IProspectRepository prospectRepository, IAdapter adapter)
        {
            _prospectRepository = prospectRepository;
            _adapter = adapter;
        }

        public async Task<IActionResult> Index()
        {
            var prospects = await _prospectRepository.AllAsync();

            var viewModels =_adapter.Adapt<IList<Prospect>, IList<ProspectViewModel>>(prospects);

            return View(viewModels);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
