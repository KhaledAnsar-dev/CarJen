using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.PackageDto
{
    public class CreatePackageDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "NumberOfReports is required.")]
        [Range(1,100)]
        public int? NumberOfReports { get; set; }
        [Required(ErrorMessage = "CreatedByUserID is required.")]
        public int? CreatedByUserID { get; set; }
    }
}
