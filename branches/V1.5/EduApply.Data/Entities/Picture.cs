using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Picture : BaseEntity<long>
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public long ApplicationId { get; set; }
    }
}
