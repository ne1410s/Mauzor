﻿using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace Mauzor.UI
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			//?Microsoft.Maui.Essentials.Platform.Init(this);

			MainPage = new MainPage();
		}
	}
}
