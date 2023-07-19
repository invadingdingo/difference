using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour {
    public Camera CamL;
    public Camera CamR;
    public AudioClip convertedAudio;
    public AudioSource audioSource;

    public GameObject smoke;

    private int found;
    public GameObject score;

    private int real;
    private int fake;

    private void Start() {

        //Set Cursor to not be visible
        Cursor.visible = false;

        found = 0;
        real = LayerMask.NameToLayer("Real");
        fake = LayerMask.NameToLayer("Fake");
    }
 
    private void Update() {
        InteractWithObjects();
    }
 
    void InteractWithObjects() {
       
        RaycastHit hit; //Create a raycast called 'hit'
        Ray ray;
        //if (Input.mousePosition.x <= (Screen.width / 2))
        //    ray = CamL.ScreenPointToRay(Input.mousePosition);
        //else
            ray = CamR.ScreenPointToRay(Input.mousePosition);
        // Fires a raycast from the camera to the camera's forward position and outputs the data of what it hit in the 'hit' variable
        if (Physics.Raycast(ray, out hit, 100)) { //If i hit something:
            transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            if (Input.GetMouseButtonDown(0)) {
                //Debug.Log(hit.collider.gameObject.layer);
                if (hit.collider.gameObject.layer == real || hit.collider.gameObject.layer == fake) {
                    GameObject hitObject = hit.collider.gameObject;
                    if (hitObject.layer == real) {
                        hit.collider.gameObject.GetComponent<Convert>().ConvertObject();
                    }
                    if (hitObject.layer == fake) {
                        GameObject realVersion = hit.collider.gameObject.GetComponent<ReferenceFake>().RealVersion;
                        realVersion.GetComponent<Convert>().ConvertObject();
                    }

                    Instantiate(smoke, new Vector3(hitObject.transform.position.x, hitObject.transform.position.y, hitObject.transform.position.z), transform.rotation * Quaternion.Euler(-90f, 0f, 0f)); 
                    audioSource.PlayOneShot(convertedAudio);
                    found += 1;
                    score.GetComponent<TextMeshProUGUI>().text = found.ToString();
                }
            }
        }
    }
}
