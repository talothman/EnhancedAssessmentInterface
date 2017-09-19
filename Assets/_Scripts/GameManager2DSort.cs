using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2DSort : MonoBehaviour {

    enum GameManagerState {SELECTION, MINIPULATION};

    GameManagerState twoDgameManagerState;
    public GameObject selectGameObject;
    public GameObject manipGameObject;
    public GameObject currentlySelectedObject;

    public Image[] selectAnswers;
    public Image[] manipulationAnswers;
    public Button submitButton;

    private void Start()
    {
        twoDgameManagerState = GameManagerState.SELECTION;
    }

    public void SetCurrentlySelectedObject(GameObject selectedObject)
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

    public void CheckSelectionAnswer()
    {
        if (currentlySelectedObject == null)
            return;

        if (currentlySelectedObject.GetComponent<AnswerState>().isAnswer)
            currentlySelectedObject.GetComponent<Image>().color = Color.green;
        else
            currentlySelectedObject.GetComponent<Image>().color = Color.red;

        StartCoroutine(MoveToManipulation());
    }

    public void CheckManipulationAnswer()
    {
        //
    }

    public IEnumerator MoveToManipulation()
    {
        yield return new WaitForSeconds(3f);

        if (twoDgameManagerState == GameManagerState.SELECTION)
        {
            selectGameObject.SetActive(false);
            manipGameObject.SetActive(true);

            twoDgameManagerState = GameManagerState.MINIPULATION;
        }
        else
        {
            //LoadFinalLevel();
        }
    }

    public void LoadFinalLevel()
    {
        SceneManager.LoadScene(3);
    }
}
