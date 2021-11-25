using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace IEC101MasterSim.View;

/// <summary>
/// Interaction logic for InfoDialog.xaml
/// </summary>
public partial class InfoDialog : UserControl
{
    public InfoDialog() => InitializeComponent();

    private static void OpenInBrowser(string url)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            url = url.Replace("&", "^&");
            _ = Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
    }

    private void EmailButton_OnClick(object sender, System.Windows.RoutedEventArgs e) => OpenInBrowser("mailto://Baatchia_Riyadh@protonmail.com");
}
