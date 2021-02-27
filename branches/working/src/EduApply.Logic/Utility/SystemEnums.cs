using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Logic.Utility
{
    public class SystemEnums
    {
    }

    public enum ModeOfStudy
    {
        Not_Available = 0,
        Full_Time = 1,
        Part_Time = 2
    }

    public enum EmailType
    {
        EmailVerification = 1,
        AccountSetup = 2,
        PasswordReset = 3,
        AccountSetUpForApplicant = 4,
        AdmissionOffered = 5
    }

    public enum AuditTrailActions
    {

        Login = 1,
        AddProgram = 2,
        AddCourse = 3,
        AddApplicationForm = 6,
        ApplicantRegistration = 7,
        EmailVerification = 8,
        PasswordReset = 9,
        PasswordChange = 10,
        AddSession = 11,
        JambBreakdownUpload = 12,
        AdmissionListUpload = 13,
        ResultUpload = 14,
        CreateUser = 15,
        UpdateUserRecord = 16,
        ActivateUser = 17,
        DeActivateUser = 18,
        AddVenue = 19,
        AddExamVenue = 20,
        AddFaculty = 21,
        AddDepartment = 22

    }
}
