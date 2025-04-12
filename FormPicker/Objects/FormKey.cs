namespace FormPicker.Objects
{
    public readonly struct FormKey
    {
        public ModItem Mod { get; }
        public string ID { get; }
        public string FormID => Mod.Index + ID;

        public FormKey(string formKey)
        {
            var formKeyData = formKey.Split(':');
            Mod = Data.Mods[formKeyData[1]];
            ID = formKeyData[0];
        }

        public override int GetHashCode() => ID.GetHashCode();

        public override bool Equals(object? obj) => obj is FormKey other && ID == other.ID;

        public override string ToString() => ID + ":" + Mod.ModName;
    }
}
