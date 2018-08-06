using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface IRegistrationService
    {
        void UpdateDb();
        void SavePersonalInformation(PersonalInformation info);
        void UpdatePersonalInformation(PersonalInformation info);
        void UpdateManualJambBreakDown(ManualJambBreakDown breakDown);
        void SaveFeeRequest(FeeRequest feeRequest);
        FeeRequest GetFeeRequest(string appNum);
        FeeRequest GetFeeRequest(long payeeId, string paymentType);
        IEnumerable<FeeRequest> GetFeeRequests();
        void SaveManualJambBreakDown(ManualJambBreakDown breakDown);
        void SaveAdminBiodata(AdminBiodata biodata);
        void SaveApplication(Application application);
        IEnumerable<Application> GetApplicationDetails(string userName);
        IEnumerable<Application> GetApplications();
        ManualJambBreakDown GetManualJambBreakDown(string regNum);
        Application GetApplicationDetails(long id);
        Application GetApplicationDetailsByAppNum(string appNUm);
        PersonalInformation GetPersonalInformation(string id);
        PersonalInformation GetPersonalInformationByEmail(string email);
        IEnumerable<OLevelGrade> GetOLevelGrades();
        IEnumerable<OLevelSubject> GetOLevelSubjects();
        IEnumerable<OLevelSubject> GetOLevelSubjectsForJamb();
        IEnumerable<Country> GetCountries();
        IEnumerable<State> GetStates();
        IEnumerable<State> GetStates(int countryId);
        IEnumerable<LocalGovernmentArea> GetLocalGovernmentAreas();
        IEnumerable<LocalGovernmentArea> GetLocalGovernmentAreas(long stateId);
        IEnumerable<Faculty> GetFaculties();
        IEnumerable<Department> GetDepartments();
        IEnumerable<Department> GetDepartments(int facultyId);
        IEnumerable<Course> GetCourses();
        IEnumerable<OLevelDetail> GetOLevelDetails(long applicationId);
        //IEnumerable<Course> GetCourses(int departmentId);
        void SavePicture(Picture picture);
        void SaveReference(Reference referee);
        void SaveWorkExperience(WorkExperience workExp);
        void SaveEducationalDetails(EducationalDetails edu);
        void SaveCertificates(Certificate cert);
        void SaveOlevelDetail(OLevelDetail detail);
        void SaveOLevelResult(OLevelResult result);
        FormTemplateSettings GetFormTemplateSettings(int appFormId, int templateId);
        IEnumerable<EducationalDetails> GetEducationalDetails(long applicationId);
        IEnumerable<WorkExperience> GetWorkExperience(long applicationId);
        IEnumerable<Reference> GetReferences(long applicationId);
        IEnumerable<Certificate> GetCertificates(long applicationId);
        IEnumerable<CertificateType> GetCertificateTypes();
        Application GetLastApplication();
        OLevelSubject GetOLevelSubjectByCode(string code);
        Picture GetPictureDetails(long applicationId);
        Reference GetReference(long id);
        void DeleteReference(Reference reference);
        WorkExperience GetWorkExperienceById(long id);
        void DeleteWorkExperience(WorkExperience experience);
        Certificate GetCertificate(long id);
        void DeleteCertificate(Certificate certificate);
        EducationalDetails GetEducationalDetail(long id);
        void DeleteEducationalDetail(EducationalDetails eduDetail);
        IEnumerable<Application> GetOpenApplication(int appFormId, string username);
        Application GetApplicationByRegNumAndFormId(string regNum, int appFormId);
        Application GetAdmittedApplicationByRegNumAndFormId(string regNum, int appFormId);
        IEnumerable<OLevelResult> GetOLevelResults(long detailId);
        OLevelDetail GetOLevelDetail(long detailId);
        void DeleteOLevelDetai(OLevelDetail detail);
        IEnumerable<ExamType> GeExamTypes();
        AdmissionList GetAdmissionList(string regNum, int appFormId);
        AdmissionList GetAdmissionList(string appNum);
        void SaveAdmissionListItem(AdmissionList admissionListItem);
        IEnumerable<NonApplicantAdmittedList> GetNonApplicantAdmittedLists();
        IEnumerable<NonApplicantAdmittedList> GetNonApplicantAdmittedLists(int? sessionId, int? programId, int? courseId, int? applicationFormId);
        NonApplicantAdmittedList GetNonApplicantAdmittedList(string regNum);
        NonApplicantAdmittedList GetNonApplicantAdmittedList(string regNum, int formId);
        void SaveNonApplicantAdmittedList(NonApplicantAdmittedList item);


    }
}
