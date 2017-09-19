using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetCube : MonoBehaviour {
    VRTK_InteractableObject vrtkInteractableObject;
    IEnumerator timerCoroutine;
    public Image radialImage;
    public float timeToSelect = 3f;
    float currentTimerCountdown;
    // Use this for initialization
    void Start () {
        vrtkInteractableObject = GetComponent<VRTK_InteractableObject>();
        vrtkInteractableObject.InteractableObjectTouched += Reset;

        timerCoroutine = Timer();
    }

    private void Reset(object sender, InteractableObjectEventArgs e)
    {
        radialImage.enabled = true;
        StartCoroutine(timerCoroutine);
    }

    private void StopTimer(object sender, InteractableObjectEventArgs e)
    {
        StopCoroutine(timerCoroutine);
        currentTimerCountdown = timeToSelect;
        radialImage.enabled = false;
    }

    private IEnumerator Timer()
    {
        currentTimerCountdown = timeToSelect;
        float normalizedFloat = 0f;

        while (currentTimerCountdown > 0f)
        {
            currentTimerCountdown -= Time.deltaTime;
            normalizedFloat = Mathf.Clamp01(1 - (currentTimerCountdown / timeToSelect));
            radialImage.fillAmount = normalizedFloat;
            print(normalizedFloat);
            yield return null;
        }

        SceneManager.LoadScene(0);
    }
}
