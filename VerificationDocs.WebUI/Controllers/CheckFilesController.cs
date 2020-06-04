using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VerificationDocs.Domain.Abstract;
using VerificationDocs.Domain.Entities;
using word = Microsoft.Office.Interop.Word;
using Moq;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace VerificationDocs.WebUI.Controllers
{
    [Authorize]
    public class CheckFilesController : Controller
    {
        //private IDocumentStructureRepository textRepositiry;
        //public CheckFilesController(IDocumentStructureRepository repo)
        //{
        //    textRepositiry = repo;
        //}

        private word.Paragraphs wordparagraphs;
        private word.Paragraph wordparagraph;
        

        [HttpGet]
        public ViewResult Index()
        {
            //if (User.Identity.IsAuthenticated)
                return View();
            //else
            //    return TempData["message"] = string.Format("Вы не авторизованы", );
            
        }
        /// <summary>
        /// Метод сохранения файла пользователю на ПК
        /// </summary>
        /// <param name="path">путь к местоположению на сервере</param>
        private void SaveAsFile(string path, string type)
        {
            Response.ContentType = "text";
            if (type == "docx")
                Response.AppendHeader("Content-Disposition", "attachment; filename=Результат.docx");
            else if (type == "txt")
                Response.AppendHeader("Content-Disposition", "attachment; filename=Результат.txt");
            Response.TransmitFile(path);
            Response.End();
        }
        /// <summary>
        /// Метод сортировки списка литературы
        /// </summary>
        /// <param name="path">Путь к проверяемому файлу</param>
        protected void ParsingFile(string path)
        {
            var pathSave = System.IO.Path.Combine(Server.MapPath("~/FilesLibrary/"), "answerReport");
            var tempPathSave = @"D:\Документы\Диплом магистра\Verification\VerificationDocs.WebUI\FilesLibrary\Ответ.docx";
            int countParagraph;
            string type = "docx";
            DocumentStructure document = new DocumentStructure();
            //запускаем Word
            var wordApp = new word.Application();
            var wordDoc = wordApp.Documents.Open(path);

            // Показывает открытый файл пользователю
            wordApp.Visible = true;

            wordparagraphs = wordDoc.Paragraphs;
            countParagraph = wordDoc.Paragraphs.Count;

            List<string> text = new List<string>();
            List<string> libr = new List<string>();
            List<int> librLevel = new List<int>();
            // https://coderoad.ru/4016960/C-%D0%BF%D0%BE%D0%B8%D1%81%D0%BA-%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0-%D0%B2-Word-%D0%B8-%D0%BF%D0%BE%D0%BB%D1%83%D1%87%D0%B5%D0%BD%D0%B8%D0%B5-%D0%B4%D0%B8%D0%B0%D0%BF%D0%B0%D0%B7%D0%BE%D0%BD%D0%B0-%D1%80%D0%B5%D0%B7%D1%83%D0%BB%D1%8C%D1%82%D0%B0%D1%82%D0%B0
            // https://coderoad.ru/11778013/%D0%9A%D0%B0%D0%BA-%D0%BF%D0%BE%D0%BB%D1%83%D1%87%D0%B8%D1%82%D1%8C-%D0%BD%D0%BE%D0%BC%D0%B5%D1%80-%D0%B0%D0%B1%D0%B7%D0%B0%D1%86%D0%B0-%D0%B2-%D0%B4%D0%BE%D0%BA%D1%83%D0%BC%D0%B5%D0%BD%D1%82%D0%B5-word-%D1%81-C
            string title = "СПИСОК ИСПОЛЬЗОВАННЫХ ИСТОЧНИКОВ";
            // Поиск объекта в документе
            /*foreach (word.Paragraph aPar in wordparagraphs)
            {
                word.Range parRng = aPar.Range;
                text.Add(parRng.Text);
                //string sText = parRng.Text;
                int nLevel = parRng.ListFormat.ListLevelNumber;
                wordApp.Selection.Find.Execute(title);
                //word.Range range2 = wordApp.Selection.Range;
                
            }
            */
            for (int i = 1; i < countParagraph; i++)
            {
                if (wordDoc.Paragraphs[i].Range.Text != "\r")
                    text.Add(wordDoc.Paragraphs[i].Range.Text);
            }
            foreach (var item in text)
            {
                
                if (item.Contains("СПИСОК ИСПОЛЬЗОВАННЫХ ИСТОЧНИКОВ"))
                {
                    var index = text.IndexOf(item);
                    wordApp.Selection.Start = index + 1;
                    //libr.Add(wordDoc.ListTemplates.)
                    //var library = wordDoc.ListParagraphs.Count;

                    foreach (var l in librLevel)
                    {
                        using (StreamWriter sw = new StreamWriter(pathSave, false, System.Text.Encoding.Default))
                        {
                            sw.WriteLine(l);
                        }

                    }
                }
            }
            
            wordDoc.Close();
            wordApp.Quit();
            SaveAsFile(tempPathSave, type);
        }

        /// <summary>
        /// Метод проверки оформления документа
        /// </summary>
        /// <param name="path">Путь к проверяемому файлу</param>
        /// <param name="action">Тип проверяемого файла</param>
        protected void ParsingFile(string path, string action)
        {
            int countParagraph, countSection = 0, countSubsection = 0, index;
            string pathSave = "", hat = "";
            string type = "txt";
            DocumentStructure document = new DocumentStructure();
            //запускаем Word
            var wordApp = new word.Application();
            var wordDoc = wordApp.Documents.Open(path);

            // Показывает открытый файл пользователю
            wordApp.Visible = false;

            wordparagraphs = wordDoc.Paragraphs;
            countParagraph = wordDoc.Paragraphs.Count;

            List<string> text = new List<string>();

            List<string> fontName = new List<string>();
            List<double> fontSize = new List<double>();
            List<string> color = new List<string>();
            List<int> inscription = new List<int>();
            List<string> alignment = new List<string>();
            List<double> leftIndention = new List<double>();
            List<double> rightIndention = new List<double>();

            List<string> answer = new List<string>();
            
            for (int i = 1; i < countParagraph; i++)
            {
                if (wordDoc.Paragraphs[i].Range.Text != "\r" && wordDoc.Paragraphs[i].Range.Text != "\r\a")
                {
                    text.Add(wordDoc.Paragraphs[i].Range.Text);
                    fontName.Add(wordDoc.Paragraphs[i].Range.Font.Name);
                    fontSize.Add(wordDoc.Paragraphs[i].Range.Font.Size);
                    color.Add(wordDoc.Paragraphs[i].Range.Font.Color.ToString());
                    inscription.Add(wordDoc.Paragraphs[i].Range.Font.Bold);
                    alignment.Add(wordDoc.Paragraphs[i].Alignment.ToString());
                    leftIndention.Add(wordDoc.Paragraphs[i].LeftIndent);
                    rightIndention.Add(wordDoc.Paragraphs[i].RightIndent);
                }
            }

            if (action == "BKP")
            {
                pathSave = System.IO.Path.Combine(Server.MapPath("~/FilesChecks/"), "answerBKR.txt");
                foreach (var item in text)
                {
                    if (item.Contains("ВВЕДЕНИЕ"))
                        countSection++;
                    if (item.Contains("ОСНОВНАЯ ЧАСТЬ"))
                        countSection++;
                    if (item.Contains("ЗАКЛЮЧЕНИЕ"))
                        countSection++;
                    //if (item.Contains("СПИСОК ИСПОЛЬЗОВАННЫХ ИСТОЧНИКОВ"))
                    //    countSection++;
                }
                if (countSection < 3)
                {
                    using (StreamWriter sw = new StreamWriter(pathSave, false, System.Text.Encoding.Default))
                        sw.WriteLine("Неправильная структура файла. Количество разделов недостаточно.");
                }
                else if (countSection >= 3)
                {
                    if (text.IndexOf("ВВЕДЕНИЕ") > text.IndexOf("ОСНОВНАЯ ЧАСТЬ") ||
                        text.IndexOf("ВВЕДЕНИЕ") > text.IndexOf("ЗАКЛЮЧЕНИЕ") ||
                        text.IndexOf("ОСНОВНАЯ ЧАСТЬ") > text.IndexOf("ЗАКЛЮЧЕНИЕ"))
                        using (StreamWriter sw = new StreamWriter(pathSave, false, System.Text.Encoding.Default))
                            sw.WriteLine("Неправильная структура файла. Порядок разделов неверен.");
                    else
                        using (StreamWriter sw = new StreamWriter(pathSave, false, System.Text.Encoding.Default))
                            sw.WriteLine("Cтруктура файла верна.");
                }
                
                SaveAsFile(pathSave, type);
            }
            else if (action == "Report")
            {
                pathSave = System.IO.Path.Combine(Server.MapPath("~/FilesChecks/"), "answerReport");
                foreach (var item in text)
                {
                    index = text.IndexOf(item);
                    if (item.Contains("МИНИСТЕРСТВО ОБРАЗОВАНИЯ И НАУКИ РОССИЙСКОЙ ФЕДЕРАЦИИ"))
                    {
                        hat = "МИНИСТЕРСТВО ОБРАЗОВАНИЯ И НАУКИ РОССИЙСКОЙ ФЕДЕРАЦИИ ";
                        if (fontName[index] != "Times New Roman")
                            answer.Add("Строка МИНИСТЕРСТВО ОБРАЗОВАНИЯ И НАУКИ РОССИЙСКОЙ ФЕДЕРАЦИИ имеет неправильный шрифт.");
                        if (fontSize[index] != 10)
                            answer.Add("Строка МИНИСТЕРСТВО ОБРАЗОВАНИЯ И НАУКИ РОССИЙСКОЙ ФЕДЕРАЦИИ имеет неправильный размер шрифта.");
                    }
                    if (item.Contains("ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ"))
                    {
                        hat = string.Concat(hat, "ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ ");
                        if (fontName[index] != "Times New Roman")
                            answer.Add("Строка ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ имеет неправильный шрифт.");
                        if (fontSize[index] != 10)
                            answer.Add("Строка ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ имеет неправильный размер шрифта.");
                    }
                    if (item.Contains("«МОСКОВСКИЙ АВИАЦИОННЫЙ ИНСТИТУТ"))
                    {
                        hat = string.Concat(hat, "«МОСКОВСКИЙ АВИАЦИОННЫЙ ИНСТИТУТ ");
                        if (fontName[index] != "Times New Roman")
                            answer.Add("Строка МОСКОВСКИЙ АВИАЦИОННЫЙ ИНСТИТУТ имеет неправильный шрифт.");
                        if (fontSize[index] != 10)
                            answer.Add("Строка МОСКОВСКИЙ АВИАЦИОННЫЙ ИНСТИТУТ имеет неправильный размер шрифта.");

                    }
                    if (item.Contains("(национальный исследовательский университет)»"))
                    {
                        hat = string.Concat(hat, "(национальный исследовательский университет)»");
                        if (fontName[index] != "Times New Roman")
                            answer.Add("Строка (национальный исследовательский университет) имеет неправильный шрифт.");
                        if (fontSize[index] != 10)
                            answer.Add("Строка (национальный исследовательский университет) имеет неправильный размер шрифта.");
                        if (inscription[index]!= 0)
                            answer.Add("Строка (национальный исследовательский университет) имеет неправильное начертание.");

                    }
                    //if (item.Contains("МИНИСТЕРСТВО ОБРАЗОВАНИЯ И НАУКИ РОССИЙСКОЙ ФЕДЕРАЦИИ ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ"))
                    //{
                    //    hat = "МИНИСТЕРСТВО ОБРАЗОВАНИЯ И НАУКИ РОССИЙСКОЙ ФЕДЕРАЦИИ ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ";
                    //}
                    if (item.Contains("студента"))
                        countSection++;
                    if (item.Contains("Институт"))
                        countSection++;
                    if (item.Contains("Кафедра"))
                        countSection++;
                    if (item.Contains("Группа"))
                        countSection++;
                    if (item.Contains("Направление подготовки"))
                        countSection++;
                    if (item.Contains("Квалификация"))
                        countSection++;
                    if (item.Contains("Наименование темы"))
                        countSection++;
                    if (item.Contains("Рецензент"))
                        countSection++;
                    if (item.StartsWith("Отмеченные достоинства"))
                        countSection++;
                    if (item.StartsWith("Отмеченные недостатки"))
                        countSection++;
                    if (item.StartsWith("Заключение"))
                        countSection++;
                }
                if (hat != "МИНИСТЕРСТВО ОБРАЗОВАНИЯ И НАУКИ РОССИЙСКОЙ ФЕДЕРАЦИИ ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ «МОСКОВСКИЙ АВИАЦИОННЫЙ ИНСТИТУТ (национальный исследовательский университет)»")
                {
                    using (StreamWriter sw1 = new StreamWriter(pathSave, false, System.Text.Encoding.Default))
                        sw1.WriteLine("Шапка в рецензии неверна.");
                }
                else if (countSection < 10)
                    using (StreamWriter sw1 = new StreamWriter(pathSave, false, System.Text.Encoding.Default))
                        sw1.WriteLine("Количество граф недостаточно. (Графы: институт, кафедра, отмеченные достоинства и так далее).");
                else if (answer.Count > 0)
                {
                    foreach (var item in answer)
                    {
                        using (StreamWriter sw1 = new StreamWriter(pathSave, false, System.Text.Encoding.Default))
                            sw1.WriteLine(item);
                    }
                }
                else
                {
                    using (StreamWriter sw1 = new StreamWriter(pathSave, false, System.Text.Encoding.Default))
                        sw1.WriteLine("Cтруктура верна.");
                }
                
            }
            wordDoc.Close();
            wordApp.Quit();
            SaveAsFile(pathSave, type);
        }
        [HttpPost]
        public ActionResult UploadFileToCheck(HttpPostedFileBase file, string action)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    // Вытягивает название загружаемого файла из веб-формы после его выбора
                    string fil = System.IO.Path.GetFileName(file.FileName);
                    // Формирует новый путь для сохранения (загрузки) файла
                    string path = System.IO.Path.Combine(Server.MapPath("~/FilesChecks/"), fil);
                    // Сохраняет загружаемый файл в эту папку
                    file.SaveAs(path);
                    ParsingFile(path, action);
                }
                return RedirectToAction("Index");
            }
            
            return View("UploadFileToCheck");
        }
        [HttpPost]
        public ActionResult UploadFileToSort(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    // Вытягивает название загружаемого файла из веб-формы после его выбора
                    string fil = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/FilesLibrary/"), fil);
                    file.SaveAs(path);
                    ParsingFile(path);
                }
                return RedirectToAction("Index");
            }
            
            return View("UploadFileToSort");
        }
    }
}