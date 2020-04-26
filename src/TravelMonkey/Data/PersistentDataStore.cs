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
            return await Cache.GetAllObjects<Destination>();
        }

        public static async Task AddDestination(Destination destination)
        {
            await Cache.InsertObject(destination.Id.ToString(), destination);
        }

        public static async Task RemoveDestination(Guid id)
        {
            await Cache.InvalidateObject<Destination>(id.ToString());
        }
    }
}