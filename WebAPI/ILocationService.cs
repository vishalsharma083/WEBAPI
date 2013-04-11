using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ComponentModel;

namespace WebAPI
{
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/getLocationBasedOnGeoPosition?geoLongitude={geoLongitude}&geoLatitude={geoLatitude}", ResponseFormat = WebMessageFormat.Json)]
        string getLocationBasedOnGeoPosition(double geoLongitude, double geoLatitude);

        [OperationContract]
        [WebGet(UriTemplate = "/getGeoPositionFromIPAddress?IpAdderss={IpAddress}", ResponseFormat = WebMessageFormat.Json)]
        string getGeoPositionFromIPAddress(string IpAddress);
    }
}
