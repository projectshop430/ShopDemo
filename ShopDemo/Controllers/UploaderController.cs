﻿using System;
using System.IO;
using shopDemo.application.Extensions;
using shopDemo.application.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ShopDemo.Controllers
{
    public class UploaderController : SiteBaseController
    {

        [HttpPost]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncName, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;
            if (!upload.IsImage())
            {
                var notImageMessage = "لطفا یک تصویر انتخاب کنید";
                var notImage = JsonConvert.DeserializeObject("{'uploaded':0, 'error': {'message': \" " + notImageMessage + " \"}}");
                return Json(notImage);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            upload.AddImageToServer(fileName, PathExtension.UploadImageServer, null, null);
            return Json(new
            {
                uploaded = true,
                url = $"{PathExtension.UploadImage}{fileName}"
            });
        }
    }
}
