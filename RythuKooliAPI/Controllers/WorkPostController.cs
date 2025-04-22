using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace RythuKooliAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPostController : ControllerBase
    {

        private readonly IWorkPostRepository _workPostRepository;
        private readonly IWebHostEnvironment _env; // Declare the _env variable
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;


        public WorkPostController(IWorkPostRepository workPostRepository, IWebHostEnvironment env, IUserRepository userRepository, INotificationService notificationService)
        {
            _workPostRepository = workPostRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
            _env = env;
        }

        [HttpGet("GetWork")]
        public async Task<ActionResult<IEnumerable<Work>>> GetAllWorkPosts()
        {
            try
            {
                var workPosts = await _workPostRepository.GetAllWorkPostsAsync();
                return Ok(workPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Failed to fetch work posts", Error = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateWorkPost([FromForm] Work workPostDto, IFormFile? image)
        {
            try
            {
                if (workPostDto == null || string.IsNullOrEmpty(workPostDto.ContactNumber))
                {
                    return BadRequest("Invalid work post data. Contact number is required.");
                }

                // Handle Image Upload
                string? fileName = null;
                string? relativePath = null;
                if (image != null)
                {
                    var uploads = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploads); // Ensure the folder exists

                    fileName = Path.GetFileName(image.FileName); // Get only the file name
                    string fullPath = Path.Combine(uploads, fileName); // Full system path

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    // Store the relative path (e.g., "/uploads/image.jpg")
                    relativePath = Path.Combine("uploads", fileName).Replace("\\", "/");
                }
                var workPost = new Work
                {

                    WorkName = workPostDto.WorkName,
                    Place = workPostDto.Place,
                    Time = workPostDto.Time,
                    ContactNumber = workPostDto.ContactNumber,
                    NoOfPeople = workPostDto.NoOfPeople,
                    postedBy = workPostDto.postedBy,
                    ImagePath = relativePath  // Save only the path in DB
                };
                await _workPostRepository.AddWorkPostAsync(workPost);
                // 2. Prepare notification data
                var title = "New Work Available!";
                var message = $"A new work '{workPostDto.WorkName}' has been posted at {workPostDto.Place}.";
                var data = new Dictionary<string, string>
                {
                    { "type", "work_post" },
                    { "workId", workPostDto.Id.ToString() } // Pass the work ID
                };

                if (_userRepository == null)
                {
                    throw new Exception("_userRepository is NULL.");
                }

                var deviceTokens = await _userRepository.GetAllDeviceTokensAsync();

                foreach (var token in deviceTokens)
                {
                    await _notificationService.SendPushNotification(token, title, message, "work_post", workPostDto.Id.ToString());
                }

                return Ok(new
                {
                    Message = "Work posted successfully!",
                    ImageFileName = fileName // Return the saved filename
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Message = "Work post not  created!" + ex.Message,

                });
            }
        }

    }

}
