using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface IStateManager
    {
        bool ConfirmWorkFlowStage(Application app, List<ApplicationFormWorkFlow> appFormWorkFlowList, string workFlowName);
        bool ConfirmFillStage(Application app, List<TemplatesInAppForms> formTemplates, string templateCode);
    }
}
