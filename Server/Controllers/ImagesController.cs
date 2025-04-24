using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Date;
using Server.Models.Entities;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ImagesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost("upload-image-user")]
        [Authorize]
        public async Task<IActionResult> UploadUserImage(IFormFile image, [FromForm] string memberName)
        {
            if (string.IsNullOrEmpty(memberName))
                return BadRequest("Member Name is Required");

            //string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string name = User.Identity.Name;
            var applicationUser = await _userManager.FindByNameAsync(name);
            if (string.IsNullOrEmpty(applicationUser.Id))
                return Unauthorized("User not found");

            if (image == null || image.Length == 0)
                return BadRequest("Image Not Found");
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserImages");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string fileExtension = Path.GetExtension(image.FileName);
            string allowedExtension = ".jpg";
            // check for .jpg only
            if (fileExtension != allowedExtension)
                return BadRequest("Invalid image format");


            string fileName = $"{memberName}{fileExtension}"; // Mohamed.jpg

            string filePath = Path.Combine(uploadsFolder, fileName); // path from begin to the project/wwwroot/UserImages
            //relative path to store in Db and use in frontend
            string relativePath = Path.Combine("UserImages", fileName).Replace("\\", "/");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var userImage = new UserImage
            {
                OwnerUserId = applicationUser.Id,
                Name = memberName,
                FileName = fileName,
                ImagePath = relativePath,
                UploadedAt = DateTime.Now,
                //OwnerUser=applicationUser
            };

            _context.UserImages.Add(userImage);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Image Uploaded Successfully",
                imageurl = $"{Request.Scheme}://{Request.Host}/{relativePath}", // for frontend
                userImage
            });
        }
    }
}
