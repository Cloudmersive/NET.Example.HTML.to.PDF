using Cloudmersive.APIClient.NET.DocumentAndDataConvert.Api;
using Cloudmersive.APIClient.NET.DocumentAndDataConvert.Client;
using Cloudmersive.APIClient.NET.VirusScan.Api;
using Cloudmersive.APIClient.NET.VirusScan.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HTMLtoPDFDemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Configure API key authorization: Apikey
            Configuration.Default.AddApiKey("Apikey", "YOUR_API_KEY");



            var apiInstance = new ConvertDocumentApi();
            var inputFile = new System.IO.FileStream(txtSource.Text, System.IO.FileMode.Open); // System.IO.Stream | Input file to perform the operation on.

            byte[] result = apiInstance.ConvertDocumentHtmlToPdf(inputFile);

            File.WriteAllBytes(txtOutput.Text, result);

            inputFile.Close();
        }

        private void btnScanForViruses_Click(object sender, RoutedEventArgs e)
        {
            var apiInstance = new ScanApi();

            apiInstance.Configuration.AddApiKey("Apikey", "YOUR_API_KEY");

            var inputFile = new System.IO.FileStream(txtSource.Text, System.IO.FileMode.Open); // System.IO.Stream | Input file to perform the operation on.
            var allowExecutables = true;  // bool? | Set to false to block executable files (program code) from being allowed in the input file.  Default is false (recommended). (optional) 
            var allowInvalidFiles = true;  // bool? | Set to false to block invalid files, such as a PDF file that is not really a valid PDF file, or a Word Document that is not a valid Word Document.  Default is false (recommended). (optional) 
            var allowScripts = true;  // bool? | Set to false to block script files, such as a PHP files, Pythong scripts, and other malicious content or security threats that can be embedded in the file.  Set to true to allow these file types.  Default is false (recommended). (optional) 
            var restrictFileTypes = "";  // string | Specify a restricted set of file formats to allow as clean as a comma-separated list of file formats, such as .pdf,.docx,.png would allow only PDF, PNG and Word document files.  All files must pass content verification against this list of file formats, if they do not, then the result will be returned as CleanResult=false.  Set restrictFileTypes parameter to null or empty string to disable; default is disabled. (optional) 

            VirusScanAdvancedResult result = apiInstance.ScanFileAdvanced(inputFile, allowExecutables, allowInvalidFiles, allowScripts, restrictFileTypes);

            if (result.CleanResult.Value)
            {
                MessageBox.Show("File is clean.");
            }
            else
            {
                MessageBox.Show("File is virus infected!");
            }

            inputFile.Close();
        }
    }
}
