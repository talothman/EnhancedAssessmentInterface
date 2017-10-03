using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public class SortItem2D : SortItem
    {
        public SortAnswerGroup2D sortAnswerGroup2D;
        public SortItemData sortItemData;
        SortAnswer2D[] sortAnswers;

        private void Start()
        {
            if(sortAnswers == null)
                sortAnswers = sortAnswerGroup2D.twoDSortAnswers;

            gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

        public override void CheckSelectedAnswers()
        {
            bool sorted = true;

            for (int i = 0; i < sortAnswers.Length; i++)
            {
                if (sortAnswers[i].correctOrder == sortAnswers[i].currentOrder)
                {
                    sortAnswers[i].gameObject.GetComponent<Image>().color = Color.green;
                }
                else
                {
                    sortAnswers[i].gameObject.GetComponent<Image>().color = Color.red;
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

        public override void InsertItemData(SortItemData newSortItemData)
        {
            sortAnswers = sortAnswerGroup2D.twoDSortAnswers;
            sortItemData = newSortItemData;
            canvasText.text = sortItemData.stem;

            for (int i = 0; i < sortAnswers.Length; i++)
            {
                sortAnswers[i].GetComponentInChildren<Text>().text = sortItemData.sortAnswers[i].answerText;
                sortAnswers[i].correctOrder = sortItemData.sortAnswers[i].correctOrder;
            }
        }
    }
}

