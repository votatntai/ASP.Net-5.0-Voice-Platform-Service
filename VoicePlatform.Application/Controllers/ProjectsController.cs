using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoicePlatform.Application.Configurations.Middleware;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Helpers;
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Application.Controllers
{
    [ApiController]
    [Route("api/v1/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ICustomerService _customerService;
        private readonly IArtistService _artistService;
        private readonly ISystemService _systemService;
        private readonly Validation validation = new Validation();
        private SendingNotification pushNotififcation = new SendingNotification();

        public ProjectsController(IProjectService projectService, ICustomerService customerService,
            IArtistService artistService, ISystemService systemService)
        {
            _projectService = projectService;
            _customerService = customerService;
            _artistService = artistService;
            _systemService = systemService;
        }

        /// <summary>
        /// Get owner project - Customer, Artist
        /// </summary>
        [Authorize("Customer", "Artist")]
        [HttpGet]
        [Route("own")]
        [ProducesResponseType(typeof(List<QuickProjectResponse>), 200)]
        public async Task<IActionResult> GetOwnProject([FromQuery] Pagination pagination,
            [FromQuery] List<string> filter, bool? isAsc, string searchString, bool? isProcess)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            var isCustomer = HttpContext.Items["Role"].ToString().Equals("Customer") ? true : false;
            var data = await _projectService.GetOwnProject(userId, pagination, isCustomer, filter, isAsc, searchString, isProcess);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Get user project - Admin
        /// </summary>
        [Authorize("Admin")]
        [HttpGet]
        [Route("user/{id}")]
        [ProducesResponseType(typeof(List<QuickProjectResponse>), 200)]
        public async Task<IActionResult> GetUserProject([Required] Guid id, [Required] bool isCustomer, [FromQuery] Pagination pagination,
            [FromQuery] List<string> filter, bool? isAsc, string searchString)
        {
            var data = await _projectService.GetUserProject(id, pagination, isCustomer, filter, isAsc, searchString);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }

        /// <summary>
        /// Get pending project - Artist
        /// </summary>
        [Authorize("Artist")]
        [HttpGet]
        [Route("pending")]
        [ProducesResponseType(typeof(List<QuickProjectResponse>), 200)]
        public async Task<IActionResult> GetPendingProject([FromQuery] Pagination pagination, [FromQuery] ArtistSearchProject search)
        {
            var listProject = await _projectService.GetPendingProject(pagination, search.Filter, search.IsAsc, search.SearchString);
            if (listProject != null)
            {
                return Ok(listProject);
            }
            return NotFound();
        }

        /// <summary>
        /// Get pending project - Admin
        /// </summary>
        [Authorize("Admin")]
        [HttpGet]
        [Route("waiting")]
        [ProducesResponseType(typeof(List<QuickProjectResponse>), 200)]
        public async Task<IActionResult> GetWaitingProject([FromQuery] Pagination pagination, [FromQuery] AdminSearchProject search)
        {
            var listProject = await _projectService.GetWaitingProject(pagination, search.Filter, search.Sort, search.SearchString);
            if (listProject != null)
            {
                return Ok(listProject);
            }
            return NotFound();
        }

        /// <summary>
        /// Get all project  - Admin
        /// </summary>
        [Authorize("Admin")]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<AdminProjectResponse>), 200)]
        public async Task<IActionResult> GetAllProject([FromQuery] Pagination pagination, [FromQuery] AdminSearchProject search)
        {
            var listProject = await _projectService.GetProject(pagination, search.Filter, search.Sort, search.SearchString);
            if (listProject != null)
            {
                return Ok(listProject);
            }
            return BadRequest();
        }

        /// <summary>
        /// Get project by id - Customer, Artist, Admin
        /// </summary>
        [Authorize("Customer", "Artist", "Admin")]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProjectResponse), 200)]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var result = await _projectService.GetProjectById(id);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get artist in project - Admin
        /// </summary>
        [Authorize("Admin")]
        [HttpGet]
        [Route("{id}/artists")]
        [ProducesResponseType(typeof(List<QuickArtistProjectResponse>), 200)]
        public async Task<IActionResult> GetArtistInProject([FromQuery] Pagination pagination, Guid id, string status)
        {
            var artistProject = await _projectService.GetArtistInProject(id, status, pagination);
            if (artistProject != null)
            {
                return Ok(artistProject);
            }
            return NotFound();
        }

        /// <summary>
        /// Get project by artist - Admin
        /// </summary>
        [Authorize("Admin")]
        [HttpGet]
        [Route("artists/{id}")]
        [ProducesResponseType(typeof(List<QuickProjectArtistResponse>), 200)]
        public async Task<IActionResult> GetProjectByArtist([FromQuery] Pagination pagination, Guid id, string status)
        {
            var artistProject = await _projectService.GetProjectByArtist(id, pagination, status);
            if (artistProject != null)
            {
                return Ok(artistProject);
            }
            return NotFound();
        }

        /// <summary>
        /// Create project - Customer
        /// </summary>
        [Authorize("Customer")]
        [HttpPost]
        [ProducesResponseType(typeof(QuickProjectResponse), 201)]
        [Route("")]
        public async Task<IActionResult> CreateProject([FromBody] ProjectRequest project)
        {
            //Get Customer ID from Http Context
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            var errors = validation.ValidateProject(project, "CreateProject");
            if (errors.Any())
            {
                return BadRequest(errors);
            }
            var result = await _projectService.CreateProject(userId, project);
            if (result is null)
            {
                BadRequest();
            }
            var poster = await _customerService.GetCustomerById(userId);
            await pushNotififcation.PushToAdmin(poster.FirstName + " đã tạo dự án mới",
                poster.FirstName + " đã tạo một dự án mới và đợi bạn duyệt!!!");
            return Created("", result);
        }

        /// <summary>
        /// Update project - Customer
        /// </summary>
        [Authorize("Customer")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditProject(Guid id, [Required] ProjectRequest project)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            var errors = validation.ValidateProject(project, "CreateProject");
            if (errors.Any())
            {
                return BadRequest(errors);
            }
            var result = await _projectService.UpdateProject(project, id, userId);
            if (result is null)
            {
                BadRequest();
            }
            var poster = await _customerService.GetCustomerById(userId);
            await pushNotififcation.PushToAdmin(poster.FirstName + " đã cập nhật dự án của họ",
                poster.FirstName + " đã cập nhật một dự án và đợi bạn duyệt!!!");
            return Created("", result);
        }

        /// <summary>
        /// Update status project - Customer
        /// </summary>
        /// <remarks>
        /// 0 = Waiting,
        /// 1 = Pending,
        /// 2 = Process,
        /// 3 = Done,
        /// 4 = Delete,
        /// 5 = Deny
        /// </remarks>
        [Authorize("Customer")]
        [HttpPut]
        [Route("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [Required] ProjectStatus status)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            bool result = await _projectService.ChangeStatusProject(id, userId, status);
            if (result == false)
            {
                return BadRequest();
            }
            var artists = await _projectService.GetArtistInProject(id, null, new Pagination() { PageSize = 9999 });
            if (artists.TotalRow != 0)
            {
                var artistsData = (List<QuickArtistProjectResponse>)artists.Data;
                var artistsId = artistsData.Select(x => x.ArtistId);
                var tokens = new List<string>();
                foreach (var artistId in artistsId)
                {
                    var token = await _systemService.GetUserToken(artistId, "Artist");
                    if (token != null)
                    {
                        token.ForEach(x => tokens.Add(x));
                    }
                }
                if (tokens.Any())
                {
                    string statusVietsub = "Đang thuê";
                    switch (status)
                    {
                        case ProjectStatus.Process:
                            statusVietsub = "Tiến hành";
                            break;
                        case ProjectStatus.Done:
                            statusVietsub = "Hoàn thành";
                            break;
                        case ProjectStatus.Delete:
                            statusVietsub = "Đã xoá";
                            break;
                        default:
                            break;
                    }
                    var projectName = await _projectService.GetProjectName(id);
                    await pushNotififcation.Push(tokens, projectName,
                        "Dự án " + projectName + " vừa chuyển trạng thái sang " + statusVietsub);
                }
            }
            return Ok();
        }

        /// <summary>
        /// Admin accpet project when create or update project - Admin
        /// </summary>
        [Authorize("Admin")]
        [HttpPut]
        [Route("{id}/check")]
        public async Task<IActionResult> AdminAcceptProject(Guid id, [Required] bool isAccept)
        {
            bool result = await _projectService.AdminCheckProject(id, isAccept);
            if (result == false)
            {
                return NotFound();
            }
            var poster = await _projectService.GetProjectPoster(id) ?? Guid.Empty;
            if (poster != Guid.Empty)
            {
                var token = await _systemService.GetUserToken(poster, "Customer");
                if (token.Any())
                {
                    var projectName = await _projectService.GetProjectName(id);
                    string statusVietsub = isAccept ? " đã được duyệt" : " đã bị từ chối";
                    await pushNotififcation.Push(token, projectName,
                        projectName + statusVietsub);
                }
            }
            return Ok();
        }

        /// <summary>
        /// Customer invite Artist to project - Customer
        /// </summary>
        [Authorize("Customer")]
        [HttpPut]
        [Route("{id}/invite")]
        public async Task<IActionResult> InviteArtist(Guid id, [Required] Guid artistId)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            bool result = await _projectService.InviteArtist(id, userId, artistId);
            if (result == false)
            {
                return BadRequest();

            }
            var token = await _systemService.GetUserToken(artistId, "Artist");
            if (token.Any())
            {
                var projectName = await _projectService.GetProjectName(id);
                await pushNotififcation.Push(token, "Bạn có lời mời mới",
                    "Bạn được mời vào dự án " + projectName);
            }
            return Ok();
        }

        /// <summary>
        /// Artist request to join project - Artist
        /// </summary>
        [Authorize("Artist")]
        [HttpPut]
        [Route("{id}/request")]
        public async Task<IActionResult> ArtistRequest(Guid id)
        {
            var artistId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            bool result = await _projectService.ArtistRequest(id, artistId);
            if (result == false)
            {
                return BadRequest();

            }
            var poster = await _projectService.GetProjectPoster(id) ?? Guid.Empty;
            if (poster != Guid.Empty)
            {
                var token = await _systemService.GetUserToken(poster, "Customer");
                if (token.Any())
                {
                    var projectName = await _projectService.GetProjectName(id);
                    var artistName = await _artistService.GetArtistName(artistId);
                    await pushNotififcation.Push(token, artistName + " đang yêu cầu tham gia",
                        artistName + " đang yêu cầu tham gia vào dự án " + projectName + " của bạn");
                }
            }
            return Ok();
        }

        [Authorize("Customer")]
        [HttpPost]
        [Route("file/status")]
        public async Task<IActionResult> ChangeFileStatus(ModifyFileRequest file)
        {
            return await _projectService.ChangeFileStatus(file) ? Ok() : BadRequest();
        }

        /// <summary>
        /// Accept Artist to join project - Customer
        /// </summary>
        [Authorize("Customer")]
        [HttpPut]
        [Route("{id}/response")]
        public async Task<IActionResult> ResponseArtist(Guid id, [Required] Guid artistId, [Required] bool isAccept)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            bool result = await _projectService.ResponseArtist(id, userId, artistId, isAccept);
            if (result == false)
            {
                return BadRequest();
            }
            if (isAccept)
            {
                var token = await _systemService.GetUserToken(artistId, "Artist");
                if (token.Any())
                {
                    var projectName = await _projectService.GetProjectName(id);
                    await pushNotififcation.Push(token, "Bạn được chấp nhận vào dự án",
                        "Bạn đã được cấp nhận tham gia vào dự án " + projectName);
                }
            }
            return Ok();
        }

        /// <summary>
        /// Artist accept to join Project - Artist
        /// </summary>
        [Authorize("Artist")]
        [HttpPut]
        [Route("{id}/reply")]
        public async Task<IActionResult> ArtistReply(Guid id, [Required] bool isAccept)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            bool result = await _projectService.ArtistReply(id, userId, isAccept);
            if (result == false)
            {
                return BadRequest();

            }
            if (isAccept)
            {
                var poster = await _projectService.GetProjectPoster(id) ?? Guid.Empty;
                if (poster != Guid.Empty)
                {
                    var token = await _systemService.GetUserToken(poster, "Customer");
                    if (token.Any())
                    {
                        var projectName = await _projectService.GetProjectName(id);
                        var artistName = await _artistService.GetArtistName(userId);
                        await pushNotififcation.Push(token, artistName + " đã chấp nhận tham gia",
                            artistName + " đã tham gia vào dự án " + projectName);
                    }
                }
            }
            return Ok();
        }

        /// <summary>
        /// Make Artist done in Project - Customer
        /// </summary>
        [Authorize("Customer")]
        [HttpPut]
        [Route("{id}/done-artist")]
        public async Task<IActionResult> MakeDoneArtist(Guid id, [Required] Guid artistId)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            var result = await _projectService.MakeDoneArtist(id, userId, artistId);
            if (result != null)
            {
                if ((bool)result == false)
                {
                    return BadRequest();
                }
                var token = await _systemService.GetUserToken(artistId, "Artist");
                if (token.Any())
                {
                    var projectName = await _projectService.GetProjectName(id);
                    await pushNotififcation.Push(token, "Ghi âm của bạn đã được chấp nhận",
                        "Ghi âm của bạn trong " + projectName + " đã được chấp nhận");
                }
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Add file to project - Customer, Artist
        /// </summary>
        [Authorize("Customer", "Artist")]
        [HttpPost]
        [Route("{id}/file")]
        public async Task<IActionResult> AddFile(Guid id, [Required] ProjectFile file)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            var role = HttpContext.Items["Role"].ToString();
            bool result = await _projectService.AddFile(id, userId, role, file);
            if (result == false)
            {
                return BadRequest();
            }

            bool isCust = role.Equals("Customer");
            if (isCust)
            {
                var artists = await _projectService.GetArtistInProject(id, null, new Pagination() { PageSize = 9999 });
                if (artists.TotalRow != 0)
                {
                    var artistsData = (List<QuickArtistProjectResponse>)artists.Data;
                    var artistsId = artistsData.Select(x => x.ArtistId);
                    var tokens = new List<string>();
                    foreach (var artistId in artistsId)
                    {
                        var token = await _systemService.GetUserToken(artistId, "Artist");
                        if (token != null)
                        {
                            token.ForEach(x => tokens.Add(x));
                        }
                    }
                    if (tokens.Any())
                    {
                        var projectName = await _projectService.GetProjectName(id);
                        await pushNotififcation.Push(tokens, "Người đăng bài vừa cập nhật một file mới",
                            "Người đăng bài của " + projectName + " vừa cập nhật một file mới");
                    }
                }
            }
            else
            {
                var poster = await _projectService.GetProjectPoster(id) ?? Guid.Empty;
                if (poster != Guid.Empty)
                {
                    var token = await _systemService.GetUserToken(poster, "Customer");
                    if (token.Any())
                    {
                        var projectName = await _projectService.GetProjectName(id);
                        var artistName = await _artistService.GetArtistName(userId);
                        await pushNotififcation.Push(token, artistName + " đã cập nhật một file mới",
                            artistName + " đã cập nhật một file mới trong dự án " + projectName);
                    }
                }
            }
            return Ok();
        }

        /// <summary>
        /// Delete file in Project - Customer, Artist
        /// </summary>
        [Authorize("Customer", "Artist")]
        [HttpDelete]
        [Route("{id}/file")]
        public async Task<IActionResult> RemoveFile(Guid id, [Required] string url)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            var role = HttpContext.Items["Role"].ToString();
            bool result = await _projectService.DeleteFile(id, userId, role, url);
            if (result == false)
            {
                return BadRequest();
            }
            bool isCust = role.Equals("Customer");
            if (isCust)
            {
                var artists = await _projectService.GetArtistInProject(id, null, new Pagination() { PageSize = 9999 });
                if (artists?.Data != null && artists?.TotalRow != 0)
                {
                    var artistsData = (List<QuickArtistProjectResponse>)artists.Data;
                    var artistsId = artistsData.Select(x => x.ArtistId);
                    var tokens = new List<string>();
                    foreach (var artistId in artistsId)
                    {
                        var token = await _systemService.GetUserToken(artistId, "Artist");
                        if (token != null)
                        {
                            token.ForEach(x => tokens.Add(x));
                        }
                    }
                    if (tokens.Any())
                    {
                        var projectName = await _projectService.GetProjectName(id);
                        await pushNotififcation.Push(tokens, "Người đăng bài vừa xoá một file",
                            "Người đăng bài của " + projectName + " vừa xoá một file");
                    }
                }
            }
            else
            {
                var poster = await _projectService.GetProjectPoster(id) ?? Guid.Empty;
                if (poster != Guid.Empty)
                {
                    var token = await _systemService.GetUserToken(poster, "Customer");
                    if (token.Any())
                    {
                        var projectName = await _projectService.GetProjectName(id);
                        var artistName = await _artistService.GetArtistName(userId);
                        await pushNotififcation.Push(token, artistName + " đã xoá một file",
                            artistName + " đã xoá một file trong dự án " + projectName);
                    }
                }
            }
            return Ok();
        }

        /// <summary>
        /// Customer rating Artist - Customer
        /// </summary>
        [Authorize("Customer")]
        [HttpPut]
        [Route("{id}/rating")]
        public async Task<IActionResult> RatingArtist(Guid id, [Required] Guid artistId, [Required][FromBody] RatingRequest rate)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"].ToString());
            var result = await _projectService.RatingArtist(id, userId, artistId, rate);
            if (result == false)
            {
                return BadRequest();
            }
            var token = await _systemService.GetUserToken(artistId, "Artist");
            if (token.Any())
            {
                var projectName = await _projectService.GetProjectName(id);
                await pushNotififcation.Push(token, "Bạn vừa nhận được một đánh giá",
                    "Chúc mừng bạn vừa nhận được đánh giá trong " + projectName);
            }
            return Ok();
        }
    }
}
