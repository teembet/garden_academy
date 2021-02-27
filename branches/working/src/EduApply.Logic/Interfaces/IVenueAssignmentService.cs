using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface IVenueAssignmentService
    {

        IEnumerable<Venues> GetVenues();
        Venues GetVenue(int id);
        IEnumerable<Venues> GetVenue(string name);
        void SaveVenue(Venues venue);
        void UpdateVenue(Venues venue);
        void UpdateExamVenue(ExamVenue examVenue);
        VenueMappings GetVenueMappings(int formId, int courseId, int programId, int examVenueId);
        void DeleteVenueMapping(VenueMappings vm);
        IEnumerable<VenueMappings> GetVenueMappings();
        IEnumerable<VenueMappings> GetVenueMappingsByExamVenueId(int examVenueId);
        IEnumerable<VenueMappings> GetVenueMappings(int formId, int courseId, int programId);
        void SaveVenueMapping(VenueMappings vn);
    

        IEnumerable<ExamVenue> GetExamVenues();
        IEnumerable<ExamVenue> GetExamVenues(int venueId);
        void SaveExamVenue(ExamVenue examVenue);
        ExamVenue GetExamVenue(int examVenueId);
    }
}
