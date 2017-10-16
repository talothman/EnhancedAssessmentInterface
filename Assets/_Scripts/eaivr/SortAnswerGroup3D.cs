using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    public class SortAnswerGroup3D : SortAnswerGroup
    {
        public SortAnswer3D[] threeDSortAnswers;
        public Vector3[] originalPositions;
        public Quaternion[] originalPose;

        // Use this for initialization
        void Start()
        {
            lRenderer = GetComponent<LineRenderer>();
            lRenderer.positionCount = threeDSortAnswers.Length;

            Array.Sort(threeDSortAnswers, ComparePosition);

            originalPositions = new Vector3[threeDSortAnswers.Length];
            originalPose = new Quaternion[threeDSortAnswers.Length];

            for (int i = 0; i < threeDSortAnswers.Length; i++)
            {
                lRenderer.SetPosition(i, threeDSortAnswers[i].transform.position);
                originalPositions[i] = threeDSortAnswers[i].transform.position;
                originalPose[i] = threeDSortAnswers[i].transform.rotation;
            }
        }

        void Update()
        {
            for(int i = 0; i < threeDSortAnswers.Length; i++)
            {
                lRenderer.SetPosition(i, threeDSortAnswers[i].transform.position);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                for (int i = 0; i < threeDSortAnswers.Length; i++)
                {                    
                    threeDSortAnswers[i].transform.position = originalPositions[i];
                    threeDSortAnswers[i].transform.rotation = originalPose[i];
                }
            }
        }

        public void UpdateSortState()
        {
            Array.Sort(threeDSortAnswers, ComparePosition);

            for (int i = 0; i < threeDSortAnswers.Length; i++)
            {
                threeDSortAnswers[i].currentOrder = i;
            }
        }

        public static int ComparePosition(SortAnswer3D objectPosition1, SortAnswer3D objectPosition2)
        {
            return objectPosition1.gameObject.GetComponent<Transform>().position.x.CompareTo(objectPosition2.gameObject.GetComponent<Transform>().position.x);
        }
    }
}

