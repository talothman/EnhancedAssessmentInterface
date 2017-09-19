using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2D : MonoBehaviour {

    public GameObject selectGameObject;
    public GameObject manipGameObject;

    public void CheckAnswer(GameObject answerObject)
    {
        if (answerObject.GetComponent<AnswerState>().isAnswer)
            answerObject.GetComponent<Image>().color = Color.green;
        else
            answerObject.GetComponent<Image>().color = Color.red;

        StartCoroutine(MoveToManipulation());
    }

    public IEnumerator MoveToManipulation()
    {
        yield return new WaitForSeconds(3f);

        if(manipGameObject.activeInHierarchy != true)
        {
            selectGameObject.SetActive(false);
            manipGameObject.SetActive(true);
        }
        else
        {
            LoadFinalLevel();
        }
    }

    public void LoadFinalLevel()
    {
        SceneManager.LoadScene(3);
    }
}
