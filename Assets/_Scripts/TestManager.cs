using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TestManager : MonoBehaviour {
    public GameObject ThreeDGameObject;
    public GameObject TwoDCanvas;

    GameObject rightHand;
    GameObject leftHand;

    public enum TestMode
    {
        D3, D2
    }

    TestMode currentState;

    void Start () {
        currentState = TestMode.D3;

        rightHand = VRTK_DeviceFinder.GetControllerRightHand();
        leftHand = VRTK_DeviceFinder.GetControllerLeftHand();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleTestMode();
        }
	}

    void ToggleTestMode()
    {
        if(currentState == TestMode.D3)
        {
            //TODO move to 2D
            ThreeDGameObject.SetActive(false);
            TwoDCanvas.SetActive(true);

            rightHand.GetComponent<VRTK_UIPointer>().enabled = true;
            rightHand.GetComponent<VRTK_Pointer>().enabled = true;
            rightHand.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;

            leftHand.GetComponent<VRTK_UIPointer>().enabled = true;
            leftHand.GetComponent<VRTK_Pointer>().enabled = true;
            leftHand.GetComponent<VRTK_StraightPointerRenderer>().enabled = true;
            currentState = TestMode.D2;

            
        }
        else
        {
            TwoDCanvas.SetActive(false);
            ThreeDGameObject.SetActive(true);

            rightHand.GetComponent<VRTK_UIPointer>().enabled = false;
            rightHand.GetComponent<VRTK_Pointer>().enabled = false;
            rightHand.GetComponent<VRTK_StraightPointerRenderer>().enabled = false;

            leftHand.GetComponent<VRTK_UIPointer>().enabled = false;
            leftHand.GetComponent<VRTK_Pointer>().enabled = false;
            leftHand.GetComponent<VRTK_StraightPointerRenderer>().enabled = false;

            currentState = TestMode.D3;            
        }
    }
}
