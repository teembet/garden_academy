using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Repository;

namespace EduApply.Logic.Service
{
    public class PrintService : SqlRepository, IPrintService
    {
        public PrintService(IDbContext context)
            : base(context)
        {

        }

        public string GetPersonalInformation(Data.Entities.PersonalInformation info)
        {
            var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/ApplicantBiodataPreview.html");
            var htmlText = System.IO.File.ReadAllText(htmlTemplate);


            htmlText = htmlText.Replace("#LastName#", info.LastName);
            htmlText = htmlText.Replace("#FirstName#", info.FirstName);
            htmlText = htmlText.Replace("#MiddleName#", info.MiddleName);
            htmlText = htmlText.Replace("#DateOfBirth#", Convert.ToDateTime(info.DateOfBirth).ToString("dd-MMM-yyyy"));
            htmlText = htmlText.Replace("#Gender#", info.Gender);
            htmlText = htmlText.Replace("#MaritalStatus#", info.MaritalStatus);
            htmlText = htmlText.Replace("#HomeAddress#", info.HomeAddress);
            htmlText = htmlText.Replace("#StateOfResidence#", info.StateOfResidence);
            htmlText = htmlText.Replace("#PostalAddress#", info.PostalAddress);
            htmlText = htmlText.Replace("#Email#", info.Email);
            htmlText = htmlText.Replace("#Phone#", info.PhoneNumber);
            htmlText = htmlText.Replace("#Religion#", info.Religion);
            htmlText = htmlText.Replace("#NextOfKin#", info.NameOfNextOfkin);
            htmlText = htmlText.Replace("#Relationship#", info.NextOfKinRelationship);
            htmlText = htmlText.Replace("#nextOfKinPhone#", info.PhoneOfNextOfkin);
            htmlText = htmlText.Replace("#nextOfKinEmail#", info.EmailOfNextOfKin);
            var country = this.GetAll<Country>().FirstOrDefault(x => x.Id == info.Nationality);
            var statOfOrigin = this.GetAll<State>().FirstOrDefault(x => x.Id == info.StateOfOrigin);
            var lga = this.GetAll<LocalGovernmentArea>().FirstOrDefault(x => x.Id == info.LocalGovernment);
            htmlText = htmlText.Replace("#country#", country != null ? country.Name : "");
            htmlText = htmlText.Replace("#StateOfOrigin#", statOfOrigin != null ? statOfOrigin.Name : "");
            htmlText = htmlText.Replace("#LocalGovernment#", lga != null ? lga.Name : "");
            return htmlText;
        }


        public string GetJambBreakDown(string RegNum)
        {
            string htmlText = "";
            if (!string.IsNullOrEmpty(RegNum))
            {
                JambBreakDown applicantsJambBreakDown = this.GetAll<JambBreakDown>().FirstOrDefault(x => x.RegNum == RegNum);
                ManualJambBreakDown manualApplicantsJambBreakDown = this.GetAll<ManualJambBreakDown>().FirstOrDefault(x => x.RegNum == RegNum);
                var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/JambResultAppForm.html");
                htmlText = System.IO.File.ReadAllText(htmlTemplate);

                htmlText = htmlText.Replace("#RegNum#", applicantsJambBreakDown != null ? applicantsJambBreakDown.RegNum : manualApplicantsJambBreakDown.RegNum);
                htmlText = htmlText.Replace("#EngScore#", applicantsJambBreakDown != null ? applicantsJambBreakDown.EngScore.ToString() : manualApplicantsJambBreakDown.EngScore.ToString());
                htmlText = htmlText.Replace("#Subject2#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject2 : manualApplicantsJambBreakDown.Subject2);
                htmlText = htmlText.Replace("#Subject2Score#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject2Score.ToString() : manualApplicantsJambBreakDown.Subject2Score.ToString());
                htmlText = htmlText.Replace("#Subject3#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject3 : manualApplicantsJambBreakDown.Subject3);
                htmlText = htmlText.Replace("#Subject3Score#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject3Score.ToString() : manualApplicantsJambBreakDown.Subject3Score.ToString());
                htmlText = htmlText.Replace("#Subject4#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject4 : manualApplicantsJambBreakDown.Subject4);
                htmlText = htmlText.Replace("#Subject4Score#", applicantsJambBreakDown != null ? applicantsJambBreakDown.Subject4Score.ToString() : manualApplicantsJambBreakDown.Subject4Score.ToString());
                htmlText = htmlText.Replace("#TotalScore#", applicantsJambBreakDown != null ? applicantsJambBreakDown.TotalScore.ToString() : manualApplicantsJambBreakDown.TotalScore.ToString());
            }

            return htmlText;
        }


        public string GetApplicationResult(string appNum, string regNum)
        {
            var appliction = this.GetAll<Application>().FirstOrDefault(x => x.AppNum == appNum);
            var formId = appliction.AppFormId;
            var formResult = this.GetAll<FormResult>().FirstOrDefault(x => x.AppNum == appNum);
            if (formResult == null)
            {
                formResult = this.GetAll<FormResult>().FirstOrDefault(x => x.RegNum == regNum && x.ApplicationFormId == formId);
            }

            string htmlText = "";
            if (formResult != null)
            {
                var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/ApplicationResultPreview.html");
                htmlText = System.IO.File.ReadAllText(htmlTemplate);
                htmlText = htmlText.Replace("#ImageLogoUrl#", "");
                htmlText = htmlText.Replace("#RegNum#", formResult.RegNum);
                htmlText = htmlText.Replace("#AppNum#", formResult.AppNum);
                htmlText = htmlText.Replace("#EngScore#", formResult.EngScore.ToString());
                htmlText = htmlText.Replace("#Subject2#", formResult.Subject2);
                htmlText = htmlText.Replace("#Subject2Score#", formResult.Subject2Score.ToString());
                htmlText = htmlText.Replace("#Subject3#", formResult.Subject3);
                htmlText = htmlText.Replace("#Subject3Score#", formResult.Subject3Score.ToString());
                htmlText = htmlText.Replace("#Subject4#", formResult.Subject4);
                htmlText = htmlText.Replace("#Subject4Score#", formResult.Subject4Score.ToString());
                htmlText = htmlText.Replace("#TotalScore#", formResult.TotalScore.ToString());
                htmlText = htmlText.Replace("#Date#", "");
                htmlText = htmlText.Replace("#SchoolName#", "");
            }
            else
            {

            }

            return htmlText;
        }


        public string GetNonApplicationResult(string RegNum)
        {
            string htmlText = "";
            if (!string.IsNullOrEmpty(RegNum))
            {
                var sessionResult = this.GetAll<SessionResult>().FirstOrDefault(x => x.RegNum == RegNum);

                if (sessionResult != null)
                {
                    var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/NonApplicationResultPreview.html");
                    htmlText = System.IO.File.ReadAllText(htmlTemplate);
                    htmlText = htmlText.Replace("#ImageLogoUrl#", "");
                    htmlText = htmlText.Replace("#RegNum#", sessionResult.RegNum);
                    htmlText = htmlText.Replace("#EngScore#", sessionResult.EngScore.ToString());
                    htmlText = htmlText.Replace("#Subject2#", sessionResult.Subject2);
                    htmlText = htmlText.Replace("#Subject2Score#", sessionResult.Subject2Score.ToString());
                    htmlText = htmlText.Replace("#Subject3#", sessionResult.Subject3);
                    htmlText = htmlText.Replace("#Subject3Score#", sessionResult.Subject3Score.ToString());
                    htmlText = htmlText.Replace("#Subject4#", sessionResult.Subject4);
                    htmlText = htmlText.Replace("#Subject4Score#", sessionResult.Subject4Score.ToString());
                    htmlText = htmlText.Replace("#TotalScore#", sessionResult.TotalScore.ToString());
                    htmlText = htmlText.Replace("#Date#", "");
                    htmlText = htmlText.Replace("#SchoolName#", "");
                }
            }

            return htmlText;
        }


        public string GetApplicantsVenue(int appFormId, int programId, int courseId, int examVenueId, int seatNo)
        {
            string htmlText = "";
            var examVenue = this.GetAll<ExamVenue>().FirstOrDefault(x => x.Id == examVenueId);
            // var venueMapping = this.GetAll<VenueMappings>().FirstOrDefault(x => x.FormId == appFormId && x.ProgramId == programId && x.CourseOfStudyId == courseId && x.ExamVenueId == examVenueId);
            if (examVenue != null)
            {
                var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/ApplicantsVenuePreview.html");
                htmlText = System.IO.File.ReadAllText(htmlTemplate);
                htmlText = htmlText.Replace("#Venue#", examVenue.Venue.Name);
                htmlText = htmlText.Replace("#ExamDate#", examVenue.ExamDate.ToString("ddd dd-MMM-yy h:mm tt"));
                htmlText = htmlText.Replace("#SeatNo#", seatNo.ToString());
            }

            return htmlText;
        }


        public string GetOLevelResult(long applicationId)
        {
            string htmlText = "<h3>OLevel RESULT</h3><hr /><table cellspacing ='7'>";
            var oLevelDetails = this.GetAll<OLevelDetail>().Where(x => x.ApplicationId == applicationId).ToList();
            if (oLevelDetails.Any())
            {

                var grades = this.GetAll<OLevelGrade>().ToList();
                var subjects = this.GetAll<OLevelSubject>().ToList();
                foreach (var detail in oLevelDetails)
                {
                    var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/OLevelPreview.html");
                    htmlText += System.IO.File.ReadAllText(htmlTemplate);


                    htmlText = htmlText.Replace("#CandidateName#", detail.CandidateName);
                    htmlText = htmlText.Replace("#ExamNumber#", detail.ExamNumber);
                    htmlText = htmlText.Replace("#Year#", detail.Year.ToString());
                    htmlText = htmlText.Replace("#ExamType#", detail.ExamType);
                    var subjectResults = this.GetAll<OLevelResult>().Where(x => x.DetailId == detail.Id).ToList();
                    for (int i = 0; i < 9; i++)
                    {
                        if (subjectResults.Any())
                        {
                            if (i == 0)
                            {
                                var subject = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId);
                                var grade = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId);
                                if (subject != null && grade != null)
                                {
                                    htmlText = htmlText.Replace("#Subject1#", subject.Name);
                                    htmlText = htmlText.Replace("#grade1#", grade.Name);
                                }
                                else
                                {
                                    htmlText = htmlText.Replace("#Subject1#(#grade1#)", "");
                                }
                            }
                            if (i == 1)
                            {
                                if (subjectResults.Count > 1)
                                {
                                    var subject = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId);
                                    var grade = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId);

                                    htmlText = htmlText.Replace("#Subject2#", subject.Name);
                                    htmlText = htmlText.Replace("#grade2#", grade.Name);
                                }
                                else
                                {
                                    htmlText = htmlText.Replace("#Subject2#(#grade2#)", "");
                                }
                            }
                            if (i == 2)
                            {
                                if (subjectResults.Count > 2)
                                {
                                    var subject = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId);
                                    var grade = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId);

                                    htmlText = htmlText.Replace("#Subject3#", subject.Name);
                                    htmlText = htmlText.Replace("#grade3#", grade.Name);
                                }
                                else
                                {
                                    htmlText = htmlText.Replace("#Subject3#(#grade3#)", "");
                                }
                            }
                            if (i == 3)
                            {
                                if (subjectResults.Count > 3)
                                {
                                    var subject = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId);
                                    var grade = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId);

                                    htmlText = htmlText.Replace("#Subject4#", subject.Name);
                                    htmlText = htmlText.Replace("#grade4#", grade.Name);
                                }
                                else
                                {
                                    htmlText = htmlText.Replace("#Subject4#(#grade4#)", "");
                                }
                            }
                            if (i == 4)
                            {
                                if (subjectResults.Count > 4)
                                {
                                    var subject = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId);
                                    var grade = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId);

                                    htmlText = htmlText.Replace("#Subject5#", subject.Name);
                                    htmlText = htmlText.Replace("#grade5#", grade.Name);
                                }
                                else
                                {
                                    htmlText = htmlText.Replace("#Subject5#(#grade5#)", "");
                                }
                            }
                            if (i == 5)
                            {
                                if (subjectResults.Count > 5)
                                {
                                    var subject = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId);
                                    var grade = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId);

                                    htmlText = htmlText.Replace("#Subject6#", subject.Name);
                                    htmlText = htmlText.Replace("#grade6#", grade.Name);
                                }
                                else
                                {
                                    htmlText = htmlText.Replace("#Subject6#(#grade6#)", "");
                                }
                            }
                            if (i == 6)
                            {
                                if (subjectResults.Count > 6)
                                {
                                    var subject = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId);
                                    var grade = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId);

                                    htmlText = htmlText.Replace("#Subject7#", subject.Name);
                                    htmlText = htmlText.Replace("#grade7#", grade.Name);
                                }
                                else
                                {
                                    htmlText = htmlText.Replace("#Subject7#(#grade7#)", "");
                                }
                            }
                            if (i == 7)
                            {
                                if (subjectResults.Count > 7)
                                {
                                    var subject = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId);
                                    var grade = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId);

                                    htmlText = htmlText.Replace("#Subject8#", subject.Name);
                                    htmlText = htmlText.Replace("#grade8#", grade.Name);
                                }
                                else
                                {
                                    htmlText = htmlText.Replace("#Subject8#(#grade8#)", "");
                                }
                            }
                            if (i == 8)
                            {
                                if (subjectResults.Count > 8)
                                {
                                    var subject = subjects.FirstOrDefault(x => x.Id == subjectResults[i].SubjectId);
                                    var grade = grades.FirstOrDefault(x => x.Id == subjectResults[i].GradeId);

                                    htmlText = htmlText.Replace("#Subject9#", subject.Name);
                                    htmlText = htmlText.Replace("#grade9#", grade.Name);
                                }
                                else
                                {
                                    htmlText = htmlText.Replace("#Subject9#(#grade9#)", "");
                                }
                            }
                        }
                    }
                }
            }
            htmlText += "</table>";
            return htmlText;
        }


        public string GetEducationalDetails(long applicationId)
        {
            string htmlText = "<h3>Educational Details</h3><hr /><table cellspacing='8'>";
            var educationalDetails = this.GetAll<EducationalDetails>().Where(x => x.ApplicationId == applicationId);
            if (educationalDetails.Any())
            {
                foreach (var detail in educationalDetails)
                {
                    var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/EducationalDetailsPreview.html");
                    htmlText += System.IO.File.ReadAllText(htmlTemplate);

                    htmlText = htmlText.Replace("#SchoolName#", detail.SchoolName);
                    htmlText = htmlText.Replace("#Qualification#", detail.Qualification);
                    htmlText = htmlText.Replace("#ClassOfDegree#", detail.ClassOfDegree);
                    htmlText = htmlText.Replace("#EntryYear#", detail.EntryYear.ToString());
                    htmlText = htmlText.Replace("#GraduationYear#", detail.GraduationYear.ToString());
                }
            }
            htmlText += "</table>";
            return htmlText;
        }


        public string GetWorkExperience(long applicationId)
        {
            string htmlText = "<h3>Work Experience</h3><hr /><table cellspacing='5'>";
            var workExperience = this.GetAll<WorkExperience>().Where(x => x.ApplicationId == applicationId);
            if (workExperience.Any())
            {
                foreach (var exp in workExperience)
                {
                    var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/WorkExperiencePreview.html");
                    htmlText += System.IO.File.ReadAllText(htmlTemplate);

                    htmlText = htmlText.Replace("#Organization#", exp.Organization);
                    htmlText = htmlText.Replace("#FromDate#", exp.FromDate.ToString("dd-MMM-yyyy"));
                    htmlText = htmlText.Replace("#ToDate#", exp.ToDate != null ? Convert.ToDateTime(exp.ToDate).ToString("dd-MMM-yyyy") : "");
                    htmlText = htmlText.Replace("#Position#", exp.Position);
                    htmlText = htmlText.Replace("#JobDescription#", exp.JobDescription);
                }
            }
            htmlText += "</table>";
            return htmlText;
        }


        public string GetReferees(long applicationId)
        {
            string htmlText = "<h3>Referees</h3><hr/><table cellspacing='5'>";
            var Referees = this.GetAll<Reference>().Where(x => x.ApplicationId == applicationId);
            if (Referees.Any())
            {
                foreach (var referee in Referees)
                {
                    var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/RefereePreview.html");
                    htmlText += System.IO.File.ReadAllText(htmlTemplate);

                    htmlText = htmlText.Replace("#Name#", referee.Name);
                    htmlText = htmlText.Replace("#Occupation#", referee.Occupation);
                    htmlText = htmlText.Replace("#Address#", referee.Address);
                    htmlText = htmlText.Replace("#PhoneNumber#", referee.PhoneNumber);
                    htmlText = htmlText.Replace("#Email#", referee.Email);
                }
            }
            htmlText += "</table>";
            return htmlText;
        }


        public string GetCertificate(long applicationId)
        {
            string htmlText = "<h3>Certificates</h3><hr /><table cellspacing='5'><tr><th style='width: 50%'>Name of Certificate</th><th style='width: 40%'>Type of Certificate:</th></tr>";
            var certificates = this.GetAll<Certificate>().Where(x => x.ApplicationId == applicationId);
            if (certificates.Any())
            {
                foreach (var cert in certificates)
                {
                    var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/CertificatePreview.html");
                    htmlText += System.IO.File.ReadAllText(htmlTemplate);

                    htmlText = htmlText.Replace("#Name#", cert.CertificateName);
                    htmlText = htmlText.Replace("#Type#", cert.CertificateType);
                }
            }
            htmlText += "</table>";
            return htmlText;
        }


        public string GetProgramCourse(int programId, int courseId)
        {
            string htmlText = "";
            var program = this.GetAll<Program>().FirstOrDefault(x => x.Id == programId);
            var course = this.GetAll<Course>().FirstOrDefault(x => x.Id == courseId);
            if (program != null && course != null)
            {
                var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/ProgramCoursePreview.html");
                htmlText = System.IO.File.ReadAllText(htmlTemplate);

                htmlText = htmlText.Replace("#Program#", program.Code);
                htmlText = htmlText.Replace("#Course#", course.Name);
            }

            return htmlText;
        }


        public string GetHeaderAndPassport(long applicationId)
        {
            string htmlText = "";
            var studentPicture = this.GetAll<Picture>().FirstOrDefault(x => x.ApplicationId == applicationId);
            string passportPath = "";
            if (studentPicture != null)
            {
                passportPath = HttpContext.Current.Server.MapPath("~/images/StudentPassport/" + studentPicture.Name);
            }
            string imgPath = HttpContext.Current.Server.MapPath("~/images/SchoolLogo.jpg");

            var htmlTemplate = HttpContext.Current.Server.MapPath("~/PrintTemplates/HeaderAndPassport.html");
            htmlText = System.IO.File.ReadAllText(htmlTemplate);

            htmlText = htmlText.Replace("#ImageLogoUrl#", imgPath);
            htmlText = htmlText.Replace("#ImageStudentUrl#", passportPath);


            return htmlText;
        }
    }
}
