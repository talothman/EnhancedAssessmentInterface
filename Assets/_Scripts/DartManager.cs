using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DartManager : MonoBehaviour {

    public GameObject dartPrefab;
    public GameObject dartRespawn;

    public void RespawnDart(object sender, InteractableObjectEventArgs e)
    {
        StartCoroutine(MakeDart());
    }

    IEnumerator MakeDart()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(dartPrefab, dartRespawn.transform.position, dartRespawn.transform.rotation);
    }
}
