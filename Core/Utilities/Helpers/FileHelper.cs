using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using Core.Utilities.Business;
using Core.Utilities.Results;

namespace Core.Utilities.Helpers
{
    public class FileHelper
    {
        //public static (string newPath, string path2) newPath(IFormFile formFile)
        //{
        //    FileInfo fileInfo = new FileInfo(formFile.FileName);
        //    string fileExtension = fileInfo.Extension;
        //    var creatingUniqueFileName = Guid.NewGuid().ToString("N") +
        //                                 "-" + DateTime.Now.Month +
        //                                 "-" + DateTime.Now.Day +
        //                                 "-" + DateTime.Now.Hour + fileExtension;
        //    string path = Environment.CurrentDirectory + @"\wwwroot\Images";
        //    string result = $@"{path} \{creatingUniqueFileName}";
        //    return (result, $@"\Images\{creatingUniqueFileName}");

        //}
        //public static string Add(IFormFile formFile)
        //{
        //    var result = newPath(formFile);
        //    try
        //    {
        //        var sourcePath = Path.GetTempFileName();
        //        if (formFile.Length > 0)
        //        {
        //            using (var stream = new FileStream(sourcePath, FileMode.Create))
        //            {
        //                formFile.CopyTo(stream);
        //            }
        //            File.Move(sourcePath, result.newPath);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //        throw;
        //    }

        //    return result.path2;
        //}
        //public static string Update(string sourcePath,IFormFile formFile)
        //{
        //    var result = newPath(formFile);
        //    try
        //    {
        //        if (sourcePath.Length>0)
        //        {
        //            using (var stream = new FileStream(result.newPath,FileMode.Create))
        //            {
        //                formFile.CopyTo(stream);
        //            }
        //        }
        //      File.Delete(sourcePath);
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //        throw;
        //    }

        //    return result.path2;
        //}

        //public static IResult Delete(string path)
        //{
        //    try
        //    {
        //        File.Delete(path);
        //    }
        //    catch (Exception e)
        //    {
        //        return new ErrorResult(e.Message);
        //        throw;
        //    }

        //    return new SuccessResult();
        //}


        public static (IResult,string dbPath) Upload(IFormFile file,string[] checkExtensions, params string[] folderNames)
        {
            
            var fileExtension = Path.GetExtension(file.FileName);

            var result = BusinessRules.Run(CheckFileIsEmpty(file),CheckFileTypeValid(fileExtension,checkExtensions));
            if (result!=null)
            {
                return (result,null);
            }

            var folderName = Path.Combine(folderNames);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var creatingUniqueFileName = Guid.NewGuid().ToString("N") + " - " + DateTime.Now.Month + " - " +
                                         DateTime.Now.Day + " - " + DateTime.Now.Year + fileExtension;
            var fullPath = Path.Combine(pathToSave, creatingUniqueFileName);
            var dbPath = Path.Combine(folderName, creatingUniqueFileName);

            using (var stream = new FileStream(fullPath,FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return (new SuccessResult(), dbPath);
        }

        private static IResult CheckFileIsEmpty(IFormFile formFile)
        {
            if (formFile !=null)
            {
                if (formFile.Length > 0)
                {
                    return new SuccessResult();
                }
            }

            return new ErrorResult();
        }

        private static IResult CheckFileTypeValid(string fileExtensions, string[] checkExtensions)
        {
            foreach (var checkExtension in checkExtensions)
            {
                if (fileExtensions==checkExtension)
                {
                    return new SuccessResult();
                }
            }

            return new ErrorResult();
        }


    }
}
