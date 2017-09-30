using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class SelectState3DButton : MonoBehaviour {
    public bool isSelected = false;
    public float sunAngle;
    public TutorialManager3D sceneGameManager;
    private VRTK_Button_UnityEvents buttonEvents;

    private void Awake()
    {
        if (sceneGameManager == null)
            return;

        buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
        if (buttonEvents == null)
        {
            buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
        }
        buttonEvents.OnPushed.AddListener((object sender, Control3DEventArgs e) => {
            sceneGameManager.SetSelectedButton(gameObject);
        });

        GetComponent<VRTK_InteractableObject>().InteractableObjectUntouched += sceneGameManager.HighlightSelected;
    }
}
