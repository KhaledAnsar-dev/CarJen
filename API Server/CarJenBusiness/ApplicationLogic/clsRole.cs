using CarJenData.DataModels;
using CarJenData.Repositories;
using CarJenShared.Dtos.RoleDtos;
using CarJenShared.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsRole
    {
        public enum enMode { Add = 0, Update = 1 };
        public short? RoleID { get; set; }
        public string RoleTitle { get; set; }
        public float Salry { get; set; }
        public int? Permission { get; set; }

        public RoleDto toRoleDto
        { 
            get
            {
                if (!this.RoleID.HasValue)
                    throw new Exception("🚨 RoleID is null!");

                if (this.Permission == null)
                    throw new Exception("🚨 Permission is null!");

                if (this.RoleTitle == null)
                    throw new Exception("🚨 RoleTitle is null!");
                return new RoleDto { roleID = (short)this.RoleID, roleTitle = this.RoleTitle, salary = this.Salry, permission = this.Permission?? 0};
            }
        }

        public enMode Mode;
        public enum enPermissions
        {
            NoPermission = 0,           // لا يوجد صلاحية
            UsersManagement = 1,         // إدارة المستخدمين
            TeamsManagement = 2,         // إدارة الفرق
            MembersManagement = 4,       // إدارة الأعضاء داخل الفرق
            CarDocsManagement = 8,       // إدارة وثائق السيارات
            InspectionAppointments = 16, // إدارة مواعيد الفحص
            TechnicalInspections = 32,   // إدارة الفحوصات التقنية
            FinalReports = 64,           // إدارة التقارير النهائية
            PublishingManagement = 128,  // إدارة النشر
            LoginToSystem = 256,         // صلاحية الدخول إلى النظام
            All = 512                    // جميع الصلاحيات
        };
        public clsRole()
        {
            RoleID = null;
            RoleTitle = string.Empty;
            Salry = 0;

            Mode = enMode.Add;
        }

        private clsRole(RoleDto roleDto)
        {
            RoleID = roleDto.roleID;
            RoleTitle = roleDto.roleTitle;
            Salry = roleDto.salary;
            Permission = roleDto.permission;
            Mode = enMode.Update;
        }

        
        public static clsRole? Find(int roleID)
        {
            var roleDto = RoleRepository.GetRoleByID(roleID);

            if (roleDto == null)
                return null;

            return new clsRole(roleDto);
        }
        public static clsRole? Find(string Title)
        {
            var roleDto = RoleRepository.GetRoleByTitle(Title);
            if (roleDto == null)
                return null;

            return new clsRole(roleDto);
        }
        static public List<RoleDto> GetRoles()
        {
            return RoleRepository.GetRoles();
        }
        static public List<RoleDto> GetRolesRequiringTeam()
        {
            return RoleRepository.GetRolesRequiringTeam();
        }

        private bool _AddUser()
        {
            // For future usadge
            return false;
        }
        private bool _UpdateUser()
        {
            return RoleRepository.UpdateRole(this.toRoleDto);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }
            return false;
        }

    }

}
