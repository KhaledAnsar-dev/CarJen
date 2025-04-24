using System;
using System.ComponentModel.DataAnnotations;

public class MemberResponseDto
{
    [Required]
    public int MemberID { get; set; }

    [Required]
    [Display(Name = "Member Name")]
    [StringLength(100, ErrorMessage = "Member name can't be longer than 100 characters.")]
    public string MemberName { get; set; }

    [Required]
    [Display(Name = "Team ID")]
    public int TeamID { get; set; }

    [Required]
    [Display(Name = "Team Type")]
    [StringLength(50, ErrorMessage = "Team type can't be longer than 50 characters.")]
    public string TeamType { get; set; }

    [Required]
    [Display(Name = "Role Title")]
    [StringLength(50, ErrorMessage = "Role title can't be longer than 50 characters.")]
    public string RoleTitle { get; set; }

    [Required]
    [Display(Name = "Join Date")]
    [DataType(DataType.Date)]
    public DateTime JoinDate { get; set; }

    [Display(Name = "Exit Time")]
    [DataType(DataType.Date)]
    public DateTime ExitDate { get; set; }
}

