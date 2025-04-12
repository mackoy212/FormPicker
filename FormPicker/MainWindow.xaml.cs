using FormPicker.Objects;
using FormPicker.Utils;
using Noggog;
using System.Collections.ObjectModel;
using System.Data;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FormPicker
{
    public partial class MainWindow : Window
    {
        [DllImport("Kernel32")]
        private static extern void AllocConsole();

        private bool DEBUG = false;

        public enum FilterRules : byte
        {
            EditorID = 0,
            Name = 1,
            ModName = 2,
            ID = 3
        }

        private FilterRules FilterRule { get; set; } = FilterRules.EditorID;
        
        private StringComparison CaseSensitivity { get; set; } = StringComparison.Ordinal;

        public ObservableCollection<RecordItem> FilteredRecords { get; } = new();

        public ObservableCollection<ModItem> FilteredMods { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            if (DEBUG) AllocConsole();

            InfoBlock.Text = CacheUtil.LoadData();

            //UpdateFilteredMods();

            RuleBox.ItemsSource = Enum.GetNames(typeof(FilterRules));
            RuleBox.SelectedIndex = (int)FilterRule;
            CaseBox.IsChecked = CaseSensitivity == StringComparison.Ordinal;
        }

        // Events

        private void RuleBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                FilterRule = (FilterRules)comboBox.SelectedIndex;
                ApplyFilterRecords();
            }
        }

        private void CaseBox_Clicked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                CaseSensitivity = checkBox.IsChecked == true
                    ? StringComparison.Ordinal
                    : StringComparison.OrdinalIgnoreCase;

                ApplyFilterRecords();
            }
        }

        private void CopyFormKey_Click(object sender, RoutedEventArgs e)
        {
            string clipboard = string.Empty;

            foreach (RecordItem recordItem in RecordsDataGrid.SelectedItems)
            {
                clipboard += $"{recordItem.FormKey}\n";
            }

            Clipboard.SetText(clipboard);
        }

        private void CopyEditorID_Click(object sender, RoutedEventArgs e)
        {
            string clipboard = string.Empty;

            foreach (RecordItem recordItem in RecordsDataGrid.SelectedItems)
            {
                clipboard += $"{recordItem.EditorID}\n";
            }

            Clipboard.SetText(clipboard);
        }

        private void CopyName_Click(object sender, RoutedEventArgs e)
        {
            string clipboard = string.Empty;

            foreach (RecordItem recordItem in RecordsDataGrid.SelectedItems)
            {
                clipboard += $"{recordItem.Name}\n";
            }

            Clipboard.SetText(clipboard);
        }

        private void CopyRow_Click(object sender, RoutedEventArgs e)
        {
            string clipboard = string.Empty;

            foreach (RecordItem recordItem in RecordsDataGrid.SelectedItems)
            {
                clipboard += $"{recordItem.FormKey}\t{recordItem.EditorID}\t{recordItem.Name}\n";
            }

            Clipboard.SetText(clipboard);
        }

        private void CopyFormID_Click(object sender, RoutedEventArgs e)
        {
            string clipboard = string.Empty;

            foreach (RecordItem recordItem in RecordsDataGrid.SelectedItems)
            {
                clipboard += $"{recordItem.FormID}\n";
            }

            Clipboard.SetText(clipboard);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ModsDataGrid.IsVisible) ModsDataGrid.Visibility = Visibility.Hidden;
            else ModsDataGrid.Visibility = Visibility.Visible;
        }

        private void ModsCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is ModItem mod)
            {
                mod.IsEnabled = checkBox.IsChecked ?? false;
                UpdateFilteredMods();
            }
        }

        private void InvertModsEnabled_Click(object sender, RoutedEventArgs e)
        {
            foreach (ModItem modItem in ModsDataGrid.SelectedItems)
            {
                modItem.IsEnabled = !modItem.IsEnabled;
            }
            UpdateFilteredMods();
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ApplyFilterRecords();
            }
        }

        //

        private void ApplyFilterRecords()
        {
            FilteredRecords.Clear();

            var input = SearchBox.Text;

            if (string.IsNullOrWhiteSpace(input)) return;

            var filtered = FilterRule switch
            {
                FilterRules.EditorID => Data.Records
                    .Where(r => r.EditorID?.AsSpan().IndexOf(input, CaseSensitivity) >= 0),
                FilterRules.Name => Data.Records
                    .Where(r => r.Name?.AsSpan().IndexOf(input, CaseSensitivity) >= 0),
                FilterRules.ModName => Data.Records
                    .Where(r => r.FormKey.Mod.ModName.AsSpan().IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0),
                FilterRules.ID => Data.Records
                    .Where(r => r.FormKey.ID.AsSpan().IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0),
                _ => Data.Records
            };

            FilteredRecords.AddRange(filtered);

            /*var stopWatch = new Stopwatch();
            stopWatch.Start();*/

            /*stopWatch.Stop();
            if (DEBUG) Console.WriteLine($"Filter time: {stopWatch.ElapsedMilliseconds / 1000.0f}");*/

            if (RecordsDataGrid.SelectedItem != null) RecordsDataGrid.ScrollIntoView(RecordsDataGrid.SelectedItem);
        }

        private void UpdateFilteredMods()
        {
            FilteredMods.Clear();
            FilteredMods.AddRange(Data.Mods.Values);
        }
    }
}