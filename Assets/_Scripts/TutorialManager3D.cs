using UnityEngine;
using System.Collections;
using VRTK;
using VRTK.UnityEventHelper;

public class TutorialManager3D : TutorialManager
{
    bool transitioning = false;
    Coroutine sunTransitionCoroutine;

    public Material defaultMat;
    Material selectedMat;
    public Color selectColor;
    int stateIndex = 0;

    private void Start()
    {
        SetupSelectionTutorial();
    }

    public override void SetupSelectionTutorial()
    {
        selectedMat = new Material(defaultMat);
        selectedMat.color = selectColor;

        submitButtons[0].GetComponent<VRTK_Button_UnityEvents>().OnPushed.AddListener((object sender, Control3DEventArgs e) =>
        {
            SetupSortingTutorial();
        });
    }

    public override void SetupSortingTutorial()
    {
        selectButtons[0].transform.parent.gameObject.SetActive(false);
        sortButtons[0].transform.parent.gameObject.SetActive(true);

    }

    public void SetSelectedButton(GameObject selectGO)
    {
        submitButtons[0].SetActive(true);
        foreach (GameObject button in selectButtons)
        {
            button.GetComponent<SelectState3DButton>().isSelected = false;
        }

        selectGO.GetComponent<SelectState3DButton>().isSelected = true;
        SetDirectionalLigthAngle(selectGO.GetComponent<SelectState3DButton>().sunAngle);
    }

    public override void SetDirectionalLigthAngle(float angle)
    {
        if(!transitioning)
            sunTransitionCoroutine = StartCoroutine(LerpSun(angle));
        else
        {
            StopCoroutine(sunTransitionCoroutine);
            transitioning = false;
            sunTransitionCoroutine = StartCoroutine(LerpSun(angle));
        }
    }

    private IEnumerator LerpSun(float targetAngle)
    {
        transitioning = true;

        Vector3 targetRotation = sceneDirectionalLight.transform.rotation.eulerAngles;
        targetRotation.x = targetAngle;
        Quaternion targetRotationQuat = Quaternion.Euler(targetRotation);

        if(Vector3.Distance(sceneDirectionalLight.transform.rotation.eulerAngles, targetRotation) > 0.01f)
        {
            while (sceneDirectionalLight.transform.rotation != targetRotationQuat)
            {
                sceneDirectionalLight.transform.rotation = Quaternion.Slerp(sceneDirectionalLight.transform.rotation, targetRotationQuat, Time.deltaTime);
                yield return null;
            }
        }
        transitioning = false;
    }

    public void HighlightSelected(object sender, InteractableObjectEventArgs e)
    {
        foreach(GameObject selectButton in selectButtons)
        {
            if (!selectButton.GetComponent<SelectState3DButton>().isSelected)
            {
                selectButton.GetComponent<Renderer>().material = defaultMat;
            }
            else
                StartCoroutine(WaitForUntouch(selectButton));
        }
    }

    IEnumerator WaitForUntouch(GameObject selectedButton)
    {
        VRTK_InteractableObject currentlySelectedObjectIO = selectedButton.GetComponent<VRTK_InteractableObject>();

        while (currentlySelectedObjectIO.IsTouched())
            yield return null;

        selectedButton.GetComponent<Renderer>().material = selectedMat;
    }
}
