using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class ExamVenue : BaseEntity<int>
    {
        public int VenueId { get; set; }
        public DateTime ExamDate { get; set; }
        public int NoOfAllocatedSeats { get; set; }
        public bool IsFilled { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("VenueId")]
        public virtual Venues Venue { get; set; }
    }
}
