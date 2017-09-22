using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTK;

public class GameManager2DSort : MonoBehaviour {

    protected enum GameManagerState {SELECTION, MINIPULATION};

    protected GameManagerState gameManagerState;
    public GameObject[] selectGameObjects;
    public GameObject[] manipGameObjects;
    public GameObject currentlySelectedObject;
    public int GoToLevel = 1;
    [SerializeField]
    private Image[] selectAnswers;
    [SerializeField]
    private Image[] manipulationAnswers;
    [SerializeField]
    private Button submitButton;

    private void Start()
    {
        gameManagerState = GameManagerState.SELECTION;
        VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>().TouchpadPressed += 
            (object sender, ControllerInteractionEventArgs e) => {
                SceneManager.LoadScene(GoToLevel);
            };

        VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>().TouchpadPressed +=
            (object sender, ControllerInteractionEventArgs e) => {
                SceneManager.LoadScene(GoToLevel);
            };
    }

    public virtual void SetCurrentlySelectedObject(GameObject selectedObject)
    {
        foreach(Image i in selectAnswers)
        {
            i.color = Color.white;
        }

        selectedObject.GetComponent<Image>().color = Color.yellow;
        if(!submitButton.interactable)
            submitButton.interactable = true;

        currentlySelectedObject = selectedObject;
    }

    public virtual void CheckSelectionAnswer()
    {
        if (currentlySelectedObject == null)
            return;

        if (currentlySelectedObject.GetComponent<AnswerState>().isAnswer)
            currentlySelectedObject.GetComponent<Image>().color = Color.green;
        else
            currentlySelectedObject.GetComponent<Image>().color = Color.red;

        StartCoroutine(MoveToManipulation());
    }

    protected int questionIterationIndex = 0;

    public virtual IEnumerator MoveToManipulation()
    {
        yield return new WaitForSeconds(3f);
        print("heeeere0");
        if (gameManagerState == GameManagerState.SELECTION && questionIterationIndex >= 2)
        {
            selectGameObjects[questionIterationIndex].SetActive(false);
            questionIterationIndex = 0;
            manipGameObjects[questionIterationIndex].SetActive(true);

            gameManagerState = GameManagerState.MINIPULATION;
            print("heeeere1");
        }
        else if (gameManagerState == GameManagerState.MINIPULATION && questionIterationIndex >= 2)
        {
            print("heeeere2");
            LoadFinalLevel();
        }
        else if (questionIterationIndex < 2)
        {
            print("heeeere3");
            if (gameManagerState == GameManagerState.SELECTION)
            {
                selectGameObjects[questionIterationIndex++].SetActive(false);
                selectGameObjects[questionIterationIndex].SetActive(true);

                foreach (Transform t in selectGameObjects[questionIterationIndex].transform)
                {
                    foreach (Transform childTransform in t.transform)
                    {
                        if (childTransform.gameObject.name.Equals("SubmitButton"))
                        {
                            submitButton = childTransform.GetComponent<Button>();
                        }
                    }
                }
            }
            else
            {
                manipGameObjects[questionIterationIndex++].SetActive(false);
                manipGameObjects[questionIterationIndex].SetActive(true);

                foreach (Transform t in manipGameObjects[questionIterationIndex].transform)
                {
                    foreach (Transform childTransform in t.transform)
                    {
                        if (childTransform.gameObject.name.Equals("SubmitButton"))
                        {
                            submitButton = childTransform.GetComponent<Button>();
                        }
                    }
                }
            }
        }
    }

    public virtual void LoadFinalLevel()
    {
        SceneManager.LoadScene(1);
    }
}
