using System.IO;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace DZ._11._12._23Task4
{
    public partial class MainWindow : Window
    {
        List<string> strings = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addsource_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == WinForms.DialogResult.OK)
            {
                SourcePath.Text = folderBrowser.SelectedPath;
            }

        }

        private void addreceiver_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == WinForms.DialogResult.OK)
            {
                ReceiverPath.Text = folderBrowser.SelectedPath;
            }
        }

        private void perenooc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                otchet.Items.Clear();
                strings.Clear();
                DirectoryInfo Sourcefolder = new DirectoryInfo(SourcePath.Text);
                DirectoryInfo Receiverfolder = new DirectoryInfo(ReceiverPath.Text);
                Task task = Task.Run(() => Transfer(Sourcefolder, Receiverfolder));
                Task.WaitAll(task);

                foreach (string s in strings)
                {
                    PrintOtchet(s);
                }
                if (strings.Count == 0) {
                    otchet.Items.Add("Все файлы уже находяться в папке");
                }

            }
            catch
            {
                WinForms.MessageBox.Show("Выбирите папку!!", "Info", MessageBoxButtons.OK);
            }


        }


        private void Transfer(DirectoryInfo source, DirectoryInfo receiver)
        {
            var sourceList = source.GetFiles();
            var receiverList = receiver.GetFiles();
            foreach (var file in sourceList)
            {
                if (receiverList.FirstOrDefault(x => x.FullName.Substring(x.FullName.LastIndexOf('\\') + 1) == file.FullName.Substring(file.FullName.LastIndexOf('\\') + 1)) == null)
                {
                    File.Copy(file.FullName, $"{receiver.FullName}{file.FullName.Substring(file.FullName.LastIndexOf('\\'))}");
                    strings.Add($"Файл: {file.FullName.Substring(file.FullName.LastIndexOf('\\') + 1)} скопирован в :{receiver.FullName.Substring(receiver.FullName.LastIndexOf('\\') + 1)}");
                }
            }
        }

        private void PrintOtchet(string otch)
        {
            Dispatcher.Invoke(() => otchet.Items.Add(otch));
        }
    }
}