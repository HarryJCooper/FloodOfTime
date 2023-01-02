using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLocationAndAttitude : MonoBehaviour
{
    public float headmotionYRot;
    private string xPos, yPos, zPos, heading;
    private string latitudePos, longitudePos, altitudePos, headingPos;
    public Text xPosText, yPosText, zPosText, headingText;
    // public Text latitudePosText, longitudePosText, altitudePosText, headingPosText;

    void SetCartesianLocationStrings(){
        xPos = iOSPlugin.location().cartesianLocation.x.ToString();
        yPos = iOSPlugin.location().cartesianLocation.y.ToString();
        zPos = iOSPlugin.location().cartesianLocation.z.ToString();
        heading = iOSPlugin.location().cartesianLocation.heading.ToString();
    }
    
    void SetCartesianLocationText(){
        xPosText.text = "xPos: " + xPos;
        yPosText.text = "yPos: " + yPos;
        zPosText.text = "zPos: " + zPos;
        headingText.text = "heading: " + heading;
    }


    void SetGlobalLocationStrings() {
        latitudePos = iOSPlugin.location().globalLocation.latitude.ToString();
        longitudePos = iOSPlugin.location().globalLocation.longitude.ToString();
        altitudePos = iOSPlugin.location().globalLocation.altitude.ToString();
        headingPos = iOSPlugin.location().globalLocation.heading.ToString();
    }

    // void SetGlobalLocationText() {
    //     latitudePosText.text = "latitudePos: " + latitudePos;
    //     longitudePosText.text = "longitudePos: " + longitudePos;
    //     altitudePosText.text = "altitudePos: " + altitudePos;
    //     headingPosText.text = "headingPos: " + headingPos;
    // }

    void Update()
    {
        SetCartesianLocationStrings();
        SetCartesianLocationText();
        // SetGlobalLocationStrings();
        // SetGlobalLocationText();
    }


}
