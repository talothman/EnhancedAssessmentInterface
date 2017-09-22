using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2DSort : MonoBehaviour {

    enum GameManagerState {SELECTION, MINIPULATION};

    GameManagerState gameManagerState;
    public GameObject selectGameObject;
    public GameObject manipGameObject;
    public GameObject currentlySelectedObject;

    [SerializeField]
    private Image[] selectAnswers;
    [SerializeField]
    private Image[] manipulationAnswers;
    [SerializeField]
    private Button submitButton;

    private void Start()
    {
        gameManagerState = GameManagerState.SELECTION;
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

    public IEnumerator MoveToManipulation()
    {
        yield return new WaitForSeconds(3f);

        if (gameManagerState == GameManagerState.SELECTION)
        {
            selectGameObject.SetActive(false);
            manipGameObject.SetActive(true);

            gameManagerState = GameManagerState.MINIPULATION;
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
