using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GroupLineRendererController : MonoBehaviour {
    LineRenderer lineRenderer;
    Transform[] objectTransforms;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();

        SortAnswerState3D[] sortAnswers3D = GetComponentsInChildren<SortAnswerState3D>();
        objectTransforms = new Transform[sortAnswers3D.Length];

        for (int i = 0; i < sortAnswers3D.Length; i++)
        {
            objectTransforms[i] = sortAnswers3D[i].transform;
        }

        lineRenderer.positionCount = objectTransforms.Length;

        for (int i = 0; i < sortAnswers3D.Length; i++)
        {
            lineRenderer.SetPosition(i, objectTransforms[i].position);    
        }
    }
	
    public void RemoveTransform(GameObject go)
    {
        //lineRenderer.SetPosition(go.GetComponent<SortAnswerState3D>().order= null;
        //objectTransforms[go.GetComponent<SortAnswerState3D>().order];
    }

    public void UpdateLineState()
    {
        Array.Sort(objectTransforms, ComparePosition);
    }

	// Update is called once per frame
	void Update () {

        for (int i = 0; i < objectTransforms.Length; i++)
        {
            lineRenderer.SetPosition(i, objectTransforms[i].position);
        }
    }

    public static int ComparePosition(Transform objectPosition1, Transform objectPosition2)
    {
        return objectPosition1.gameObject.GetComponent<Transform>().position.x.CompareTo(objectPosition2.gameObject.GetComponent<Transform>().position.x);
    }
}
