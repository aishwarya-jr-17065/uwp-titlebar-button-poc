using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpTitlebarButtonPoc
{
    public sealed partial class MainPage : Page
    {
        private bool _isInitialized;

        public MainPage()
        {
            this.InitializeComponent();
            this.Unloaded += MainPage_Unloaded;
        }

        public void InitializeTitleBar()
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            Window.Current.SetTitleBar(DragRegion);

            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            titleBar.LayoutMetricsChanged += TitleBar_LayoutMetricsChanged;
            titleBar.IsVisibleChanged += TitleBar_IsVisibleChanged;

            UpdateTitleBarLayout();
        }

        private async void TitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, UpdateTitleBarLayout);
        }

        private async void TitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                CustomTitleBar.Visibility = sender.IsVisible ? Visibility.Visible : Visibility.Collapsed;
                UpdateTitleBarLayout();
            });
        }

        private void UpdateTitleBarLayout()
        {
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            CustomTitleBar.Height = titleBar.Height;
            CustomTitleBar.Padding = new Thickness(0, 0, titleBar.SystemOverlayRightInset, 0);
            DragRegion.MinWidth = 48;
        }

        private void ChevronButton_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "Chevron button clicked.";
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
            var titleBar = CoreApplication.GetCurrentView().TitleBar;
            titleBar.LayoutMetricsChanged -= TitleBar_LayoutMetricsChanged;
            titleBar.IsVisibleChanged -= TitleBar_IsVisibleChanged;
        }
    }
}
