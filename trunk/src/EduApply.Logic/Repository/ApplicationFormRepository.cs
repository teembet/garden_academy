using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;


namespace EduApply.Logic.Repository
{
    public class ApplicationFormRepository : SqlRepository, IApplicationFormRepository
    {
        private IConfigurationService _configurationService;
        private IRegistrationService _registrationService;
        public ApplicationFormRepository(IDbContext context, IConfigurationService configurationService, IRegistrationService registrationService)
            : base(context)
        {
            _configurationService = configurationService;
            _registrationService = registrationService;
        }
        

      

        public void Save(ApplicationForm AppForm)
        {
            this.Insert<ApplicationForm>(AppForm);
            this.SaveChanges();
        }

        public IEnumerable<ApplicationForm> GetAppForms()
        {
            var appForms = this.GetAll<ApplicationForm>();
            return appForms.ToList();
        }
        public IEnumerable<ApplicationForm> GetAppForms(string name)
        {
            var appForms = this.GetAll<ApplicationForm>().Where(x => x.Name == name);
            return appForms.ToList();
        }


        public IEnumerable<FormTemplate> GetFormTemplates()
        {
            var formTemplates = this.GetAll<FormTemplate>();
            return formTemplates.ToList();
        }

        public TemplatesInAppForms GetFirstTemplatesInApp(int applicationFormId)
        {
            var templatesInApp = this.GetAll<TemplatesInAppForms>().FirstOrDefault(x => x.ApplicationFormId == applicationFormId);
            return templatesInApp;
        }

        public IEnumerable<TemplatesInAppForms> GeTemplatesInApp(int applicationFormId)
        {
            var templatesInApp = this.GetAll<TemplatesInAppForms>();
            return templatesInApp.Where(x => x.ApplicationFormId == applicationFormId).ToList();
        }

        public void SaveTemplatesInApp(TemplatesInAppForms tempInApp)
        {
            this.Insert<TemplatesInAppForms>(tempInApp);
            this.SaveChanges();
        }

        public void Delete(ApplicationForm appForm)
        {
            this.Delete<ApplicationForm>(appForm);
            this.SaveChanges();
        }


        public ApplicationForm GetAppForms(int appFormId)
        {
            var appForms = this.GetAll<ApplicationForm>().FirstOrDefault(x => x.Id == appFormId);
            return appForms;
        }


        public void DeleteTempInApps(TemplatesInAppForms tempInApp)
        {
            this.Delete<TemplatesInAppForms>(tempInApp);
            this.SaveChanges();
        }


        public void Update()
        {
            this.SaveChanges();
        }


        public TemplatesInAppForms GetTemplatesInAppFormsByFormIdAndTempId(int formId, int tempId)
        {
            var templateInApp = this.GetAll<TemplatesInAppForms>().FirstOrDefault(t => t.ApplicationFormId == formId && t.FormTemplateId == tempId);
            return templateInApp;
        }


        public IEnumerable<WorkFlow> GetDefaultWorkFlow()
        {
            var workFlowList = this.GetAll<WorkFlow>();
            return workFlowList.ToList();
        }

        public void SaveApplicationFormWorkFlow(ApplicationFormWorkFlow appWorkFlow)
        {
            if (appWorkFlow.Id <= 0)
            {
                this.Insert<ApplicationFormWorkFlow>(appWorkFlow);
            }
            this.SaveChanges();
        }


        public ApplicationFormWorkFlow GetFirstApplicationFormWorkFlow(int applicationFormId)
        {
            var applicationFormWorkFlow = GetAll<ApplicationFormWorkFlow>().FirstOrDefault(x => x.ApplicationFormId == applicationFormId);
            return applicationFormWorkFlow;
        }

        public FormTemplateSettings GetFormTemplateSettings(int applicationFormId, int formTemplateId)
        {
            var formTemplateSettings = this.GetAll<FormTemplateSettings>().SingleOrDefault(f => f.ApplicationFormId == applicationFormId && f.FormTemplateId == formTemplateId);
            return formTemplateSettings;
        }


        public void SaveFormTemplateSettings(FormTemplateSettings ft)
        {
            if (ft.Id <= 0)
            {
                this.Insert<FormTemplateSettings>(ft);
            }

            this.SaveChanges();
        }

        public FormTemplate GetFormTemplate(int id)
        {
            var formTemplate = this.GetAll<FormTemplate>().SingleOrDefault(x => x.Id == id);
            return formTemplate;
        }


        public IEnumerable<ApplicationFormWorkFlow> GetApplicationFormWorkFlow2(int applicationFormId)
        {
            var workflow = this.GetAll<ApplicationFormWorkFlow>().Where(x => x.ApplicationFormId == applicationFormId);
            return workflow.ToList();
        }


        public WorkFlow GetWorkFlowItem(int id)
        {
            var workFlowItem = this.GetAll<WorkFlow>().FirstOrDefault(x => x.Id == id);
            return workFlowItem;
        }


        public ApplicationFormWorkFlow GetApplicationFormWorkFlow(int applicationFormId, int workFlowId)
        {
            var applicationFormWorkFlow =
                this.GetAll<ApplicationFormWorkFlow>()
                    .FirstOrDefault(x => x.ApplicationFormId == applicationFormId && workFlowId == x.WorkFlowId);
            return applicationFormWorkFlow;
        }


        public void DeleteTemplateSettings(FormTemplateSettings settings)
        {
            this.Delete<FormTemplateSettings>(settings);
            this.SaveChanges();
        }


        public void DeleteAppFormWorkFlow(List<ApplicationFormWorkFlow> workFlowItems)
        {
            if (workFlowItems.Any())
            {
                foreach (var item in workFlowItems)
                {
                    this.Delete<ApplicationFormWorkFlow>(item);
                }
                this.SaveChanges();
            }
        }


        public IEnumerable<ApplicationForm> GetAppFormsBySession(Session session)
        {
            var applicationForms =
                this.GetAll<ApplicationForm>()
                    .Where(x => session.StartDate <= x.StartDate && session.EndDate >= x.EndDate);
            return applicationForms;
        }


        public IEnumerable<FormTemplate> GetFormTemplatesByFormId(int formId)
        {
            var templatesInAppForm = this.GetAll<TemplatesInAppForms>().Where(x => x.ApplicationFormId == formId);
            var idzOfTemplatesInAppForm = templatesInAppForm.Select(x => x.FormTemplateId);
            var formTemplates = this.GetAll<FormTemplate>().Where(x => idzOfTemplatesInAppForm.Contains(x.Id));
            return formTemplates.ToList();
        }


        public IEnumerable<FormCategory> GetFormCategories()
        {
            var formCategories = this.GetAll<FormCategory>().ToList();
            return formCategories;
        }

        public IEnumerable<ApplicationForm> GetAppFormsBySessionId(int sessionId)
        {
            var appForms = this.GetAll<ApplicationForm>().Where(x => x.SessionId == sessionId).ToList();
            return appForms;
        }

        public void GetProgramFacultyCourse(string appNumOrRegNum, int formId, out string _courseOfstudy, out string _faculty, out string _department)
        {
           
            var program = new Program();
            var courseOfStudy = new Course();
            var faculty = new Faculty();
            var session = new Session();
            var application = new Application();
            var department = new Department();
            var personalInformation = new PersonalInformation();
            var applicationForm = new ApplicationForm();
            var admLetaFormat = new AdmissionLetterFormat();
            var nonApplicantAdmittedEntry = new NonApplicantAdmittedList();
            var regNum = "";
            var email = "";
            //if (User.IsInRole("Student"))
            //{
            //email = User.Identity.GetUserName();
            var applicationFormId = formId; //Convert.ToInt32(Session["AppFormId"]);
            applicationForm = GetAppForms(applicationFormId);
            //admLetaFormat = _configurationService.GetAdmissionLetterFormat(Convert.ToInt32(applicationForm.AdmissionLetterFormatId));
            var mappedToFormIdz = _configurationService.GetMappedForms(applicationFormId).Select(x => x.MappedFormId).ToArray();
            //var mappedFormId = currentAapplicationForm.MappedToFormId;
            if (mappedToFormIdz.Any())
            {
                //first we check for applicant
                //first we assume appNumOrRegNum is application number 
                application = _configurationService.GetApplications(appNumOrRegNum);
                if (application != null)
                {
                    program = _configurationService.GetProgram(application.ProgramIdAdmittedInto);
                    courseOfStudy = _configurationService.GetCourse(application.CourseIdAdmittedInto);
                    faculty = courseOfStudy.Department.Faculty;
                    session = _configurationService.GetSession(application.SessionId);
                    //regNum = application.RegNum;
                    
                }
                else
                {
                    foreach (var mappedFormId in mappedToFormIdz)
                    {
                        //next we assume appNumOrRegNum is a registration number
                        application = _registrationService.GetAdmittedApplicationByRegNumAndFormId(appNumOrRegNum, mappedFormId);
                        if (application != null)
                        {
                            if (application.ProgramIdAdmittedInto > 0)
                            {
                                program = _configurationService.GetProgram(application.ProgramIdAdmittedInto);
                                courseOfStudy = _configurationService.GetCourse(application.CourseIdAdmittedInto);
                                faculty = courseOfStudy.Department.Faculty;
                                session = _configurationService.GetSession(application.SessionId);
                               // regNum = application.RegNum;
                                
                            }
                            else
                            {
                                nonApplicantAdmittedEntry = _registrationService.GetNonApplicantAdmittedList(appNumOrRegNum, mappedFormId);
                                if (nonApplicantAdmittedEntry != null)
                                {
                                    program = _configurationService.GetProgram(nonApplicantAdmittedEntry.ProgramId);
                                    courseOfStudy = _configurationService.GetCourse(nonApplicantAdmittedEntry.CourseId);
                                    faculty = courseOfStudy.Department.Faculty;
                                    session = _configurationService.GetSession(application.SessionId);
                                    //regNum = application.RegNum;
                                }
                            }
                            break;
                        }


                        //finally we check if application is null, if it is then no application record was found while we assumed it appNumOrRegNum was for ann applicant
                        //therefore we would now check non applicantAdmittedList
                        nonApplicantAdmittedEntry = _registrationService.GetNonApplicantAdmittedList(appNumOrRegNum, mappedFormId);
                        if (nonApplicantAdmittedEntry != null)
                        {
                            application = new Application();
                            var programIdAdmittedInto = nonApplicantAdmittedEntry.ProgramId;
                            var courseIdAdmittedInto = nonApplicantAdmittedEntry.CourseId;
                            program = _configurationService.GetProgram(programIdAdmittedInto);
                            courseOfStudy = _configurationService.GetCourse(courseIdAdmittedInto);
                            faculty = courseOfStudy.Department.Faculty;
                            session = _configurationService.GetSession(application.SessionId);
                           // regNum = application.RegNum;
                            application.ModeOfEntry = nonApplicantAdmittedEntry.ModeOfEntry;
                            break;
                        }
                    }
                }
            }
           _courseOfstudy = program.Code;
           _faculty = faculty.Name;
            _department = courseOfStudy.Name;
        }

    }
}
