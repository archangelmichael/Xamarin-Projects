using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;
using UIKit;

namespace MicrosoftGraph.iOS.Helpers
{
    public class Mail
    {
		/// <summary>
		/// Compose and send a new email.
		/// </summary>
		/// <param name="subject">The subject line of the email.</param>
		/// <param name="bodyContent">The body of the email.</param>
		/// <param name="recipients">A semicolon-separated list of email addresses.</param>
		/// <returns></returns>
		public async Task ComposeAndSendMailAsync(string subject,
                                                  string bodyContent,
                                                  string recipients)
		{
            Stream photoStream = await GetCurrentUserPhotoStreamAsync();
			if (photoStream == null)
			{
                var myImage = UIImage.FromBundle("Second");
                photoStream = myImage.AsPNG().AsStream();
			}

			MemoryStream photoStreamMS = new MemoryStream();
			// Copy stream to MemoryStream object so that it can be converted to byte array.
			photoStream.CopyTo(photoStreamMS);

			DriveItem photoFile = await UploadFileToOneDriveAsync(photoStreamMS.ToArray());

			MessageAttachmentsCollectionPage attachments = new MessageAttachmentsCollectionPage();
			attachments.Add(new FileAttachment
			{
				ODataType = "#microsoft.graph.fileAttachment",
				ContentBytes = photoStreamMS.ToArray(),
				ContentType = "image/png",
				Name = "me.png"
			});

			Permission sharingLink = await GetSharingLinkAsync(photoFile.Id);
			string bodyContentWithSharingLink = String.Format(bodyContent, sharingLink.Link.WebUrl);

			// Prepare the recipient list
			string[] splitter = { ";" };
			var splitRecipientsString = recipients.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
			List<Recipient> recipientList = new List<Recipient>();

			foreach (string recipient in splitRecipientsString)
			{
				recipientList.Add(new Recipient { EmailAddress = new EmailAddress { Address = recipient.Trim() } });
			}

			try
			{
				var graphClient = Authentication.GetAuthenticatedClient();
				var email = new Message
				{
					Body = new ItemBody
					{
						Content = bodyContentWithSharingLink,
						ContentType = BodyType.Html,
					},
					Subject = subject,
					ToRecipients = recipientList,
					Attachments = attachments
				};

				try
				{
					await graphClient.Me.SendMail(email, true).Request().PostAsync();
				}
				catch (ServiceException exception)
				{
					throw new Exception("We could not send the message: " + exception.Error == null ? "No error message returned." : exception.Error.Message);
				}
			}
			catch (Exception e)
			{
				throw new Exception("We could not send the message: " + e.Message);
			}
		}

		public async Task<Stream> GetCurrentUserPhotoStreamAsync()
		{
			Stream currentUserPhotoStream = null;

			try
			{
				var graphClient = Authentication.GetAuthenticatedClient();
				currentUserPhotoStream = await graphClient.Me.Photo.Content.Request().GetAsync();
			}
			// If the user account is MSA (not work or school), the service will throw an exception.
			catch (ServiceException)
			{
				return null;
			}

			return currentUserPhotoStream;
		}

		public async Task<DriveItem> UploadFileToOneDriveAsync(byte[] file)
		{
			DriveItem uploadedFile = null;

			try
			{
				var graphClient = Authentication.GetAuthenticatedClient();
				MemoryStream fileStream = new MemoryStream(file);
				uploadedFile = await graphClient.Me.Drive.Root.ItemWithPath("me.png").Content.Request().PutAsync<DriveItem>(fileStream);
			}
			catch (ServiceException)
			{
				return null;
			}

			return uploadedFile;
		}

		public static async Task<Permission> GetSharingLinkAsync(string Id)
		{
			Permission permission = null;

			try
			{
				var graphClient = Authentication.GetAuthenticatedClient();
				permission = await graphClient.Me.Drive.Items[Id].CreateLink("view").Request().PostAsync();
			}
			catch (ServiceException)
			{
				return null;
			}

			return permission;
		}
    }
}
