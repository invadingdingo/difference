using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convert : MonoBehaviour {
    public GameObject FakeVersion;

    public void ConvertObject() {
        gameObject.layer = LayerMask.NameToLayer("Default");
        FakeVersion.SetActive(false);
    }

}
