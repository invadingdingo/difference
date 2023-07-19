using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public TMPro.TextMeshProUGUI dialogueBox;
    public GameObject nextSentence;
    private Queue<string> sentences;

    void Start() {
        sentences = new Queue<string>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        nextSentence.SetActive(true);
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        
        string sentence = sentences.Dequeue();
        dialogueBox.text = sentence;
        

    }

    void EndDialogue() {
        dialogueBox.text = "";
        nextSentence.SetActive(false);
    }

}
