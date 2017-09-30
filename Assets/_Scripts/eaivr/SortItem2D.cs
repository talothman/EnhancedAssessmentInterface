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
            sortAnswers = sortAnswerGroup2D.twoDSortAnswers;
            //InsertItemData();
        }

        public override void CheckSelectedAnswer()
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

            NextQuestion();
        }

        public void InsertItemData(SortItemData sorItemData)
        {
            sortItemData = sorItemData;
            canvasText.text = sortItemData.stem;

            for (int i = 0; i < sortAnswers.Length; i++)
            {
                sortAnswers[i].GetComponentInChildren<Text>().text = sortItemData.sortAnswers[i].answerText;
                sortAnswers[i].correctOrder = sortItemData.sortAnswers[i].correctOrder;
            }
        }

        public override void NextQuestion()
        {
            throw new System.NotImplementedException();
        }
    }
}

