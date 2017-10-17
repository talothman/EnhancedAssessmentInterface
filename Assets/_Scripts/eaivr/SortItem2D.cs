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

        public override void NextQuestion()
        {
            bool sorted = true;

            for (int i = 0; i < sortAnswers.Length; i++)
            {
                if (sortAnswers[i].correctOrder != sortAnswers[i].currentOrder)
                {
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

            base.NextQuestion();
        }

        public override void InsertItemData(SortItemData newSortItemData)
        {
            sortAnswers = sortAnswerGroup2D.twoDSortAnswers;
            sortItemData = newSortItemData;
            canvasText.text = sortItemData.stem;
            questionID = sortItemData.questionID;
            timeStart = Time.time;

            for (int i = 0; i < sortAnswers.Length; i++)
            {
                int tempRan = Random.Range(i, sortAnswers.Length);
                SortAnswerData tempSortAnswerData = sortItemData.sortAnswers[tempRan];
                sortItemData.sortAnswers[tempRan] = sortItemData.sortAnswers[i];
                sortItemData.sortAnswers[i] = tempSortAnswerData;

                sortAnswers[i].GetComponentInChildren<Text>().text = sortItemData.sortAnswers[i].answerText;
                sortAnswers[i].correctOrder = sortItemData.sortAnswers[i].correctOrder;
            }
        }

        public override string[] GetOrderedItems()
        {
            string[] sortedAnswers = new string[sortAnswers.Length];

            for (int i = 0; i < sortAnswers.Length; i++)
            {
                sortedAnswers[i] = sortAnswers[i].GetComponentInChildren<Text>().text;
            }

            return sortedAnswers;
        }
    }
}

