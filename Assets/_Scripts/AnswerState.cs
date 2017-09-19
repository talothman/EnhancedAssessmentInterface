using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerState : MonoBehaviour {

    public bool isAnswer = false;
    public GameManager2DSort sceneGameManager;

    private void Awake()
    {
        sceneGameManager = GameObject.FindGameObjectWithTag("SceneGameManager").GetComponent<GameManager2DSort>();
        GetComponent<Button>().onClick.AddListener(() => sceneGameManager.SetCurrentlySelectedObject(gameObject));
    }

}
