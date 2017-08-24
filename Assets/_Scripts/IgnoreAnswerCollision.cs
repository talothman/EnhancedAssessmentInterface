using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreAnswerCollision : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "AnswerCubes")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
