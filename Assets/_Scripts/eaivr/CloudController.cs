using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

	void Update () {
		transform.Rotate (Vector3.up * Time.deltaTime * 0.4f);
	}
}
