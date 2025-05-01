using CarJenData.DataModels;
using CarJenShared.Dtos.ImageDtos;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static CarJenShared.Helpers.Logger;

namespace CarJenData.Repositories
{
    public class ImageRepository
    {
        public static ImageDto? GetImageByCollectionIDAndView(int? imageCollectionID, short? viewType)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.Images
                        .Where(img => img.ImageCollectionId == imageCollectionID && img.ViewType == viewType)
                        .Select(img => new ImageDto
                        {
                            ImageID = img.ImageId,
                            ImagePath = img.ImagePath,
                            ViewType = img.ViewType
                        })
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetImageByCollectionIDAndView));
                return null;
            }
        }
    }

}
