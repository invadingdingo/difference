using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {

    public bool open = false;
    private Vector3 endRotation;
    private AudioSource audioSource;

    Animator animator;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    
    void Update() {
        if (open) {
            OpenDoor();
        } else if (!open) {
            CloseDoor();
        }
    }

    public void OpenDoor() {
        open = true;
        animator.SetBool("open", open);
    }

    public void CloseDoor() {
        open = false;
        animator.SetBool("open", open);
    }
}
