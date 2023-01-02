using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float moveSpeedX, moveSpeedZ;
    private Transform playerTransform;
    [SerializeField] private Vector3 initialOffSet;
    public bool turnOn;
    private bool playClip;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator TurnOffAfterUse(){
        yield return new WaitForSeconds(audioClip.length);
        turnOn = false;
        playClip = false;
    }

    void MoveAudioSource(){
        if(!playClip){
            playerTransform = GameObject.Find("Player").transform;
            this.transform.position = new Vector3(playerTransform.position.x + initialOffSet.x, playerTransform.position.y + initialOffSet.y, playerTransform.position.z + initialOffSet.z);
            audioSource.PlayOneShot(audioClip);
            playClip = true;
        }
        this.transform.position = new Vector3(this.transform.position.x + moveSpeedX, this.transform.position.y, this.transform.position.z + moveSpeedZ);
    }

    void Update(){
        if(turnOn){
            MoveAudioSource();
        }
    }
}
