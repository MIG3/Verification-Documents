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
        /// <summary>
        /// Вызов метода View() подобного рода (без указания имени представления) 
        /// сообщает инфраструктуре о том, что нужно визуализировать стандартное представление для метода действия. 
        /// Передавая методу View() список объектов Style, мы снабжаем инфраструктуру данными, которыми необходимо 
        /// заполнить объект Model в строго типизированном представлении.
        /// </summary>
        /// <returns></returns>
        public ViewResult List()
        {
            return View(repository.Styles);
        }
    }
}