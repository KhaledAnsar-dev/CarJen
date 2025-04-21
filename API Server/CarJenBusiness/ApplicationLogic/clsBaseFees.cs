using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public abstract class clsBaseFees
    {
        public int? FeeID { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Updates the fee price and marks the previous fee record as canceled.
        /// </summary>
        /// <returns>
        /// A boolean indicating whether the operation was successfully completed.
        /// </returns>
        public abstract bool Renew();


        /// <summary>
        /// Deletes an unused fee record and updates the status of the previous one.
        /// </summary>
        /// <param name="RecordID">
        /// The ID of the fee record to delete. It must not have been used in the system yet.
        /// </param>
        /// <returns>
        /// A boolean indicating whether the operation was successfully completed.
        /// </returns>
        public abstract bool Delete(int RecordID);
    }

}
