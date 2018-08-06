using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EduApply.Data.Entities;
using EduApply.Web.Models;

namespace EduApply.Web
{
    public static class AutoMapConfig
    {
        public static void Register()
        {
            ModelToEntities();
            EntitiesToModel();
        }

        private static void ModelToEntities()
        {
            Mapper.CreateMap<ApplicationFormModel, ApplicationForm>();
            Mapper.CreateMap<FormTemplateModel, FormTemplate>();
            Mapper.CreateMap<AdminRegistrationModel, AdminBiodata>();
            Mapper.CreateMap<FacultyModel, Faculty>();
            Mapper.CreateMap<DepartmentModel, Department>();
            Mapper.CreateMap<CourseModel, Course>();
            Mapper.CreateMap<ProgramModel, Program>();
            Mapper.CreateMap<SessionModel, Session>();
            Mapper.CreateMap<WorkFlowModel, WorkFlow>();
            Mapper.CreateMap<PersonalInformationModel, PersonalInformation>();
            Mapper.CreateMap<Country, CountryModel>();
            Mapper.CreateMap<State, StateModel>();
            Mapper.CreateMap<LocalGovernmentArea, LocalGovernmentAreaModel>();
            Mapper.CreateMap<ApplicantsProgramCourse, ApplicantsProgramCourseModel>();
            Mapper.CreateMap<JambBreakDownModel, JambBreakDown>();
            Mapper.CreateMap<FormResultModel, FormResult>();
            Mapper.CreateMap<SessionResultModel, SessionResult>();
            Mapper.CreateMap<PictureModel, Picture>();
        }

        private static void EntitiesToModel()
        {
            Mapper.CreateMap<ApplicationForm, ApplicationFormModel>();
            Mapper.CreateMap<FormTemplate, FormTemplateModel>();
            Mapper.CreateMap<Faculty, FacultyModel>();
            Mapper.CreateMap<Department, DepartmentModel>();
            Mapper.CreateMap<Course, CourseModel>();
            Mapper.CreateMap<Program, ProgramModel>();
            Mapper.CreateMap<Session, SessionModel>();
            Mapper.CreateMap<WorkFlow, WorkFlowModel>();
            Mapper.CreateMap<PersonalInformation, PersonalInformationModel>();
            Mapper.CreateMap<PersonalInformation, PersonalInfoPreviewModel>();
            Mapper.CreateMap<ApplicationUser, AdminUserModel>();
            Mapper.CreateMap<CountryModel, Country>();
            Mapper.CreateMap<StateModel, State>();
            Mapper.CreateMap<LocalGovernmentAreaModel, LocalGovernmentArea>();
            Mapper.CreateMap<ApplicantsProgramCourseModel, ApplicantsProgramCourse>();
            Mapper.CreateMap<JambBreakDown, JambBreakDownModel>();
            Mapper.CreateMap<FormResult, FormResultModel>();
            Mapper.CreateMap<SessionResult, SessionResultModel>();
            Mapper.CreateMap<WorkExperience, WorkExperienceModel>();
            Mapper.CreateMap<Reference, ReferenceModel>();
            Mapper.CreateMap<EducationalDetails, EducationalDetailsModel>();
            Mapper.CreateMap<Picture, PictureModel>();
            Mapper.CreateMap<ManualJambBreakDown, ManualJambBreakDownModel>();
            Mapper.CreateMap<Venues, VenueModel>();
            Mapper.CreateMap<ExamVenue, ExamVenueModel>();
            Mapper.CreateMap<AttemptedPayment, AttemptedPaymentModel>();
            Mapper.CreateMap<ApplicationNoFormat, ApplicationNoFormatModel>();
            Mapper.CreateMap<ManualJambBreakDown, JambBreakDown>();
            Mapper.CreateMap<JambBiodata, JambBiodataModel>();
        }
    }
}