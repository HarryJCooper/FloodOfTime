using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Transform player;    

    void FollowPlayer(){
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 100, player.transform.position.z);
    }

    void Update(){
        FollowPlayer();        
    }
}
