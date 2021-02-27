using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class EncryptionKeys : BaseEntity<int>
    {
        public virtual byte[] EncryptionKey { get; set; }
        public virtual byte[] EncryptionIv { get; set; }
    }
}
