using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UwpTitlebarButtonPoc
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;
            }

            var view = ApplicationView.GetForCurrentView();
            view.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            view.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            view.TitleBar.ButtonForegroundColor = Colors.White;
            view.TitleBar.ButtonInactiveForegroundColor = Color.FromArgb(255, 185, 185, 185);
            view.TitleBar.ButtonHoverBackgroundColor = Color.FromArgb(35, 255, 255, 255);
            view.TitleBar.ButtonPressedBackgroundColor = Color.FromArgb(60, 255, 255, 255);
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            if (rootFrame.Content is MainPage mainPage)
            {
                mainPage.InitializeTitleBar();
            }

            Window.Current.Activate();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new System.Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            // No-op for this POC.
        }
    }
}
