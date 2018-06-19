using AutoCompleteControl.AutoComplete.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace AutoCompleteControl.AutoComplete
{
    /// <summary>
    /// Interaction logic for AutoCompleteUC.xaml
    /// </summary>
    public partial class AutoCompleteUC : UserControl
    {
        private bool _insertText;
        private int _delayTime;
        private int _searchThreshold;
        private int _maxLength;
        private List<AutoCompleteEntry> _autoCompletionList;
        private readonly System.Timers.Timer _keypressTimer;
        public Action<string> OnSearch { get; set; }

        public Action OnClear { get; set; }

        public Action<Collection<AutoCompleteEntry>> OnSelection { get; set; }

        public Action OnFocus { get; set; }

        public bool IsMultiSelect { get; set; }

        public double AutoBoxWidth
        {
            get { return (double)GetValue(AutoBoxWidthProperty); }
            set { SetValue(AutoBoxWidthProperty, value); }
        }

        public int AutoBoxHeight { get; set; } = 23;

        public int CompleteBoxHeight { get; set; } = 35;

        public double LabelWidth { get; set; } = 50;

        public ObservableCollection<AutoCompleteEntry> Selections
        {
            get { return (ObservableCollection<AutoCompleteEntry>)GetValue(SelectionsProperty); }
            set { SetValue(SelectionsProperty, value); }
        }

        public string AutoBoxWatermark
        {
            get { return (string)GetValue(AutoBoxWatermarkProperty); }
            set { SetValue(AutoBoxWatermarkProperty, value); }
        }

        public int AutoBoxTabIndex
        {
            get { return (int)GetValue(AutoBoxTabIndexProperty); }
            set { SetValue(AutoBoxTabIndexProperty, value); }
        }

        static FrameworkPropertyMetadata selectionsPropertymetadata = new FrameworkPropertyMetadata(new ObservableCollection<AutoCompleteEntry>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(Selections_Changed));

        public static readonly DependencyProperty SelectionsProperty = DependencyProperty.Register("Selections", typeof(ObservableCollection<AutoCompleteEntry>), typeof(AutoCompleteUC), selectionsPropertymetadata, null);

        static FrameworkPropertyMetadata autoBoxWidthMetaData = new FrameworkPropertyMetadata(Double.Parse("200"), FrameworkPropertyMetadataOptions.AffectsMeasure, null);

        public static readonly DependencyProperty AutoBoxWidthProperty = DependencyProperty.Register("AutoBoxWidth", typeof(double), typeof(AutoCompleteUC), autoBoxWidthMetaData, null);

        static FrameworkPropertyMetadata autoBoxWatermarkMetaData = new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.None, null);

        public static readonly DependencyProperty AutoBoxWatermarkProperty = DependencyProperty.Register("AutoBoxWatermark", typeof(string), typeof(AutoCompleteUC), autoBoxWatermarkMetaData, null);

        static FrameworkPropertyMetadata autoBoxTabIndexMetaData = new FrameworkPropertyMetadata(int.Parse("0"), FrameworkPropertyMetadataOptions.AffectsMeasure, null);

        public static readonly DependencyProperty AutoBoxTabIndexProperty = DependencyProperty.Register("AutoBoxTabIndex", typeof(int), typeof(AutoCompleteUC), autoBoxTabIndexMetaData, null);
        public string Text
        {
            get { return TextBoxAutoComplete.Text; }
            set
            {
                _insertText = true;
                TextBoxAutoComplete.Text = value;
            }
        }

        public int DelayTime
        {
            get { return _delayTime; }
            set { _delayTime = value; }
        }

        public int Threshold
        {
            get { return _searchThreshold; }
            set { _searchThreshold = value; }
        }


        public int MaxTextLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }
        public void Clear()
        {
            Selections?.Clear();
            ResultPanel.Children.Clear();

            TextBoxAutoComplete.Text = null;
            LblNoResults.Content = null;

        }

        public void SetAutoCompleteFocus()
        {
            TextBoxAutoComplete.Focusable = true;
            TextBoxAutoComplete.Focus();
            Keyboard.Focus(TextBoxAutoComplete);
        }
        public List<AutoCompleteEntry> AutoCompletionList
        {
            get { return _autoCompletionList; }
            set
            {
                Dispatcher.InvokeAsync(() => { LblNoResults.Visibility = value == null ? Visibility.Visible : Visibility.Collapsed; });

                if (_autoCompletionList == value) return;

                _autoCompletionList = value;
                OnAutoCompletionListChanged();
            }
        }

        public AutoCompleteUC()
        {
            try
            {
                InitializeComponent();

                _autoCompletionList = new List<AutoCompleteEntry>();
                _searchThreshold = 2;        // default threshold to 2 char
                _maxLength = 16;

                // set up the key press timer
                _keypressTimer = new System.Timers.Timer();
                _keypressTimer.Elapsed += OnTimedEvent;

                Loaded += AutoCompleteControl_Loaded;
            }
            catch (Exception ex)
            {
            }
        }

        private void AutoCompleteControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxAutoComplete.Height = ComboAutoComplete.Height = AutoBoxHeight;
            BrdAuto.Width = ResultPanel.Width = AutoBoxWidth;
            BrdAuto.Height = ResultPanel.Height = CompleteBoxHeight;
            TextBoxAutoComplete.Width = BrdAuto.Width - 15;
            ComboAutoComplete.Width = BrdAuto.Width;
            ResultPanel.Visibility = IsMultiSelect ? Visibility.Visible : Visibility.Collapsed;
            TextBoxAutoComplete.TabIndex = AutoBoxTabIndex;
            ComboAutoComplete.Visibility = Visibility.Hidden;
            TextBoxAutoComplete.MaxLength = MaxTextLength;
        }

        private static bool CheckItemsHasChanged(NotifyCollectionChangedEventArgs e)
        {
            bool hasChanged = false;

            if (e.NewItems == null && e.OldItems == null)
            {
                hasChanged = false;
            }
            else
            {
                if ((e.NewItems != null && e.OldItems == null) || (e.NewItems == null && e.OldItems != null))
                {
                    hasChanged = true;
                }
                else if (e.NewItems != null && e.OldItems != null)
                {
                    var newItemCnt = 0;

                    while (hasChanged || newItemCnt < e.NewItems.Count)
                    {
                        var newItem = e.NewItems[newItemCnt];
                        var newItemAuto = newItem as AutoCompleteEntry;

                        if (newItemAuto == null)
                        {
                            hasChanged = true;
                            break;
                        }
                        else
                        {
                            var oldItemCnt = 0;

                            while (hasChanged || oldItemCnt < e.OldItems.Count)
                            {
                                var oldItem = e.OldItems[oldItemCnt];
                                var oldItemAuto = oldItem as AutoCompleteEntry;

                                if (oldItemAuto == null)
                                {
                                    hasChanged = true;
                                    break;
                                }

                                if (newItemAuto.Id != oldItemAuto.Id)
                                {
                                    hasChanged = true;
                                    break;
                                }

                                oldItemCnt++;
                            }
                        }

                        newItemCnt++;
                    }
                }
            }
            return hasChanged;
        }

        private void ComboAutoComplete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == ComboAutoComplete.SelectedItem) return;
            _insertText = true;
            var cbItem = (AutoCompleteEntry)ComboAutoComplete.SelectedItem;

            if (IsMultiSelect)
            {
                AddLabel(cbItem);
                Selections.Add(cbItem);
            }
            else
            {
                TextBoxAutoComplete.Text = cbItem.DisplayName;

                Selections.Clear();
                Selections.Add(cbItem);
            }

            ComboAutoComplete.Visibility = Visibility.Hidden;
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            _keypressTimer.Stop();
            Dispatcher.InvokeAsync(TextChanged, DispatcherPriority.Normal);
        }

        private void TextBoxAutoComplete_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // text was not typed, do nothing and consume the flag
            if (_insertText == true) _insertText = false;

            // if the delay time is set, delay handling of text changed
            else
            {
                if (_delayTime > 0)
                {
                    _keypressTimer.Interval = _delayTime;
                    _keypressTimer.Start();
                }
                else
                    TextChanged();
            }
        }

        private void TextChanged()
        {
            try
            {
                if (TextBoxAutoComplete.Text.Length > 0)
                {
                    TextBoxAutoComplete.SelectionStart = TextBoxAutoComplete.Text.Length;
                    TextBoxAutoComplete.SelectionLength = 0;
                }
                ComboAutoComplete.Items.Clear();

                if (!IsMultiSelect)
                    Selections.Clear();

                var searchText = GetSearchText();

                if (TextBoxAutoComplete.Text.Length < _searchThreshold)
                {
                    ComboAutoComplete.IsDropDownOpen = false;
                    OnClear?.Invoke();
                    OnFocus?.Invoke();
                    return;
                }

                if (string.IsNullOrEmpty(searchText)) return;

                TextBoxAutoComplete.IsEnabled = false;

                if (OnSearch != null)
                {
                    if (ImgLoader != null)
                        ImgLoader.Visibility = Visibility.Visible;

                    var searchTask = Task.Run(() =>
                    {
                        OnSearch?.Invoke(searchText);
                    }, CancellationToken.None);

                    searchTask.ContinueWith((t) =>
                    {
                        TextBoxAutoComplete.IsEnabled = true; TextBoxAutoComplete.Focus();

                        Task.Delay(2000)
                            .ContinueWith((delayTask) => { ImgLoader.Visibility = Visibility.Collapsed; },
                                TaskScheduler.FromCurrentSynchronizationContext());

                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }

            }
            catch { }
        }

        private void AddLabel(AutoCompleteEntry addedEntry)
        {
            string truncatedContent = "";
            short charCount = 1;

            var labelContent = addedEntry.DisplayName;

            foreach (var lblContent in labelContent)
            {
                if (charCount <= 4)
                    truncatedContent = $"{truncatedContent}{lblContent}";

                if (charCount == 4 || (charCount < 4 && charCount == labelContent.Length))
                    truncatedContent = $"{truncatedContent}...";

                charCount++;
            }
            var selectedText = new Label { Content = truncatedContent, ToolTip = labelContent, Width = LabelWidth };
            var lblStyle = FindResource("AutoLabelStyle") as Style;

            if (lblStyle != null)
                selectedText.Style = lblStyle;

            ResultPanel.Children.Add(selectedText);
            TextBoxAutoComplete.Text = "";
        }

        private string GetSearchText()
        {
            if (string.IsNullOrEmpty(TextBoxAutoComplete.Text)) return "";

            return TextBoxAutoComplete.Text;
        }

        private void OnAutoCompletionListChanged()
        {
            Dispatcher.Invoke(() =>
            {
                TextBoxAutoComplete.IsEnabled = true;
                TextBoxAutoComplete.Focus();

                if (_autoCompletionList != null)
                {
                    ComboAutoComplete.Items.Clear();

                    foreach (var entry in _autoCompletionList)
                    {
                        if (Selections.Any((sel => sel.Id == entry.Id))) continue;

                        ComboAutoComplete.Items.Add(entry);
                    }
                }

                if (ComboAutoComplete.HasItems)
                {
                    ComboAutoComplete.Visibility = Visibility.Visible;
                }
                else
                {
                    ComboAutoComplete.Visibility = Visibility.Hidden;
                }

                ComboAutoComplete.IsDropDownOpen = ComboAutoComplete.HasItems;

                if (ImgLoader != null) ImgLoader.Visibility = Visibility.Hidden;

            }, DispatcherPriority.Normal);
        }

        private void TextBoxAutoComplete_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Back && TextBoxAutoComplete.Text.Length == 0)
            {
                ToolTip = null;
            }

            if (e.Key == Key.Back && TextBoxAutoComplete.Text.Length > 0)
            {
                var textEntered = ((TextBox)sender);
                if (TextBoxAutoComplete.SelectionLength > 0)
                {
                    textEntered.Text = textEntered.Text.Remove(textEntered.Text.Length - TextBoxAutoComplete.SelectionLength);
                }
                else
                {
                    textEntered.Text = textEntered.Text.Remove(textEntered.Text.Length - 1);
                    TextBoxAutoComplete.Text = textEntered.Text;
                    TextBoxAutoComplete.SelectionStart = TextBoxAutoComplete.Text.Length;
                    TextBoxAutoComplete.SelectionLength = 0;
                    LblNoResults.Visibility = Visibility.Collapsed;
                    OnClear?.Invoke();
                    e.Handled = true;
                }

            }

            if (e.Key == Key.Down)
            {
                ComboAutoComplete.Focus();
            }

            if (e.Key == Key.Tab)
            {
                if (ComboAutoComplete.Items.Count == 1)
                {
                    _insertText = true;
                    ComboAutoComplete.Visibility = Visibility.Hidden;
                    var cbItem = (AutoCompleteEntry)ComboAutoComplete.Items[0];
                    TextBoxAutoComplete.Text = cbItem.DisplayName;
                    Selections.Clear();
                    Selections.Add(cbItem);
                }

            }
        }

        private void ComboAutoComplete_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (ComboAutoComplete.SelectedIndex == 0 && e.Key == Key.Up)
            {
                TextBoxAutoComplete.Focus();
            }
        }

        private void ComboAutoComplete_KeyDown(object sender, KeyEventArgs e)
        {
            if (ComboAutoComplete.SelectedIndex <= -1) return;

            if (e.Key == Key.Enter)
            {
                TextBoxAutoComplete.Text = ComboAutoComplete.SelectedItem.ToString();
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            var senderLabel = btn?.TemplatedParent as Label;

            if (senderLabel == null) return;

            ResultPanel.Children.Remove(senderLabel);

            var deletedItem = Selections.FirstOrDefault((selectedItem) => selectedItem.DisplayName == senderLabel.ToolTip.ToString());

            if (deletedItem != null)
                Selections.Remove(deletedItem);
        }

        private static void Selections_Changed(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
        {
            var instance = dobj as AutoCompleteUC;

            if (instance == null) return;

            instance.LblNoResults.Visibility = Visibility.Collapsed;//Initialize always

            var newCollection = e.NewValue as ObservableCollection<AutoCompleteEntry>;

            instance.ResultPanel.Children.Clear();

            if (newCollection != null && newCollection.Count >= 0)
            {
                newCollection.Each(item =>
                {
                    instance.AddLabel(item);
                });
            }

            var oldCollection = e.OldValue as ObservableCollection<AutoCompleteEntry>;

            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= instance.Selections_CollectionChanged;
            }

            if (newCollection != null)
            {
                newCollection.CollectionChanged += instance.Selections_CollectionChanged;
            }
        }

        private void Selections_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Selections == null)
            {
                ResultPanel.Children.Clear();
            }

            var hasChanged = CheckItemsHasChanged(e);

            if (hasChanged)
                OnSelection?.Invoke(Selections);
        }

        private void TextBoxAutoComplete_GotFocus(object sender, RoutedEventArgs e)
        {
            if (OnFocus != null && TextBoxAutoComplete.Text == "" && !IsMultiSelect)
            {
                ComboAutoComplete.IsDropDownOpen = true;
                OnFocus?.Invoke();
            }
        }

        private void TextBoxAutoComplete_LostFocus(object sender, RoutedEventArgs e)
        {
            var focused_element = FocusManager.GetFocusedElement(Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive));

            if (focused_element != null)
            {
                var elementType = focused_element.GetType();

                var elementName = ((FrameworkElement)focused_element).Name;

                if (elementType != null && elementName != null)
                {
                    if (elementType == typeof(ComboBoxItem) || elementName.Equals("ComboAutoComplete"))
                        ComboAutoComplete.IsDropDownOpen = true;
                    else
                        ComboAutoComplete.IsDropDownOpen = false;
                }
            }
        }

        private void TextBoxAutoComplete_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (OnFocus != null && TextBoxAutoComplete.Text == "")
            {
                ComboAutoComplete.IsDropDownOpen = true;
                OnFocus?.Invoke();
            }
        }
    }
}
