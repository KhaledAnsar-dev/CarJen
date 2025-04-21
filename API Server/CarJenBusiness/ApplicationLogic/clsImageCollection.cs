using CarJenData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsImageCollection
    {
        public int? imageCollectionID { get; set; }
        public int? carID { get; set; }
        public bool? status { get; set; }
        public DateTime? createdDate { get; set; }
        public string frontView { get; set; }
        public string rearView { get; set; }
        public string sideView { get; set; }
        public string interiorView { get; set; }
        private clsImageCollection(int? imageCollectionID, int? carID, bool? status, DateTime? createdDate)
        {
            this.imageCollectionID = imageCollectionID;
            this.carID = carID;
            this.status = status;
            this.createdDate = createdDate;
            frontView = clsImage.Find(imageCollectionID, (short)clsImage.enViewType.FrontView).imagePath;
            rearView = clsImage.Find(imageCollectionID, (short)clsImage.enViewType.RearView).imagePath;
            sideView = clsImage.Find(imageCollectionID, (short)clsImage.enViewType.SideView).imagePath;
            interiorView = clsImage.Find(imageCollectionID, (short)clsImage.enViewType.InteriorView).imagePath;

            mode = enMode.Update;
        }
        public clsImageCollection()
        {
            this.imageCollectionID = null;
            this.carID = null;
            this.status = null;
            this.createdDate = null;
            this.frontView = string.Empty;
            this.rearView = string.Empty;
            this.sideView = string.Empty;
            this.interiorView = string.Empty;
            mode = enMode.Add;
        }

        enum enMode { Add = 1, Update = 2 }
        enMode mode;

        private bool _Add()
        {
            this.imageCollectionID = ImageCollectionRepository.AddImageCollection(carID, frontView, rearView, sideView, interiorView);
            return this.imageCollectionID != null;
        }
        private bool _Update()
        {
            return ImageCollectionRepository.UpdateImageCollection(imageCollectionID, frontView, rearView, sideView, interiorView);
        }
        public bool Save()
        {
            switch (mode)
            {
                case enMode.Add:
                    if (_Add())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _Update();

            }
            return false;
        }

        static public clsImageCollection Find(int? imageCollection, bool? status)
        {
            int? carID = null; DateTime? createdDate = null;
            if (ImageCollectionRepository.GetImageCollectionByID(imageCollection, status, ref carID, ref createdDate))
            {
                return new clsImageCollection(imageCollection, carID, status, createdDate);
            }
            else
                return null;

        }
        static public clsImageCollection Find(int? CarID)
        {
            int? imageCollection = null; bool? status = null; DateTime? createdDate = null;
            if (ImageCollectionRepository.GetImageCollectionByCarID(CarID, ref imageCollection, ref status, ref createdDate))
            {
                return new clsImageCollection(imageCollection, CarID, status, createdDate);
            }
            else
                return null;

        }

    }

}
