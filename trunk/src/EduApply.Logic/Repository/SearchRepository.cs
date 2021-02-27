using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Service;
using EduApply.Logic.Utility;

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
            //    var tenancy = EngineContext.Resolve<Tenancy>();
            //    var connectionString = tenancy.ConnectionString;
            //    var srList = new List<SearchResult>();

            //    using (var conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        var cmd = new SqlCommand("SearchApplication", conn);
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        using (SqlDataReader rdr = cmd.ExecuteReader())
            //        {

            //            srList = rdr.Cast<IDataRecord>().Select(r => new SearchResult
            //            {
            //                ApplicationId = r["ApplicationId"] is DBNull ? 0 : Convert.ToInt64(r["ApplicationId"]),
            //                RegNum = r["RegNum"] is DBNull ? null : r["RegNum"].ToString(),
            //                AppNum = r["AppNum"] is DBNull ? null : r["AppNum"].ToString(),
            //                Lastname = r["LastName"] is DBNull ? null : r["LastName"].ToString(),
            //                Firsname = r["FirstName"] is DBNull ? null : r["FirstName"].ToString(),
            //                Middlename = r["MiddleName"] is DBNull ? null : r["MiddleName"].ToString(),
            //                ProgramCourse = "",
            //                ApplicationDate = r["AppDate"] is DBNull ? new DateTime() : Convert.ToDateTime(r["AppDate"]),
            //                SubmissionDate = r["SubDate"] is DBNull ? new DateTime() : Convert.ToDateTime(r["SubDate"]),
            //                PaymentDate = r["PayDate"] is DBNull ? new DateTime() : Convert.ToDateTime(r["PayDate"]),
            //                AdmittedDate = r["AdmDate"] is DBNull ? new DateTime() : Convert.ToDateTime(r["AdmDate"]),
            //            }).ToList();
            //        }
            //    }



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
                var appPC = this.GetAll<ApplicantsProgramCourse>().ToList();
                var app = appPC.Where(x => x.ProgramId == query.ProgramId).Select(x => x.ApplicationId).Distinct();
                application = application.Where(x => app.Contains(x.Id)).ToList();
            }
            if (query.CourseOfStudyId != null)
            {
                var appPC = this.GetAll<ApplicantsProgramCourse>();
                var app = appPC.Where(x => x.CourseId == query.CourseOfStudyId).Select(x => x.ApplicationId).Distinct();
                application = application.Where(x => app.Contains(x.Id)).ToList();
            }
            if (query.VenueId != null)
            {
                //application = application.Where(x => x.VenueId == query.VenueId).ToList();
                application = application.Where(x => x.ExamVenueId > 0 &&
                    this.GetAll<ExamVenue>().FirstOrDefault(e => e.Id == x.ExamVenueId).VenueId == query.VenueId).ToList();
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
                        application = application.Where(x => x.ExamVenueId > 0 && this.GetAll<ExamVenue>().FirstOrDefault(p => p.Id == x.ExamVenueId).ExamDate >= query.StartDate && this.GetAll<ExamVenue>().FirstOrDefault(p => p.Id == x.ExamVenueId).ExamDate <= query.EndDate).ToList();
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
                personalInformation = personalInformation.Where(x => !string.IsNullOrEmpty(x.LastName) && name.Contains(x.LastName.ToLower()) || !string.IsNullOrEmpty(x.LastName) && x.LastName.ToLower().Contains(name) || !string.IsNullOrEmpty(x.FirstName) && name.Contains(x.FirstName.ToLower()) || !string.IsNullOrEmpty(x.FirstName) && x.FirstName.ToLower().Contains(name) || (x.MiddleName != null) && (name.Contains(x.MiddleName.ToLower()) || x.MiddleName.ToLower().Contains(name))).ToList();
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
                              //programId = app.ProgramId,
                              //courseId = app.CourseOfStudyId,
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
                    string progCourse = "";
                    var selectedItem = item;
                    var applicantsProgramCourse = this.GetAll<ApplicantsProgramCourse>().Where(x => x.ApplicationId == selectedItem.ApplicationId);
                    if (applicantsProgramCourse != null)
                    {
                        foreach (var appPc in applicantsProgramCourse)
                        {
                            var programCode = this.GetAll<Program>().FirstOrDefault(x => x.Id == appPc.ProgramId).Code;
                            var courseName = appPc.CourseId > 0 ? this.GetAll<Course>().FirstOrDefault(x => x.Id == appPc.CourseId).Name : "";
                            progCourse += "(" + programCode + ") " + courseName + ",";
                        }

                        if (!string.IsNullOrEmpty(progCourse))
                        {
                            int lasTIndexOfComma = progCourse.LastIndexOf(',');
                            progCourse = progCourse.Remove(lasTIndexOfComma);
                        }

                    }
                    var sResult = new SearchResult()
                    {
                        ApplicationId = selectedItem.ApplicationId,
                        RegNum = selectedItem.RegNum,
                        AppNum = selectedItem.AppNum,
                        Lastname = selectedItem.Lastname,
                        Firsname = selectedItem.Firsname,
                        Middlename = selectedItem.Middlename,
                        ProgramCourse = progCourse,
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


        public IEnumerable<SearchResult> GetSearchResult(string regNum, string appNum)
        {
            var applications = new List<Application>();
            var personalInformations = new List<PersonalInformation>();
            var searchResult = new List<SearchResult>();
            if (!string.IsNullOrEmpty(regNum) && !string.IsNullOrEmpty(appNum))
            {
                applications =
                    this.GetAll<Application>().Where(x => x.RegNum.Equals(regNum) && x.AppNum.Equals(appNum)).ToList();
            }
            else if (!string.IsNullOrEmpty(regNum))
            {
                applications =
                    this.GetAll<Application>().Where(x => x.RegNum.Equals(regNum)).ToList();
            }
            else if (!string.IsNullOrEmpty(appNum))
            {
                applications =
                    this.GetAll<Application>().Where(x => x.AppNum.Equals(appNum)).ToList();
            }
            var emails = applications.Select(x => x.UserName).ToList();
            personalInformations = this.GetAll<PersonalInformation>().Where(x => emails.Contains(x.Email)).ToList();

            var merger = (from app in applications
                          join per in personalInformations on app.UserName equals per.Email
                          select new
                          {
                              applicationId = app.Id,
                              regNum = app.RegNum,
                              appNum = app.AppNum,
                              lastName = per.LastName,
                              firstName = per.FirstName,
                              middleName = per.MiddleName,
                              phone = per.PhoneNumber,
                              email = app.UserName,
                              applicationDate = app.ApplicationDate,
                              submissionDate = app.SubmissionDate,
                              paymentDate = app.PaymentDate
                          }).ToList();


            if (merger.Any())
            {
                foreach (var item in merger)
                {
                    string progCourse = "";
                    var selectedItem = item;
                    var applicantsProgramCourse =
                        this.GetAll<ApplicantsProgramCourse>().Where(x => x.ApplicationId == selectedItem.applicationId);
                    if (applicantsProgramCourse.Any())
                    {
                        foreach (var appPc in applicantsProgramCourse)
                        {
                            var programCode = this.GetAll<Program>().FirstOrDefault(x => x.Id == appPc.ProgramId).Code;
                            var courseName = this.GetAll<Course>().FirstOrDefault(x => x.Id == appPc.CourseId).Name;
                            progCourse += "(" + programCode + ") " + courseName + ",";
                        }

                        if (!string.IsNullOrEmpty(progCourse))
                        {
                            int lasTIndexOfComma = progCourse.LastIndexOf(',');
                            progCourse = progCourse.Remove(lasTIndexOfComma);
                        }

                    }
                    var sResult = new SearchResult()
                    {
                        ApplicationId = selectedItem.applicationId,
                        RegNum = selectedItem.regNum,
                        AppNum = selectedItem.appNum,
                        Lastname = selectedItem.lastName,
                        Firsname = selectedItem.firstName,
                        Middlename = selectedItem.middleName,
                        ProgramCourse = progCourse,
                        Email = selectedItem.email,
                        PhoneNumber = selectedItem.phone,
                        ApplicationDate = selectedItem.applicationDate,
                        SubmissionDate = selectedItem.submissionDate,
                        PaymentDate = selectedItem.paymentDate

                    };
                    searchResult.Add(sResult);
                }
            }
            return searchResult;

        }
    }
}
