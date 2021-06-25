using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TextService.Entities;
using TextService.Services;
using System.Collections.Generic;
using TextService.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace TextService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextController : ControllerBase
    {
        private readonly ITextService _textService;
        private readonly ILogger<TextController> _logger;

        public TextController(ITextService textService, ILogger<TextController> logger)
        {
            _textService = textService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TextFile>> GetById(Guid id)
        {
            return await _textService.GetById(id);
        }

        [HttpGet]
        public async Task<IEnumerable<TextModel>> GetAllTexts()
        {
            var token = Request.Headers["Authorization"];       
            return await _textService.GetAllText();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TextFile>> Post([FromBody]string text)
        {
            //do something
            var textFile = await _textService.AddFile(text);
            return new OkObjectResult(textFile);
        }


        [HttpPost("file")]
        public async Task<ActionResult<string>> PostFile(IFormFile formFile)
        {
            if (formFile != null)
            {
                var result = await _textService.UploadFileFormData(formFile);
                return new OkObjectResult(result);
            }

            return new OkObjectResult($"formFile Is Empty");
        }

        [HttpPost("url/{fileUrl}")]
        public async Task<ActionResult<string>> PostFileUrl(string textFileUrl)
        {
            var textFile = await _textService.UploadFileFromUri(textFileUrl);
            return new OkObjectResult(textFileUrl);
        }

    }
}
