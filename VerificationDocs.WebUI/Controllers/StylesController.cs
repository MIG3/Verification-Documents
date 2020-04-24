using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerificationDocs.Domain.Abstract;
using VerificationDocs.Domain.Entities;

namespace VerificationDocs.WebUI.Controllers
{
    public class StylesController : Controller
    {
        // GET: Styles
        private IStyleRepository repository;
        public StylesController(IStyleRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Styles);
        }
    }
}