using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nippon_Final_Project.Models;

namespace Nippon_Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        [HttpPost]
        [Route("UploadFile")]
        public Response UploadFile([FromForm] FileModel fileModel)
        {
            Response response = new Response();

            try
            {
                // Access the uploaded file from the FileModel
                IFormFile file = fileModel.FormFile;

                if (file != null)
                {
                    string path = Path.Combine(@"D:\Nippon_Photos", fileModel.FileName);

                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    response.StatusCode = 200;
                    response.ErrorMessage = "Image created successfully";
                }
                else
                {
                    response.StatusCode = 400; // Bad Request
                    response.ErrorMessage = "No file was uploaded.";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500; // Internal Server Error
                response.ErrorMessage = "Some error occurred: " + ex.Message;
            }

            return response;
        }

    }
}
