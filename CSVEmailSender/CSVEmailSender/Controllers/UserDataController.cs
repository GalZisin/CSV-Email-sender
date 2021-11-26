using CSVEmailSender.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEmailSender.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserDataController : ControllerBase
    {
        private readonly EmailConfiguration _emailConfig;
        [HttpPost]
        [Route("userdata")]
        public async Task<IActionResult> GetUserData(UserData userData)
        {
            try
            {
                UserData newUserdata = new UserData();

                newUserdata.Id = userData.Id;
                newUserdata.FirstName = userData.FirstName;
                newUserdata.LastName = userData.LastName;
                newUserdata.Email = userData.Email;
                newUserdata.Phone = userData.Phone;
                newUserdata.City = userData.City;
                newUserdata.Street = userData.Street;
                newUserdata.ApartmentNum = userData.ApartmentNum;

                Helper helper = new();
                
                helper.CreateCSVFile(newUserdata);

                bool fileCreated = helper.CreateZipFile();

                EmailSender emailSender = new EmailSender();

                if (fileCreated)
                {
                    //var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();

                    FormFileCollection files = new FormFileCollection();
                    //var message = new Message(new string[] { "galzisin86@gmail.com" }, "Test email", "This is the content from our email with attachment.", files);

                    //MainAppPath.appPath = MainApp.appPath + @"\Files\user_data.zip";
                    await emailSender.SendEmailAsync(new string[] { MainAppPath.emailConfiguration.To }, "Test email", "This is the content from our email with attachment.", files, helper.zipPath);

                    System.Threading.Thread.Sleep(300);
                    System.IO.File.Delete(helper.filePath + ".csv");
                    System.IO.File.Delete(helper.zipPath);

                }

                return Ok("email sent");
            }
            catch(Exception ex)
            {
                return BadRequest("Failed!!" + ex.Message);
            }
        }
    }
}
