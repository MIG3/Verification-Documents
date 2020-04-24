using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VerificationDocs.Domain.Entities
{
    public class DocumentStructure
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Display(Name = "ID документа")]
        public int DocID { get; set; }

        [Display(Name = "Тип документа")]
        public string DocType { get; set; }

        [Display(Name = "Номер раздела")]
        public int NumberSection { get; set; }

        [Display(Name = "Текст абзаца")]
        public string TextParagraph { get; set; }

        [Display(Name = "Номер абзаца")]
        public int NumberParagraph { get; set; }
    }
}
