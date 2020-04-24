using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using VerificationDocs.Domain.Entities;

namespace VerificationDocs.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        // Имя свойства указывает таблицу, а параметр типа результата DbSet - тип модели, 
        // который инфраструктура Entity Framework должна использовать для представления строк в этой таблице
        public DbSet<StyleP> Style { get; set; }
    }
}
