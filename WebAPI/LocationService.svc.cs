using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Activation;
using System.Data.SqlClient;
using System.Web;
using System.Data;
using System.Net;
using System.IO;

namespace WebAPI
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LocationService : ILocationService
    {
        public string GetLocationBasedOnGeoPosition(double geoLongitude, double geoLatitude)
        {
            string geoDetails = string.Empty;
            if (geoLongitude < 1)
            {
                geoLongitude = 0;
            }
            if (geoLatitude < 1)
            {
                geoLatitude = 0;
            }
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString()))
            {
                conn.Open();
                string sqlSelect = "SELECT TOP 1 [Code],[AirportCityName],[CountryCode],[StateCode],[NickName],[TimeZone],[Longitude],[Latitude] FROM [DTAirportCity] where Latitude is not null ORDER BY ABS( Latitude - " + geoLatitude + " ) + ABS( Longitude - " + geoLongitude + " )";
                SqlCommand cmd = new SqlCommand(sqlSelect, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                geoDetails += "{";
                while (rdr.Read())
                {
                    geoDetails += "\"LocationCode\":";
                    geoDetails += "\"" + rdr["Code"] + "\",";
                    geoDetails += "\"LocationName\":";
                    geoDetails += "\"" + rdr["AirportCityName"] + "\",";
                    geoDetails += "\"CountryCode\":";
                    geoDetails += "\"" + rdr["CountryCode"] + "\",";
                    geoDetails += "\"StateCode\":";
                    geoDetails += "\"" + rdr["StateCode"] + "\",";
                    geoDetails += "\"LocationNickName\":";
                    geoDetails += "\"" + rdr["NickName"] + "\",";
                    geoDetails += "\"TimeZone\":";
                    geoDetails += "\"" + rdr["TimeZone"] + "\",";
                    geoDetails += "\"Longitude\":";
                    geoDetails += "\"" + rdr["Longitude"] + "\",";
                    geoDetails += "\"Latitude\":";
                    geoDetails += "\"" + rdr["Latitude"] + "\"";
                }
                geoDetails += "}";
                conn.Close();
            }
            return geoDetails;
        }

        public string getLocationBasedOnIP(string ResponseString, string curIP)
        {
            string locationIP = string.Empty;
            string locationName = string.Empty;
            if (!string.IsNullOrEmpty(curIP))
            {
                locationIP = curIP;
            }
            else
            {
                locationIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(locationIP))
                {
                    locationIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                }
                else
                {
                    //locationIP = locationIP.Split(",")[0];
                }
            }
            string IP2LocationProviders = System.Configuration.ConfigurationManager.GetSection("IP2LocationProvider").ToString();
            //string IP2LocationPrefix = IP2LocationProviders[0]["BaseURL"].ToString();
            //string IP2LocationSuffix = IP2LocationProviders[0]["ExtendedURL"].ToString();
            //string locationName = "{'" + ResponseString + "':''}";
            //string IP2LocationProvider = IP2LocationPrefix + locationIP + IP2LocationSuffix;
            //try
            //{
            //    HttpWebRequest request = WebRequest.Create(IP2LocationProvider) as HttpWebRequest;
            //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //    {
            //        if (response != null)
            //        {
            //            StreamReader reader = new StreamReader(response.GetResponseStream());
            //            locationName = reader.ReadToEnd();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //
            //}
            return locationName;
        }
    }

}
