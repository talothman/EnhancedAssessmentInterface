using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

	void Update () {
		transform.Rotate (Vector3.one * Time.deltaTime * 0.1f);
	}
}
