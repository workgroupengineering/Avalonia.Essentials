using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;

namespace Samples.ViewModel
{
	class ScreenshotViewModel : BaseViewModel
	{
		Bitmap screenshot;

		public ScreenshotViewModel()
		{
			ScreenshotCommand = new RelayCommand(async () => await CaptureScreenshot(), () => Screenshot.IsCaptureSupported);
			EmailCommand = new RelayCommand(async () => await EmailScreenshot(), () => Screenshot.IsCaptureSupported);
		}

		public ICommand ScreenshotCommand { get; }

		public ICommand EmailCommand { get; }

		public Bitmap Image
		{
			get => screenshot;
			set => SetProperty(ref screenshot, value);
		}

		async Task CaptureScreenshot()
		{
			var mediaFile = await Screenshot.CaptureAsync();
			var stream = await mediaFile.OpenReadAsync(ScreenshotFormat.Png);

			Image = new Bitmap(stream);
		}

		async Task EmailScreenshot()
		{
			var mediaFile = await Screenshot.CaptureAsync();

			var temp = Path.Combine(FileSystem.CacheDirectory, "screenshot.jpg");
			using (var stream = await mediaFile.OpenReadAsync(ScreenshotFormat.Jpeg))
			using (var file = File.Create(temp))
			{
				await stream.CopyToAsync(file);
			}

			await Email.ComposeAsync(new EmailMessage
			{
				Attachments = { new EmailAttachment(temp) }
			});
		}
	}
}
