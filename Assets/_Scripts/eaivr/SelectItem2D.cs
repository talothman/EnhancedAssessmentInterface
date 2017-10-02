using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public class SelectItem2D : SelectItem
    {
        //move to selectItem
        public SelectAnswer2D[] selectAnswers2D;
        public SelectItemData selectItemData;
        
        // consider moving to selectItem
        void Start()
        {
            //InsertItemData();
            gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

        public override void InsertItemData(SelectItemData selItemData)
        {
            selectItemData = selItemData;
            canvasText.text = selectItemData.stem;

            for (int i = 0; i < selectAnswers2D.Length; i++)
            {
                selectAnswers2D[i].GetComponentInChildren<Text>().text = selectItemData.selectAnswers[i].answerText;
                selectAnswers2D[i].isKey = selectItemData.selectAnswers[i].isKey;
            }
        }

        //consider move to selectItem
        public override void SetSelectedAnswer(GameObject selectedObject)
        {
            if (!submitGameObject.activeInHierarchy)
                submitGameObject.SetActive(true);

            foreach (SelectAnswer2D selAnswer in selectAnswers2D)
            {
                selAnswer.isSelected = false;
            }

            selectedObject.GetComponent<SelectAnswer2D>().isSelected = true;
            SetHighlightedAnswer(selectedObject);
        }

        public void SetHighlightedAnswer(GameObject selectedObject)
        {
            foreach (SelectAnswer2D answer in selectAnswers2D)
            {
                if (!answer.isSelected)
                {
                    answer.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    answer.GetComponent<Image>().color = selectColor;
                }
            }
        }

        //consider moving to SelectItem
        public override void CheckSelectedAnswer()
        {
            foreach (SelectAnswer2D selectedAnswer in selectAnswers2D)
            {
                if (selectedAnswer.isSelected)
                {
                    if (selectedAnswer.isKey)
                    {
                        selectedAnswer.GetComponent<Image>().color = Color.green;
                        answeredCorreclty = true;
                    }
                    else
                    {
                        selectedAnswer.GetComponent<Image>().color = Color.red;
                        answeredCorreclty = false;
                    }

                    //NextQuestion();
                }
            }
        }
    }
}

