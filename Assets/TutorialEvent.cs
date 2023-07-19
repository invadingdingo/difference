using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        GameObject.Find("TutorialDialogue").GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    
}
