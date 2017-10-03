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

        public GameObject selectItemTutorialPrefab;
        public GameObject sortItemTutorialPrefab;

        public GameObject selectItemPrefab;
        public GameObject sortItemPrefab;

        public int currentSelectItemIndex = 0;
        public int currentSortItemIndex = 0;
        public GameObject currentActiveItem;

        //Stage currentStage;
        bool loadedSelectTutorial = false;
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

        public void LoadSelectTutorial()
        {
            currentActiveItem = Instantiate(selectItemTutorialPrefab);
            currentActiveItem.GetComponent<SelectItem>().InsertItemData(selectItemData[currentSelectItemIndex++]);
            loadedSelectTutorial = true;
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

        public void LoadSortTutorial()
        {
            if (currentActiveItem != null)
                Destroy(currentActiveItem, 0.5f);

            StartCoroutine(PauseBeforeInstantiateSortTutorial());
        }

        IEnumerator PauseBeforeInstantiateSortTutorial()
        {
            yield return new WaitForSeconds(0.6f);
            currentActiveItem = Instantiate(sortItemTutorialPrefab);
            currentActiveItem.GetComponent<SortItem>().InsertItemData(sortItemData[currentSortItemIndex++]);
            loadedSortTutorial = true;
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

        public void Next()
        {
            if (!loadedSelectTutorial)
            {
                LoadSelectTutorial();
            }
            else if (loadedSelectTutorial && currentSelectItemIndex < selectItemData.Length)
            {
                // wait until shift to sun is done
                LoadSelectQuestion();
            }
            else if (!loadedSortTutorial)
            {
                LoadSortTutorial();
            }
            else if (loadedSortTutorial && currentSortItemIndex < sortItemData.Length)
            {
                LoadSortQuestion();
            }
            else
                LoadOutro();
        }
    }    
}