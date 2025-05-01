using CarJenData.DataModels;
using CarJenShared.Dtos.ImageCollectionDto;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using static CarJenShared.Helpers.Logger;
namespace CarJenData.Repositories
{
    public class ImageCollectionRepository
    {
        public static int? GetAssociatedCarID(int? imageCollectionID)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    return context.ImageCollections
                        .Where(ic => ic.ImageCollectionId == imageCollectionID)
                        .Select(ic => ic.CarId)
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(GetAssociatedCarID));
                return null;
            }
        }
        public static int? AddImageCollection(ImagesDto images)
        {
            int? CreatedID = null;

            try
            {
                using (SqlConnection Connection = new SqlConnection(clsDataSettings.ConnectionString))
                using (SqlCommand Command = new SqlCommand("SP_AddImgCollection", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    Command.Parameters.AddWithValue("@carID", images.CarID);
                    Command.Parameters.AddWithValue("@createdDate", DateTime.Now);
                    Command.Parameters.AddWithValue("@frontImage", images.FrontView);
                    Command.Parameters.AddWithValue("@rearImage", images.RearView);
                    Command.Parameters.AddWithValue("@sideImage", images.SideView);
                    Command.Parameters.AddWithValue("@interiorImage", images.InteriorView);

                    SqlParameter OutputID = new SqlParameter("@imageCollectionID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutputID);

                    Connection.Open();
                    Command.ExecuteNonQuery();

                    CreatedID = (int)Command.Parameters["@imageCollectionID"].Value;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddImageCollection));
                return null;
            }

            return CreatedID;
        }
        public static bool UpdateImageCollection(ImagesDto dto)
        {
            try
            {
                using (var context = new CarJenDbContext())
                {
                    var images = context.Images
                        .Where(i => i.ImageCollectionId == dto.ImageCollectionID).ToList();

                    if (images == null)
                        return false;

                    foreach(var image in images)
                    {
                        switch (image.ViewType)
                        {
                            case 1: image.ImagePath = dto.FrontView; break;
                            case 2: image.ImagePath = dto.RearView; break;
                            case 3: image.ImagePath = dto.SideView; break;
                            case 4: image.ImagePath = dto.InteriorView; break;
                        }
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdateImageCollection));
                return false;
            }
        }
    }
}
