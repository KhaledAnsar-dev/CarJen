using CarJenBusiness.ApplicationLogic;

namespace CarJenWebApi.Dtos.ImageDtos
{
    public class ImagesResponseDto
    {
        public int? ImageCollectionID { get; set; }
        public string FrontView { get; set; }
        public string RearView { get; set; }
        public string SideView { get; set; }
        public string InteriorView { get; set; }
    }
}
