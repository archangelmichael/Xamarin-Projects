using System;
using Newtonsoft.Json;

namespace XImgUpDown
{
    public class UploadModel
    {
		[JsonProperty("file")]
        public string FileId { get; set; }

        [JsonProperty("original_file_url")]
        public string OriginalFileUrl { get; set; }

		[JsonProperty("is_ready")]
		public bool IsReady { get; set; }

		[JsonProperty("uuid")]
        public string UUID { get; set; }

		[JsonProperty("size")]
        public double Size { get; set; }

        public double UploadSeconds { get; set; }
    }
}
