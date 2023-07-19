using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOff : MonoBehaviour {

    public GameObject lights;

    void OnPreCull () {
        if (lights != null)
            lights.SetActive(false);
    }
    
    void OnPreRender() {
        if (lights != null)
            lights.SetActive(false);
    }

    void OnPostRender() {
        if (lights != null)
            lights.SetActive(true);
    }
}
