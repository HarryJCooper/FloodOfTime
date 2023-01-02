using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class iOSPlugin : MonoBehaviour
{
    #region structs
    public struct CartesianLocationStruct {
        public double x;
        public double y;
        public double z;
        public double heading;
    };

    public struct GlobalLocationStruct {
        public double latitude;
        public double longitude;
        public double altitude;
        public double heading;
        public int    accuracy; //GlobalLocationAccuracyLOW = 0, GlobalLocationAccuracyHIGH = 1
    };

    public struct LocationStruct {
        public CartesianLocationStruct cartesianLocation;
        public GlobalLocationStruct    globalLocation;
    };

    public struct EulerStruct {
        public double roll;
        public double pitch;
        public double yaw;
    };

    public struct QuaternionStruct {
        public double w;
        public double x;
        public double y;
        public double z;
    };

    public struct AttitudeStruct {
        public EulerStruct euler;
        public QuaternionStruct quaternion;
    };

    #endregion
    
    public static double testTimestamp;
    
    #region DllImports
    // we have to bind to our C method in here.
    [DllImport("__Internal")]
    private static extern void _ShowAlert(string title, string message); // THIS LINKS TO THE C FUNCTION!

    [DllImport("__Internal")]
    private static extern void _start(); // THIS LINKS TO THE C FUNCTION!

    [DllImport("__Internal")]
    private static extern string _ID(); // THIS LINKS TO THE C FUNCTION!

    [DllImport("__Internal")]
    private static extern double _timestamp(); // THIS LINKS TO THE C FUNCTION!

    [DllImport("__Internal")]
    private static extern LocationStruct _location();

    [DllImport("__Internal")]
    private static extern AttitudeStruct _attitude();

    #endregion

    public static void ShowAlert(string title, string message) // THIS LIVES IN UNITY
    {
        _ShowAlert(title, message); // this calls the above function!!
        Debug.Log(timestamp());
    }

    public static void start() // THIS LIVES IN UNITY
    {
        _start(); // this calls the above function!!     
    }

    public void lick_joannas_hungrypussy(){
        Debug.Log("oooooh, aaaaah, right there.. Harder!");
    }

    public static string ID() // THIS LIVES IN UNITY
    {
        return _ID();
         // this calls the above function!!
    }

    public static double timestamp() // THIS LIVES IN UNITY
    {
        testTimestamp = _timestamp();
        Debug.Log("this is Unity Debug.Log" + testTimestamp);
        return testTimestamp;
    }

    public static LocationStruct location(){
        return _location();
    }

    public static AttitudeStruct attitude(){
        return _attitude();
    }
}
