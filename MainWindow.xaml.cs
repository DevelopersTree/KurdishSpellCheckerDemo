using LevenshteinDistance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpellCheckerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Word> _dictionary;
        public MainWindow()
        {
            InitializeComponent();

            _dictionary = File.ReadAllLines("wordlist.txt").Select(l =>
            {
                var parts = l.Split(',');
                var word = new Word();
                word.Value = parts[0];
                word.Frequencey = int.Parse(parts[1]);

                return word;
            }).Take(100_000).ToList();
        }

        private void txtWord_TextChanged(object sender, TextChangedEventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var suggestions = _dictionary.OrderBy(w => Lavenshtein.GetDistanceTwoRows(w.Value, txtWord.Text))
                                             .ThenByDescending(w => w.Frequencey)
                                             .Take(10)
                                             .Select(w => w.Value).ToList();

            stopwatch.Stop();
            Debug.WriteLine($"{stopwatch.ElapsedMilliseconds:n0}");
            ellapsed.Text = $"Time ellapsed: {stopwatch.ElapsedMilliseconds:n0} ms";

            listBox.ItemsSource = suggestions;
        }
    }
}
