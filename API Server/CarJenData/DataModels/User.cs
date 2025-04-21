using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class User
{
    public int UserId { get; set; }

    public int PersonId { get; set; }

    public int RoleId { get; set; }

    public string NationalNumber { get; set; } = null!;

    public int EvaluationScore { get; set; }

    public int? CreatedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual ICollection<User> InverseCreatedByUser { get; set; } = new List<User>();

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    public virtual Person Person { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
