using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class RemoveSnapZoneText : MonoBehaviour {
    public GameObject snapZoneTextGameObject;
    public GameManager3D gameManager3D;

	void Start () {
        GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += HideSnapZoneText;
        GetComponent<VRTK_SnapDropZone>().ObjectUnsnappedFromDropZone += ShowSnapZoneText;
	}

    void HideSnapZoneText(object sender, SnapDropZoneEventArgs e)
    {
        snapZoneTextGameObject.SetActive(false);
        gameManager3D.CheckAnswer(e.snappedObject);
    }

    void ShowSnapZoneText(object sender, SnapDropZoneEventArgs e)
    {
        snapZoneTextGameObject.SetActive(true);
    }
}
