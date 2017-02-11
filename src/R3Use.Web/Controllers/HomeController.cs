using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using R3Use.Core;
using R3Use.Core.Entities;
using R3Use.Core.Repository.Contracts;
using R3Use.Web.ViewModels;

namespace R3Use.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAdapter _adapter;

        public HomeController(IAssignmentRepository assignmentRepository, IAdapter adapter)
        {
            _assignmentRepository = assignmentRepository;
            _adapter = adapter;
        }

        public async Task<IActionResult> Index()
        {
            var prospects = await _assignmentRepository.AllAsync();

            var viewModels =_adapter.Adapt<IList<Assignment>, IList<AssignmentViewModel>>(prospects);

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
