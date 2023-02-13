using NetCoreWebApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Domain.Entities
{
    public class Image : BaseEntity
    {
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }

        public virtual Product Product { get; set; }

        public virtual Category Category { get; set; }

        public Image(string imageUrl, bool isMain)
        {
            Id = Guid.NewGuid();
            ImageUrl = imageUrl;
            SetIsMain(isMain);
        }

        protected Image()
        {
        }

        public void SetIsMain(bool isMain)
            => IsMain = isMain;
    }
}
