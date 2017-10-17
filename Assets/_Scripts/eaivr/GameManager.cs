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

    enum Stage { TUTORIAL, SELECT, SORT, MEMORY, HEALTH, OUTRO }
    
    public class GameManager : MonoBehaviour
    {
        public string pathToSelectBank;
        public string pathToSortBank;
        public string pathToMemoryBank;

        public SelectItemData[] selectItemData;
        public SortItemData[] sortItemData;
        public SelectItemData[] memoryItemData;

        public GameObject sliderQuestionPrefab;
        //public GameObject sortItemTutorialPrefab;

        public GameObject selectItemPrefab;
        public GameObject sortItemPrefab;

        int currentIntroItemIndex = 0;
        int currentSelectItemIndex = 0;
        int currentSortItemIndex = 0;
        int currentMemoryQuestionItemIndex = 0;

        public GameObject currentActiveItem;

        bool finishedAllIntroQuestions = false;

        public UserInfo userInfo;
        private MasterDriver masterDriver;

        private Stage stage;
        private HayehAnimation hayehAnimation;

        delegate void DelayedFunction();

        float tutStartTime, tutEndTime;

        public ParticleSystem particleSystem;
        public GameObject particleParty;

        private void Start()
        {
            masterDriver = GameObject.FindGameObjectWithTag("MD").GetComponent<MasterDriver>();
            //hayehAnimation = GameObject.FindGameObjectWithTag("hayeh").GetComponent<HayehAnimation>();
            stage = Stage.TUTORIAL;
            LoadQuestionsFromDisk();
        }

        public void LoadQuestionsFromDisk()
        {

#if UNITY_EDITOR
            pathToSelectBank = Application.dataPath + "/_Xml/selectSerial.xml";
            pathToSortBank = Application.dataPath + "/_Xml/sortSerial.xml";
            pathToMemoryBank = Application.dataPath + "/_Xml/memorySerial.xml";

            selectItemData = XmlUtility.Deserialize<SelectItemData[]>(pathToSelectBank);
            sortItemData = XmlUtility.Deserialize<SortItemData[]>(pathToSortBank);
            memoryItemData = XmlUtility.Deserialize<SelectItemData[]>(pathToMemoryBank);
#endif

#if UNITY_STANDALONE_WIN           

            selectItemData = XmlUtility.DeserializeBuild<SelectItemData[]>("selectSerial");
            sortItemData = XmlUtility.DeserializeBuild<SortItemData[]>("sortSerial");
            memoryItemData = XmlUtility.DeserializeBuild<SelectItemData[]>("memorySerial");
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
            else if (stage == Stage.MEMORY)
                LoadMemoryQuestion();
            else if (stage == Stage.HEALTH)
                LoadHealthQuestions();
            else if (stage == Stage.OUTRO)
                LoadOutro();
        }

        IEnumerator DelayedFunctionRunner(DelayedFunction delayedFunction)
        {
            yield return new WaitForSeconds(0.9f);
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
                        tutStartTime = Time.time;
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

                        tutEndTime = Time.time;
                        masterDriver.WriteBasicInfo((tutEndTime - tutStartTime), userInfo);
                        particleSystem.Play();
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
                    currentActiveItem.GetComponent<SliderItem>().InsertSliderData("age0", "Set your age by dragging" +
                        " the handle left or right", 16, 65);
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
                    stage = Stage.MEMORY;

                particleSystem.Play();
                Next();
                return;
            }
            
            if (currentActiveItem != null)
                Destroy(currentActiveItem, 0.5f);

            int randomInt = Random.Range(currentSelectItemIndex, selectItemData.Length);
            SelectItemData tempSID = selectItemData[randomInt];
            selectItemData[randomInt] = selectItemData[currentSelectItemIndex];
            selectItemData[currentSelectItemIndex] = tempSID;

            if(currentSelectItemIndex == 2)
            {
                selectItemData[currentSelectItemIndex].stem = "Now, " + selectItemData[currentSelectItemIndex].stem;
            }

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
                    stage = Stage.MEMORY;
                else
                    stage = Stage.SELECT;

                particleSystem.Play();
                Next();
                return;
            }

            if (currentActiveItem != null)
                Destroy(currentActiveItem, 0.5f);

            int randomInt = Random.Range(currentSortItemIndex, sortItemData.Length);
            SortItemData tempSID = sortItemData[randomInt];
            sortItemData[randomInt] = sortItemData[currentSortItemIndex];
            sortItemData[currentSortItemIndex] = tempSID;

            if (currentSortItemIndex == 1)
            {
                sortItemData[currentSortItemIndex].stem = "Now, " + sortItemData[currentSortItemIndex].stem;
            }

            StartCoroutine(DelayedFunctionRunner(() => {
                currentActiveItem = Instantiate(sortItemPrefab);
                currentActiveItem.GetComponent<SortItem>().InsertItemData(sortItemData[currentSortItemIndex++]);
            }));
        }

        public void LoadMemoryQuestion()
        {
            if (currentMemoryQuestionItemIndex != 0)
                masterDriver.WriteItemResponse(currentActiveItem.GetComponent<Item>());

            if (currentMemoryQuestionItemIndex >= memoryItemData.Length)
            {                
                stage = Stage.HEALTH;

                particleSystem.Play();
                Next();
                return;
            }

            if (currentActiveItem != null)
                Destroy(currentActiveItem, 0.5f);

            int randomInt = Random.Range(currentMemoryQuestionItemIndex, memoryItemData.Length);
            SelectItemData tempSID = memoryItemData[randomInt];
            memoryItemData[randomInt] = memoryItemData[currentMemoryQuestionItemIndex];
            memoryItemData[currentMemoryQuestionItemIndex] = tempSID;

            if (currentMemoryQuestionItemIndex == 0)
            {
                memoryItemData[currentMemoryQuestionItemIndex].stem = "Now, " + memoryItemData[currentMemoryQuestionItemIndex].stem;
            }

            StartCoroutine(DelayedFunctionRunner(() => {
                currentActiveItem = Instantiate(selectItemPrefab);
                currentActiveItem.GetComponent<SelectItem>().InsertItemData(memoryItemData[currentMemoryQuestionItemIndex++]);
            }));
        }

        int currentHealthQuestionIndex = 0;

        public void LoadHealthQuestions()
        {
            if(currentHealthQuestionIndex == 0)
            {
                if (currentActiveItem != null)
                    Destroy(currentActiveItem, 0.5f);

                StartCoroutine(DelayedFunctionRunner(() => {
                    currentActiveItem = Instantiate(sliderQuestionPrefab);
                    currentActiveItem.GetComponent<SliderItem>().InsertSliderData("hel0", "On a scale from 1 to 5, with 1 being not comfortable at" +
                        " all and 5 being very comfortable, how comfortable was the experience?", 1, 5);
                    currentHealthQuestionIndex++;
                }));

            }
            else if(currentHealthQuestionIndex == 1)
            {
                masterDriver.WriteSliderResponse(currentActiveItem.GetComponent<SliderItem>());

                if (currentActiveItem != null)
                    Destroy(currentActiveItem, 0.5f);

                StartCoroutine(DelayedFunctionRunner(() => {
                    currentActiveItem = Instantiate(sliderQuestionPrefab);
                    currentActiveItem.GetComponent<SliderItem>().InsertSliderData("hel1", "On a scale from 1 to 5, with 1 being not immersed" +
                        " and 5 being very immersed, how immersive was the experience?", 1, 5);
                    currentHealthQuestionIndex++;
                }));
            }
            else if (currentHealthQuestionIndex == 2)
            {
                masterDriver.WriteSliderResponse(currentActiveItem.GetComponent<SliderItem>());
                //write to file
                stage = Stage.OUTRO;
                Next();
            }
        }

        public void LoadOutro()
        {
            masterDriver.CloseDataFile();
            Destroy(currentActiveItem, 0.5f);
            
            StartCoroutine(PartyThenDie());
        }

        IEnumerator PartyThenDie()
        {
            particleParty.SetActive(true);
            yield return new WaitForSeconds(30f);
            Application.Quit();
        }
    }    
}