using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatAmbience : MonoBehaviour
{
    [SerializeField] private AudioClip[] ambienceClips;
    private AudioSource ambienceSource;

    void Start(){  
        ambienceSource = GetComponent<AudioSource>();
    }

    public void StopCoroutine(){
        StopAllCoroutines();
    }

    public IEnumerator RepeatAmbienceClips(){
        ambienceSource.PlayOneShot(ambienceClips[Random.Range(0, ambienceClips.Length)]);
        yield return new WaitForSeconds(ambienceClips[0].length / 2);
        StartCoroutine(RepeatAmbienceClips());
    }

}
