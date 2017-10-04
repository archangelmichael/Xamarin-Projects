using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Foundation;
using Newtonsoft.Json;
using UIKit;

namespace XImgUpDown
{
    public partial class ViewController : UIViewController
    {
		private const string api_url = "https://api.uploadcare.com/";
		private const string upload_url = "https://upload.uploadcare.com/";
		private const string public_key = "f0a5f24b13717e719baa";
		private const string private_key = "8bbf5aa41bb1b34db513";
        private const string api_auth_key = "Uploadcare.Simple";
        private const string file_name = "speedtest1Mb.jpg";

        private const string file_download_id = "d97c5106-8a2b-4b66-a08d-90d818c9e81c";
        private const string file_download_name = "lyncdownloadspeedtest.jpg";
        private const string file_upload_name = "lyncuploadspeedtest.jpg";

        private const string upload_public_key = "UPLOADCARE_PUB_KEY";
        private const string upload_store = "UPLOADCARE_STORE";

        private const string upload_file_format = "{0}/base/";
        private const string download_file_format = "{0}/files/{1}/";

        DateTime startDownload;
        DateTime endDownload;


		// POST Request
		private string GetUploadUrl()
		{
			return string.Format(upload_file_format, upload_url);
		}

		// GET or DELETE Request
		private string GetDownloadUrl(string fileID)
		{
			return string.Format(download_file_format, api_url, fileID);
		}

		private string GetBasicAuthValue()
		{
			return string.Format("{0}:{1}", public_key, private_key);
		}


		protected ViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
            OnUpload();
		}

        async void OnUpload()
        {
			try
			{
				var imgToUpload = UIImage.FromFile(file_name);
				using (NSData imgData = imgToUpload.AsPNG())
				{
					Byte[] imgByteArray = new Byte[imgData.Length];
                    var uploadFileSizeInKb = imgByteArray.Length / 1024;
					System.Runtime.InteropServices.Marshal.Copy(imgData.Bytes, imgByteArray, 0, Convert.ToInt32(imgData.Length));

                    var uploadedImage = await UploadImage(imgByteArray);
					if (uploadedImage != null)
					{
						Console.WriteLine("Image uploaded");
                        double kbsec = Math.Round(uploadFileSizeInKb / uploadedImage.UploadSeconds, 1);
                        Console.WriteLine(kbsec + "kb/sec");

                        startDownload = DateTime.Now;
                        OnDownload(uploadedImage.FileId);
					}
					else
					{
						Console.WriteLine("Image failed to upload");
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
        }

        static int counter = 0;

        async void OnDownload(string fileID = file_download_id)
		{
			try
            {
                var downloadImage = await GetImage(fileID);
                if (downloadImage != null && !downloadImage.IsReady)
                {
                    counter += 1;
                    OnDownload(null);
                    return;
                }

                endDownload = DateTime.Now;
                Console.WriteLine("Storing image completed in {0}", (endDownload - startDownload).TotalSeconds);
                var download = await DownloadImage(downloadImage.OriginalFileUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
			}
        }

        public async Task<UploadModel> UploadImage(byte[] imageBytes)
		{
            DateTime startTime;
            DateTime endTime;
			using (HttpClient httpClient = new HttpClient())
			{
				using (MultipartFormDataContent form = new MultipartFormDataContent())
				{
                    form.Add(new StringContent(public_key), upload_public_key);
                    form.Add(new StringContent("1"), upload_store);
                    form.Add(new ByteArrayContent(imageBytes, 0, imageBytes.Length), "file", file_upload_name);

                    startTime = DateTime.Now;
					var httpResponse = await httpClient.PostAsync(GetUploadUrl(), form);
					if (httpResponse.Content != null)
					{
                        endTime = DateTime.Now;
						var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<UploadModel>(responseContent);
                        if (response != null)
                        {
							var diffInSeconds = (endTime - startTime).TotalSeconds;
							response.UploadSeconds = diffInSeconds;
                        }

						return response;
					}

					throw new Exception("Upload test is not possible.");
				}
			}
		}

        public async Task<UploadModel> GetImage(string fileId) 
        {
            var fileUrl = GetDownloadUrl(fileId);
			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(api_auth_key, GetBasicAuthValue());
				var httpResponse = await httpClient.GetAsync(fileUrl);
				if (httpResponse.Content != null)
				{
					var responseContent = await httpResponse.Content.ReadAsStringAsync();
					var response = JsonConvert.DeserializeObject<UploadModel>(responseContent);
					return response;
				}

				throw new Exception("Get test is not possible.");
			}
        }

		public async Task<string> DownloadImage(string fileUrl)
		{
			DateTime startTime;
			DateTime endTime;

			using (var httpClient = new HttpClient())
			{
				//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(api_auth_key, GetBasicAuthValue());
				startTime = DateTime.Now;
				using (var request = new HttpRequestMessage(HttpMethod.Get, fileUrl))
				{
                    var responseContent = await httpClient.SendAsync(request);
                    endTime = DateTime.Now;
                    using (var contentStream = await responseContent.Content.ReadAsStreamAsync()) 
                    {
                        Console.WriteLine("File downloaded");
                    }
				}

				throw new Exception("download test is not possible.");
			}
		}

        private void SaveImage(Stream dataStream)
		{
            var bytesArray = ReadFully(dataStream);
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string localFilename = file_name;
			string localPath = Path.Combine(documentsPath, localFilename);
			File.WriteAllBytes(localPath, bytesArray);
		}

		public static byte[] ReadFully(Stream input)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				input.CopyTo(ms);
				return ms.ToArray();
			}
		}
		
    }
}
