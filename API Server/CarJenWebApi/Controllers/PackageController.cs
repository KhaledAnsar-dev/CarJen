using CarJenBusiness.ApplicationLogic;
using CarJenWebApi.Dtos.PackageDto;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllPackages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PackageResponseDto>> GetAllPackages()
        {
            var packages = clsPackage.GetAllPackages()
                .Select(PackageMapper.ToPackageResponseDto)
                .ToList();

            if (!packages.Any())
                return NotFound("No packages found.");

            return Ok(packages);
        }

        [HttpGet("{packageID:int}", Name = "GetPackageByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PackageResponseDto> GetPackageByID(int packageID)
        {
            if (packageID < 1)
                return BadRequest("Invalid package ID.");

            var package = clsPackage.Find(packageID);

            if (package == null)
                return NotFound($"Package with ID {packageID} not found.");

            var response = PackageMapper.ToPackageResponseDto(package.ToPackageDto);

            return Ok(response);
        }

        [HttpPost("Add", Name = "AddPackage")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PackageResponseDto> AddPackage( CreatePackageDto newPackage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var package = PackageMapper.ToclsPackage(newPackage);

            if (package.Save())
            {
                var response = PackageMapper.ToPackageResponseDto(package.ToPackageDto);
                return CreatedAtRoute("GetPackageByID", new { packageID = response.PackageID }, response);
            }

            return BadRequest("Failed to add package.");
        }

        [HttpPut("Update/{packageID:int}", Name = "UpdatePackage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PackageResponseDto> UpdatePackage(int packageID,UpdatePackageDto updatedPackage)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingPackage = clsPackage.Find(packageID);

            if (existingPackage == null)
                return NotFound($"Package with ID {packageID} does not exist.");

            existingPackage.Title = updatedPackage.Title;
            existingPackage.NumberOfReports = updatedPackage.NumberOfReports;

            if (existingPackage.Save())
            {
                var response = PackageMapper.ToPackageResponseDto(existingPackage.ToPackageDto);
                return Ok(response);
            }

            return BadRequest("Failed to update package.");
        }

        [HttpGet("Exists/{title}", Name = "IsPackageExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsPackageExist(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title cannot be empty.");

            var exists = clsPackage.IsPackageExist(title);
            return Ok(exists);
        }
    }
}
