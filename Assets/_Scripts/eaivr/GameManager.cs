using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    enum Stage { SELECT, SORT }

    public class GameManager : MonoBehaviour
    {
        public string pathToSelectBank;
        public string pathToSortBank;

        public SelectItemData[] selectItemData;
        public SortItemData[] sortItemData;

        public GameObject sliderQuestionPrefab;
        //public GameObject sortItemTutorialPrefab;

        public GameObject selectItemPrefab;
        public GameObject sortItemPrefab;

        int currentIntroItemIndex = 0;
        int currentSelectItemIndex = 0;
        int currentSortItemIndex = 0;
        public GameObject currentActiveItem;

        bool finishedAllIntroQuestions = false;
        bool loadedSortTutorial = false;

        private void Start()
        {
            LoadQuestionsFromDisk();
        }

        public void LoadQuestionsFromDisk()
        {

#if UNITY_EDITOR
            pathToSelectBank = Application.dataPath + "/_Xml/selectSerial.xml";
            pathToSortBank = Application.dataPath + "/_Xml/sortSerial.xml";
#endif

#if UNITY_STANDALONE_WIN

#endif
            selectItemData = XmlUtility.Deserialize<SelectItemData[]>(pathToSelectBank);
            sortItemData = XmlUtility.Deserialize<SortItemData[]>(pathToSortBank);
            Next();
        }

        public void Next()
        {
            if (!finishedAllIntroQuestions)
            {
                LoadNextIntroQuestion();
            }
            else if (finishedAllIntroQuestions && currentSelectItemIndex < selectItemData.Length)
            {
                LoadSelectQuestion();
            }
            else if (currentSortItemIndex < sortItemData.Length)
            {
                LoadSortQuestion();
            }
            else
                LoadOutro();
        }

        public void LoadNextIntroQuestion()
        {
            if (currentActiveItem != null)
                Destroy(currentActiveItem);

            if (currentIntroItemIndex == 0)
            {
                currentActiveItem = Instantiate(sliderQuestionPrefab);
            }
            else if (currentIntroItemIndex == 3)
            {
                currentActiveItem = Instantiate(sortItemPrefab);
                currentActiveItem.GetComponent<SortItem>().InsertItemData(sortItemData[currentSortItemIndex++]);
            }
            else
            {
                currentActiveItem = Instantiate(selectItemPrefab);
                currentActiveItem.GetComponent<SelectItem>().InsertItemData(selectItemData[currentSelectItemIndex++]);   
            }

            if(++currentIntroItemIndex == 4)
                finishedAllIntroQuestions = true;
            
        }

        public void LoadSelectQuestion()
        {
            if (currentActiveItem != null)
                Destroy(currentActiveItem);

            StartCoroutine(PauseBeforeInstantiateSelect());
        }

        IEnumerator PauseBeforeInstantiateSelect()
        {
            yield return new WaitForSeconds(0.6f);
            currentActiveItem = Instantiate(selectItemPrefab);
            currentActiveItem.GetComponent<SelectItem>().InsertItemData(selectItemData[currentSelectItemIndex++]);
        }

        public void LoadSortQuestion()
        {
            if (currentActiveItem != null)
                Destroy(currentActiveItem, 0.5f);

            StartCoroutine(PauseBeforeInstantiateSort());
        }

        IEnumerator PauseBeforeInstantiateSort()
        {
            yield return new WaitForSeconds(0.6f);
            currentActiveItem = Instantiate(sortItemPrefab);
            currentActiveItem.GetComponent<SortItem>().InsertItemData(sortItemData[currentSortItemIndex++]);
        }

        public void LoadOutro()
        {
            Application.Quit();
        }
    }    
}