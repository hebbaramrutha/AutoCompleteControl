namespace AutoCompleteControl.AutoComplete
{
    public class AutoCompleteEntry
    {
        private string _id;
        private string _displayString;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string DisplayName
        {
            get { return _displayString; }
            set { _displayString = value; }
        }

        public AutoCompleteEntry(string name, string id)
        {
            _displayString = name;
            _id = id;
        }
    }
}
