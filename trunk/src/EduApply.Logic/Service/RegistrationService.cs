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
    public class RegistrationService : SqlRepository, IRegistrationService
    {
        public RegistrationService(IDbContext context)
            : base(context)
        {

        }

        public void SavePersonalInformation(PersonalInformation info)
        {
            this.Insert<PersonalInformation>(info);
            this.SaveChanges();
        }

        public void UpdatePersonalInformation(PersonalInformation info)
        {
            this.Update<PersonalInformation>(info);
            this.SaveChanges();
        }

        public void SaveAdminBiodata(AdminBiodata biodata)
        {
            this.Insert<AdminBiodata>(biodata);
            this.SaveChanges();
        }


        public IEnumerable<OLevelGrade> GetOLevelGrades()
        {
            var grades = this.GetAll<OLevelGrade>();
            return grades.ToList();
        }


        public IEnumerable<OLevelSubject> GetOLevelSubjects()
        {
            var subjects = this.GetAll<OLevelSubject>().OrderBy(x => x.Name);
            return subjects.ToList();
        }

        public IEnumerable<Country> GetCountries()
        {
            var countries = this.GetAll<Country>();
            return countries.ToList();
        }

        public IEnumerable<State> GetStates()
        {
            var states = this.GetAll<State>();
            return states.ToList();
        }

        public IEnumerable<LocalGovernmentArea> GetLocalGovernmentAreas()
        {
            var lgas = this.GetAll<LocalGovernmentArea>();
            return lgas.ToList();
        }


        public IEnumerable<State> GetStates(int countryId)
        {
            var states = this.GetAll<State>().Where(x => x.CountryId == countryId);
            return states.ToList();
        }

        public IEnumerable<LocalGovernmentArea> GetLocalGovernmentAreas(long stateId)
        {
            var lgas = this.GetAll<LocalGovernmentArea>().Where(x => x.StateId == stateId);
            return lgas.ToList();
        }


        public IEnumerable<Faculty> GetFaculties()
        {
            var faculties = this.GetAll<Faculty>();
            return faculties.ToList();
        }

        public IEnumerable<Department> GetDepartments()
        {
            var departments = this.GetAll<Department>();
            return departments.ToList();
        }

        public IEnumerable<Department> GetDepartments(int facultyId)
        {
            var departments = this.GetAll<Department>().Where(x => x.FacultyId == facultyId);
            return departments.ToList();
        }

        public IEnumerable<Course> GetCourses()
        {
            var courses = this.GetAll<Course>();
            return courses.ToList();
        }

        //public IEnumerable<Course> GetCourses(int departmentId)
        //{
        //    var courses = this.GetAll<Course>().Where(x => x.DepartmentId == departmentId);
        //    return courses.ToList();
        //}


        public IEnumerable<Application> GetApplicationDetails(string userName)
        {
            var application = this.GetAll<Application>().Where(x => x.UserName == userName);
            return application.ToList();
        }


        public void SaveApplication(Application application)
        {
            if (application.Id <= 0)
            {
                this.Insert<Application>(application);
            }
            this.SaveChanges();
        }


        public Application GetApplicationDetails(long id)
        {
            var application = this.GetAll<Application>().FirstOrDefault(x => x.Id == id);
            return application;
        }


        public PersonalInformation GetPersonalInformation(string id)
        {
            var personalInformation = this.GetAll<PersonalInformation>().SingleOrDefault(x => x.Id == id);
            return personalInformation;
        }

        public void UpdateDb()
        {
            this.SaveChanges();
        }

        public void SavePicture(Picture picture)
        {
            if (picture.Id <= 0)
            {
                this.Insert<Picture>(picture);
            }

            this.SaveChanges();
        }


        public void SaveReference(Reference referee)
        {
            if (referee.Id <= 0)
            {
                this.Insert<Reference>(referee);
            }
            this.SaveChanges();
        }

        public void SaveWorkExperience(WorkExperience workExp)
        {
            if (workExp.Id <= 0)
            {
                this.Insert<WorkExperience>(workExp);
            }
            this.SaveChanges();
        }


        public void SaveEducationalDetails(EducationalDetails edu)
        {
            if (edu.Id <= 0)
            {
                this.Insert<EducationalDetails>(edu);
            }
            this.SaveChanges();
        }


        public void SaveCertificates(Certificate cert)
        {
            if (cert.Id <= 0)
            {
                this.Insert<Certificate>(cert);
                this.SaveChanges();
            }
        }


        public void SaveOlevelDetail(OLevelDetail detail)
        {
            if (detail.Id <= 0)
            {
                this.Insert<OLevelDetail>(detail);
            }

            this.SaveChanges();
        }


        public void SaveOLevelResult(OLevelResult result)
        {
            this.Insert<OLevelResult>(result);
            this.SaveChanges();
        }


        public FormTemplateSettings GetFormTemplateSettings(int appFormId, int templateId)
        {
            var templateSettings =
                this.GetAll<FormTemplateSettings>()
                    .FirstOrDefault(x => x.ApplicationFormId == appFormId && x.FormTemplateId == templateId);
            return templateSettings;
        }


        public IEnumerable<OLevelDetail> GetOLevelDetails(long applicationId)
        {
            var applicationDetails = this.GetAll<OLevelDetail>().Where(x => x.ApplicationId == applicationId);
            return applicationDetails.ToList();
        }


        public IEnumerable<EducationalDetails> GetEducationalDetails(long applicationId)
        {
            var educationalDetails =
                this.GetAll<EducationalDetails>().Where(x => x.ApplicationId == applicationId);
            return educationalDetails.ToList();
        }





        public IEnumerable<WorkExperience> GetWorkExperience(long applicationId)
        {
            var workExperience = this.GetAll<WorkExperience>().Where(x => x.ApplicationId == applicationId);
            return workExperience.ToList();
        }


        public IEnumerable<Reference> GetReferences(long applicationId)
        {
            var reference = this.GetAll<Reference>().Where(x => x.ApplicationId == applicationId);
            return reference.ToList();
        }


        public IEnumerable<Certificate> GetCertificates(long applicationId)
        {
            var certificates =
             this.GetAll<Certificate>().Where(x => x.ApplicationId == applicationId);
            return certificates.ToList();
        }


        public IEnumerable<CertificateType> GetCertificateTypes()
        {
            var certificateTypes = this.GetAll<CertificateType>();
            return certificateTypes.ToList();
        }


        public Application GetLastApplication()
        {
            var application = this.GetAll<Application>().OrderByDescending(x => x.Id).FirstOrDefault();
            return application;
        }


        public OLevelSubject GetOLevelSubjectByCode(string code)
        {
            var subject = this.GetAll<OLevelSubject>().FirstOrDefault(x => x.Code == code);
            return subject;
        }


        public Picture GetPictureDetails(long applicationId)
        {
            var pictureDetails = this.GetAll<Picture>().FirstOrDefault(x => x.ApplicationId == applicationId);
            return pictureDetails;
        }


        public Reference GetReference(long id)
        {
            var reference = this.GetAll<Reference>().FirstOrDefault(x => x.Id == id);
            return reference;
        }

        public void DeleteReference(Reference reference)
        {
            this.Delete<Reference>(reference);
            this.SaveChanges();
        }


        public WorkExperience GetWorkExperienceById(long id)
        {
            var workExperience = this.GetAll<WorkExperience>().FirstOrDefault(x => x.Id == id);
            return workExperience;
        }

        public void DeleteWorkExperience(WorkExperience experience)
        {
            this.Delete<WorkExperience>(experience);
            this.SaveChanges();
        }


        public Certificate GetCertificate(long id)
        {
            var certificate = this.GetAll<Certificate>().FirstOrDefault(x => x.Id == id);
            return certificate;
        }

        public void DeleteCertificate(Certificate certificate)
        {
            this.Delete<Certificate>(certificate);
            this.SaveChanges();
        }


        public EducationalDetails GetEducationalDetail(long id)
        {
            var educationalDetail = this.GetAll<EducationalDetails>().FirstOrDefault(x => x.Id == id);
            return educationalDetail;
        }

        public void DeleteEducationalDetail(EducationalDetails eduDetail)
        {
            this.Delete<EducationalDetails>(eduDetail);
            this.SaveChanges();
        }


        public IEnumerable<Application> GetOpenApplication(int appFormId, string username)
        {
            var application =
                this.GetAll<Application>().Where(x => x.AppFormId == appFormId && x.UserName == username);
            return application;
        }


        public IEnumerable<OLevelResult> GetOLevelResults(long detailId)
        {
            var result = this.GetAll<OLevelResult>().Where(x => x.DetailId == detailId);
            return result;
        }


        public OLevelDetail GetOLevelDetail(long detailId)
        {
            var oLevelDetail = this.GetAll<OLevelDetail>().FirstOrDefault(x => x.Id == detailId);
            return oLevelDetail;
        }


        public void DeleteOLevelDetai(OLevelDetail detail)
        {
            this.Delete<OLevelDetail>(detail);
            this.SaveChanges();
        }


        public IEnumerable<ExamType> GeExamTypes()
        {
            var examTypes = this.GetAll<ExamType>();
            return examTypes.ToList();
        }


        public ManualJambBreakDown GetManualJambBreakDown(string regNum)
        {
            var jambBreakDown = this.GetAll<ManualJambBreakDown>().FirstOrDefault(x => x.RegNum == regNum);
            return jambBreakDown;
        }


        public void UpdateManualJambBreakDown(ManualJambBreakDown breakDown)
        {
            this.Update<ManualJambBreakDown>(breakDown);
            this.SaveChanges();
        }


        public void SaveManualJambBreakDown(ManualJambBreakDown breakDown)
        {
            this.Insert<ManualJambBreakDown>(breakDown);
            this.SaveChanges();
        }


        public IEnumerable<Application> GetApplications()
        {
            var applications = this.GetAll<Application>().ToList();
            return applications;
        }


        public Application GetApplicationDetailsByAppNum(string appNum)
        {
            var applicationDetails = this.GetAll<Application>().FirstOrDefault(x => x.AppNum == appNum);
            return applicationDetails;
        }


        public void SaveFeeRequest(FeeRequestPayment feeRequest)
        {
            this.Insert<FeeRequestPayment>(feeRequest);
            this.SaveChanges();
        }


        public FeeRequestPayment GetFeeRequest(string appNum)
        {
            var feeRequest = this.GetAll<FeeRequestPayment>().FirstOrDefault(x => x.ApplicationNumber == appNum);
            return feeRequest;
        }
        public FeeRequestPayment GetFeeRequest(long payeeId, string paymentType)
        {
            var feeRequest = this.GetAll<FeeRequestPayment>().FirstOrDefault(x => x.PayeeID == payeeId && x.PaymentType.ToLower().Equals(paymentType.ToLower()));
            return feeRequest;
        }
        public FeeRequestPayment GetFeeRequest(long payeeId)
        {
            var feeRequest = this.GetAll<FeeRequestPayment>().FirstOrDefault(x => x.PayeeID == payeeId);
            return feeRequest;
        }

        public IEnumerable<FeeRequestPayment> GetFeeRequests()
        {
            var feeRequest = this.GetAll<FeeRequestPayment>();
            return feeRequest;
        }


        public PersonalInformation GetPersonalInformationByEmail(string email)
        {
            var pinfo = this.GetAll<PersonalInformation>().FirstOrDefault(x => x.Email == email);
            return pinfo;
        }


        public AdmissionList GetAdmissionList(string regNum, int appFormId)
        {
            var admissionList =
                this.GetAll<AdmissionList>().FirstOrDefault(x => x.RegNum == regNum && x.FormId == appFormId);
            return admissionList;
        }


        public void SaveAdmissionListItem(AdmissionList admissionListItem)
        {
            if (admissionListItem.Id <= 0)
            {
                this.Insert<AdmissionList>(admissionListItem);
            }
            this.SaveChanges();
        }


        public AdmissionList GetAdmissionList(string appNum)
        {
            var admissionEntry = this.GetAll<AdmissionList>().FirstOrDefault(x => x.AppNum == appNum);
            return admissionEntry;
        }


        public Application GetApplicationByRegNumAndFormId(string regNum, int appFormId)
        {
            var application =
                this.GetAll<Application>().FirstOrDefault(x => x.RegNum == regNum && x.AppFormId == appFormId);
            return application;
        }


        public Application GetAdmittedApplicationByRegNumAndFormId(string regNum, int appFormId)
        {
            var application =
               this.GetAll<Application>().FirstOrDefault(x => x.RegNum == regNum && x.AppFormId == appFormId && x.IsAdmitted);
            return application;
        }


        public IEnumerable<NonApplicantAdmittedList> GetNonApplicantAdmittedLists()
        {
            var nonApplicantAdmittedList = this.GetAll<NonApplicantAdmittedList>();
            return nonApplicantAdmittedList.ToList();
        }

        public IEnumerable<NonApplicantAdmittedList> GetNonApplicantAdmittedLists(int? sessionId, int? programId, int? courseId, int? applicationFormId)
        {
            bool isAllParametersNull = true;
            var nonApplicantAdmittedList = this.GetAll<NonApplicantAdmittedList>();
            if (sessionId != null)
            {
                isAllParametersNull = false;
                nonApplicantAdmittedList = nonApplicantAdmittedList.Where(x => x.SessionId == sessionId);
            }
            if (programId != null)
            {
                isAllParametersNull = false;
                nonApplicantAdmittedList = nonApplicantAdmittedList.Where(x => x.ProgramId == programId);
            }
            if (courseId != null)
            {
                isAllParametersNull = false;
                nonApplicantAdmittedList = nonApplicantAdmittedList.Where(x => x.CourseId == courseId);
            }
            if (applicationFormId != null)
            {
                isAllParametersNull = false;
                nonApplicantAdmittedList = nonApplicantAdmittedList.Where(x => x.FormId == applicationFormId);
            }
            if (isAllParametersNull)
            {
                nonApplicantAdmittedList = nonApplicantAdmittedList.Where(x => x.Id == -1);
            }
            return nonApplicantAdmittedList.ToList();
        }

        public NonApplicantAdmittedList GetNonApplicantAdmittedList(string regNum)
        {
            var nonApplicantAdmittedList =
                this.GetAll<NonApplicantAdmittedList>().FirstOrDefault(x => x.RegNum == regNum);
            return nonApplicantAdmittedList;
        }

        public void SaveNonApplicantAdmittedList(NonApplicantAdmittedList item)
        {
            if (item.Id <= 0)
            {
                this.Insert<NonApplicantAdmittedList>(item);
            }
            this.SaveChanges();
        }


        public NonApplicantAdmittedList GetNonApplicantAdmittedList(string regNum, int formId)
        {
            var nonApplicantAdmittedList =
                this.GetAll<NonApplicantAdmittedList>().FirstOrDefault(x => x.RegNum == regNum && x.FormId == formId);
            return nonApplicantAdmittedList;
        }


        public IEnumerable<OLevelSubject> GetOLevelSubjectsForJamb()
        {
            var subjects = this.GetAll<OLevelSubject>().Where(x => x.Code != "ENG");
            return subjects.ToList();
        }


        public IEnumerable<ApplicantsProgramCourse> GetApplicantsProgramCourses(long applicationId)
        {
            var applicantsProgramCourse =
                this.GetAll<ApplicantsProgramCourse>().Where(x => x.ApplicationId == applicationId);
            return applicantsProgramCourse.ToList();
        }


        public void SaveApplicantsProgramCourse(ApplicantsProgramCourse progCourse)
        {
            if (progCourse.Id <= 0)
            {
                this.Insert<ApplicantsProgramCourse>(progCourse);
            }
            this.SaveChanges();
        }

        public void DeleteApplicantsProgramCourse(ApplicantsProgramCourse progCourse)
        {
            this.Delete<ApplicantsProgramCourse>(progCourse);
            this.SaveChanges();
        }


        public IEnumerable<Application> GetApplicationsByRegNum(string regNum)
        {
            var applications = this.GetAll<Application>().Where(x => x.RegNum == regNum);
            return applications.ToList();
        }
        public void DeleteTemplateSettings(FormTemplateSettings settings)
        {
            this.Delete<FormTemplateSettings>(settings);
            this.SaveChanges();
        }


        public IEnumerable<ApplicantsProgramCourse> GetApplicantsProgramCourses()
        {
            var applicantsProgramCourse = this.GetAll<ApplicantsProgramCourse>();
            return applicantsProgramCourse.ToList();
        }


        public int GetTotalJambScore(string regNum)
        {
            var totalScore = 0;
            var jambScore = this.GetAll<JambBreakDown>().FirstOrDefault(x => x.RegNum == regNum);
            if (jambScore == null)
            {
                var manualJambScore = this.GetAll<ManualJambBreakDown>().FirstOrDefault(x => x.RegNum == regNum);
                if (manualJambScore != null)
                {
                    totalScore = Convert.ToInt32(manualJambScore.TotalScore);
                }
            }
            else
            {
                totalScore = jambScore.TotalScore;
            }


            return totalScore;
        }


        public void DeleteApplication(Application app)
        {
            this.Delete<Application>(app);
            this.SaveChanges();
        }
    }
}
