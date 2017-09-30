using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TutorialManager : MonoBehaviour {

    public GameObject[] textPanels;
    public GameObject[] selectButtons;
    public GameObject[] sortButtons;
    public GameObject[] submitButtons;
    public Light sceneDirectionalLight;

    public virtual void SetDirectionalLigthAngle(float angle)
    {
        Vector3 currentLightDirection = sceneDirectionalLight.transform.rotation.eulerAngles;
        currentLightDirection.x = angle;
        sceneDirectionalLight.transform.rotation = Quaternion.Euler(currentLightDirection);
    }

    public abstract void SetupSelectionTutorial();
    public abstract void SetupSortingTutorial();
}
