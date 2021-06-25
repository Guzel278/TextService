using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Refit;
using TextService.Entities;
using TextService.Entities.Models;

namespace TextService.Client
{
    public interface ITextClient
    {
        [Get("/text/{id}")]
        Task<TextFile> GetById(Guid id);
        [Get("/api/textservice")]
        Task<IEnumerable<TextModel>> GetAllTexts();
        [Post("/text")]
        Task<TextFile> Post([Body] string text);
        [Post("/text/file/{streamTextFile}")]
        Task<TextFile> PostFile(Stream streamTextFile);
        [Post("/api/textservice/url/{fileUrl}")]
        Task<TextModel> PostFileUrl([Body] string fileUrl);
    }
}
