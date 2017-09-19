using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortGroup : MonoBehaviour {

    public SortAnswerState[] sortAnswers;
    public int[] currentSortRanking;
    public float[] currentXPositions;
    public Button submitButton;

    // Use this for initialization
	void Start () {
        sortAnswers = GetComponentsInChildren<SortAnswerState>();

        currentSortRanking = new int[sortAnswers.Length];
        currentXPositions = new float[sortAnswers.Length];

        for(int i = 0; i < sortAnswers.Length; i++)
        {
            currentSortRanking[i] = sortAnswers[i].order;
            currentXPositions[i] = sortAnswers[i].GetComponent<RectTransform>().localPosition.x;
        }
	}

    public void UpdateSortState()
    {
        if (!submitButton.interactable)
            submitButton.interactable = true;

        Array.Sort<SortAnswerState>(sortAnswers, CompareSortAnswersByPosition);

        for (int i = 0; i < sortAnswers.Length; i++)
        {
            currentSortRanking[i] = sortAnswers[i].order;
        }
    }

    public void CheckSortStateAnswer()
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

    private void ColorAnswersGreen()
    {
        foreach(SortAnswerState sortAnswer in sortAnswers)
        {
            sortAnswer.gameObject.GetComponent<Image>().color = Color.green;
        }
    }

    private void ColorAnswersRed()
    {
        foreach (SortAnswerState sortAnswer in sortAnswers)
        {
            sortAnswer.gameObject.GetComponent<Image>().color = Color.red;
        }
    }

    public static int CompareSortAnswersByPosition(SortAnswerState objectPosition1, SortAnswerState objectPosition2)
    {
        return objectPosition1.gameObject.GetComponent<RectTransform>().position.x.CompareTo(objectPosition2.gameObject.GetComponent<RectTransform>().position.x);
    }
	
}
