using System.IO;
using System.Windows;

namespace DZ._11._12._23
{

    public partial class MainWindow : Window
    {
        List<Task> tasks = new List<Task>();
        List<string> strings = new List<string>();
        char[] delimiterChars = { '?', '!', '.' };
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void analiz_Click(object sender, RoutedEventArgs e)
        {
            tester.Items.Clear();
            if (offers.IsChecked == true)
            {
                tasks.Add(new Task(CountOffers));
            }
            
            if (characters.IsChecked == true)
            {
                tasks.Add(new Task(CountCharacters));
            }
           
            if (words.IsChecked == true)
            {
                tasks.Add(new Task(CountWords));
            }
           
            if (interrogative.IsChecked == true)
            {
                tasks.Add(new Task(CountInterrogativeOffers));
            }           
            if (exclamatory.IsChecked == true)
            {
                tasks.Add(new Task(CountExclamationOffers));
            }

            foreach (var task in tasks)
            {
                task.Start();
            }

            await Task.WhenAll(tasks);
            Input();
            strings.Clear();
            tasks.Clear();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }          
     

        private void CountOffers()
        {
            string myText = "";
            Dispatcher.Invoke(() => inpuTexts.SelectAll());
            Dispatcher.Invoke(() => { myText = inpuTexts.Selection.Text; });
            var res = myText.Split(delimiterChars).Select(x => x.Trim());
            strings.Add($"Количество предложений в тексте: {myText.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Count().ToString()}");           
        }

        private void CountInterrogativeOffers()
        {
            string myText = "";
            Dispatcher.Invoke(() => inpuTexts.SelectAll());
            Dispatcher.Invoke(() => { myText = inpuTexts.Selection.Text; });
            strings.Add($"Количество вопросительных предложений  в тексте: {myText.Count(x => x=='?').ToString()}");
        }

        private void CountExclamationOffers()
        {
            string myText = "";
            Dispatcher.Invoke(() => inpuTexts.SelectAll());
            Dispatcher.Invoke(() => { myText = inpuTexts.Selection.Text; });
            strings.Add($"Количество восклицательных предложений  в тексте: {myText.Count(x => x == '!').ToString()}");
        }


        private void CountCharacters()
        {
            string myText = "";
            Dispatcher.Invoke(() => inpuTexts.SelectAll());
            Dispatcher.Invoke(() => { myText = inpuTexts.Selection.Text; });
            strings.Add($"Количество символов в тексте: {myText.Trim().Length.ToString()}");
        }

        private void CountWords()
        {
            string myText = "";
            Dispatcher.Invoke(() => inpuTexts.SelectAll());
            Dispatcher.Invoke(() => { myText = inpuTexts.Selection.Text; });
            strings.Add($"Количество слов в тексте: {myText.Split(" ").Length.ToString()}");
        }

        private void Input()
        {
            if (monitor.IsChecked == true)
            {
                foreach (var s in strings)
                {
                    Thread.Sleep(3000);
                    tester.Items.Add(s);
                }
            }
            if (file.IsChecked == true)
            {
                using (StreamWriter writer = new StreamWriter("Test.txt", false))
                {
                    foreach (var s in strings)
                    {
                        writer.WriteLine(s);
                    }
                }
            }
        }
    }
}