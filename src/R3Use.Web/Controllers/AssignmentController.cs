using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using R3Use.Core.Entities;
using R3Use.Core.Repository.Contracts;
using R3Use.Web.ViewModels;

namespace R3Use.Web.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IAdapter _adapter;
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentController(IAdapter adapter, IAssignmentRepository assignmentRepository)
        {
            _adapter = adapter;
            _assignmentRepository = assignmentRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var assignments = await _assignmentRepository.AllAsync();

            return View(_adapter.Adapt<IList<AssignmentViewModel>>(assignments));
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(AssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var assignment = _adapter.Adapt<Assignment>(model);

                _assignmentRepository.AddAsync(assignment);

                return RedirectToAction("Get");

            }

            return View();
        }
    }
}