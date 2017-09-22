using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRTK;
using VRTK.Highlighters;

public class GameManager3DSort : GameManager2DSort {
    public Material TransparentCubeMat;
    Material originalMaterial;
    Material selectedMaterial;
    public Color selectColor;

    [SerializeField]
    private Renderer[] selectAnswerRenderers;
    [SerializeField]
    private VRTK_Button vrtk_Button;

    private void Start()
    {
        originalMaterial = new Material(TransparentCubeMat);
        originalMaterial.CopyPropertiesFromMaterial(TransparentCubeMat);

        selectedMaterial = new Material(TransparentCubeMat)
        {
            color = selectColor
        };

        foreach (Renderer r in selectAnswerRenderers)
        {
            r.gameObject.GetComponent<AnswerState3D>().isSelected = false;
        }

        currentlySelectedObject = null;

        VRTK_DeviceFinder.GetControllerRightHand().GetComponent<VRTK_ControllerEvents>().TouchpadPressed +=
            (object sender, ControllerInteractionEventArgs e) => {
                SceneManager.LoadScene(GoToLevel);
            };

        VRTK_DeviceFinder.GetControllerLeftHand().GetComponent<VRTK_ControllerEvents>().TouchpadPressed +=
            (object sender, ControllerInteractionEventArgs e) => {
                SceneManager.LoadScene(GoToLevel);
            };
    }

    public override void SetCurrentlySelectedObject(GameObject selectedObject)
    {
        foreach (Renderer r in selectAnswerRenderers)
        {
            r.gameObject.GetComponent<AnswerState3D>().isSelected = false;
        }

        selectedObject.GetComponent<AnswerState3D>().isSelected = true;

        if (!vrtk_Button.gameObject.activeInHierarchy)
            vrtk_Button.gameObject.SetActive(true);

        currentlySelectedObject = selectedObject;
    }

    public void HighlightSelected(object sender, InteractableObjectEventArgs e)
    {
        foreach (Renderer r in selectAnswerRenderers)
        {
            if (!r.gameObject.GetComponent<AnswerState3D>().isSelected)
            {
                r.material = TransparentCubeMat;
            }
            else
            {
                StartCoroutine(WaitForUntouch());
            }
        }
    }

    IEnumerator WaitForUntouch()
    {
        VRTK_InteractableObject currentlySelectedObjectIO = currentlySelectedObject.GetComponent<VRTK_InteractableObject>();

        while (currentlySelectedObjectIO.IsTouched())
            yield return null;

        currentlySelectedObject.GetComponent<Renderer>().material = selectedMaterial;
        print("Highlight Selected: " + currentlySelectedObject);
    }

    public override void CheckSelectionAnswer()
    {
        if (currentlySelectedObject == null)
            return;

        if (currentlySelectedObject.GetComponent<AnswerState3D>().isAnswer)
            currentlySelectedObject.GetComponent<Renderer>().material.color = Color.green;
        else
            currentlySelectedObject.GetComponent<Renderer>().material.color = Color.red;

        StartCoroutine(MoveToManipulation());
    }

    public override IEnumerator MoveToManipulation()
    {
        yield return new WaitForSeconds(3f);
        print("heeeere0");
        if (gameManagerState == GameManagerState.SELECTION && questionIterationIndex >= 2)
        {
            selectGameObjects[questionIterationIndex].SetActive(false);
            questionIterationIndex = 0;
            manipGameObjects[questionIterationIndex].SetActive(true);

            gameManagerState = GameManagerState.MINIPULATION;
            print("heeeere1");
        }
        else if (gameManagerState == GameManagerState.MINIPULATION && questionIterationIndex >= 2)
        {
            print("heeeere2");
            LoadFinalLevel();
        }
        else if (questionIterationIndex < 2)
        {
            print("heeeere3");
            if (gameManagerState == GameManagerState.SELECTION)
            {
                selectGameObjects[questionIterationIndex++].SetActive(false);
                selectGameObjects[questionIterationIndex].SetActive(true);
                print("heeeere4");
                foreach (Transform t in selectGameObjects[questionIterationIndex].transform)
                {
                    if (t.gameObject.name.Equals("SubmitButton"))
                    {
                        vrtk_Button = t.GetComponent<VRTK_Button>();
                    }
                }

                AnswerState3D[] newAnswers = selectGameObjects[questionIterationIndex].GetComponentsInChildren<AnswerState3D>();
                for(int i = 0; i < selectAnswerRenderers.Length; i++)
                {
                    selectAnswerRenderers[i] = newAnswers[i].GetComponent<Renderer>();
                }
            }
            else
            {
                print("heeeere5");
                manipGameObjects[questionIterationIndex++].SetActive(false);
                manipGameObjects[questionIterationIndex].SetActive(true);

                foreach (Transform t in manipGameObjects[questionIterationIndex].transform)
                {
                    if (t.gameObject.name.Equals("SubmitButton"))
                    {
                        vrtk_Button = t.GetComponent<VRTK_Button>();
                    }
                }
            }
        }
    }

    public override void LoadFinalLevel()
    {
        SceneManager.LoadScene(0);
    }
}
