using CarJenData.DataModels;
using CarJenData.Repositories;
using CarJenShared.Dtos.TeamDtos;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsTeam
    {
        enum enMode { Add = 0, Update = 1 };
        public enum enTeamType { InspectTeam = 1, TechInspect = 2 }
        enMode Mode;

        public int? TeamID { get; set; }
        public string TeamCode { get; set; }
        public short? TeamType { get; set; }
        public string TeamTypeText
        {
            get
            {
                return this.TeamType == 1 ? "Initial Inspection Team" : "Technical Inspection Team"; ;
            }
        }

        public int? CreatedByUserID { get; set; }
        public clsUser CreatedByUser;
        public DateTime? CreatedDate { get; set; }
        public TeamDto toTeamDto
        {
            get
            {
                return new TeamDto {
                    TeamID = this.TeamID?? 0 ,
                    TeamType = this.TeamType ,
                    TeamCode = this.TeamCode ,
                    CreatedByUserID = this.CreatedByUserID ,
                    CreatedDate = this.CreatedDate
                };
            }
        }

        public static short MaxInspectTeamMembers => 2;
        public static short MaxTechnicalTeamMembers => 5;


        private clsTeam(TeamDto teamDto)
        {
            this.TeamID = teamDto.TeamID;
            this.TeamCode = teamDto.TeamCode;
            this.TeamType = teamDto.TeamType;
            this.CreatedDate = teamDto.CreatedDate;
            this.CreatedByUserID = teamDto.CreatedByUserID;
            this.CreatedByUser = clsUser.Find(teamDto.CreatedByUserID);

            Mode = enMode.Update;
        }
        public clsTeam()
        {
            this.TeamID = null;
            this.TeamCode = "";
            this.TeamType = null;
            this.CreatedDate = null;
            this.CreatedByUserID = null;
            Mode = enMode.Add;
        }

        // CRUD Opperations
        private bool _AddTeam()
        {
            this.TeamID = TeamRepository.AddTeam(this.toTeamDto);
            return this.TeamID != null;
        }
        private bool _UpdateTeam()
        {
            return TeamRepository.UpdateTeam(this.toTeamDto);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_AddTeam())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTeam();

            }
            return false;
        }
        static public bool Delete(int teamID)
        {
            return TeamRepository.DeleteTeam(teamID);
        }
        static public clsTeam? Find(int? teamID)
        {
            var teamDto = TeamRepository.GetTeamByID(teamID);
            if (teamDto == null)
                return null;

            return new clsTeam(teamDto);
        }
        static public clsTeam? Find(string teamCode)
        {
            var teamDto = TeamRepository.GetTeamByCode(teamCode);
            if (teamDto == null)
                return null;

            return new clsTeam(teamDto);
        }
        static public clsTeam? FindTeamByUserID(int? userID)
        {
            var teamDto = TeamRepository.GetTeamByUserID(userID);
            if (teamDto == null)
                return null;

            return new clsTeam(teamDto);
        }

        static public List<TeamDto> GetAllTeams()
        {
            return TeamRepository.GetAllTeams();
        }
        public static bool IsTeamExist(string TeamCode)
        {
            return TeamRepository.IsTeamExist(TeamCode);
        }


        // Team business logic
        private static short? _GetTeamMemberCount(int? TeamID, short? TeamType)
        {
            return TeamRepository.GetTeamMemberCount(TeamID, TeamType);
        }
        public bool IsTeamComplete()
        {
            short? CurrentMembers = _GetTeamMemberCount(TeamID, (short)TeamType);

            if (CurrentMembers.HasValue)
            {
                short MaxTeamMember = TeamType == (short?)enTeamType.InspectTeam ? MaxInspectTeamMembers : MaxTechnicalTeamMembers;

                return CurrentMembers == MaxTeamMember;
            }
            return false;
        }
        public static bool LogInitialTeamUpdates(int CarDocumentationID, int TeamID)
        {
            return TeamRepository.LogInitialTeamUpdates(CarDocumentationID, TeamID);
        }
    }

}
