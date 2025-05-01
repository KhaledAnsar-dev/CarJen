using CarJenData.DataModels;
using CarJenData.Repositories;
using CarJenShared.Dtos.ImageCollectionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace CarJenBusiness.ApplicationLogic
{
    public class clsImageCollection
    {
        enum enMode { Add = 1, Update = 2 }
        private enMode mode;

        public int? ImageCollectionID { get; set; }
        public int? CarID { get; set; }
        public clsImage FrontView { get; set; }
        public clsImage RearView { get; set; }
        public clsImage SideView { get; set; }
        public clsImage InteriorView { get; set; }
        public ImagesDto ToImagesDto
        {
            get
            {
                return new ImagesDto
                {
                    ImageCollectionID = ImageCollectionID ?? 0,
                    CarID = CarID ?? 0,
                    FrontView = this.FrontView.ImagePath,
                    RearView = this.RearView.ImagePath,
                    SideView = this.SideView.ImagePath,
                    InteriorView = this.InteriorView.ImagePath,
                };
            }
        }
        public clsImageCollection()
        {
            this.ImageCollectionID = null;
            this.CarID = null;
            mode = enMode.Add;
        }
        
        private bool _Add()
        {
            this.ImageCollectionID = ImageCollectionRepository.AddImageCollection(this.ToImagesDto);
            return this.ImageCollectionID != null;
        }
        private bool _Update()
        {
            return ImageCollectionRepository.UpdateImageCollection(ToImagesDto);
        }
        public bool Save()
        {
            bool success = mode == enMode.Add ? _Add() : _Update();
            if (success && mode == enMode.Add) mode = enMode.Update;
            return success;
        }
        static public clsImageCollection? Find(int? imageCollectionID)
        {
            return new clsImageCollection
            {
                ImageCollectionID = imageCollectionID,
                CarID = ImageCollectionRepository.GetAssociatedCarID(imageCollectionID),
                FrontView = clsImage.Find(imageCollectionID, (short)clsImage.enViewType.FrontView),
                RearView = clsImage.Find(imageCollectionID, (short)clsImage.enViewType.RearView),
                SideView = clsImage.Find(imageCollectionID, (short)clsImage.enViewType.SideView),
                InteriorView = clsImage.Find(imageCollectionID, (short)clsImage.enViewType.InteriorView),

                mode = enMode.Update
            };
        }
    }
}
