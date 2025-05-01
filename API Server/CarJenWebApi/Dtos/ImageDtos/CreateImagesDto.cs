namespace CarJenWebApi.Dtos.ImageDtos
{
    using System.ComponentModel.DataAnnotations;

    public class CreateImagesDto
    {
        [Required(ErrorMessage = "CarID is required.")]
        public int? CarID { get; set; }

        [Required(ErrorMessage = "Front view image path is required.")]
        [MaxLength(300, ErrorMessage = "Front view path cannot exceed 300 characters.")]
        public string FrontView { get; set; }

        [Required(ErrorMessage = "Rear view image path is required.")]
        [MaxLength(300, ErrorMessage = "Rear view path cannot exceed 300 characters.")]
        public string RearView { get; set; }

        [Required(ErrorMessage = "Side view image path is required.")]
        [MaxLength(300, ErrorMessage = "Side view path cannot exceed 300 characters.")]
        public string SideView { get; set; }

        [Required(ErrorMessage = "Interior view image path is required.")]
        [MaxLength(300, ErrorMessage = "Interior view path cannot exceed 300 characters.")]
        public string InteriorView { get; set; }
    }

}
