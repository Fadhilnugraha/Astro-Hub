using UnityEngine;
using System.Text;
using System.Collections.Generic;
using TimeAndDate.Services;
using TimeAndDate.Services.DataTypes.Places;
using TimeAndDate.Services.DataTypes.Astro;
using UnityEditor;
using System;
using UnityEngine.InputSystem;
using TMPro;
public class Calendar_Timeanddate
{
    const string AccessKey = "R8f9RWdpo8";
    const string SecretKey = "pBfsooOdUWXu6GqenqhU ";

    public TextMeshProUGUI eventcontent;


    public void eventtaker()
    {
        var coordinates = new Coordinates(Latitude:59.912m, Longitude: 10.730m);
        var place = new LocationId(coordinates);
        var date = new DateTime(year: 2026, month: 1, day: 20);
        var service = new AstronomyService(AccessKey,SecretKey);
        service.Types = AstronomyEventClass.Meridian | AstronomyEventClass.Phase;
        var astroInfo = service.GetAstronomicalInfo(objectType: AstronomyObjectType.Moon, placeId: place, date);
        StringBuilder sb=new StringBuilder();

                sb.AppendLine(value:$"Count = {astroInfo.Count}");
        foreach(AstronomyLocation loc in astroInfo)
        {
            var locationName=loc.Geography.Name;
            sb.AppendLine(value: $"[{locationName}]");
            
            foreach(var AstronomyObjectDetails in loc.Objects)
            {
                sb.AppendLine(value: AstronomyObjectDetails.Name.ToString());
            }
            sb.AppendLine();
        }
        Debug.Log(message: sb.ToString());

         eventcontent.text= sb.ToString(); 

    }
    public void taker()
    {
      
    }

}
