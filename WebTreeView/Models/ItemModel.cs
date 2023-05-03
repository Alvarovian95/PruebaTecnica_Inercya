using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTreeView.Models
{
    public class ItemModel
    {
        public string Name { get; set; }
        public List<ItemModel> Children { get; set; }
    }
}