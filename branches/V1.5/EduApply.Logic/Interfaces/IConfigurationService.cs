using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface IConfigurationService
    {
        IEnumerable<Session> GetSessions();
        Session GetSession(int id);
        Session GetSessionByAppForm(ApplicationForm applicationForm);
        Faculty GetFaculty(int id);
        Department GetDepartment(int id);
        IEnumerable<Faculty> GetFaculties(string name);
        IEnumerable<Department> GetDepartments(string name);
        IEnumerable<Department> GetDepartmentsByCode(string code);
        IEnumerable<Session> GetSessions(string name);
        IEnumerable<Program> GetPrograms(string name);
        IEnumerable<Program> GetProgramsByCode(string code);
        IEnumerable<Course> GetCourses(string name);
        IEnumerable<Course> GetCoursesByDepId(int departmentId);
        IEnumerable<Course> GetCoursesByCode(string code);
        IEnumerable<Faculty> GetFaculties();
        IEnumerable<Department> GetDepartments();
        IEnumerable<Department> GetDepartments(int facultyId);
        IEnumerable<Program> GetPrograms();
        IEnumerable<ClassOfDegree> GetDegrees();
        IEnumerable<JambBreakDown> GetJambBreakDown(int sessionId);

        IEnumerable<SessionResult> GetSessionResult(int? sessionId);
        PersonalInformation GetPersonalInformationByRegNum(string regNum);
        IEnumerable<PersonalInformation> GetPersonalInformations();
        PersonalInformation GetPersonalInformation(string id);
        Program GetProgram(int id);
        JambBreakDown GetJambBreakDown(string regNum);
        JambBreakDown GetJambResult(long id);
        SessionResult GetSessionResult(long id);
        SessionResult GetSessionResult(string regNum);
        SessionResult GetSessionResult(int sessionId, string regNum);
        IEnumerable<Course> GetCourses();
        IEnumerable<Course> GetCourses(int programId);
        IEnumerable<Program> GetProgramsByCourseId(int courseId);
        IEnumerable<WorkFlow> GetWorkFlow();

        WorkFlow GetWorkFlow(int workFlowId);
        Course GetCourse(int id);
        //IEnumerable<Course> GetCourses(int departmentId);
        void SaveFaculty(Faculty faculty);
        void SaveDepartment(Department department);
        void SaveCourse(Course course);
        void SaveSession(Session session);
        void SaveProgram(Program program);
        void SaveJambBreakDown(JambBreakDown jambBreakDown);
        void SaveSessionResult(SessionResult sessionResult);
        void SaveProgramCourse(ProgramCourse programCourse);
        void SaveAppFormProgramCourse(AppFormProgramCourse appFormProgramCourse);
        IEnumerable<AppFormProgramCourse> GetAppFormProgramCourses(int appFormId);
        void DeleteAppFormProgramCourse(AppFormProgramCourse appFormProgramCourse);
        //void ApplicationFormWorkFlow(ApplicationFormWorkFlow appWorkFlow);
        void DeleteProgramCourse(ProgramCourse programCourse);
        void DeleteSessionResult(SessionResult sessionResult);
        void DeleteJambBreakDown(JambBreakDown jambBreakDown);
        IEnumerable<ProgramCourse> GetProgramCoursesByProgramId(int programId);
        IEnumerable<ProgramCourse> GetProgramCoursesByCourseId(int courseId);
        IEnumerable<ProgramCourse> GetProgramCourses();
        ProgramCourse GetProgramCourseByCourseIdAndProgramId(int programId, int courseId);
        FormResult GetFormResult(long id);
        //FormResult GetFormResult(string regNum);
        FormResult GetFormResult(string appNum);
        FormResult GetFormResult(int formId, string regNum);
        void SaveFormResult(FormResult formResult);
        void DeleteFormResult(FormResult formResult);
        IEnumerable<FormResult> GetFormResult(int? applicationFormId);
        Application GetApplications(string appNum);
        Application GetApplicationByRegNumAndAppNum(string regNum, string appNum);
        IEnumerable<Application> GetApplicationsByRegNum(string regNum);
        IEnumerable<Application> GetApplications(int applicationFormId);
        IEnumerable<Application> GetApplicationsByParams(int? sessionId, int? programId, int? courseId, int? appFormId);
        IEnumerable<Application> GetApplications();
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
        IEnumerable<MappedForm> GetMappedForms(int formId);
        void SaveMappedForm(MappedForm mappedForm);
        void DeleteMappedForm(MappedForm mappedForm);

        IEnumerable<ExamVenue> GetExamVenues();
        IEnumerable<ExamVenue> GetExamVenues(int venueId);
        void SaveExamVenue(ExamVenue examVenue);
        ExamVenue GetExamVenue(int examVenueId);
        DateTime GetCurrentWestAfricanDateTime();
        string GetCurrentUrl();
        string GetSchoolName();
    }
}
