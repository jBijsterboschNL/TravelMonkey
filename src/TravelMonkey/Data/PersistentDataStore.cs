using Akavache;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using TravelMonkey.Models;

namespace TravelMonkey.Data
{
    public class PersistentDataStore
    {
        private static readonly IBlobCache Cache = Akavache.BlobCache.UserAccount;

        public static async Task<IEnumerable<Destination>> GetDestinations()
        {
            var x = await Cache.GetAllKeys();
            return await Cache.GetAllObjects<Destination>();
        }

        public static async Task AddOrUpdateDestination(Destination destination)
        {
            await Cache.InsertObject(destination.Id.ToString(), destination);

            var x = await Cache.GetAllKeys();
        }

        public static async Task RemoveDestination(Guid id)
        {
            await Cache.InvalidateObject<Destination>(id.ToString());
        }
    }
}