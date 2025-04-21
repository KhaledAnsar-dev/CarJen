using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsTeamMember
    {
        enum enMode { Add = 0, Update = 1 };
        enMode Mode;

        public int? TeamMemberID { get; set; }
        public int? TeamID { get; set; }
        public clsTeam Team;
        public int? UserID { get; set; }
        public clsUser User;
        public DateTime? JoinDate { get; set; }
        public DateTime? ExitDate { get; set; }


        private clsTeamMember(int? TeamMemberID, int? TeamID, int? UserID, DateTime? JoinDate, DateTime? ExitDate)
        {
            this.TeamMemberID = TeamMemberID;
            this.TeamID = TeamID;
            this.UserID = UserID;
            this.JoinDate = JoinDate;
            this.ExitDate = ExitDate;
            this.Team = clsTeam.Find(TeamID);
            this.User = clsUser.Find(UserID);

            Mode = enMode.Update;
        }
        public clsTeamMember()
        {
            this.TeamMemberID = null;
            this.TeamID = null;
            this.UserID = null;
            this.JoinDate = null;
            this.ExitDate = null;
            Mode = enMode.Add;
        }

        // CRUD Opperations
        private bool _AddTeamMember()
        {
            this.TeamMemberID = TeamMemberRepository.AddTeamMember(TeamID, UserID);
            return this.TeamMemberID != null;
        }
        private static bool _ReplaceMmeber(int? NewUserID, int? OldUserID)
        {
            return TeamMemberRepository.ReplaceMember(NewUserID, OldUserID);
        }


        public bool AddMember()
        {
            if (_AddTeamMember())
            {
                Mode = enMode.Update;
                return true;
            }
            else
                return false;
        }
        public bool ReplaceMember(int? OldUserID)
        {
            if (this.UserID != OldUserID)
                return _ReplaceMmeber(this.UserID, OldUserID);
            else
                return false;
        }

        static public bool Delete(int TeamMemberID)
        {
            return TeamMemberRepository.DeleteTeamMember(TeamMemberID);
        }
        static public bool ExitTeam(int TeamMemberID)
        {
            return TeamMemberRepository.ExitTeam(TeamMemberID);
        }

        static public clsTeamMember Find(int? TeamMemberID)
        {
            int? TeamID = null; int? UserID = null; DateTime? JoinDate = null; DateTime? ExitDate = null;

            if (TeamMemberRepository.GetTeamMemberByID(TeamMemberID, ref TeamID, ref UserID, ref JoinDate, ref ExitDate))
                return new clsTeamMember(TeamMemberID, TeamID, UserID, JoinDate, ExitDate);
            else
                return null;
        }
        static public DataTable GetAllMembers()
        {
            return TeamMemberRepository.GetAllTeamMembers();
        }

        // Team members business logic
        public static bool IsUserMember(int? UserID)
        {
            return TeamMemberRepository.IsUserMember(UserID);
        }

        private static short? _GetTeamRoleMemberCount(int? TeamID, short? RoleID)
        {
            return TeamMemberRepository.GetTeamRoleMemberCount(TeamID, RoleID);
        }
        public static bool IsRoleCapacityReached(int? TeamID, short? RoleID, short MaxRoleLimit = 1)
        {
            short? CurrentRoleMemberCount = _GetTeamRoleMemberCount(TeamID, RoleID);

            return CurrentRoleMemberCount == MaxRoleLimit;
        }

    }

}
