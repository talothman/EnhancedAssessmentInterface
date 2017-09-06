using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeManager : MonoBehaviour {

    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
