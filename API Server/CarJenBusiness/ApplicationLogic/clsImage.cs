using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsImage
    {
        public int? imageID { get; set; }
        public int? imageCollectionID { get; set; }
        public string imagePath { get; set; }
        public short? viewType { get; set; }

        public enum enViewType
        {
            FrontView = 1,
            RearView = 2,
            SideView = 3,
            InteriorView = 4
        }

        private clsImage(int? imageID, int? imageCollection, string imagePath, short? viewType)
        {
            this.imageID = imageID;
            this.imageCollectionID = imageCollection;
            this.imagePath = imagePath;
            this.viewType = viewType;
        }
        public clsImage()
        {
            this.imageID = null;
            this.imageCollectionID = null;
            this.imagePath = "";
            this.viewType = null;
        }


        public bool Add()
        {
            this.imageID = ImageRepository.AddImage(imageCollectionID, imagePath, viewType);
            return this.imageID != null;
        }

        static public clsImage Find(int? imageCollectionID, short viewType)
        {
            int? imageID = null;
            string imagePath = "";
            if (ImageRepository.GetImageByCollectionIDAndView(imageCollectionID, viewType, ref imageID, ref imagePath))
            {
                return new clsImage(imageID, imageCollectionID, imagePath, viewType);
            }
            else
                return null;

        }

    }

}
