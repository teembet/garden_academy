using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Repository;

namespace EduApply.Logic.Service
{
    public class VenueAssignmentService : SqlRepository, IVenueAssignmentService
    {
        public VenueAssignmentService(IDbContext context)
            : base(context)
        {

        }


        public IEnumerable<ExamVenue> GetExamVenues()
        {
            var examVenues = this.GetAll<ExamVenue>();
            return examVenues.ToList();
        }

        public void SaveExamVenue(ExamVenue examVenue)
        {
            if (examVenue.Id <= 0)
            {
                this.Insert<ExamVenue>(examVenue);
            }
            this.SaveChanges();
        }

        public ExamVenue GetExamVenue(int examVenueId)
        {
            var examVenue = this.GetAll<ExamVenue>().FirstOrDefault(x => x.Id == examVenueId);
            return examVenue;
        }

        public IEnumerable<ExamVenue> GetExamVenues(int venueId)
        {
            var examVenues = this.GetAll<ExamVenue>().Where(x => x.VenueId == venueId);
            return examVenues.ToList();
        }

        public IEnumerable<Venues> GetVenues()
        {
            var venues = this.GetAll<Venues>().OrderBy(x => x.Name);
            return venues.ToList();
        }

        public Venues GetVenue(int id)
        {
            var venue = this.Find<Venues>(id);
            return venue;
        }

        public IEnumerable<Venues> GetVenue(string name)
        {
            var venue = this.GetAll<Venues>().Where(x => x.Name == name);
            return venue;
        }

        public void SaveVenue(Venues venue)
        {
            if (venue.Id <= 0)
            {
                this.Insert<Venues>(venue);
            }
            this.SaveChanges();
        }

        public void UpdateVenue(Venues venue)
        {
            this.Update<Venues>(venue);
            this.SaveChanges();
        }
        public void UpdateExamVenue(ExamVenue examVenue)
        {
            this.Update<ExamVenue>(examVenue);
            this.SaveChanges();
        }



        public void SaveVenueMapping(VenueMappings vn)
        {
            if (vn.Id <= 0)
            {
                this.Insert<VenueMappings>(vn);
            }
            this.SaveChanges();
        }

        public IEnumerable<VenueMappings> GetVenueMappings()
        {
            var venueMappings = this.GetAll<VenueMappings>();
            return venueMappings;
        }

        public IEnumerable<VenueMappings> GetVenueMappings(int formId, int courseId, int programId)
        {
            var venueMappings =
                this.GetAll<VenueMappings>()
                    .Where(x => x.FormId == formId && x.CourseOfStudyId == courseId && x.ProgramId == programId);
            return venueMappings;
        }


        public VenueMappings GetVenueMappings(int formId, int courseId, int programId, int examVenueId)
        {
            var venueMapping = this.GetAll<VenueMappings>().FirstOrDefault(x => x.FormId == formId && x.CourseOfStudyId == courseId && x.ProgramId == programId && x.ExamVenueId == examVenueId);
            return venueMapping;
        }


        public void DeleteVenueMapping(VenueMappings vm)
        {
            this.Delete<VenueMappings>(vm);
            this.SaveChanges();
        }


        public IEnumerable<VenueMappings> GetVenueMappingsByExamVenueId(int examVenueId)
        {
            var venueMappings = this.GetAll<VenueMappings>().Where(x => x.ExamVenueId == examVenueId);
            return venueMappings;
        }


    }
}
