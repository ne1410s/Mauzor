using System;
using Android.App;
using Android.Runtime;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Mauzor.UI
{
	[Application]
	public class MainApplication : MauiApplication
	{
		public MainApplication(IntPtr handle, JniHandleOwnership ownership)
			: base(handle, ownership)
		{
		}

		protected override MauiApp CreateMauiApp()
		{
			Microsoft.Maui.Essentials.Platform.Init(this);

			return MauiProgram.CreateMauiApp();
		}
	}
}