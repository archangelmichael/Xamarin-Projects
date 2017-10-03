using System;
using Newtonsoft.Json;

namespace XImgUpDown
{
    public class UploadModel
    {
		[JsonProperty("file")]
        public string FileId { get; set; }
    }
}
