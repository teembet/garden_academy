using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface IApplicationFormRepository
    {
        void Save(ApplicationForm appForm);
        void Update();
        void SaveTemplatesInApp(TemplatesInAppForms tempInApp);
        void SaveApplicationFormWorkFlow(ApplicationFormWorkFlow appWorkFlow);
        void DeleteTempInApps(TemplatesInAppForms tempInApp);
        void DeleteTemplateSettings(FormTemplateSettings settings);
        void Delete(ApplicationForm appForm);
        IEnumerable<ApplicationForm> GetAppForms();
        IEnumerable<FormCategory> GetFormCategories();
        IEnumerable<ApplicationForm> GetAppFormsBySessionId(int sessionId);
        IEnumerable<ApplicationForm> GetAppFormsBySession(Session session);
        IEnumerable<ApplicationForm> GetAppForms(string name);
        ApplicationForm GetAppForms(int appFormId);
        FormTemplate GetFormTemplate(int id);
        IEnumerable<FormTemplate> GetFormTemplates();
        IEnumerable<FormTemplate> GetFormTemplatesByFormId(int formId);
        IEnumerable<WorkFlow> GetDefaultWorkFlow();
        TemplatesInAppForms GetFirstTemplatesInApp(int applicationFormId);
        IEnumerable<TemplatesInAppForms> GeTemplatesInApp(int applicationFormId);
        TemplatesInAppForms GetTemplatesInAppFormsByFormIdAndTempId(int formId, int tempId);
        ApplicationFormWorkFlow GetFirstApplicationFormWorkFlow(int applicationFormId);
        ApplicationFormWorkFlow GetApplicationFormWorkFlow(int applicationFormId, int workFlowId);
        IEnumerable<ApplicationFormWorkFlow> GetApplicationFormWorkFlow2(int applicationFormId);
        FormTemplateSettings GetFormTemplateSettings(int applicationFormId, int formTemplateId);
        void SaveFormTemplateSettings(FormTemplateSettings ft);
        WorkFlow GetWorkFlowItem(int id);
        void DeleteAppFormWorkFlow(List<ApplicationFormWorkFlow> workFlowItems);
    }
}
