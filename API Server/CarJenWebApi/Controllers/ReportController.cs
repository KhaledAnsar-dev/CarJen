using CarJenBusiness.ApplicationLogic;
using CarJenShared.Dtos.ReportDtos;
using CarJenWebApi.Dtos.ReportDtos;
using CarJenWebApi.Dtos.TeamDtos;
using CarJenWebApi.Mappings.CustomMappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarJenWebApi.Controllers
{
    [Route("api/Report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpPost("Create", Name = "CreateReport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<int> CreateReport(CreateReportDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var report = ReportMapper.ToclsReport(dto);
            if (report.AddReport())
                return Ok(report.ReportID);

            return BadRequest("Failed to create report.");
        }

        [HttpPut("UpdateStatus", Name = "UpdateReportStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateStatus( UpdateReportDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (clsReport.UpdateStatus(dto.ReportID, dto.Status))
                return Ok("Report status updated successfully.");

            return BadRequest("Failed to update report status.");
        }

        [HttpGet("All", Name = "GetAllReports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<ReportResponseDto>> GetAllReports()
        {
            var reports = clsReport.GetAllReports();

            if (reports == null)
                return NotFound("No generated reports found");

            var response = reports.Select(ReportMapper.ToReportResponseDto).ToList();
            return Ok(response);
        }

        [HttpGet("AllApproved", Name = "GetAllApprovedReports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<FinalReportDto>> GetAllApprovedReports()
        {
            var reports = clsReport.GetAllApprovedReports();

            if (reports == null)
                return NotFound("No published reports found");

            return Ok(reports);
        }
    }
}
