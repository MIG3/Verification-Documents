using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerificationDocs.Domain.Entities;

namespace VerificationDocs.Domain.Abstract
{
    public interface IDocumentStructureRepository
    {
        IEnumerable<DocumentStructure> docStructures { get; }
        /// <summary>
        /// Метод сохранения изменений в таблице (хранилище)  структуры документа
        /// </summary>
        /// <param name="style"></param>
        void SaveStructureDocs(DocumentStructure struc);
        /// <summary>
        /// Метод удаления данных из таблицы (хранилища) структуры документа
        /// </summary>
        /// <param name="paragraphID"></param>
        /// <returns></returns>
        StyleP DeleteStructureDocs(int docID);
    }
}
