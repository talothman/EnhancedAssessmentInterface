using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DartReloader : MonoBehaviour {
	
    // Use this for initialization
	void Start () {
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += GameObject.FindGameObjectWithTag("Man").GetComponent<DartManager>().RespawnDart;
	}
	
}
