namespace FormPicker.Objects
{
    public readonly struct RecordItem
    {
        public string Type { get; }
        public FormKey FormKey { get; }
        public string FormID => FormKey.FormID;
        public string? EditorID { get; }
        public string? Name { get; }

        public RecordItem(string type, string formKey, string? editorID, string? name)
        {
            Type = string.Intern(type);
            FormKey = new FormKey(formKey);
            EditorID = editorID;
            Name = name != null ? string.Intern(name) : null;            
        }

        public override int GetHashCode() => FormKey.GetHashCode();

        public override bool Equals(object? obj) => obj is RecordItem other && FormKey.Equals(other.FormKey);
    }
}