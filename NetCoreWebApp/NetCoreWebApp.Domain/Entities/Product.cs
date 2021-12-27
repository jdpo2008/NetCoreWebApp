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
        public Guid CetegoryId { get; set; }
        public Guid MarcaId { get; set; }
        public string ImageUrl { get; set; }

        private readonly List<Image> images = new List<Image>();
        public virtual Marca Marca { get; set; }
        public virtual Category Category { get; set; }
        public virtual IReadOnlyCollection<Image> Images => images;

        public Product(string name, decimal price, int stock, string description, string imageUrl)
        {
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            ImageUrl = imageUrl;
        }

        protected Product()
        {
        }

        public void AddImage(Image image)
            => images.Add(image);
    }
}
