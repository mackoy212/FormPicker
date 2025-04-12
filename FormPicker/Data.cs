using FormPicker.Objects;

namespace FormPicker
{
    public static class Data
    {
        public static HashSet<RecordItem> Records { get; } = new(200000);

        // ModName, ModItem
        public static Dictionary<string, ModItem> Mods { get; } = new(100);

        // ModItem, Index
        public static Dictionary<ModItem, string> ModsIndexs
        {
            get
            {
                Dictionary<ModItem, string> result = new();

                int esp = 0;
                int esl = 0;

                foreach (var mod in Mods.Values)
                {
                    if (!mod.IsEnabled) continue;

                    if (mod.IsESL)
                    {
                        result[mod] = "FE:" + esl.ToString("X3");
                        esl++;
                    }
                    else
                    {
                        result[mod] = esp.ToString("X2");
                        esp++;
                    }
                }

                return result;
            }
        }
    }
}
