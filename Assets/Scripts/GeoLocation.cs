using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeoLocation : MonoBehaviour
{
    private float latitude;
    private float longitude;
    public static GeoLocation instance { get; set; }

    public Text debugText;
    // Use this for initialization
    private void Start()
    {
        instance = this;
        //RequestPermission();
        DontDestroyOnLoad(instance);
        //UniAndroidPermission.IsPermitted(AndroidPermission.WRITE_EXTERNAL_STORAGE);
        StartCoroutine(StartLocationService());

    }
    void Update()
    {
        StartCoroutine(StartLocationService());
    }

    //public void RequestPermission()
    //{
    //    UniAndroidPermission.RequestPermission(AndroidPermission.WRITE_EXTERNAL_STORAGE);
    //}

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            debugText.text = "User has not Enabled GPS";
            yield break;
        }

        Input.location.Start();
        int maxWait = 10;
        //int maxWait = 5;

        //while(Input.location.status == LocationServiceStatus.Running){
        //    debugText.text = Input.location.lastData.latitude.ToString();
        //}

        while (Input.location.status == LocationServiceStatus.Running && maxWait > 0)
        {
            debugText.text = "Initializing";
            yield return new WaitForSeconds(1);
            maxWait--;

        }

        if (maxWait <= 1)
        {
            debugText.text = "Timed Out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            debugText.text = "Unable to Determine the device location";
            yield break;
        }
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        // FOR DEBUGGING
        debugText.text = latitude.ToString() + " lat,  " + longitude.ToString() + " long. " + GoalDist().ToString() + " m away from FFP ";
        yield break;
    }

    public float[] currLocation()
    {
        return new float[] { latitude, longitude };
    }

    void RetrieveGPSData()
    {
        LocationInfo currentGPSPosition = Input.location.lastData;
        string gpsString = "::" + currentGPSPosition.latitude + "//" + currentGPSPosition.longitude;
    }

    public int GoalDist()
    {
        double goal = DistanceTo(latitude, longitude, 40.749831, -73.961290) * 1000;
        return (int)Math.Round(goal);
    }

    public double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
    {
        double rlat1 = Math.PI * lat1 / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = lon1 - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
            Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
            Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;

        switch (unit)
        {
            case 'K': //Kilometers -> default
                return dist * 1.609344;
            case 'N': //Nautical Miles 
                return dist * 0.8684;
            case 'M': //Miles
                return dist;
        }

        return dist / 1000;
    }

}
