using CarJenData.DataModels;
using CarJenData.Repositories;
using CarJenShared.Dtos.MemberDtos;
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

        public MemberDto ToMemberDto
        {
            get
            {
                return new MemberDto
                {
                    TeamMemberId = (int)this.TeamMemberID,
                    JoinDate = Convert.ToDateTime(this.JoinDate),
                    ExitDate = this.ExitDate,
                    Team = this.Team.toTeamDto,
                    User = this.User.toUserDto
                };
            }
        }

        private clsTeamMember(MemberDto memberDto)
        {
            this.TeamMemberID = memberDto.TeamMemberId;
            this.TeamID = memberDto.Team.TeamID;
            this.UserID = memberDto.User.UserId;
            this.JoinDate = memberDto.JoinDate;
            this.ExitDate = memberDto.ExitDate;
            this.Team = clsTeam.Find(memberDto.Team.TeamID);
            this.User = clsUser.Find(memberDto.User.UserId);

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
        private static bool? _ReplaceMmeber(int? NewUserID, int? OldUserID)
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
        public bool? ReplaceMember(int? newUserID)
        {
            //this.UserID define the current user as team memeber
            if (this.UserID != newUserID)
                return _ReplaceMmeber(newUserID, this.UserID);
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

        static public clsTeamMember? Find(int? TeamMemberID)
        {

            var memberDto = TeamMemberRepository.GetTeamMemberByID(TeamMemberID);
            
            if(memberDto == null)
                return null;

            return new clsTeamMember(memberDto);

        }
        static public List<MemberDto> GetAllMembers()
        {
            return TeamMemberRepository.GetAllTeamMembers();
        }

        // Team members business logic
        public static bool? IsUserMember(int? UserID)
        {
            return TeamMemberRepository.IsUserMember(UserID);
        }
        private static int? _GetTeamRoleMemberCount(int? TeamID, short? RoleID)
        {
            return TeamMemberRepository.GetTeamRoleMemberCount(TeamID, RoleID);
        }
        public static bool? IsRoleCapacityReached(int? TeamID, short? RoleID, short MaxRoleLimit = 1)
        {

         
            int? CurrentRoleMemberCount = _GetTeamRoleMemberCount(TeamID, RoleID);

            // This code will changed later
            short maxRoleLimit = 1;
            if (RoleID == 5)
                maxRoleLimit = 2;


            if (CurrentRoleMemberCount.HasValue)
                return CurrentRoleMemberCount == maxRoleLimit;

            return null;
        }

    }

}
