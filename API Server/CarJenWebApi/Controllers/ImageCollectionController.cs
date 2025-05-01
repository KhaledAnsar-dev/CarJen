using CarJenBusiness.ApplicationLogic;
using CarJenWebApi.Dtos.ImageDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/ImageCollection")]
    [ApiController]
    public class ImageCollectionController : ControllerBase
    {
        [HttpPost("Create", Name = "CreateImageCollection")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> CreateImageCollection(CreateImagesDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var imageCollection = ImagesMapper.ToclsImageCollection(dto);

            if (imageCollection.Save())
                return CreatedAtRoute("GetImageCollection", new { id = imageCollection.ImageCollectionID }, imageCollection.ImageCollectionID);

            return BadRequest("Failed to create image collection.");
        }

        [HttpPut("Update", Name = "UpdateImageCollection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ImagesResponseDto> UpdateImageCollection(int imageCollectionID ,UpdateImagesDto dto)
        {
            var collection = clsImageCollection.Find(imageCollectionID);


            if (collection == null)
                return NotFound("No images found");

            collection.FrontView.ImagePath = dto.FrontView;
            collection.RearView.ImagePath = dto.RearView;
            collection.SideView.ImagePath = dto.SideView;
            collection.InteriorView.ImagePath = dto.InteriorView;

            collection.Save();

            var response = ImagesMapper.ToImagesResponseDto(collection.ToImagesDto);

            return Ok(response);
        }

        [HttpGet("{id:int}", Name = "GetImageCollection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ImagesResponseDto> GetImageCollection(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID.");

            var collection = clsImageCollection.Find(id);

            if (collection == null)
                return NotFound("Image collection not found.");

            var response = ImagesMapper.ToImagesResponseDto(collection.ToImagesDto);
            return Ok(response);
        }
    }
}

