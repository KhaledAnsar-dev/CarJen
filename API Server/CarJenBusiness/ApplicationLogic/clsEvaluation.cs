using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsEvaluation
    {
        public int? UserID { get; }

        private int _Score;
        public int Score
        {
            get
            {
                return _Score;
            }
            set
            {
                _Score = value > 0 ? value : 0;
            }
        }



        public void AddPresencePoints()
        {
            this.Score += 5;
        }
        public void AddPunctualityPoints()
        {
            this.Score += 5;
        }
        public void AddTeamworkPoints()
        {
            this.Score += 5;
        }
        public void AddEndorsementPoints()
        {
            this.Score += 35;
        }
        public bool SaveScore()
        {
            return EvaluationRepository.UpdateScore(this.UserID, this._Score);
        }

        static public bool ResetCurrent(int? UserID)
        {
            return EvaluationRepository.ResetCurrent(UserID);
        }
        static public bool ResetAll()
        {
            return EvaluationRepository.ResetAll();

        }

        public clsEvaluation(int? UserID)
        {
            this.Score = clsUser.Find(UserID).EvaluationScore;
            this.UserID = UserID;
        }

    }

}
