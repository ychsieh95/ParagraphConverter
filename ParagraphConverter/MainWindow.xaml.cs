using IniParser;
using IniParser.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace ParagraphConverter
{
    public partial class MainWindow : Window
    {
        private string configFilePath = "ParagraphConverter.ini";
        private string tempClipboardText = "";
        private DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += timer_Tick;

            if (System.IO.File.Exists(configFilePath))
            {
                IniData data = new FileIniDataParser().ReadFile(configFilePath);
                if (bool.TryParse(data["CONFIG"]["ListenClipboard"], out bool listenClipboard))
                    listenClipboardMenuItem.IsChecked = listenClipboard;
                if (bool.TryParse(data["CONFIG"]["AppendText"], out bool appendText))
                    appendTextModeMenuItem.IsChecked = appendText;
                if (bool.TryParse(data["CONFIG"]["AutoConvert"], out bool autoConvert))
                    autoConvertMenuItem.IsChecked = autoConvert;
                if (bool.TryParse(data["CONFIG"]["AutoCopyToClipboard"], out bool autoCopyToClipboard))
                    autoCopyToClipboardMenuItem.IsChecked = autoCopyToClipboard;
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            IniData data = new IniData();
            data["CONFIG"]["ListenClipboard"] = listenClipboardMenuItem.IsChecked.ToString();
            data["CONFIG"]["AppendText"] = appendTextModeMenuItem.IsChecked.ToString();
            data["CONFIG"]["AutoConvert"] = autoConvertMenuItem.IsChecked.ToString();
            data["CONFIG"]["AutoCopyToClipboard"] = autoCopyToClipboardMenuItem.IsChecked.ToString();
            new FileIniDataParser().WriteFile(configFilePath, data);
        }

        private void listenClipboardMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            tempClipboardText = "";
            Clipboard.Clear();
            timer.IsEnabled = ((MenuItem)sender).IsChecked;
        }

        private void listenClipboardMenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            timer.IsEnabled = ((MenuItem)sender).IsChecked;
        }
        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Listen clipboard
            if (tempClipboardText != Clipboard.GetText())
            {
                // Append text or replace it
                if (appendTextModeMenuItem.IsChecked)
                    contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(Clipboard.GetText())));
                else
                {
                    contentRichTextBox.Document.Blocks.Clear();
                    contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(Clipboard.GetText())));
                }

                // Auto convert
                if (autoConvertMenuItem.IsChecked)
                    convertButton_Click(null, null);

                // Auto copy to clipboard
                if (autoCopyToClipboardMenuItem.IsChecked)
                    Clipboard.SetDataObject(new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd).Text);

                // Replace temp text
                tempClipboardText = Clipboard.GetText();
            }
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            string content = "";

            string test = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd).Text;

            bool isHyphen = false;
            foreach (string line in (
                new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd).Text)
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                content += (isHyphen ? "" : " ") + line.TrimEnd('-');
                isHyphen = line.Last().Equals('-');
            }
            content = content.Trim(' ');
            while (content.Contains("  "))
                content = content.Replace("  ", " ");

            contentRichTextBox.Document.Blocks.Clear();
            contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(content)));
        }

        private void copyToClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd).Text);
        }

        private void clearClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.Clear();
        }
    }
}
