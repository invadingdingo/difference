using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }
    
}
