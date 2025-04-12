namespace FormPicker.Objects
{
    public class ModItem
    {
        public string ModName { get; }
        public string Index => Data.ModsIndexs[this];
        public bool IsEnabled { get; set; }
        public bool IsESL { get; }

        public ModItem(string modName, bool isEnabled, bool isESL) 
        {
            ModName = modName;
            IsEnabled = isEnabled;
            IsESL = isESL;
        }

        public override int GetHashCode() => ModName.GetHashCode();

        public override bool Equals(object? obj) => obj is ModItem other && ModName == other.ModName;
    }
}