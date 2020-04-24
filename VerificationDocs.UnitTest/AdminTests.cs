using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VerificationDocs.Domain.Abstract;
using VerificationDocs.Domain.Entities;
using VerificationDocs.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace VerificationDocs.UnitTest
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Styles()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IStyleRepository> mock = new Mock<IStyleRepository>();
            mock.Setup(m => m.Styles).Returns(new List<StyleP>
            {
                new StyleP { ParagraphID = 0, Name = "Параграф 1"},
                new StyleP { ParagraphID = 1, Name = "Параграф 2"},
                new StyleP { ParagraphID = 2, Name = "Параграф 3"},
                new StyleP { ParagraphID = 3, Name = "Параграф 4"},
                new StyleP { ParagraphID = 4, Name = "Параграф 5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            List<StyleP> result = ((IEnumerable<StyleP>)controller.Index().
                ViewData.Model).ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Параграф 1", result[0].Name);
            Assert.AreEqual("Параграф 2", result[1].Name);
            Assert.AreEqual("Параграф 3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Style()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IStyleRepository> mock = new Mock<IStyleRepository>();
            mock.Setup(m => m.Styles).Returns(new List<StyleP>
    {
        new StyleP { ParagraphID = 1, Name = "Параграф 1"},
        new StyleP { ParagraphID = 2, Name = "Параграф 2"},
        new StyleP { ParagraphID = 3, Name = "Параграф 3"},
        new StyleP { ParagraphID = 4, Name = "Параграф 4"},
        new StyleP { ParagraphID = 5, Name = "Параграф 5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            StyleP game1 = controller.Edit(1).ViewData.Model as StyleP;
            StyleP game2 = controller.Edit(2).ViewData.Model as StyleP;
            StyleP game3 = controller.Edit(3).ViewData.Model as StyleP;

            // Assert
            Assert.AreEqual(1, game1.ParagraphID);
            Assert.AreEqual(2, game2.ParagraphID);
            Assert.AreEqual(3, game3.ParagraphID);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Style()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IStyleRepository> mock = new Mock<IStyleRepository>();
            mock.Setup(m => m.Styles).Returns(new List<StyleP>
    {
        new StyleP { ParagraphID = 1, Name = "Параграф 1"},
        new StyleP { ParagraphID = 2, Name = "Параграф 2"},
        new StyleP { ParagraphID = 3, Name = "Параграф 3"},
        new StyleP { ParagraphID = 4, Name = "Параграф 4"},
        new StyleP { ParagraphID = 5, Name = "Параграф 5"}
    });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие
            StyleP result = controller.Edit(6).ViewData.Model as StyleP;

            // Assert
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IStyleRepository> mock = new Mock<IStyleRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Game
            StyleP game = new StyleP { Name = "Test" };

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(game);

            // Утверждение - проверка того, что к хранилищу производится обращение
            mock.Verify(m => m.SaveStyleParagraph(game));

            // Утверждение - проверка типа результата метода
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IStyleRepository> mock = new Mock<IStyleRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Game
            StyleP game = new StyleP { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(game);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveStyleParagraph(It.IsAny<StyleP>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Games()
        {
            // Организация - создание объекта Game
            StyleP game = new StyleP { ParagraphID = 2, Name = "Абзац 1" };

            // Организация - создание имитированного хранилища данных
            Mock<IStyleRepository> mock = new Mock<IStyleRepository>();
            mock.Setup(m => m.Styles).Returns(new List<StyleP>
            {
                new StyleP { ParagraphID = 1, Name = "Абзац 1"},
                new StyleP { ParagraphID = 2, Name = "Абзац 2"},
                new StyleP { ParagraphID = 3, Name = "Абзац 3"},
                new StyleP { ParagraphID = 4, Name = "Абзац 4"},
                new StyleP { ParagraphID = 5, Name = "Абзац 5"}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление игры
            controller.Delete(game.ParagraphID);

            // Утверждение - проверка того, что метод удаления в хранилище
            // вызывается для корректного объекта Style
            mock.Verify(m => m.DeleteStyleParagraph(game.ParagraphID));
        }
    }
}
