using AutoCompleteControl.AutoComplete;
using AutoCompleteControl.AutoComplete.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AutoCompleteControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainVM _mainVM;

        public MainWindow()
        {
            InitializeComponent();

            _mainVM = new MainVM();
            DataContext = _mainVM;

            CityAutoComplete.OnSearch = GetCityForSearchText;
            CityAutoComplete.OnSelection = OnCitySelection;
            CityAutoComplete.OnClear = ClearSelectedCity;
            CityAutoComplete.OnFocus = GetAllCities;


            CountryAutoComplete.OnSearch = GetCountryForSearchText;
            CountryAutoComplete.OnSelection = OnCountrySelection;
            CountryAutoComplete.OnClear = ClearSelectedCountry;
            CountryAutoComplete.OnFocus = GetAllCountries;
        }

        /// <summary>
        /// Get city for the selected text
        /// </summary>
        /// <param name="searchText">The search text.</param>
        private void GetCityForSearchText(string searchText)
        {
            try
            {
                if (searchText.IsNotNull() && !string.IsNullOrEmpty((searchText)))
                {
                    var autoCompleteTask = Task.Run(() =>
                    {
                        List<LookUpEntity> cityList = new List<LookUpEntity>();

                        if (_mainVM.CityList.IsCollectionValid())
                        {
                            var cities = _mainVM.CityList.Where(x =>
                                    (x.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)))
                                .Select(i => new { i.Name, i.Value }).ToList();

                            foreach (var item in cities)
                            {
                                cityList.Add(new LookUpEntity
                                {
                                    Name = item.Name,
                                    Value = item.Value
                                });
                            }
                        }

                        if (!cityList.IsCollectionValid())
                            return;

                        var autocompleteList = new List<AutoCompleteEntry>();

                        cityList.ForEach(keyWordItem =>
                        {
                            var entry = new AutoCompleteEntry(keyWordItem.Name, keyWordItem.Value);
                            autocompleteList.Add(entry);
                        });

                        CityAutoComplete.AutoCompletionList = autocompleteList;

                    });

                    autoCompleteTask.ContinueWith((t) =>
                    {
                        if (t.IsFaulted || t.Exception != null)
                        {
                            throw t.Exception.GetBaseException();
                        }
                    });
                }
                else
                { }
            }
            catch (Exception ex)
            {
            }

        }

        /// <summary>
        /// Clears the selected city.
        /// </summary>
        private void ClearSelectedCity()
        {
            if (CityAutoComplete.Text.Length == 0)
                CityAutoComplete.Clear();

            CityAutoComplete.ToolTip = null;
        }

        /// <summary>
        /// Gets the city on selection.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnCitySelection(Collection<AutoCompleteEntry> obj)
        {
            if (obj.IsCollectionValid())
            {
                CityAutoComplete.TextBoxAutoComplete.Focusable = true;
                CityAutoComplete.TextBoxAutoComplete.Focus();
            }
        }

        /// <summary>
        /// Method to set selected text for auto complete control.
        /// </summary>
        /// <param name="cityCode"></param>
        private void SetSelectedCustomerCountry(string cityCode)
        {
            if (cityCode.IsNotEmpty() && _mainVM.CityList.IsCollectionValid())
            {
                var selectedCountry = _mainVM.CityList.Where(c => c.Value.EqualsIgnoreCase(cityCode))
                                                     .FirstOrDefault();
                if (selectedCountry.IsNotNull())
                    CityAutoComplete.Text = selectedCountry.Name;
            }
            else
                CityAutoComplete.Text = string.Empty;
        }

        /// <summary>
        /// Method to get all cities on focus.
        /// </summary>
        private void GetAllCities()
        {
            Task.Run(() =>
            {
                List<LookUpEntity> cityList = new List<LookUpEntity>();

                if (_mainVM.CityList.IsCollectionValid())
                {
                    var cities = _mainVM.CityList.ToList();

                    foreach (var item in cities)
                    {
                        cityList.Add(new LookUpEntity
                        {
                            Name = item.Name,
                            Value = item.Value
                        });
                    }
                }

                if (!cityList.IsCollectionValid())
                    return;

                var autocompleteList = new List<AutoCompleteEntry>();

                cityList.ForEach(keyWordItem =>
                {
                    var entry = new AutoCompleteEntry(keyWordItem.Name, keyWordItem.Value);
                    autocompleteList.Add(entry);
                });

                CityAutoComplete.AutoCompletionList = autocompleteList;
            });
        }


        /// <summary>
        /// Get country for the selected text
        /// </summary>
        /// <param name="searchText">The search text.</param>
        private void GetCountryForSearchText(string searchText)
        {
            try
            {
                if (searchText.IsNotNull() && !string.IsNullOrEmpty((searchText)))
                {
                    var autoCompleteTask = Task.Run(() =>
                    {
                        List<LookUpEntity> countryList = new List<LookUpEntity>();

                        if (_mainVM.CountryList.IsCollectionValid())
                        {
                            var countries = _mainVM.CityList.Where(x =>
                                    (x.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)))
                                .Select(i => new { i.Name, i.Value }).ToList();

                            foreach (var item in countries)
                            {
                                countryList.Add(new LookUpEntity
                                {
                                    Name = item.Name,
                                    Value = item.Value
                                });
                            }
                        }

                        if (!countryList.IsCollectionValid())
                            return;

                        var autocompleteList = new List<AutoCompleteEntry>();

                        countryList.ForEach(keyWordItem =>
                        {
                            var entry = new AutoCompleteEntry(keyWordItem.Name, keyWordItem.Value);
                            autocompleteList.Add(entry);
                        });

                        CountryAutoComplete.AutoCompletionList = autocompleteList;

                    });

                    autoCompleteTask.ContinueWith((t) =>
                    {
                        if (t.IsFaulted || t.Exception != null)
                        {
                            throw t.Exception.GetBaseException();
                        }
                    });
                }
                else
                { }
            }
            catch (Exception ex)
            {
            }

        }

        /// <summary>
        /// Clears the selected country.
        /// </summary>
        private void ClearSelectedCountry()
        {
            if (CountryAutoComplete.Text.Length == 0)
                CountryAutoComplete.Clear();

            CountryAutoComplete.ToolTip = null;
        }

        /// <summary>
        /// Gets the country on selection.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnCountrySelection(Collection<AutoCompleteEntry> obj)
        {
            if (obj.IsCollectionValid())
            {
                CountryAutoComplete.TextBoxAutoComplete.Focusable = true;
                CountryAutoComplete.TextBoxAutoComplete.Focus();
            }
        }

        /// <summary>
        /// Method to set selected text for auto complete control.
        /// </summary>
        /// <param name="countryCode"></param>
        private void SetSelectedCountry(string countryCode)
        {
            if (countryCode.IsNotEmpty() && _mainVM.CountryList.IsCollectionValid())
            {
                var selectedCountry = _mainVM.CountryList.Where(c => c.Value.EqualsIgnoreCase(countryCode))
                                                     .FirstOrDefault();
                if (selectedCountry.IsNotNull())
                    CountryAutoComplete.Text = selectedCountry.Name;
            }
            else
                CountryAutoComplete.Text = string.Empty;
        }

        /// <summary>
        /// Method to get all countries on focus.
        /// </summary>
        private void GetAllCountries()
        {
            Task.Run(() =>
            {
                List<LookUpEntity> countryList = new List<LookUpEntity>();

                if (_mainVM.CountryList.IsCollectionValid())
                    countryList.AddRange(_mainVM.CountryList);

                if (!countryList.IsCollectionValid())
                    return;

                var autocompleteList = new List<AutoCompleteEntry>();

                countryList.ForEach(keyWordItem =>
                {
                    var entry = new AutoCompleteEntry(keyWordItem.Name, keyWordItem.Value);
                    autocompleteList.Add(entry);
                });

                CountryAutoComplete.AutoCompletionList = autocompleteList;
            });
        }


        /// <summary>
        /// This method has been added only for testing the autocomplete selected value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_mainVM.AutoCompleteSelectedCity.IsCollectionValid())
                MessageBox.Show(_mainVM.AutoCompleteSelectedCity.FirstOrDefault().Id);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_mainVM.AutoCompleteSelectedCountry.IsCollectionValid())
            {
                string autoCompleteValues = string.Join(Environment.NewLine, _mainVM.AutoCompleteSelectedCountry.Select(x=>x.Id));
                MessageBox.Show(autoCompleteValues);
            }
        }
    }
}
