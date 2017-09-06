using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void Load2D()
    {
        SceneManager.LoadScene(1);
    }

    public void Load3D()
    {
        SceneManager.LoadScene(2);
    }
}
