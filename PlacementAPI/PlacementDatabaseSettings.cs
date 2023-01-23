using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacementAPI
{
    public class PlacementDatabaseSettings : IPlacementDatabaseSettings
    {
        public string PlacementCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IPlacementDatabaseSettings
    {
        string PlacementCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
