using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortAnswerState : MonoBehaviour {

    public int order;
    public GameManager2DSort sceneGameManager;
    public SortGroup sortGroup;

    private void Awake()
    {
        sceneGameManager = GameObject.FindGameObjectWithTag("SceneGameManager").GetComponent<GameManager2DSort>();
        sortGroup = GetComponentInParent<SortGroup>();
    }
}
