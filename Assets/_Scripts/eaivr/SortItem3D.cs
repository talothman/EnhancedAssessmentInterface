using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public class SortItem3D : SortItem
    {
        public SortAnswerGroup3D sortAnswerGroup3D;
        SortAnswer3D[] threeDSortAnswers;

        void Start()
        {
            if (sortAnswerGroup3D == null)
                sortAnswerGroup3D = GetComponent<SortAnswerGroup3D>();

            InsertItemData();
        }

        public override void InsertItemData()
        {
            threeDSortAnswers = sortAnswerGroup3D.threeDSortAnswers;
            canvasText.text = itemData.stem;

            for (int i = 0; i < threeDSortAnswers.Length; i++)
            {
                threeDSortAnswers[i].GetComponentInChildren<Text>().text = itemData.answers[i];
            }
        }

        public override void NextQuestion()
        {
            throw new System.NotImplementedException();
        }

        public override void CheckSelectedAnswer()
        {
            SortAnswer3D[] sortAnswers = sortAnswerGroup3D.threeDSortAnswers;

            bool sorted = true;

            for(int i = 0; i < sortAnswers.Length; i++)
            {
                if(sortAnswers[i].correctOrder == sortAnswers[i].currentOrder)
                {
                    sortAnswers[i].gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
                else
                {
                    sortAnswers[i].gameObject.GetComponent<Renderer>().material.color = Color.red;
                    sorted = false;
                }
            }

            if (sorted)
            {
                answeredCorreclty = true;
            }
            else
            {
                answeredCorreclty = false;
            }

            NextQuestion();
        }
    }
}

