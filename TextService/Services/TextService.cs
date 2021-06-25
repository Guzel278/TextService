using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Text.Service.Repositories;
using TextService.Entities;
using TextService.Entities.Models;

namespace TextService.Services
{
    public class TextService : ITextService
    {
        private readonly ITextRepository _textRepository;
        private readonly IMapper _mapper;

        public TextService(ITextRepository textRepository,
        IMapper mapper)
        {
            _textRepository = textRepository;
            _mapper = mapper;
        }

        public async Task<TextFile> AddFile(string text)
        {
            var textFile = new Text.Service.Repositories.Text();
            textFile.TextValue = text;

            textFile = await _textRepository.Create(textFile);
            textFile.TextValue = null;

            return _mapper.Map<TextFile>(textFile);
        }

        public async Task<TextFile> GetById(Guid id)
        {
            var text = await _textRepository.GetById(id);
            return _mapper.Map<TextFile>(text);
        }

        public async Task<IEnumerable<TextModel>> GetAllText()
        {
            var text = await _textRepository.GetAll();

            return _mapper.Map<IEnumerable<Text.Service.Repositories.Text>, IEnumerable<TextModel>>(text);
        }
        public async Task<string> UploadFileFormData(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    using (var sr = new StreamReader(file.OpenReadStream()))
                    {
                        var body = await sr.ReadToEndAsync();

                        await this.AddText(body);
                    }

                    return "Done";
                }
                else
                {
                    return "Not done";
                }
            }
            catch (Exception ex)
            {
                return $"Faild {ex.Message}";
            }      
        }
        public async Task<TextModel> AddText(string text)
        {
            var textFile = new Text.Service.Repositories.Text { TextValue = text };
            textFile = await _textRepository.Create(textFile);

            return _mapper.Map<TextModel>(textFile);
        }

        public async Task<string> UploadFileFromUri(string uriValue)
        {
            try
            {
                Uri filePath;
                if (Uri.TryCreate(uriValue, UriKind.Absolute, out filePath))
                {
                    filePath = new Uri(uriValue);

                    string filename = System.IO.Path.GetFileName(filePath.AbsolutePath);
                    string ex = System.IO.Path.GetExtension(filename);

                    if (ex == ".txt")
                    {
                        using (System.Net.WebClient wc = new System.Net.WebClient())
                        {
                            var body = wc.DownloadString(filePath);

                            await this.AddText(body);
                        }
                        return "Done";
                    }

                    return $"Wrong file format {filename}";

                }
                else
                {
                    return $"Wrong uri {uriValue}";
                }
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
    }
}