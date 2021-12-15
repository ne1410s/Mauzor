using Application = Microsoft.Maui.Controls.Application;

namespace Mauzor.UI
{
    public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}
	}
}
