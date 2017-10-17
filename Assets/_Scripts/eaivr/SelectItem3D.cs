using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

namespace eaivr
{
    public class SelectItem3D : SelectItem
    {
        public SelectAnswer3D[] selectAnswers3D;
        public SelectItemData selectItemData;
        public Material defaultMaterial;
        protected Material selectMaterial;

        // Use this for initialization
        protected virtual void Start()
        {
            selectMaterial = new Material(defaultMaterial)
            {
                color = selectColor
            };

            gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

        public override void InsertItemData(SelectItemData selItemData)
        {
            selectItemData = selItemData;
            canvasText.text = selectItemData.stem;
            questionID = selectItemData.questionID;
            timeStart = Time.time;
            
            for (int i = 0; i < selectAnswers3D.Length; i++)
            {
                int tempRan = Random.Range(i, selectAnswers3D.Length);
                SelectAnswerData tempSelectAnswerData = selectItemData.selectAnswers[tempRan];
                selectItemData.selectAnswers[tempRan] = selectItemData.selectAnswers[i];
                selectItemData.selectAnswers[i] = tempSelectAnswerData;

                selectAnswers3D[i].GetComponentInChildren<Text>().text = selectItemData.selectAnswers[i].answerText;
                selectAnswers3D[i].isKey = selectItemData.selectAnswers[i].isKey;
            }
        }

        public override void SetSelectedAnswer(GameObject selectedObject)
        {
            if (!submitGameObject.activeInHierarchy)
                submitGameObject.SetActive(true);

            foreach (SelectAnswer3D selAnswer in selectAnswers3D)
            {
                selAnswer.isSelected = false;
            }

            selectedObject.GetComponent<SelectAnswer3D>().isSelected = true;
        }

        public void SetHighlightedAnswer(GameObject selectedAnswer)
        {
            foreach (SelectAnswer3D answer in selectAnswers3D)
            {
                if (!answer.isSelected)
                {
                    answer.GetComponent<Renderer>().material = defaultMaterial;
                }
                else
                {
                    StartCoroutine(WaitForUntouch(answer.gameObject));
                }
            }
        }

        IEnumerator WaitForUntouch(GameObject selectedAnswer)
        {
            VRTK_InteractableObject selectedAnswerInteractableObject = selectedAnswer.GetComponent<VRTK_InteractableObject>();

            while (selectedAnswerInteractableObject.IsTouched())
                yield return null;

            selectedAnswer.GetComponent<Renderer>().material = selectMaterial;
        }

        public override void CheckSelectedAnswers()
        {
            foreach(SelectAnswer3D selectedAnswer in selectAnswers3D)
            {
                if (selectedAnswer.isSelected)
                {
                    if (selectedAnswer.isKey)
                    {
                        selectedAnswer.GetComponent<Renderer>().material.color = Color.green;
                        answeredCorreclty = true;
                    }
                    else
                    {
                        selectedAnswer.GetComponent<Renderer>().material.color = Color.red;
                        answeredCorreclty = false;
                    }
                }
             // deactivate buttons
            }
        }

        public override void NextQuestion()
        {
            foreach (SelectAnswer3D selectedAnswer in selectAnswers3D)
            {
                if (selectedAnswer.isSelected)
                {
                    if (selectedAnswer.isKey)
                    {                        
                        answeredCorreclty = true;
                    }
                    else
                    {
                        answeredCorreclty = false;
                    }
                }
                // deactivate buttons
            }

            base.NextQuestion();
        }

        public override string GetSelectedAnswer()
        {
            string selectedText = "";

            foreach (SelectAnswer3D selectedAnswer in selectAnswers3D)
            {
                if (selectedAnswer.isSelected)
                {
                    selectedText = selectedAnswer.GetComponentInChildren<Text>().text;
                }
            }

            return selectedText;
        }
    }
}

