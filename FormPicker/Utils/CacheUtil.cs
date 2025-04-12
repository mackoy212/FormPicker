using FormPicker.Objects;
using Microsoft.Win32;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Environments;
using Mutagen.Bethesda.Plugins.Aspects;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Skyrim;
using System.Diagnostics;
using System.IO;

namespace FormPicker.Utils
{
    public static class CacheUtil
    {
        public static string PATH_DATA
        {
            get => _PATH_DATA;
        }
        public static string PATH_CACHE
        {
            get => _PATH_CACHE;
        }

        public const string EXTENSION_CACHE = "cache";

        private static string _PATH_DATA;
        private static string _PATH_CACHE;

        static CacheUtil()
        {
            using (RegistryKey? key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Bethesda Softworks\Skyrim Special Edition"))
            {
                var path = key?.GetValue("Installed Path")?.ToString() ?? throw new Exception("Skyrim SE is not installed.");
                _PATH_DATA = Path.Combine(path, "Data");
            }
            _PATH_CACHE = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache");
        }

        public static string LoadData()
        {
            var timer = new Stopwatch();
            timer.Start();

            using var env = GameEnvironment.Typical.Builder<ISkyrimMod, ISkyrimModGetter>(GameRelease.SkyrimSE)
                .TransformLoadOrderListings(mods => mods.Where(mod => mod.Enabled))
                .Build();

            if (!Path.Exists(PATH_CACHE)) Directory.CreateDirectory(PATH_CACHE);

            foreach (var mod in env.LinkCache.ListedOrder)
            {
                var modName = mod.ModKey.FileName;

                Data.Mods[modName] = new ModItem(modName, true, mod.IsSmallMaster);

                var fileCache = Path.Combine(PATH_CACHE, $"{modName}.{EXTENSION_CACHE}");

                if (!File.Exists(fileCache))
                {
                    /*Console.WriteLine($"Generate cache file for {modName}");*/
                    CreateCache(fileCache, mod);
                }
                else
                {
                    var modCreateDateTime = GetModLastWriteTime(mod);
                    var cacheCreateDateTime = GetCacheCreationDateTime(fileCache);

                    if (!modCreateDateTime.Equals(cacheCreateDateTime))
                    {
                        /*Console.WriteLine($"Not equals:\n{modCreateDateTime}\n{cacheCreateDateTime}");
                        Console.WriteLine($"Regenerate cache file for {modName}");*/
                        CreateCache(fileCache, mod);
                    }
                    else
                    {
                        /*Console.WriteLine($"Equals: {modName}\n{modCreateDateTime}\n{cacheCreateDateTime}");*/
                    }
                }

                ReadCache(fileCache);
            }

            Data.Mods.TrimExcess();
            Data.Records.TrimExcess();

            timer.Stop();

            return $"Loading time: {timer.ElapsedMilliseconds / 1000.0f}\n"
                 + $"Records count: {Data.Records.Count}";
        }

        private static void ReadCache(string path)
        {
            if (new FileInfo(path).Length <= 8) return; // File contains only DateTime

            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new BinaryReader(stream);

            reader.ReadInt64();

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                var data = reader.ReadString().Split('|');
                Data.Records.Add(new RecordItem(data[0], data[1], data[2], data[3]));
            }
        }

        private static void CreateCache(string path, IModGetter mod)
        {
            using var stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new BinaryWriter(stream);

            writer.Write(GetModLastWriteTime(mod).Ticks);

            foreach (var record in mod.EnumerateMajorRecords())
            {
                string? editorID = record.EditorID;
                string? name = record is ITranslatedNamedRequiredGetter named ? RecordUtil.GetName(named) : null;

                if (!string.IsNullOrWhiteSpace(editorID) || !string.IsNullOrWhiteSpace(name))
                {
                    writer.Write($"{record.Type.Name}|{record.FormKey}|{editorID}|{name}");
                }
            }
        }

        //

        private static DateTime GetModLastWriteTime(IModGetter mod)
        {
            return File.GetLastWriteTime(Path.Combine(PATH_DATA, mod.ModKey.FileName));
        }

        private static DateTime GetCacheCreationDateTime(string path)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new BinaryReader(stream);
            return new DateTime(reader.ReadInt64());
        }
    }
}
