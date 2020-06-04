using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerificationDocs.Domain.Abstract;

namespace VerificationDocs.WebUI.Controllers
{
    public class DocumentStatusController : Controller
    {
        // GET: DocumentStatus
        private IDocumentStatusRepository repository;

        public DocumentStatusController(IDocumentStatusRepository repo)
        {
            repository = repo;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}