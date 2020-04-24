using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using VerificationDocs.Domain.Entities;

namespace VerificationDocs.Domain.Abstract
{
    public interface IStyleRepository
    {
        IEnumerable<StyleP> Styles { get; }
        /// <summary>
        /// Метод сохранения изменений в таблице (хранилище) стилей абзацев
        /// </summary>
        /// <param name="style"></param>
        void SaveStyleParagraph(StyleP style);
        /// <summary>
        /// Метод удаления данных из таблицы (хранилища) стилей абзацев
        /// </summary>
        /// <param name="paragraphID"></param>
        /// <returns></returns>
        StyleP DeleteStyleParagraph(int paragraphID);
    }
}
