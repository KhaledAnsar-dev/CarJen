using CarJenData.Repositories;
using CarJenShared.Dtos.ImageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsImage
    {
        public int? ImageID { get; set; }
        public string ImagePath { get; set; }
        public short? ViewType { get; set; }

        public enum enViewType
        {
            FrontView = 1,
            RearView = 2,
            SideView = 3,
            InteriorView = 4
        }

        private clsImage(ImageDto imageDto)
        {
            this.ImageID = imageDto.ImageID;
            this.ImagePath = imageDto.ImagePath;
            this.ViewType = imageDto.ViewType;
        }
        public clsImage(string image , enViewType view)
        {
            ImagePath = image;
            ViewType = (short)view;
        }
        static public clsImage? Find(int? imageCollectionID, short viewType)
        {
            var imageDto = ImageRepository.GetImageByCollectionIDAndView(imageCollectionID, viewType);

            if (imageDto != null)
                return new clsImage(imageDto);
            else
                return null;
        }
    }

}
