using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using VRTK;

public class GameManager3D : MonoBehaviour {

    public GameObject selectGameObject;
    public GameObject manipGameObject;
    //bool loaded = false;
    public Material answerFeedbackMaterial;

    public void CheckAnswer(GameObject answerObject)
    {
        answerFeedbackMaterial = new Material(answerObject.GetComponent<Renderer>().material);

        if (answerObject.GetComponent<AnswerState>().isAnswer)
            answerFeedbackMaterial.color = new Color(0f, 255f, 0f, 111f);
        else
            answerFeedbackMaterial.color = new Color(255f, 0f, 0f, 111f);

        answerObject.GetComponent<Renderer>().material = answerFeedbackMaterial;
        StartCoroutine(MoveToManipulation());
    }

    public IEnumerator MoveToManipulation()
    {
        yield return new WaitForSeconds(3f);

        if (manipGameObject.activeInHierarchy != true)
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
