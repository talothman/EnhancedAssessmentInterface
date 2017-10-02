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
        protected void Start()
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

            for (int i = 0; i < selectAnswers3D.Length; i++)
            {
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

        public override void CheckSelectedAnswer()
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
    }
}

