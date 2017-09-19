using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class TimedSelect : MonoBehaviour {
    VRTK_InteractableObject vrtkInteractableObject;
    IEnumerator timerCoroutine;
    float currentTimerCountdown;
    public Image radialImage;
    public float timeToSelect = 3f;
    public GameManager3D game3DManager;

	// Use this for initialization
	void Start () {
        vrtkInteractableObject = GetComponent<VRTK_InteractableObject>();
        timerCoroutine = Timer();
        if(vrtkInteractableObject == null)
        {
            print("Missing VRTK_InteractableObject script");
            return;
        }

        vrtkInteractableObject.InteractableObjectTouched += StartTimer;
        vrtkInteractableObject.InteractableObjectUntouched += StopTimer;
    }
	
	private void StartTimer(object sender, InteractableObjectEventArgs e)
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

        game3DManager.CheckAnswer(gameObject);
    }
}

