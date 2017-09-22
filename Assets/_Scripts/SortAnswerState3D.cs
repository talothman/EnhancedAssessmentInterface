using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SortAnswerState3D : MonoBehaviour {

    public int order;
    public GameManager3DSort sceneGameManager;
    public SortGroup3D sortGroup;

    private void Awake()
    {
        sceneGameManager = GameObject.FindGameObjectWithTag("SceneGameManager").GetComponent<GameManager3DSort>();
        sortGroup = GetComponentInParent<SortGroup3D>();

        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += SortAnswerState3D_InteractableObjectUngrabbed;
    }

    private void SortAnswerState3D_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        sortGroup.UpdateSortState();
    }
}
