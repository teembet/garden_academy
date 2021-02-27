using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
    public interface IPrintService
    {
        string GetPersonalInformation(PersonalInformation info);
        string GetJambBreakDown(string RegNum);
        string GetApplicationResult(string appNum, string regNum);
        string GetNonApplicationResult(string RegNum);
        string GetApplicantsVenue(int appFormId, int programId, int courseId, int venueId, int seatNo);
        string GetOLevelResult(long applicationId);
        string GetEducationalDetails(long applicationId);
        string GetWorkExperience(long applicationId);
        string GetReferees(long applicationId);
        string GetCertificate(long applicationId);
        string GetProgramCourse(int programId, int courseId);
        string GetHeaderAndPassport(long applicationId);
    }
}
