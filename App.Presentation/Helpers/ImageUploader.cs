

namespace App.Presentation.Helpers
{
	public static class ImageUploader
	{
		public static string UploadImage(IFormFile Image, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting, string OldImage = "")
		{
			string ImageName = "";
			if (Image != null)
			{
				try
				{
					string uploads = Path.Combine(hosting.WebRootPath, "Images");
					if (!Directory.Exists(uploads))
					{
						Directory.CreateDirectory(uploads);
					}
					ImageName = Image.FileName;
					string ImagePath = Path.Combine(uploads, ImageName);
					string OldImagePath = Path.Combine(uploads, OldImage != null ? OldImage : "");
					if (System.IO.File.Exists(OldImagePath))
					{
						System.IO.File.Delete(OldImagePath);
						Image.CopyTo(new FileStream(ImagePath, FileMode.Create));
					}
					else
					{
						Image.CopyTo(new FileStream(ImagePath, FileMode.Create));
					}
				}
				catch (Exception ex)
				{
					//ImageName = "";
				}

			}
			return ImageName;

		}
	}
}
