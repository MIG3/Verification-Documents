using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VerificationDocs.Domain.Entities
{
    public class StyleP
    {
        //Инфраструктуре MVC Framework можно указать, каким образом создавать редакторы для свойств, 
        //используя метаданные модели. Это позволяет применять к свойствам нового класса модели атрибуты, 
        //оказывая влияние на вывод метода Html.EditorForModel().
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ParagraphID { get; set; }

        [Display(Name = "Название стиля")]
        public string Name { get; set; }

        [Display(Name = "Шрифт")]
        [Required(ErrorMessage = "Пожалуйста, введите название шрифта")]
        public string Font { get; set; }

        [Display(Name = "Размер шрифта")]
        [Required(ErrorMessage = "Пожалуйста, введите размер текста")]
        public string Size { get; set; }

        [Display(Name = "Начертание")]
        [Required(ErrorMessage = "Пожалуйста, введите тип начертания (курсив, обычный, полужирный) текста")]
        public string Inscription { get; set; }

        [Display(Name = "Цвет текста")]
        [Required(ErrorMessage = "Пожалуйста, введите значение цвета текста")]
        public string Color { get; set; }

        [Display(Name = "Выравнивание")]
        [Required(ErrorMessage = "Пожалуйста, введите значение выравнивания (по центру, по ширине) текста")]
        public string Alignment { get; set; }

        //[Display(Name = "Межстрочный интервал")]
        //[Required]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение > 0 межстрочного интервала")]
        //public double Interval { get; set; }

        [Display(Name = "Отступ по левому краю")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение отступа по правому краю > 0")]
        public double LeftIndention { get; set; }

        [Display(Name = "Отступ по правому краю")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите значение > 0 отступа по правому краю")]
        public double RightIndention { get; set; }
    }
}
