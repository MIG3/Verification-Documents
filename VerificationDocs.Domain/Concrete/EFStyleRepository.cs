using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using VerificationDocs.Domain.Abstract;
using VerificationDocs.Domain.Entities;

namespace VerificationDocs.Domain.Concrete
{
    public class EFStyleRepository : IStyleRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<StyleP> Styles
        {
            get { return context.Style; }
        }
        /// <summary>
        /// Запись в БД (админ)
        /// </summary>
        /// <param name="style"></param>
        public void SaveStyleParagraph(StyleP style)
        {
            if (style.ParagraphID == 0)
                context.Style.Add(style);
            else
            {
                StyleP dbEntry = context.Style.Find(style.ParagraphID);
                if (dbEntry != null)
                {
                    dbEntry.Name = style.Name;
                    dbEntry.Font = style.Font;
                    dbEntry.Size = style.Size;
                    dbEntry.Color = style.Color;
                    dbEntry.Inscription = style.Inscription;
                    dbEntry.Alignment = style.Alignment;
                    dbEntry.LeftIndention = style.LeftIndention;
                    dbEntry.RightIndention = style.RightIndention;
                    //dbEntry.Interval = style.Interval;
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Удаление из БД (админ)
        /// </summary>
        /// <param name="paragraphID"></param>
        /// <returns></returns>
        public StyleP DeleteStyleParagraph(int paragraphID)
        {
            StyleP dbEntry = context.Style.Find(paragraphID);
            if (dbEntry != null)
            {
                context.Style.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
