using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using VRTK.Highlighters;

public class GameManager3DSort : GameManager2DSort {
    public Material TransparentCubeMat;
    Material originalMaterial;
    Material selectedMaterial;
    public Color selectColor;

    [SerializeField]
    private Renderer[] selectAnswerRenderers;
    [SerializeField]
    private VRTK_Button vrtk_Button;

    private void Start()
    {
        originalMaterial = new Material(TransparentCubeMat);
        originalMaterial.CopyPropertiesFromMaterial(TransparentCubeMat);

        selectedMaterial = new Material(TransparentCubeMat)
        {
            color = selectColor
        };

        foreach (Renderer r in selectAnswerRenderers)
        {
            r.gameObject.GetComponent<AnswerState3D>().isSelected = false;
        }

        currentlySelectedObject = null;
    }

    public override void SetCurrentlySelectedObject(GameObject selectedObject)
    {
        foreach (Renderer r in selectAnswerRenderers)
        {
            r.gameObject.GetComponent<AnswerState3D>().isSelected = false;
        }

        selectedObject.GetComponent<AnswerState3D>().isSelected = true;

        if (!vrtk_Button.gameObject.activeInHierarchy)
            vrtk_Button.gameObject.SetActive(true);

        currentlySelectedObject = selectedObject;
    }

    public void HighlightSelected(object sender, InteractableObjectEventArgs e)
    {
        foreach (Renderer r in selectAnswerRenderers)
        {
            if (!r.gameObject.GetComponent<AnswerState3D>().isSelected)
            {
                r.material = TransparentCubeMat;
            }
            else
            {
                currentlySelectedObject.GetComponent<Renderer>().material = selectedMaterial;
                print("Highlight Selected: " + currentlySelectedObject);
            }
        }
    }

    public override void CheckSelectionAnswer()
    {
        if (currentlySelectedObject == null)
            return;

        if (currentlySelectedObject.GetComponent<AnswerState3D>().isAnswer)
            currentlySelectedObject.GetComponent<Renderer>().material.color = Color.green;
        else
            currentlySelectedObject.GetComponent<Renderer>().material.color = Color.red;

        StartCoroutine(MoveToManipulation());
    }
}
