using System;
using System.Collections.Generic;
using System.Net.Http;
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

        DateTime startTime;
        DateTime endTime;

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
				var imgToUpload = UIImage.FromFile("speedtest1Mb.jpg");
				using (NSData imgData = imgToUpload.AsPNG())
				{
					Byte[] imgByteArray = new Byte[imgData.Length];
					System.Runtime.InteropServices.Marshal.Copy(imgData.Bytes, imgByteArray, 0, Convert.ToInt32(imgData.Length));

					var uploadedImage = await UploadImage(imgByteArray);
					if (uploadedImage != null)
					{
						Console.WriteLine("Image uploaded");
                        var diffInSeconds = (endTime - startTime).TotalSeconds;
                        double kbsec = Math.Round(15.7 * 1024 / diffInSeconds);
                        Console.WriteLine(kbsec + "kb/sec");



						// TODO: detect speed
						// TODO: Download the image
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

        void OnDownload(string fileID)
        {



		//	NSArray* paths = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, YES);
		//	NSString* documentsDirectoryPath = [paths objectAtIndex: 0];

		//	downloadDirectoryPath = [NSString stringWithFormat: @"%@/Downloads/", documentsDirectoryPath];

		//	if (![[NSFileManager defaultManager] fileExistsAtPath: downloadDirectoryPath])

		//[[NSFileManager defaultManager] createDirectoryAtPath:downloadDirectoryPath withIntermediateDirectories:NO attributes:nil error:nil];



        }

		// POST Request
		private string GetUploadUrl()
		{
			return string.Format("{0}/base/", upload_url);
		}

		// GET Request
		private string GetDownloadUrl(string fileID)
		{
			return string.Format("{0}/files/{1}/", api_url, fileID);
		}

		// DELETE Request
		private string GetDeleteUrl(string fileID)
		{
			return string.Format("{0}/files/{1}/", api_url, fileID);
		}

        public async Task<UploadModel> UploadImage(byte[] imageBytes)
		{
			var parameters = new Dictionary<string, string>
			{
					{ "UPLOADCARE_PUB_KEY", public_key },
					{ "UPLOADCARE_STORE", "0" }
			};

			using (HttpClient httpClient = new HttpClient())
			{
				using (MultipartFormDataContent form = new MultipartFormDataContent())
				{
                    form.Add(new StringContent(public_key), "UPLOADCARE_PUB_KEY");
					form.Add(new StringContent("0"), "UPLOADCARE_STORE");
					form.Add(new ByteArrayContent(imageBytes, 0, imageBytes.Length), "file", "@lyncspeedtest.jpg");

                    startTime = DateTime.Now;
					var httpResponse = await httpClient.PostAsync(GetUploadUrl(), form);
					if (httpResponse.Content != null)
					{
                        endTime = DateTime.Now;
						var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<UploadModel>(responseContent);
						return response;
					}

					throw new Exception("Upload test is not possible.");
				}
			}
		}

		public void DownloadImage()
		{

		}

		public void DeleteImage()
		{

		}

		private string GetBasicAuth()
		{
			return string.Format("Uploadcare.Simple {0}:{1}", public_key, private_key);
		}
    }
}
