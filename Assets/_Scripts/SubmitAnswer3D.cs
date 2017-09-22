using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class SubmitAnswer3D : MonoBehaviour {
    public GameManager3DSort sceneGameManager;
    private VRTK_Button_UnityEvents buttonEvents;

    private void Awake()
    {
        sceneGameManager = GameObject.FindGameObjectWithTag("SceneGameManager").GetComponent<GameManager3DSort>();

        buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
        if (buttonEvents == null)
        {
            buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
        }
        buttonEvents.OnPushed.AddListener((object sender, Control3DEventArgs e) => sceneGameManager.CheckSelectionAnswer());
    }
}
