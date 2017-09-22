using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortGroup : MonoBehaviour {

    private SortAnswerState[] sortAnswers;
    public int[] currentSortRanking;
    public float[] currentXPositions;
    [SerializeField]
    private Button submitButton;

    // Use this for initialization
    public virtual void Start () {
        sortAnswers = GetComponentsInChildren<SortAnswerState>();

        currentSortRanking = new int[sortAnswers.Length];
        currentXPositions = new float[sortAnswers.Length];

        for(int i = 0; i < sortAnswers.Length; i++)
        {
            currentSortRanking[i] = sortAnswers[i].order;
            currentXPositions[i] = sortAnswers[i].GetComponent<RectTransform>().localPosition.x;
        }
	}

    public virtual void UpdateSortState()
    {
        if (!submitButton.interactable)
            submitButton.interactable = true;

        Array.Sort<SortAnswerState>(sortAnswers, CompareSortAnswersByPosition);

        for (int i = 0; i < sortAnswers.Length; i++)
        {
            currentSortRanking[i] = sortAnswers[i].order;
        }
    }

    public virtual void CheckSortStateAnswer()
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

        StartCoroutine(GameObject.FindGameObjectWithTag("SceneGameManager").GetComponent<GameManager2DSort>().MoveToManipulation());
    }

    public virtual void ColorAnswersGreen()
    {
        foreach(SortAnswerState sortAnswer in sortAnswers)
        {
            sortAnswer.gameObject.GetComponent<Image>().color = Color.green;
        }
    }

    public virtual void ColorAnswersRed()
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
