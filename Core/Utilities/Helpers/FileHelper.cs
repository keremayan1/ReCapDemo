using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using Core.Utilities.Results;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        public static (string newPath, string path2) newPath(IFormFile formFile)
        {
            FileInfo fileInfo = new FileInfo(formFile.FileName);
            string fileExtension = fileInfo.Extension;
            var creatingUniqueFileName = Guid.NewGuid().ToString("N") +
                                         "-" + DateTime.Now.Month +
                                         "-" + DateTime.Now.Day +
                                         "-" + DateTime.Now.Hour + fileExtension;
            string path = Environment.CurrentDirectory + @"\wwwroot\Images";
            string result = $@"{path} \{creatingUniqueFileName}";
            return (result, $@"\Images\{creatingUniqueFileName}");

        }
        public static string Add(IFormFile formFile)
        {
            var result = newPath(formFile);
            try
            {
                var sourcePath = Path.GetTempFileName();
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(sourcePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                    File.Move(sourcePath, result.newPath);
                }
            }
            catch (Exception e)
            {
                return e.Message;
                throw;
            }

            return result.path2;
        }
        public static string Update(string sourcePath,IFormFile formFile)
        {
            var result = newPath(formFile);
            try
            {
                if (sourcePath.Length>0)
                {
                    using (var stream = new FileStream(result.newPath,FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                }
              File.Delete(sourcePath);
            }
            catch (Exception e)
            {
                return e.Message;
                throw;
            }

            return result.path2;
        }

        public static IResult Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
                throw;
            }

            return new SuccessResult();
        }
    }
}
