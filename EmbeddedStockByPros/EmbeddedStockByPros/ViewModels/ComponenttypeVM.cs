using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmbeddedStockByPros.Models;

namespace EmbeddedStockByPros.ViewModels
{
    public class ComponenttypeVM
    {
        public string ComponentName { get; set; }
        public string ComponentInfo { get; set; }
        public string Location { get; set; }
        public ComponentTypeStatus Status { get; set; }
        public string Datasheet { get; set; }
        public string ImageUrl { get; set; }
        public string Manufacturer { get; set; }
        public string WikiLink { get; set; }
        public string AdminComment { get; set; }
        public virtual ESImage Image { get; set; }
        //public ICollection<Component> Components { get; protected set; }
        public string Categories { get; set; }
    }
}
