using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers.FileHelper
{
    public interface IFileHelper
    {
        string Upload(IFormFile file, string root);
        void Delete(string root);
        string Update(IFormFile file, string filePath, string root);
    }
}
