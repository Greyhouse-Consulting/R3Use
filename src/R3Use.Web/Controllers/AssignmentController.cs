using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

            return View( _adapter.Adapt<IList<AssignmentViewModel>>(assignments));
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




            var errors = ModelState.ErrorCodes(model);

            return View();
        }
    }


    public static class ModelStateExtensions
    {
        public static IDictionary<string, int> ErrorCodes<T>(this ModelStateDictionary modelState, T t) where T : class
        {
            
            var propertyErrorCodes = new Dictionary<string, int>();
            var props = typeof(T).GetProperties();
            foreach (var propertyInfo in props)
            {
                var attribues = propertyInfo.GetCustomAttributes();

                foreach (var attribute in attribues)
                {
                    var code = attribute as IErrorCode;
                    var validation = attribute as ValidationAttribute;
                    if (code != null && validation != null)
                    {
                        try
                        {
                            validation.Validate(propertyInfo.GetValue(t), propertyInfo.Name);
                        }
                        catch (ValidationException e)
                        {
                            propertyErrorCodes.Add(propertyInfo.Name, code.ErrorCode);
                        }
                    }
                }
            }
            return propertyErrorCodes;
        }
    }
}