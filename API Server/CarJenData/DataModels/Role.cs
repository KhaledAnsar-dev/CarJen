using System;
using System.Collections.Generic;

namespace CarJenData.DataModels;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleTitle { get; set; } = null!;

    public decimal Salary { get; set; }

    public int Permission { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
