using FullStack.Av.Domain;
using Microsoft.Maui.Essentials;
using System;
using System.Threading.Tasks;

namespace Mauzor.UI.Shared
{
    internal static class InteropExtensions
    {
        public static async Task<FileResult?> PickFile(bool pickVideo)
        {
#if WINDOWS
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            var extensions = pickVideo ? FileExtensions.VideoExtensions : FileExtensions.ImageExtensions;
            foreach (var extension in extensions)
            {
                filePicker.FileTypeFilter.Add(extension);
            }

            filePicker.SuggestedStartLocation = pickVideo
                ? Windows.Storage.Pickers.PickerLocationId.VideosLibrary
                : Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

            var mauiWindow = Microsoft.Maui.Controls.Application.Current!.Windows[0];
            var xamlWindow = mauiWindow.Handler.MauiContext!.Services.GetService(typeof(Microsoft.UI.Xaml.Window));
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(xamlWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            var winResult = await filePicker.PickSingleFileAsync();
            return winResult == null ? null : new FileResult(winResult.Path, winResult.ContentType);
#else
            return await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = pickVideo
                    ? FilePickerFileType.Videos
                    : FilePickerFileType.Images
            });
#endif
        }
    }
}
