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
    public class StateManager : SqlRepository, IStateManager
    {
        public StateManager(IDbContext context): base(context)
        {

        }


        public bool ConfirmWorkFlowStage(Application app, List<ApplicationFormWorkFlow> appFormWorkFlowList, string workFlowName)
        {
            var currentFormWorkFlow = appFormWorkFlowList[app.WorkFlowStage];
            var currentWorkFlowId = currentFormWorkFlow.WorkFlowId;
            var currentWorkFlowItem = this.GetAll<WorkFlow>().FirstOrDefault(x => x.Id == currentWorkFlowId) ?? new WorkFlow();
            if (currentWorkFlowItem.Name == workFlowName)
            {
                return true;
            }
            return false;
        }

        public bool ConfirmFillStage(Application app, List<TemplatesInAppForms> formTemplates, string templateCode, List<ApplicationFormWorkFlow> appFormWorkFlowList, string workFlowName)
        {
            var currentFormWorkFlow = appFormWorkFlowList[app.WorkFlowStage];
            var currentWorkFlowId = currentFormWorkFlow.WorkFlowId;
            var currentWorkFlowItem = this.GetAll<WorkFlow>().FirstOrDefault(x => x.Id == currentWorkFlowId) ?? new WorkFlow();

            var currentFormTemplates = formTemplates[app.FillStage];
            var currentformTemplateId = currentFormTemplates.FormTemplateId;
            var formTemplate = this.GetAll<FormTemplate>().FirstOrDefault(x => x.Id == currentformTemplateId) ?? new FormTemplate();
            if (formTemplate.Code == templateCode && currentWorkFlowItem.Name == workFlowName)
            {
                return true;
            }
            return false;
        }
    }
}
