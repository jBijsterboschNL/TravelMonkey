using Akavache;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using TravelMonkey.Models;

namespace TravelMonkey.Data
{
    public class PersistentDataStore
    {
        private static readonly IBlobCache Cache = Akavache.BlobCache.UserAccount;

        private const string PictureEntriesKey = "PictureEntries";
        public static ObservableCollection<PictureEntry> Pictures { get; set; } = new ObservableCollection<PictureEntry>();

        public static async Task<IEnumerable<PictureEntry>> GetPictures()
        {
            return await Cache.GetAllObjects<PictureEntry>();
        }

        public static async Task AddPicture(string description, byte[] picture)
        {
            var entry = new PictureEntry(description, picture);
            await Cache.InsertObject(entry.Id.ToString(), entry);
        }

        public static async Task RemovePicture(Guid id)
        {
            await Cache.InvalidateObject<PictureEntry>(id.ToString());
        }
    }
}