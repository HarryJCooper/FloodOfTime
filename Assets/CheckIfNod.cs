using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckIfNod : MonoBehaviour
{

    Quaternion headmotionQuaternion;
    float eulerAngleX, eulerAngleY;
    public bool hasNodded, canNod, headDown = false;
    public bool lookingDown, lookingUp, lookingRight, lookingLeft, lookingForward;
    public Text hasNoddedText;
    public Text directionText;


    void CheckWhereLooking(){
        if(transform.rotation.x < -0.24) {
            lookingDown = true; 
            lookingUp = lookingRight = lookingLeft = lookingForward = false;
            directionText.text = "Looking Down";
        }  else if(transform.rotation.x > 0.07) {
            lookingUp = true; 
            lookingDown = lookingRight = lookingLeft = lookingForward = false; 
            directionText.text = "Looking Up"; 
        } else if(transform.rotation.y > 0.2) {
            lookingLeft = true; 
            lookingDown = lookingRight = lookingUp = lookingForward = false; 
            directionText.text = "Looking Left"; }
        else if(transform.rotation.y < -0.2) {
            lookingRight = true; 
            lookingDown = lookingLeft = lookingUp = lookingForward = false;
            directionText.text = "Looking Right"; 
        } else {
            lookingForward = true;
            lookingDown = lookingLeft = lookingRight = lookingUp = false;
            directionText.text = "Looking Forward";
        } 
    }

    void CheckIfNodded(){
        if (transform.rotation.x < -0.2){
            headDown = true;
        }
        if (transform.rotation.x > -0.05 && headDown){
            hasNodded = true;
            headDown = false;
        }
    }

    void Update(){
        CheckWhereLooking();
        if(canNod){
            CheckIfNodded();
        }
    }
}
