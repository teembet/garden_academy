using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;

namespace EduApply.Logic.Repository
{
    public class SearchRepository : SqlRepository, ISearchRepository
    {
        public SearchRepository(IDbContext context)
            : base(context)
        {

        }

        public IEnumerable<SearchResult> GetSearchResult(SearchResultQuery query)
        {
            var application = this.GetAll<Application>().ToList();
            var personalInformation = this.GetAll<PersonalInformation>().ToList();
            var eventLog = new List<EventLog>();



            if (query.SessionId != null)
            {
                application = application.Where(x => x.SessionId == query.SessionId).ToList();
            }
            if (query.FormId != null)
            {
                application = application.Where(x => x.AppFormId == query.FormId).ToList();
            }
            if (query.FacultyId != null)
            {
                application = application.Where(x => x.FacultyId == query.FacultyId).ToList();
            }
            if (query.DepartmentId != null)
            {
                application = application.Where(x => x.DepartmentId == query.DepartmentId).ToList();
            }
            if (query.ProgramId != null)
            {
                application = application.Where(x => x.ProgramId == query.ProgramId).ToList();
            }
            if (query.CourseOfStudyId != null)
            {
                application = application.Where(x => x.CourseOfStudyId == query.CourseOfStudyId).ToList();
            }
            if (query.VenueId != null)
            {
                //application = application.Where(x => x.VenueId == query.VenueId).ToList();
                application = application.Where(x => x.ExamVenueId > 0 && this.GetAll<ExamVenue>().FirstOrDefault(e => e.Id == x.ExamVenueId).VenueId == query.VenueId).ToList();
            }
            if (query.IsAdmitted != null)
            {
                application = application.Where(x => x.IsAdmitted == query.IsAdmitted).ToList();
            }
            if (query.IsSubmitted != null)
            {
                application = application.Where(x => x.IsSubmitted == query.IsSubmitted).ToList();
            }
            if (query.IsPaid != null)
            {
                application = application.Where(x => x.IsPaid == query.IsPaid).ToList();
            }
            if (!string.IsNullOrEmpty(query.RegNo))
            {
                application = application.Where(x => !string.IsNullOrEmpty(x.RegNum) && x.RegNum.ToLower().Equals(query.RegNo.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(query.AppNo))
            {
                application = application.Where(x => x.AppNum == query.AppNo).ToList();
            }

            switch (query.DateType)
            {
                case "All":
                    if (query.StartDate != null && query.EndDate != null)
                    {
                        application = application.Where(x => x.ApplicationDate >= query.StartDate && x.ApplicationDate <= query.EndDate).ToList();
                    }

                    break;
                case "Application Entry Date":
                    if (query.StartDate != null && query.EndDate != null)
                    {
                        application = application.Where(x => x.ApplicationDate >= query.StartDate && x.ApplicationDate <= query.EndDate).ToList();
                    }
                    break;
                case "Submission Date":
                    if (query.StartDate != null && query.EndDate != null)
                    {
                        application = application.Where(x => x.SubmissionDate >= query.StartDate && x.SubmissionDate <= query.EndDate).ToList();
                    }
                    break;
                case "Payment Date":
                    if (query.StartDate != null && query.EndDate != null)
                    {
                        application = application.Where(x => x.PaymentDate >= query.StartDate && x.PaymentDate <= query.EndDate).ToList();
                    }

                    break;
                case "Exam Date":
                    if (query.StartDate != null && query.EndDate != null)
                    {
                        application = application.Where(x => x.ExamVenueId > 0 &&  this.GetAll<ExamVenue>().FirstOrDefault(p => p.Id == x.ExamVenueId).ExamDate >= query.StartDate && this.GetAll<ExamVenue>().FirstOrDefault(p => p.Id == x.ExamVenueId).ExamDate <= query.EndDate).ToList();
                    }

                    break;
                case "Form Template Completion Date":
                    if (query.StartDate != null && query.EndDate != null && query.FormTemplateId != null)
                    {
                        eventLog = this.GetAll<EventLog>().Where(x => x.FormTemplateId == query.FormTemplateId && x.Timestamp >= query.StartDate && x.Timestamp <= query.EndDate).ToList();
                        if (eventLog.Any())
                        {
                            var appNums = eventLog.Select(x => x.AppNum).ToList();
                            application = application.Where(x => appNums.Contains(x.AppNum)).ToList();
                        }
                        else
                        {
                            application.Clear();
                        }

                    }

                    break;
            }

            if (query.Name != null)
            {
                var name = query.Name.ToLower();
                personalInformation = personalInformation.Where(x => name.Contains(x.LastName.ToLower()) || x.LastName.ToLower().Contains(name) || name.Contains(x.FirstName.ToLower()) || x.FirstName.ToLower().Contains(name) || (x.MiddleName != null) && (name.Contains(x.MiddleName.ToLower()) || x.MiddleName.ToLower().Contains(name))).ToList();
            }

            var result = (from app in application
                          join information in personalInformation on app.UserName equals information.Email
                          select new
                          {
                              ApplicationId = app.Id,
                              RegNum = app.RegNum,
                              AppNum = app.AppNum,
                              Lastname = information.LastName,
                              Firsname = information.FirstName,
                              Middlename = information.MiddleName,
                              programId = app.ProgramId,
                              courseId = app.CourseOfStudyId,
                              ApplicationDate = app.ApplicationDate,
                              SubmissionDate = app.SubmissionDate,
                              PaymentDate = app.PaymentDate,
                              AdmittedDate = app.AdmittedDate
                          }).ToList();






            //Add them all to a List

            var searchResult = new List<SearchResult>();
            if (result.Any())
            {
                foreach (var item in result)
                {
                    var sResult = new SearchResult()
                    {
                        ApplicationId = item.ApplicationId,
                        RegNum = item.RegNum,
                        AppNum = item.AppNum,
                        Lastname = item.Lastname,
                        Firsname = item.Firsname,
                        Middlename = item.Middlename,
                        Program = item.programId > 0 ? (this.GetAll<Program>().FirstOrDefault(x => x.Id == item.programId)).Code : "",
                        Course = item.courseId > 0 ? (this.GetAll<Course>().FirstOrDefault(x => x.Id == item.courseId)).Name : "",
                        ApplicationDate = item.ApplicationDate,
                        SubmissionDate = item.SubmissionDate,
                        PaymentDate = item.PaymentDate,
                        AdmittedDate = item.AdmittedDate

                    };
                    searchResult.Add(sResult);
                }
            }
            return searchResult;

        }
    }
}
