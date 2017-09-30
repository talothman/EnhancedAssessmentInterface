using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameManager : MonoBehaviour {

    public string pathToSelectBank;
    public string pathToSortBank;

    public GameObject selectPrefab;
    public GameObject sortPrefab;

    public abstract void Setup();
}
