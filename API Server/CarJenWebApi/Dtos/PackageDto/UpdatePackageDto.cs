using System.ComponentModel.DataAnnotations;

namespace CarJenWebApi.Dtos.PackageDto
{
    public class UpdatePackageDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "NumberOfReports is required.")]
        [Range(1,100)]
        public int? NumberOfReports { get; set; }
    }
}
