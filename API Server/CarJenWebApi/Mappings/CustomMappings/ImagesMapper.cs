using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.AppointmentDtos;
using CarJenShared.Dtos.ImageCollectionDto;
using CarJenShared.Dtos.ImageDtos;
using CarJenWebApi.Dtos.AppointmentDtos;
using CarJenWebApi.Dtos.ImageDtos;

namespace CarJenWebApi.Mappings.CustomMappings
{
    public class ImagesMapper
    {
        public static clsImageCollection ToclsImageCollection(CreateImagesDto dto)
        {
            return new clsImageCollection
            {
                CarID = dto.CarID,
                FrontView = new clsImage(dto.FrontView,clsImage.enViewType.FrontView),
                RearView = new clsImage(dto.RearView, clsImage.enViewType.RearView),
                SideView = new clsImage(dto.SideView, clsImage.enViewType.SideView),
                InteriorView = new clsImage(dto.InteriorView, clsImage.enViewType.InteriorView)
            };

        }
        public static ImagesResponseDto ToImagesResponseDto(ImagesDto dto)
        {
            return new ImagesResponseDto
            {
                ImageCollectionID = dto.ImageCollectionID,
                FrontView = dto.FrontView,
                RearView = dto.RearView,
                SideView = dto.SideView,
                InteriorView = dto.InteriorView
            };
        }
    }
}
