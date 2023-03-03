using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DevJobsWeb.Controllers
{
    public class FileUploadController : Controller
    {

        private readonly IRepositoryWrapper _repository;    
       public FileUploadController(IRepositoryWrapper repository)
       {
          _repository = repository;
       }

        public IActionResult FileUploadIndex()
        {
            return View();
        }

        [HttpPost("FileUpload")]
        public async Task<ActionResult> FileUploadIndex(List<IFormFile> files)
        {
            long size=files.Sum(f =>f.Length);  
            var filePaths = new List<string>();
            foreach(var fromFile in files)
            {
                if(fromFile.Length>0)
                {
                    var filePath = Path.GetTempFileName();
                    filePaths.Add(filePath);    
                    
                    using( var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fromFile.CopyToAsync(stream);
                    }

                }
                                                 
            }
            return Ok(new {count =files.Count, size, filePaths});
        }

    }
}
