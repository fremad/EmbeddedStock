using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmbeddedStockByPros.Models
{
    public class CategoryComponenttypebinding
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public long ComponentTypeId { get; set; }
        public ComponentType ComponentType { get; set; }

    }
}
