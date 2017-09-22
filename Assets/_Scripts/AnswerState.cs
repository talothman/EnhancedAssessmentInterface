using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class AnswerState : MonoBehaviour {

    public bool isAnswer = false;
    public GameManager2DSort sceneGameManager;

    private void Awake()
    {
        sceneGameManager = GameObject.FindGameObjectWithTag("SceneGameManager").GetComponent(typeof(GameManager2DSort)) as GameManager2DSort;
        GetComponent<Button>().onClick.AddListener(() => sceneGameManager.SetCurrentlySelectedObject(gameObject));

    }

}
