using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextService.Entities;
using TextService.Entities.Models;

namespace TextService.Services
{
    public interface ITextService
    {
        Task<TextFile> AddFile(string text);
        Task<TextFile> GetById(Guid id);
        Task<IEnumerable<TextModel>> GetAllText();
        Task<string> UploadFileFormData(IFormFile file);       

        Task<string> UploadFileFromUri(string uriValue);
    }
}