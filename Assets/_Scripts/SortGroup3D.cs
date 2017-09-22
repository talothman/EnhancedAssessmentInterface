using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortGroup3D : SortGroup {

    private SortAnswerState3D[] sortAnswers;
    [SerializeField]
    private GameObject sortSubmitButton;
    private GroupLineRendererController gLineRendererController;

    public override void Start()
    {
        sortAnswers = GetComponentsInChildren<SortAnswerState3D>();

        currentSortRanking = new int[sortAnswers.Length];
        currentXPositions = new float[sortAnswers.Length];

        for (int i = 0; i < sortAnswers.Length; i++)
        {
            currentSortRanking[i] = sortAnswers[i].order;
            currentXPositions[i] = sortAnswers[i].GetComponent<Transform>().localPosition.x;
        }

        gLineRendererController = GetComponent<GroupLineRendererController>();
    }

    public void HandleGrab(GameObject go)
    {
        //gLineRendererController.RemoveTransform(go);
    }

    public override void UpdateSortState()
    {
        if (!sortSubmitButton.activeInHierarchy)
            sortSubmitButton.SetActive(true);

        Array.Sort<SortAnswerState3D>(sortAnswers, CompareSortAnswersByPosition);

        for (int i = 0; i < sortAnswers.Length; i++)
        {
            currentSortRanking[i] = sortAnswers[i].order;
        }

        gLineRendererController.UpdateLineState();
    }

    public override void CheckSortStateAnswer()
    {
        int previousRank = currentSortRanking[0];
        bool sorted = true;

        for (int i = 1; i < currentSortRanking.Length; i++)
        {
            if (previousRank < currentSortRanking[i])
            {
                previousRank = currentSortRanking[i];
                continue;
            }
            else
            {
                sorted = false;
                break;
            }
        }

        if (sorted)
        {
            ColorAnswersGreen();
        }
        else
        {
            ColorAnswersRed();
        }
    }

    public override void ColorAnswersGreen()
    {
        foreach (SortAnswerState3D sortAnswer in sortAnswers)
        {
            sortAnswer.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    public override void ColorAnswersRed()
    {
        foreach (SortAnswerState3D sortAnswer in sortAnswers)
        {
            sortAnswer.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public static int CompareSortAnswersByPosition(SortAnswerState3D objectPosition1, SortAnswerState3D objectPosition2)
    {
        return objectPosition1.gameObject.GetComponent<Transform>().position.x.CompareTo(objectPosition2.gameObject.GetComponent<Transform>().position.x);
    }
}
