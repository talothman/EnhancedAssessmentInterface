using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public class SortItem3D : SortItem
    {
        public SortAnswerGroup3D sortAnswerGroup3D;
        public SortItemData sortItemData;
        SortAnswer3D[] threeDSortAnswers;

        public virtual void Start()
        {
            if (sortAnswerGroup3D == null)
                sortAnswerGroup3D = GetComponent<SortAnswerGroup3D>();

            gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

        public override void InsertItemData(SortItemData sorItemData)
        {
            threeDSortAnswers = sortAnswerGroup3D.threeDSortAnswers;
            sortItemData = sorItemData;
            canvasText.text = sortItemData.stem;

            for (int i = 0; i < threeDSortAnswers.Length; i++)
            {
                threeDSortAnswers[i].GetComponentInChildren<Text>().text = sortItemData.sortAnswers[i].answerText;
                threeDSortAnswers[i].correctOrder = sortItemData.sortAnswers[i].correctOrder;
            }
        }
        
        public override void CheckSelectedAnswers()
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
        }
    }
}

