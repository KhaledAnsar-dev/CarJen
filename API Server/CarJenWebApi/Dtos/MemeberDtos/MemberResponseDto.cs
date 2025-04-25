using System;
using System.ComponentModel.DataAnnotations;

public class MemberResponseDto
{
    public int MemberID { get; set; }
    public string MemberName { get; set; }
    public int TeamID { get; set; }
    public string TeamType { get; set; }
    public string RoleTitle { get; set; }
    public DateTime JoinDate { get; set; }
    public DateTime ExitDate { get; set; }
}

