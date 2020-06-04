using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerificationDocs.Domain.Abstract;

namespace VerificationDocs.WebUI.Controllers
{
    public class DocumentStructureController : Controller
    {
        // GET: DocumentStructure
        private IDocumentStructureRepository docStrucRepository;
        public DocumentStructureController(IDocumentStructureRepository repo)
        {
            docStrucRepository = repo;
        }
    }
}