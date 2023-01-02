using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBindings : MonoBehaviour
{
    [SerializeField] private Button showAlertButton;
    [SerializeField] private Button startMotionDnaButton;
    [SerializeField] private Button hasPressedYesButton;
    [SerializeField] private Button increaseMaxDistanceButton;
    [SerializeField] private Button decreaseMaxDistanceButton;
    [SerializeField] private Button increaseAnnaWalkSpeedButton;
    [SerializeField] private Button decreaseAnnaWalkSpeedButton;
    [SerializeField] UpdatePlayerPosition updatePlayerPosition;
    [SerializeField] NarrativeSequence narrativeSequence;
    [SerializeField] private Text startedFloodOfTimeText;
    private bool hasStartedExperience;


    void Start()
    { 
        showAlertButton.onClick.AddListener(ShowAlert); // CREATES AN ONCLICK EVENT THAT CALLS ShowAlert
        startMotionDnaButton.onClick.AddListener(start);
        hasPressedYesButton.onClick.AddListener(HasPressedYes);
        increaseMaxDistanceButton.onClick.AddListener(IncreaseMaxDistance);
        decreaseMaxDistanceButton.onClick.AddListener(DecreaseMaxDistance);
        increaseAnnaWalkSpeedButton.onClick.AddListener(IncreaseAnnaWalkSpeed);
        decreaseAnnaWalkSpeedButton.onClick.AddListener(DecreaseAnnaWalkSpeed);
    }

    void ShowAlert() => iOSPlugin.ShowAlert("FLOOD OF", "TIME"); // THIS CALLS THE ShowAlert function in iOSPlugin.cs
        // iOSPlugin.cs the calls the C function _ShowAlert
        // _ShowAlert

    public void start() {
        if(!hasStartedExperience){
            hasStartedExperience = true;
            iOSPlugin.start();
            StartCoroutine(updatePlayerPosition.SetSourceAndPlayerPositionAtStart());   
            startedFloodOfTimeText.text = "Started Flood Of Time";
            StartCoroutine(narrativeSequence.WaitForNavisens());
            narrativeSequence.maxDistanceFromAnnaText.text = narrativeSequence.maxDistanceFromAnna.ToString();
            narrativeSequence.annaWalkSpeedText.text = narrativeSequence.annaWalkSpeed.ToString();
        }
    }

    void HasPressedYes(){
       StartCoroutine(HasPressedYesTiming());
    }

    void IncreaseMaxDistance(){
        narrativeSequence.maxDistanceFromAnna += 1;
        narrativeSequence.maxDistanceFromAnnaText.text = narrativeSequence.maxDistanceFromAnna.ToString();
    }

    void DecreaseMaxDistance(){
        narrativeSequence.maxDistanceFromAnna -= 1;
        narrativeSequence.maxDistanceFromAnnaText.text = narrativeSequence.maxDistanceFromAnna.ToString();
    }

    void IncreaseAnnaWalkSpeed(){
        narrativeSequence.annaWalkSpeed += 0.002f;
        narrativeSequence.annaWalkSpeedText.text = narrativeSequence.annaWalkSpeed.ToString();
    }

    void DecreaseAnnaWalkSpeed(){
        narrativeSequence.annaWalkSpeed -= 0.002f;
        narrativeSequence.annaWalkSpeedText.text = narrativeSequence.annaWalkSpeed.ToString();
    }

    IEnumerator HasPressedYesTiming(){
        narrativeSequence.hasPressedYes = true;
        yield return new WaitForSeconds(5f);
        narrativeSequence.hasPressedYes = false;
    }

}
