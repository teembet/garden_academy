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
    public class ConfigurationService : SqlRepository, IConfigurationService
    {
        public ConfigurationService(IDbContext context)
            : base(context)
        {

        }

        public IEnumerable<Session> GetSessions()
        {
            var sessions = this.GetAll<Session>();
            return sessions.ToList();
        }
        public IEnumerable<Program> GetPrograms()
        {
            var programs = this.GetAll<Program>();
            return programs.ToList();
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
        //    var courses = this.GetAll<Course>().Where(c => c.DepartmentId == departmentId);
        //    return courses.ToList();
        //}

        public void SaveFaculty(Faculty faculty)
        {
            if (faculty.Id <= 0)
            {
                this.Insert<Faculty>(faculty);
            }

            this.SaveChanges();
        }

        public void SaveDepartment(Department department)
        {
            if (department.Id <= 0)
            {
                this.Insert<Department>(department);
            }

            this.SaveChanges();
        }

        public void SaveCourse(Course course)
        {
            if (course.Id <= 0)
            {
                this.Insert<Course>(course);
            }
            this.SaveChanges();
        }


        public void SaveSession(Session session)
        {
            if (session.Id <= 0)
            {
                this.Insert<Session>(session);
            }
            this.SaveChanges();
        }




        public void SaveProgram(Program program)
        {
            if (program.Id <= 0)
            {
                this.Insert<Program>(program);
            }
            this.SaveChanges();
        }


        public Session GetSession(int id)
        {
            var session = this.GetAll<Session>().FirstOrDefault(s => s.Id == id);
            return session;
        }

        public Program GetProgram(int id)
        {
            var program = this.GetAll<Program>().FirstOrDefault(p => p.Id == id);
            return program;
        }

        public Course GetCourse(int id)
        {
            var course = this.GetAll<Course>().FirstOrDefault(c => c.Id == id);
            return course;
        }


        public void SaveProgramCourse(ProgramCourse programCourse)
        {
            this.Insert<ProgramCourse>(programCourse);
            this.SaveChanges();
        }


        public IEnumerable<ProgramCourse> GetProgramCoursesByProgramId(int programId)
        {
            var programcourses = this.GetAll<ProgramCourse>().Where(pc => pc.ProgramId == programId);
            return programcourses.ToList();
        }

        public IEnumerable<ProgramCourse> GetProgramCoursesByCourseId(int courseId)
        {
            var programcourses = this.GetAll<ProgramCourse>().Where(pc => pc.CourseId == courseId);
            return programcourses.ToList();
        }


        public void DeleteProgramCourse(ProgramCourse programCourse)
        {
            this.Delete(programCourse);
            this.SaveChanges();
        }


        public ProgramCourse GetProgramCourseByCourseIdAndProgramId(int programId, int courseId)
        {
            var programcourse = this.GetAll<ProgramCourse>().FirstOrDefault(pc => pc.ProgramId == programId && pc.CourseId == courseId);
            return programcourse;
        }


        public IEnumerable<WorkFlow> GetWorkFlow()
        {
            var workFlowItems = this.GetAll<WorkFlow>();
            return workFlowItems.ToList();
        }


        //public void ApplicationFormWorkFlow(ApplicationFormWorkFlow appWorkFlow)
        //{
        //    this.Insert<ApplicationFormWorkFlow>(appWorkFlow);
        //    this.SaveChanges();
        //}


        public IEnumerable<Session> GetSessions(string name)
        {
            var sessions = this.GetAll<Session>().Where(x => x.Name == name);
            return sessions;
        }


        public IEnumerable<Program> GetPrograms(string name)
        {
            var programs = this.GetAll<Program>().Where(x => x.Name == name);
            return programs;
        }

        public IEnumerable<Course> GetCourses(string name)
        {
            var courses = this.GetAll<Course>().Where(x => x.Name == name);
            return courses;
        }


        public IEnumerable<Program> GetProgramsByCode(string code)
        {
            var programs = this.GetAll<Program>().Where(x => x.Code == code);
            return programs;
        }



        public IEnumerable<ClassOfDegree> GetDegrees()
        {
            var degrees = this.GetAll<ClassOfDegree>();
            return degrees.ToList();
        }


        public IEnumerable<Course> GetCourses(int programId)
        {
            //get all programs from third table connecting program and course and then select the courseId
            var idzOfCoursesForProgram = this.GetAll<ProgramCourse>().Where(pc => pc.ProgramId == programId).Select(x => x.CourseId).ToList();
            var courses = this.GetAll<Course>().Where(x => idzOfCoursesForProgram.Contains(x.Id));
            return courses.ToList();
        }


        public void SaveJambBreakDown(JambBreakDown jambBreakDown)
        {
            if (jambBreakDown.Id <= 0)
            {
                this.Insert<JambBreakDown>(jambBreakDown);
            }
            this.SaveChanges();
        }


        public JambBreakDown GetJambBreakDown(string regNum)
        {
            var jambList = this.GetAll<JambBreakDown>().FirstOrDefault(j => j.RegNum == regNum);
            return jambList;
        }
        public IEnumerable<JambBreakDown> GetJambBreakDown(int sessionId)
        {
            var jambList = this.GetAll<JambBreakDown>().Where(j => j.SessionId == sessionId);
            return jambList.ToList();
        }


        public IEnumerable<SessionResult> GetSessionResult(int? sessionId)
        {
            var results = this.GetAll<SessionResult>();
            results = sessionId == null
                ? results.Where(x => x.SessionId == -1)
                : results.Where(x => x.SessionId == sessionId);

            return results.ToList();
        }
        public IEnumerable<FormResult> GetFormResult(int? applicationFormId)
        {

            var results = this.GetAll<FormResult>();
            results = applicationFormId == null
                ? results.Where(x => x.ApplicationFormId == -1)
                : results.Where(x => x.ApplicationFormId == applicationFormId);

            return results.ToList();
        }

        public void SaveFormResult(FormResult formResult)
        {
            if (formResult.Id <= 0)
            {
                this.Insert<FormResult>(formResult);
            }
            this.SaveChanges();
        }


        //public FormResult GetFormResult(string regNum)
        //{
        //    var formResult = this.GetAll<FormResult>().FirstOrDefault(f => f.RegNum == regNum);
        //    return formResult;
        //}
        public FormResult GetFormResult(string appNum)
        {
            var formResult = this.GetAll<FormResult>().FirstOrDefault(f => f.AppNum == appNum);
            return formResult;
        }

        public IEnumerable<Application> GetApplications(int applicationFormId)
        {
            var applications = this.GetAll<Application>().Where(ap => ap.AppFormId == applicationFormId);
            return applications.ToList();
        }

        public IEnumerable<Application> GetApplications()
        {
            var applications = this.GetAll<Application>();
            return applications.ToList();
        }

        public PersonalInformation GetPersonalInformationByRegNum(string regNum)
        {
            var personalInfo = this.GetAll<PersonalInformation>().FirstOrDefault(p => p.RegNum == regNum);
            return personalInfo;
        }
        public IEnumerable<PersonalInformation> GetPersonalInformations()
        {
            var personalInfos = this.GetAll<PersonalInformation>();
            return personalInfos.ToList();
        }


        public Application GetApplications(string appNum)
        {
            var application = this.GetAll<Application>().FirstOrDefault(app => app.AppNum == appNum);
            return application;
        }


        public IEnumerable<Application> GetApplicationsByParams(int? sessionId, int? programId, int? courseId, int? appFormId)
        {
            bool isAllParametersNull = true;
            var applications = this.GetAll<Application>().ToList();

            if (sessionId != null)
            {
                int sesId = Convert.ToInt32(sessionId);
                isAllParametersNull = false;
                applications = applications.Where(x => x.SessionId == sesId).ToList();
            }
            if (programId != null)
            {
                int progId = Convert.ToInt32(programId);
                isAllParametersNull = false;
                applications = applications.Where(x => x.ProgramIdAdmittedInto == progId).ToList();
            }
            if (courseId != null)
            {
                int cosId = Convert.ToInt32(courseId);
                isAllParametersNull = false;
                applications = applications.Where(x => x.CourseIdAdmittedInto == cosId).ToList();
            }
            if (appFormId != null)
            {
                int appformId = Convert.ToInt32(appFormId);
                isAllParametersNull = false;
                applications = applications.Where(x => x.AppFormId == appformId).ToList();
            }
            if (isAllParametersNull)
            {
                //this line is used to remove any data currently in the list
                applications = applications.Where(x => x.SessionId == -1).ToList();
            }
            return applications.ToList();
        }

        public PersonalInformation GetPersonalInformation(string id)
        {
            var personalInformation = this.GetAll<PersonalInformation>().FirstOrDefault(x => x.Id == id);
            return personalInformation;
        }




        public SessionResult GetSessionResult(string regNum)
        {
            var sessionResult = this.GetAll<SessionResult>().FirstOrDefault(x => x.RegNum == regNum);
            return sessionResult;
        }

        public void SaveSessionResult(SessionResult sessionResult)
        {

            if (sessionResult.Id <= 0)
            {
                this.Insert<SessionResult>(sessionResult);
            }
            this.SaveChanges();
        }


        public void DeleteFormResult(FormResult formResult)
        {
            this.Delete(formResult);
            this.SaveChanges();
        }

        public void DeleteSessionResult(SessionResult sessionResult)
        {
            this.Delete(sessionResult);
            this.SaveChanges();
        }

        public void DeleteJambBreakDown(JambBreakDown jambBreakDown)
        {
            this.Delete(jambBreakDown);
            this.SaveChanges();
        }


        public FormResult GetFormResult(long id)
        {
            var formResult = this.GetAll<FormResult>().FirstOrDefault(x => x.Id == id);
            return formResult;
        }


        public SessionResult GetSessionResult(long id)
        {
            var sessionResult = this.GetAll<SessionResult>().FirstOrDefault(x => x.Id == id);
            return sessionResult;
        }


        public JambBreakDown GetJambResult(long id)
        {
            var jambResult = this.GetAll<JambBreakDown>().FirstOrDefault(x => x.Id == id);
            return jambResult;
        }


        public IEnumerable<Application> GetApplicationsByRegNum(string regNum)
        {
            var applications = this.GetAll<Application>().Where(x => x.RegNum == regNum);
            return applications.ToList();
        }


        public IEnumerable<Course> GetCoursesByCode(string code)
        {
            var courses = this.GetAll<Course>().Where(x => x.Code == code);
            return courses.ToList();
        }


        public IEnumerable<Faculty> GetFaculties(string name)
        {
            var faculties = this.GetAll<Faculty>().Where(x => x.Name == name);
            return faculties;
        }

        public IEnumerable<Department> GetDepartments(string name)
        {
            var departments = this.GetAll<Department>().Where(x => x.Name == name);
            return departments;
        }


        public Faculty GetFaculty(int id)
        {
            var faculty = this.GetAll<Faculty>().FirstOrDefault(x => x.Id == id);
            return faculty;
        }


        public Department GetDepartment(int id)
        {
            var department = this.GetAll<Department>().FirstOrDefault(x => x.Id == id);
            return department;
        }


        public IEnumerable<Department> GetDepartmentsByCode(string code)
        {
            var departments = this.GetAll<Department>().Where(x => x.Code == code);
            return departments;
        }


        public WorkFlow GetWorkFlow(int workFlowId)
        {
            var workFlow = this.GetAll<WorkFlow>().FirstOrDefault(x => x.Id == workFlowId);
            return workFlow;
        }


        public IEnumerable<ProgramCourse> GetProgramCourses()
        {
            var programCourses = this.GetAll<ProgramCourse>();
            return programCourses.ToList();
        }


        public void SaveAppFormProgramCourse(AppFormProgramCourse appFormProgramCourse)
        {
            this.Insert<AppFormProgramCourse>(appFormProgramCourse);
            this.SaveChanges();
        }


        public IEnumerable<AppFormProgramCourse> GetAppFormProgramCourses(int appFormId)
        {
            var appFormProgramCourse = this.GetAll<AppFormProgramCourse>().Where(x => x.AppFormId == appFormId);
            return appFormProgramCourse.ToList();
        }


        public void DeleteAppFormProgramCourse(AppFormProgramCourse appFormProgramCourse)
        {
            this.Delete<AppFormProgramCourse>(appFormProgramCourse);
            this.SaveChanges();
        }


        public IEnumerable<Venues> GetVenues()
        {
            var venues = this.GetAll<Venues>();
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

        public IEnumerable<VenueMappings> GetVenueMappings()
        {
            var venueMappings = this.GetAll<VenueMappings>();
            return venueMappings;
        }

        public IEnumerable<Course> GetCoursesByDepId(int departmentId)
        {
            var courses = this.GetAll<Course>().Where(x => x.DepartmentId == departmentId);
            return courses.ToList();
        }


        public IEnumerable<Program> GetProgramsByCourseId(int courseId)
        {
            //get all programs from third table connecting program and course and then select the courseId
            var idzOfProgramsForCourse = this.GetAll<ProgramCourse>().Where(pc => pc.CourseId == courseId).Select(x => x.ProgramId).ToList();
            var programs = this.GetAll<Program>().Where(x => idzOfProgramsForCourse.Contains(x.Id));
            return programs.ToList();
        }


        public void SaveVenueMapping(VenueMappings vn)
        {
            if (vn.Id <= 0)
            {
                this.Insert<VenueMappings>(vn);
            }
            this.SaveChanges();
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


        public Session GetSessionByAppForm(ApplicationForm applicationForm)
        {
            var session =
                this.GetAll<Session>()
                    .FirstOrDefault(
                        x => x.StartDate <= applicationForm.StartDate && x.EndDate >= applicationForm.EndDate);

            return session;
        }


        public Application GetApplicationByRegNumAndAppNum(string regNum, string appNum)
        {
            var application = this.GetAll<Application>().FirstOrDefault(x => x.RegNum == regNum && x.AppNum == appNum);
            return application;
        }


        public FormResult GetFormResult(int formId, string regNum)
        {
            var formResult =
                this.GetAll<FormResult>().FirstOrDefault(x => x.RegNum == regNum && x.ApplicationFormId == formId);
            return formResult;
        }


        public IEnumerable<MappedForm> GetMappedForms(int formId)
        {
            var mappedForms = this.GetAll<MappedForm>().Where(x => x.FormId == formId);
            return mappedForms.ToList();
        }

        public void SaveMappedForm(MappedForm mappedForm)
        {
            if (mappedForm.Id <= 0)
            {
                this.Insert<MappedForm>(mappedForm);
                this.SaveChanges();
            }
        }

        public void DeleteMappedForm(MappedForm mappedForm)
        {
            this.Delete<MappedForm>(mappedForm);
            this.SaveChanges();
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


        public DateTime GetCurrentWestAfricanDateTime()
        {
            string timeZoneSetting = "W. Central Africa Standard Time";
            DateTime userdate = DateTime.Now;
            TimeZoneInfo timeZone;
            try
            {
                timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneSetting);
                userdate = TimeZoneInfo.ConvertTime(userdate, timeZone);
            }
            catch (Exception ex)
            {
            }

            return userdate;
        }


        public string GetCurrentUrl()
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            var urlWithoutQueryString = url.Split('?');
            return urlWithoutQueryString[0];
        }


        public string GetSchoolName()
        {
            var schoolName = "University Of Ibadan";
            return schoolName;
        }


        public SessionResult GetSessionResult(int sessionId, string regNum)
        {
            var sessionResult =
                this.GetAll<SessionResult>().FirstOrDefault(x => x.RegNum == regNum && x.SessionId == sessionId);
            return sessionResult;
        }
    }
}
