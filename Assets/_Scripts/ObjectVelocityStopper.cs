using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ObjectVelocityStopper : MonoBehaviour {
	public float damppingFactor = 5;
	VRTK_InteractableObject thisObject;
	Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		thisObject = GetComponent<VRTK_InteractableObject> ();
		thisObject.InteractableObjectUngrabbed += CoroutineStarter;

		rigidBody = GetComponent<Rigidbody> ();
	}

	private void CoroutineStarter(object sender, InteractableObjectEventArgs e){
		StartCoroutine(ActualKiller());
	}

	IEnumerator ActualKiller(){
		Vector3 currentVelocity;
		Vector3 currentAngularVelocity;

		while (rigidBody.velocity != Vector3.zero || rigidBody.angularVelocity != Vector3.zero) {
			currentVelocity = rigidBody.velocity;
			currentAngularVelocity = rigidBody.angularVelocity;

			rigidBody.velocity = Vector3.Lerp (currentVelocity, Vector3.zero, damppingFactor * Time.deltaTime);
			rigidBody.angularVelocity = Vector3.Lerp (currentAngularVelocity, Vector3.zero, damppingFactor *Time.deltaTime);

			yield return null;
		}
	}
}
