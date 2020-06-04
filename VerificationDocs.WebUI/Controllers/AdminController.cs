using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerificationDocs.Domain.Abstract;
using VerificationDocs.Domain.Entities;

namespace VerificationDocs.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        IStyleRepository styleRepository;
        IDocumentStatusRepository docStatusRepository;
        IDocumentStructureRepository docStructRepository;
        IDocumentRepository docRepository;
        IUserRepository userRepository;
        ISecurityRepository secRepository;

        public AdminController(IStyleRepository repo)
        {
            styleRepository = repo;
        }

        public ViewResult MainAdmin()
        {
            return View();
        }

        public ViewResult IndexStyle()
        {
            return View(styleRepository.Styles);
        }

        /// <summary>
        /// Этот простой метод ищет стиль с идентификатором,
        /// соответствующим значению параметра paragraphId, и передает его как объект модели представления методу View().
        /// </summary>
        /// <param name="paragraphId"></param>
        /// <returns></returns>
        public ViewResult Edit(int paragraphId)
        {
            StyleP style = styleRepository.Styles
                .FirstOrDefault(s => s.ParagraphID == paragraphId);
            return View(style);
        }
        // Перегруженная версия Edit() для сохранения изменений
        [HttpPost]
        public ActionResult Edit(StyleP style)
        {
            if (ModelState.IsValid)
            {
                styleRepository.SaveStyleParagraph(style);
                // После записи изменений в хранилище мы сохраняем сообщение с применением средства TempData. 
                // Объект TempData - это словарь пар "ключ/значение", похожий на используемые ранее данные сеанса и ViewBag. 
                // Основное его отличие от данных сеанса состоит в том, что в конце HTTP-запроса объект TempData удаляется.
                TempData["message"] = string.Format("Изменения в стиле абзаца \"{0}\" были сохранены", style.Name);
                return RedirectToAction("IndexStyle");
            }
            else
            {
                return View(style);
            }
        }
        /// <summary>
        /// Метод добавления новых требований. Он не визуализирует свое стандартное представление.
        /// </summary>
        /// <returns></returns>
        public ViewResult Create()
        {
            return View("Edit", new StyleP());
        }

        /// <summary>
        /// Метод удаления данных.
        /// </summary>
        /// <param name="paragraphID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int paragraphID)
        {
            StyleP deletedGame = styleRepository.DeleteStyleParagraph(paragraphID);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Стиль \"{0}\" абзаца был удалён",
                    deletedGame.Name);
            }
            return RedirectToAction("IndexStyle");
        }
    }
}
