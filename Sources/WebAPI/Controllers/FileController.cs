using WebAPI.DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public enum TargetType 
    {
        Cabinet,
        Profile,
        Request
    }

    public enum FileType 
    {
        Image,
        Docs
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FileController(DB_Context context) : ControllerBase
    {
        private DB_Context _context = context;

        //[HttpGet("download")]
        //public async Task<IActionResult> DownloadFile(string fileName)
        //{

        //}

        /*[HttpGet("images/count/cabId={cabId}")]
        public async Task<IActionResult> GetImagesCountByCabId([FromRoute] int cabId) 
        {
            var cab = await _context.Cabinets.Where(c => c.Id == cabId).FirstOrDefaultAsync();

            if (cab == null)
                return BadRequest();

            string folderName = Path.Combine("Resources", "images", "cabinets", cab.Num.ToString());

            if (!Directory.Exists(folderName))
                return BadRequest();

            var images = Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories).ToList();

            return Ok(images.Count);
        }*/

        [HttpGet("images/cabId={cabId}")]
        public async Task<IActionResult> GetImagesByCabId([FromRoute] int cabId)
        {
            var cab = await _context.Cabinets.Where(c => c.Id == cabId).FirstOrDefaultAsync();

            if (cab == null)
                return BadRequest("Cabinet not found");

            //string folderName = Path.Combine("Resources", "images", "cabinets", cab.Num.ToString());
            var cabPhotos = await _context.CabPhotos
                .Where(cp => cp.CabId == cabId)
                .Select(cp => cp.ImagePath)
                .ToListAsync();

            /*if (!Directory.Exists(folderName))
                return BadRequest("Resources not found");

            var images = Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories).ToList();*/

            List<string> Urls = [];

            foreach(var img in cabPhotos)
            {
                var repUrl = img.Replace("\\", @"/");

                Urls.Add($"http://{HttpContext.Request.Host.Value}/{repUrl}");
            }

            return Ok(Urls);
        }  

        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadModel model, FileType file, TargetType target, int cabinetId, bool canUpdate = false) 
        {
            if (model.File == null && model.File?.Length == 0)
                return BadRequest("Invalid file");

            var author = await _context.Users
                .Where(u => u.Id == model.FileAuthorId)
                .FirstOrDefaultAsync();

            if (author == null)
                return BadRequest("User not exist");

            string folderName = string.Empty;

            folderName = file switch
            {
                FileType.Image => Path.Combine("Resources", "images"),
                FileType.Docs => Path.Combine("Resources", "docs"),
                _ => Path.Combine("Resources", "files")
            };

            folderName = target switch
            {
                TargetType.Cabinet => Path.Combine(folderName, "cabinets"),
                TargetType.Profile => Path.Combine(folderName, "profiles"),
                TargetType.Request => Path.Combine(folderName, "requests"),
                _ => string.Empty
            };

            return await Cabinet();

            async Task<IActionResult> Cabinet() 
            {
                var cabinet = await _context.Cabinets
                    .Where(c => c.Id == cabinetId)
                    .FirstOrDefaultAsync();

                if (cabinet == null)
                    return BadRequest("Cabinet not exist");

                var savePath = Path.Combine(Directory.GetCurrentDirectory(), folderName, cabinet.Num.ToString());

                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);

                //var fileName = model.File.FileName;
                var fileName = $"{Guid.NewGuid()}.png";
                var fullPath = Path.Combine(savePath, fileName);

                var dbPath = Path.Combine(folderName, fileName);

                if (System.IO.File.Exists(fullPath))
                    if (!canUpdate)
                        return BadRequest("File already exist");

                using var stream = new FileStream(fullPath, FileMode.Create);
                await model.File.CopyToAsync(stream);

                await _context.CabPhotos.AddAsync(new CabPhoto()
                {
                    ImagePath = dbPath,
                    //FileName = model.FileName,
                    //Description = model.Descrption,
                    CabId = cabinet.Id,
                    ImageAuthor = author.Id
                });
                await _context.SaveChangesAsync();

                return Ok("File uploaded!");
            }
        }
    }
}
