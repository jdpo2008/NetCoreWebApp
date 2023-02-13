using NetCoreWebApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Domain.Entities
{
    public class Product : BaseEntity
    {
        

        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubCategoryId { get; set; }

        //private readonly List<Image> images = new List<Image>();
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        //public virtual IReadOnlyCollection<Image> Images => images;

        public Product(string name, decimal price, int stock, string description)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }

        protected Product()
        {
        }

        //public void AddImage(Image image)
        //    => images.Add(image);
    }
}
