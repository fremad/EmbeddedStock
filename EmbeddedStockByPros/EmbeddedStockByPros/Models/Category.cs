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
            ComponentTypes = new List<ComponentType>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public ICollection<ComponentType> ComponentTypes { get; protected set; }
    }
}
