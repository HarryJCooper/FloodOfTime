using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerPosition : MonoBehaviour
{
    public GameObject[] audioSources;
    public bool gameStarted;

    public IEnumerator SetSourceAndPlayerPositionAtStart(){
        Debug.Log("Set Source and Player Position At Start 1");
        yield return new WaitForSeconds(10);
        Debug.Log("Set Source and Player Position At Start - after 10 seconds");
        double xPos = iOSPlugin.location().cartesianLocation.x;
        double zPos = iOSPlugin.location().cartesianLocation.y;
        foreach (GameObject audioSource in audioSources){
            float tempX = audioSource.transform.position.x + (float)xPos;
            float tempZ = audioSource.transform.position.z + (float)zPos;
            audioSource.transform.position = new Vector3(tempX, 0, tempZ);
        }
        gameStarted=true;
    }

    void SetPlayerPosition(){
        double xPos = iOSPlugin.location().cartesianLocation.y * -1f;
        double zPos = iOSPlugin.location().cartesianLocation.x;

        float heading = (float)iOSPlugin.location().cartesianLocation.heading;
        this.transform.position = new Vector3((float)xPos, 0, (float)zPos);
    }

    void Update(){
        if (gameStarted){
            SetPlayerPosition();
        }
    }
}
