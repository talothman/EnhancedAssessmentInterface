using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class SortItemState : MonoBehaviour {

    public int order;
    public TutorialManager3D sceneGameManager;
    public SortGroup3D sortGroup;

    protected virtual void Awake()
    {
        sceneGameManager = GameObject.FindGameObjectWithTag("SceneGameManager").GetComponent<TutorialManager3D>();
        sortGroup = GetComponentInParent<SortGroup3D>();

        //GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += SortAnswerState3D_InteractableObjectGrabbed;
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += SortAnswerState3D_InteractableObjectUngrabbed;
    }

    private void SortAnswerState3D_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        sortGroup.HandleGrab(gameObject);
    }

    private void SortAnswerState3D_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        sortGroup.UpdateSortState();
    }
}
