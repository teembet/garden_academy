using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Repository;
using EduApply.Logic.Utility;

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
            var faculties = this.GetAll<Faculty>().OrderBy(x => x.Name);
            return faculties.ToList();
        }

        public IEnumerable<Department> GetDepartments()
        {
            var departments = this.GetAll<Department>().OrderBy(x => x.Name);
            return departments.ToList();
        }

        public IEnumerable<Department> GetDepartments(int facultyId)
        {
            var departments = this.GetAll<Department>().Where(x => x.FacultyId == facultyId).OrderBy(x => x.Name);
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
        public JambPageBreakdown GetJambBreakDown(int sessionId, int skip, int length, string sSearch)
        {
            var count = this.GetAll<JambBreakDown>().Count(j => j.SessionId == sessionId);
            int filteredCount = 0;
            var jambList = new List<JambBreakDown>();
            if (string.IsNullOrEmpty(sSearch))
            {
                jambList = this.GetAll<JambBreakDown>().Where(j => j.SessionId == sessionId).OrderBy(x => x.LastName).Skip(skip).Take(length).ToList();
            }
            else
            {
                filteredCount = this.GetAll<JambBreakDown>().Count(j => j.SessionId == sessionId && (j.RegNum.Contains(sSearch) || j.LastName.Contains(sSearch) || j.FirstName.Contains(sSearch) || j.MiddleName.Contains(sSearch)));
                jambList = this.GetAll<JambBreakDown>().Where
                    (j => j.SessionId == sessionId && (j.RegNum.Contains(sSearch) || j.LastName.Contains(sSearch) || j.FirstName.Contains(sSearch) || j.MiddleName.Contains(sSearch))).OrderBy(x => x.LastName).Skip(skip).Take(length).ToList();
            }

            var jambPageBreakDown = new JambPageBreakdown()
            {
                FilteredCount = filteredCount,
                TotalCount = count,
                JambList = jambList
            };
            return jambPageBreakDown;
        }

        public JambBiodataPageBreakdown GetJambBiodata(int sessionId, int skip, int length, string sSearch)
        {
            var count = this.GetAll<JambBiodata>().Count(j => j.SessionId == sessionId);
            int filteredCount = 0;
            var jambList = new List<JambBiodata>();
            if (string.IsNullOrEmpty(sSearch))
            {
                jambList = this.GetAll<JambBiodata>().Where(j => j.SessionId == sessionId).OrderBy(x => x.LastName).Skip(skip).Take(length).ToList();
            }
            else
            {
                filteredCount = this.GetAll<JambBiodata>().Count(j => j.SessionId == sessionId && (j.RegNum.Contains(sSearch) || j.LastName.Contains(sSearch) || j.FirstName.Contains(sSearch) || j.MiddleName.Contains(sSearch)));
                jambList = this.GetAll<JambBiodata>().Where
                    (j => j.SessionId == sessionId && (j.RegNum.Contains(sSearch) || j.LastName.Contains(sSearch) || j.FirstName.Contains(sSearch) || j.MiddleName.Contains(sSearch))).OrderBy(x => x.LastName).Skip(skip).Take(length).ToList();
            }

            var jambPageBreakDown = new JambBiodataPageBreakdown()
            {
                FilteredCount = filteredCount,
                TotalCount = count,
                JambList = jambList
            };
            return jambPageBreakDown;
        }


        public IEnumerable<SessionResult> GetSessionResult(int? sessionId)
        {
            var results = this.GetAll<SessionResult>();
            results = sessionId == null
                ? results.Where(x => x.SessionId == -1)
                : results.Where(x => x.SessionId == sessionId);

            return results.ToList();
        }
        public FormPageBreakdown GetFormResult(int applicationFormId, int skip, int length, string sSearch)
        {
            var count = this.GetAll<FormResult>().Count(j => j.ApplicationFormId == applicationFormId);
            int filteredCount = 0;
            var formResultList = new List<FormResult>();
            if (string.IsNullOrEmpty(sSearch))
            {
                formResultList = this.GetAll<FormResult>().Where(j => j.ApplicationFormId == applicationFormId).OrderBy(x => x.RegNum).Skip(skip).Take(length).ToList();
            }
            else
            {
                filteredCount = this.GetAll<FormResult>().Count(j => j.ApplicationFormId == applicationFormId && (j.RegNum.Contains(sSearch) || j.AppNum.Contains(sSearch)));
                formResultList = this.GetAll<FormResult>().Where
                    (j => j.ApplicationFormId == applicationFormId && (j.RegNum.Contains(sSearch) || j.AppNum.Contains(sSearch))).OrderBy(x => x.RegNum).Skip(skip).Take(length).ToList();
            }

            var formPageBreakDown = new FormPageBreakdown()
            {
                FilteredCount = filteredCount,
                TotalCount = count,
                FormResultList = formResultList
            };
            return formPageBreakDown;
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
        public Application GetApplicationByEmailAndFormId(string email, int formId)
        {
            var application = this.GetAll<Application>().FirstOrDefault(x => x.UserName.Equals(email) && x.AppFormId == formId && x.IsSubmitted);
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


        public IEnumerable<Course> GetCoursesByDepId(int departmentId)
        {
            var courses = this.GetAll<Course>().Where(x => x.DepartmentId == departmentId).OrderBy(x => x.Name);
            return courses.ToList();
        }


        public IEnumerable<Program> GetProgramsByCourseId(int courseId)
        {
            //get all programs from third table connecting program and course and then select the courseId
            var idzOfProgramsForCourse = this.GetAll<ProgramCourse>().Where(pc => pc.CourseId == courseId).Select(x => x.ProgramId).ToList();
            var programs = this.GetAll<Program>().Where(x => idzOfProgramsForCourse.Contains(x.Id)).OrderBy(x => x.Code);
            return programs.ToList();
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
            var application = this.GetAll<Application>().Where(x => x.RegNum == regNum && x.AppNum == appNum).FirstOrDefault();
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
            var theme = EngineContext.Resolve<Theme>();
            var schoolName = theme.SchoolName;
            return schoolName;
        }


        public SessionResult GetSessionResult(int sessionId, string regNum)
        {
            var sessionResult =
                this.GetAll<SessionResult>().FirstOrDefault(x => x.RegNum == regNum && x.SessionId == sessionId);
            return sessionResult;
        }


        public IEnumerable<AppFormGateway> GetAppFormGateways(int formId)
        {
            var appFormGateways = this.GetAll<AppFormGateway>().Where(x => x.AppFormId == formId);
            return appFormGateways.ToList();
        }


        public void DeleteAppFormGateway(AppFormGateway appFormGateway)
        {
            this.Delete<AppFormGateway>(appFormGateway);
            this.SaveChanges();
        }


        public void SaveAppFormGateway(AppFormGateway appFormGateway)
        {
            if (appFormGateway.Id <= 0)
            {
                this.Insert<AppFormGateway>(appFormGateway);
            }
            this.SaveChanges();
        }


        public IEnumerable<Gateway> GetGateways()
        {
            var gateways = this.GetAll<Gateway>();
            return gateways.ToList();
        }


        public AttemptedPayment GetAttemptedPayment(long applicationId)
        {
            var attemptedPayment = this.GetAll<AttemptedPayment>().Where(x => x.ApplicationId == applicationId)
                .OrderByDescending(x => x.TransactionReference);
            return attemptedPayment.FirstOrDefault();
        }


        public IEnumerable<AttemptedPayment> GetAttemptedPayments(long applicationId)
        {
            var attemptedPayments = this.GetAll<AttemptedPayment>().Where(x => x.ApplicationId == applicationId);
            return attemptedPayments.ToList();
        }


        public IEnumerable<Bank> GetBanks()
        {
            var banks = this.GetAll<Bank>().OrderBy(x => x.Name);
            return banks.ToList();
        }


        public IEnumerable<Split> GetSplits(int applicationFormId)
        {
            var splits = this.GetAll<Split>().Where(x => x.ApplicationFormId == applicationFormId);
            return splits.ToList();
        }


        public void SaveSplit(Split split)
        {
            if (split.Id <= 0)
            {
                this.Insert<Split>(split);
            }
            this.SaveChanges();
        }


        public Split GetSplit(long id)
        {
            var split = this.GetAll<Split>().FirstOrDefault(x => x.Id == id);
            return split;
        }


        public void DeleteSplit(Split split)
        {
            this.Delete<Split>(split);
            this.SaveChanges();
        }


        public void UpdateSplit(Split split)
        {
            this.Update<Split>(split);
            this.SaveChanges();
        }


        public string GetPmb()
        {
            var tenancy = EngineContext.Resolve<Tenancy>();
            var pmb = tenancy.Pmb;
            return pmb;

        }


        public ApplicationNoFormat GetApplicationNoFormatByFormId(int applicationFormId)
        {
            var format = this.GetAll<ApplicationNoFormat>()
                .FirstOrDefault(x => x.ApplicationFormId == applicationFormId);
            return format;
        }

        public void SaveApplicationNoFormat(ApplicationNoFormat format)
        {
            if (format.Id <= 0)
            {
                this.Insert<ApplicationNoFormat>(format);
            }
            this.SaveChanges();
        }

        public void DeleteApplicationNoFormat(int applicationFormId)
        {
            var format = this.GetAll<ApplicationNoFormat>().FirstOrDefault(x => x.ApplicationFormId == applicationFormId);
            if (format != null)
            {
                this.Delete<ApplicationNoFormat>(format);
                this.SaveChanges();
            }

        }


        public IEnumerable<ApplicationNoFormat> GetFormFormats()
        {
            var formats = this.GetAll<ApplicationNoFormat>();
            return formats.ToList();
        }


        public ApplicationNoFormat GetApplicationNoFormat(long id)
        {
            var applicationNoFormat = this.GetAll<ApplicationNoFormat>().FirstOrDefault(x => x.Id == id);
            return applicationNoFormat;
        }


        public void UpdateApplicationNoFormat(ApplicationNoFormat format)
        {
            this.Update<ApplicationNoFormat>(format);
            this.SaveChanges();
        }


        public WorkFlow GetWorkFlowbyActionName(string actionName)
        {
            var workFlow = this.GetAll<WorkFlow>().FirstOrDefault(x => x.ActionName.Equals(actionName));
            return workFlow;
        }


        public FormTemplate GetFormTemplateByCode(string code)
        {
            var formTemplate = this.GetAll<FormTemplate>().FirstOrDefault(x => x.Code.Equals(code));
            return formTemplate;
        }


        public IEnumerable<AppFormClassOfDegree> GetAppFormClassOfDegrees(int formId)
        {
            var appFormClassOfDegress = this.GetAll<AppFormClassOfDegree>().Where(x => x.AppFormId == formId);
            return appFormClassOfDegress.ToList();
        }

        public void DeleteAppFormClassOfDegree(AppFormClassOfDegree appFormClassOfDegree)
        {
            this.Delete<AppFormClassOfDegree>(appFormClassOfDegree);
            this.SaveChanges();
        }


        public void SaveAppFormClassOfDegree(AppFormClassOfDegree appFormClassOfDegree)
        {
            if (appFormClassOfDegree.Id <= 0)
            {
                this.Insert<AppFormClassOfDegree>(appFormClassOfDegree);
            }
            this.SaveChanges();
        }


        public IEnumerable<ClassOfDegree> GetDegrees(int formId)
        {
            var formClassOfDegreeIdz = this.GetAll<AppFormClassOfDegree>().Where(x => x.AppFormId == formId).Select(x => x.ClassOfDegreeId).ToArray();
            var classOfDegrees = this.GetAll<ClassOfDegree>().Where(x => formClassOfDegreeIdz.Contains(x.Id));
            return classOfDegrees;
        }


        public IEnumerable<AdmissionLetterFormat> GetAdmissionLetterFormats()
        {
            var formats = this.GetAll<AdmissionLetterFormat>();
            return formats.ToList();
        }

        public AdmissionLetterFormat GetAdmissionLetterFormat(int id)
        {
            var format = this.GetAll<AdmissionLetterFormat>().FirstOrDefault(x => x.Id == id);
            return format;
        }


        public JambBiodata GetJambBiodata(string regNum)
        {
            var biodata = this.GetAll<JambBiodata>().FirstOrDefault(x => x.RegNum == regNum);
            return biodata;
        }


        public void SaveJambBiodata(JambBiodata biodata)
        {
            if (biodata.Id <= 0)
            {
                this.Insert<JambBiodata>(biodata);
            }
            this.SaveChanges();
        }
    }
}
