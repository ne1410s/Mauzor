using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Essentials;

namespace Mauzor.UI.Shared
{
    internal static class InteropExtensions
    {
        public static async Task<IEnumerable<FileResult>> PickMediaAsync(bool isVideo, bool isMultiple = true)
        {
#if WINDOWS
            var filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            var extensions = isVideo
                ? FullStack.Av.Media.MediaFormatExtensions.VideoExtensions
                : FullStack.Av.Media.MediaFormatExtensions.ImageExtensions;

            foreach (var extension in extensions)
            {
                filePicker.FileTypeFilter.Add(extension);
            }

            filePicker.SuggestedStartLocation = isVideo
                ? Windows.Storage.Pickers.PickerLocationId.VideosLibrary
                : Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

            var mauiWindow = Microsoft.Maui.Controls.Application.Current!.Windows[0];
            var serviceProvider = mauiWindow.Handler.MauiContext!.Services;
            var xamlWindow = serviceProvider.GetService(typeof(Microsoft.UI.Xaml.Window));
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(xamlWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            var results = isMultiple
                ? await filePicker.PickMultipleFilesAsync()
                : new[] { await filePicker.PickSingleFileAsync() };
            return results
                .Where(r => r != null)
                .Select(r => new FileResult(r.Path, r.ContentType))
                .ToList();
#else
            var types = isVideo ? FilePickerFileType.Videos : FilePickerFileType.Images;
            var options = new PickOptions { FileTypes = types };
            var results = isMultiple
                ? await FilePicker.PickMultipleAsync(options)
                : new[] { await FilePicker.PickAsync(options) };
            return (results ?? Array.Empty<FileResult>())
                .Where(r => r != null)
                .ToList();
#endif
        }
    }
}
