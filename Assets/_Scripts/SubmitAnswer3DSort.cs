using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class SubmitAnswer3DSort : MonoBehaviour {

    public SortGroup3D parentSortGroup3D;
    private VRTK_Button_UnityEvents buttonEvents;

    private void Awake()
    {
        parentSortGroup3D = GetComponentInParent<SortGroup3D>();

        buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
        if (buttonEvents == null)
        {
            buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
        }
        buttonEvents.OnPushed.AddListener((object sender, Control3DEventArgs e) => parentSortGroup3D.CheckSortStateAnswer());
    }
}
