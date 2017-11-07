using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStockByPros.Models
{
    public class Category
    {
        public Category()
        {
            CategoryComponenttypebindings = new List<CategoryComponenttypebinding>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryComponenttypebinding> CategoryComponenttypebindings { get; protected set; }
    }
}
