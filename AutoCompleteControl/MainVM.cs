using AutoCompleteControl.AutoComplete;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoCompleteControl
{
    public class MainVM
    {
        /// <summary>
        /// Auto complete for selected city.
        /// </summary>
        public ObservableCollection<AutoCompleteEntry> AutoCompleteSelectedCity { get; set; } = new ObservableCollection<AutoCompleteEntry>();

        /// <summary>
        /// Auto complete for selected country(Multiselect).
        /// </summary>
        public ObservableCollection<AutoCompleteEntry> AutoCompleteSelectedCountry { get; set; } = new ObservableCollection<AutoCompleteEntry>();

        /// <summary>
        /// List of cities
        /// </summary>
        public List<LookUpEntity> CityList { get; set; }


        /// <summary>
        /// List of countries
        /// </summary>
        public List<LookUpEntity> CountryList { get; set; }

        public MainVM()
        {
            CityList = new List<LookUpEntity>()
            {
                new LookUpEntity{ Name ="Bangalore", Value="BLR"},
                new LookUpEntity{ Name ="Banaras", Value="BNS"},
                new LookUpEntity{ Name ="Mumbai", Value="MUM" },
                new LookUpEntity{ Name = "Mysore", Value="MYS"},
                new LookUpEntity{ Name ="Pune", Value="PNQ"},
                new LookUpEntity{ Name ="Chandigarh", Value="CHD"},
                new LookUpEntity{ Name ="Amritsar", Value="AMT" },
                new LookUpEntity{ Name = "Ahmedabad", Value="AMD"},
                new LookUpEntity{ Name ="Cochin", Value="COK"},
                new LookUpEntity{ Name ="Munnar", Value="MUN"},
                new LookUpEntity{ Name ="Allepey", Value="ALY" },
                new LookUpEntity{ Name = "Delhi", Value="DEL"},
            };

            CountryList = new List<LookUpEntity>()
            {
                new LookUpEntity{ Name ="India", Value="IND"},
                new LookUpEntity{ Name="Indonesia", Value="INS"},
                new LookUpEntity{ Name="Iceland",Value="ICL"},
                new LookUpEntity{ Name="Iran", Value="IRN"}
            };
        }
    }
}
