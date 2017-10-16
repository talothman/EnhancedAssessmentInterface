using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    public struct UserInfo
    {
        public string age;
        public string gender;
        public string educationLevel;
        public string[] knowHow;
    }

    enum Stage { TUTORIAL, SELECT, SORT, OUTRO }
    
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

        public UserInfo userInfo;
        private MasterDriver masterDriver;

        private Stage stage;

        delegate void DelayedFunction();

        private void Start()
        {
            masterDriver = GameObject.FindGameObjectWithTag("MD").GetComponent<MasterDriver>();
            stage = Stage.TUTORIAL;
            LoadQuestionsFromDisk();
        }

        public void LoadQuestionsFromDisk()
        {

#if UNITY_EDITOR
            pathToSelectBank = Application.dataPath + "/_Xml/selectSerial.xml";
            pathToSortBank = Application.dataPath + "/_Xml/sortSerial.xml";

            selectItemData = XmlUtility.Deserialize<SelectItemData[]>(pathToSelectBank);
            sortItemData = XmlUtility.Deserialize<SortItemData[]>(pathToSortBank);
#endif

#if UNITY_STANDALONE_WIN           

            selectItemData = XmlUtility.DeserializeBuild<SelectItemData[]>("selectSerial");
            sortItemData = XmlUtility.DeserializeBuild<SortItemData[]>("sortSerial");
#endif

            Next();
        }

        public void Next()
        {
            if (stage == Stage.TUTORIAL)
            {
                LoadNextIntroQuestion();
            }
            else if (stage == Stage.SELECT)
            {
                LoadSelectQuestion();
            }
            else if (stage == Stage.SORT)
            {
                LoadSortQuestion();
            }
            else
                LoadOutro();
        }

        IEnumerator DelayedFunctionRunner(DelayedFunction delayedFunction)
        {
            yield return new WaitForSeconds(0.6f);
            delayedFunction.Invoke();
        }

        public void LoadNextIntroQuestion()
        {
            if (currentActiveItem != null)
            {
                switch (currentIntroItemIndex)
                {
                    case 1:
                        userInfo.age = currentActiveItem.GetComponent<SliderItem>().GetSliderValue();
                        break;
                    case 2:
                        userInfo.gender = currentActiveItem.GetComponent<SelectItem>().GetSelectedAnswer();
                        break;
                    case 3:
                        userInfo.educationLevel = currentActiveItem.GetComponent<SelectItem>().GetSelectedAnswer();
                        break;
                    case 4:
                        userInfo.knowHow = currentActiveItem.GetComponent<SortItem>().GetOrderedItems();

                        if (masterDriver.experinmentType == MasterDriver.ExperinmentType.RAY)
                            stage = Stage.SELECT;
                        else
                            stage = Stage.SORT;

                        masterDriver.WriteBasicInfo(userInfo);
                        Next();
                        return;
                    default:
                        Debug.LogError("tutorial switch case error");
                        break;
                }

                Destroy(currentActiveItem, 0.5f);
            }
                
            if (currentIntroItemIndex == 0)
            {
                StartCoroutine(DelayedFunctionRunner(() => {
                    currentActiveItem = Instantiate(sliderQuestionPrefab);
                }));
            }
            else if (currentIntroItemIndex == 3)
            {
                StartCoroutine(DelayedFunctionRunner(() => {
                    currentActiveItem = Instantiate(sortItemPrefab);
                    currentActiveItem.GetComponent<SortItem>().InsertItemData(sortItemData[currentSortItemIndex++]);
                })); 
            }
            else
            {
                StartCoroutine(DelayedFunctionRunner(() => {
                    currentActiveItem = Instantiate(selectItemPrefab);
                    currentActiveItem.GetComponent<SelectItem>().InsertItemData(selectItemData[currentSelectItemIndex++]);
                }));
            }

            ++currentIntroItemIndex;               
        }

        public void LoadSelectQuestion()
        {
            if (currentActiveItem.GetComponent<SelectItem>() != null)
                masterDriver.WriteItemResponse(currentActiveItem.GetComponent<Item>());

            if (currentSelectItemIndex >= selectItemData.Length)
            {
                if(masterDriver.experinmentType == MasterDriver.ExperinmentType.RAY)
                    stage = Stage.SORT;
                else
                    stage = Stage.OUTRO;

                Next();
                return;
            }
            
            if (currentActiveItem != null)
                Destroy(currentActiveItem, 0.5f);

            int randomInt = Random.Range(currentSelectItemIndex, selectItemData.Length);
            SelectItemData tempSID = selectItemData[randomInt];
            selectItemData[randomInt] = selectItemData[currentSelectItemIndex];
            selectItemData[currentSelectItemIndex] = tempSID;

            StartCoroutine(DelayedFunctionRunner(() => {
                currentActiveItem = Instantiate(selectItemPrefab);
                currentActiveItem.GetComponent<SelectItem>().InsertItemData(selectItemData[currentSelectItemIndex++]);
            }));
        }

        public void LoadSortQuestion()
        {
            if (currentActiveItem.GetComponent<SortItem>() != null && currentActiveItem.GetComponent<SortItem>().questionID != "tut3")
                masterDriver.WriteItemResponse(currentActiveItem.GetComponent<Item>());

            if (currentSortItemIndex >= sortItemData.Length)
            {
                if (masterDriver.experinmentType == MasterDriver.ExperinmentType.RAY)
                    stage = Stage.OUTRO;
                else
                    stage = Stage.SELECT;

                Next();
                return;
            }

            if (currentActiveItem != null)
                Destroy(currentActiveItem, 0.5f);

            int randomInt = Random.Range(currentSortItemIndex, sortItemData.Length);
            SortItemData tempSID = sortItemData[randomInt];
            sortItemData[randomInt] = sortItemData[currentSortItemIndex];
            sortItemData[currentSortItemIndex] = tempSID;

            StartCoroutine(DelayedFunctionRunner(() => {
                currentActiveItem = Instantiate(sortItemPrefab);
                currentActiveItem.GetComponent<SortItem>().InsertItemData(sortItemData[currentSortItemIndex++]);
            }));
        }

        public void LoadOutro()
        {
            masterDriver.CloseDataFile();
            Destroy(currentActiveItem, 0.5f);
            Application.Quit();
        }
    }    
}