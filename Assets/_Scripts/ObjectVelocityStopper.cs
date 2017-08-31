using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ObjectVelocityStopper : MonoBehaviour {
    public float damppingFactor = 5;

    Vector3 previousPosition;
    Quaternion previousRotation;

	VRTK_InteractableObject vrtkInteractableObject;
	Rigidbody rigidBody;

    Coroutine currentRunningCoroutine;
	// Use this for initialization
	void Start () {
		vrtkInteractableObject = GetComponent<VRTK_InteractableObject> ();
        previousPosition = transform.position;
        previousRotation = transform.rotation;
        
        // update previous position once object is grabbed
        vrtkInteractableObject.InteractableObjectGrabbed += (object sender, InteractableObjectEventArgs e) => {
            previousPosition = transform.position;
            previousRotation = transform.rotation;
        };
        vrtkInteractableObject.InteractableObjectUngrabbed += SlowDownCoroutineWrapper;

		rigidBody = GetComponent<Rigidbody> ();
	}

	private void SlowDownCoroutineWrapper(object sender, InteractableObjectEventArgs e){
        if (currentRunningCoroutine != null)
            StopCoroutine(currentRunningCoroutine);
        currentRunningCoroutine = StartCoroutine(SlowDown());
	}

	IEnumerator SlowDown(){
        Vector3 currentVelocity;
		Vector3 currentAngularVelocity;
        
		while (rigidBody.velocity != Vector3.zero || rigidBody.angularVelocity != Vector3.zero) {
            if(!(rigidBody.velocity.magnitude < .05f))
            {
                currentVelocity = rigidBody.velocity;
                currentAngularVelocity = rigidBody.angularVelocity;
            }
            else
            {
                currentVelocity = Vector3.zero;
                currentAngularVelocity = Vector3.zero;
            }

            rigidBody.velocity = Vector3.Lerp(currentVelocity, Vector3.zero, damppingFactor * Time.deltaTime);
            rigidBody.angularVelocity = Vector3.Lerp(currentAngularVelocity, Vector3.zero, damppingFactor * Time.deltaTime);

            yield return null;
		}
	}

    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Boundry")
        {
            print("checking for tag");
            if (!vrtkInteractableObject.IsGrabbed() && rigidBody.velocity == Vector3.zero && rigidBody.angularVelocity == Vector3.zero)
            {
                print("waiting to stop " + collider.gameObject.name + collider.gameObject.transform.position);
                rigidBody.velocity = Vector3.zero;
                rigidBody.angularVelocity = Vector3.zero;
                StartCoroutine(MoveToPreviousLocation());
            }
        }
    }

    IEnumerator MoveToPreviousLocation()
    {
        //StopAllCoroutines();
        rigidBody.isKinematic = true;
        while(transform.position != previousPosition)
        {
            transform.position = Vector3.Lerp(transform.position, previousPosition, damppingFactor * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, previousRotation, damppingFactor * Time.deltaTime);
            yield return null;
        }
        rigidBody.isKinematic = false;
        StopAllCoroutines();
    }
}
