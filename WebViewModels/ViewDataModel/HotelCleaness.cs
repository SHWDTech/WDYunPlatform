using System;

namespace WebViewModels.ViewDataModel
{
    public class HotelCleaness
    {
        public Guid DistrictGuid { get; set; }

        public string ProjectName { get; set; }

        public string ProjectCleaness { get; set; }
    }

    public class HotelLocations
    {
        public string Name { get; set; }

        public LocationPoint Point { get; set; } = new LocationPoint();

        public Guid Id { get; set; }

        public Guid DistrictGuid { get; set; }

        public string Status { get; set; }
    }

    public class LocationPoint
    {
        public string Longitude { get; set; }

        public string Latitude { get; set; }
    }
}
