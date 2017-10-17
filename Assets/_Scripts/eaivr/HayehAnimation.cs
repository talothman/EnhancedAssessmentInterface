using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class HayehAnimation : MonoBehaviour {

    public Animator hayehAnimator;
    public Animator textBoxAnimator;

    int showTextHash = Animator.StringToHash("ShowText");
    int hideTextHash = Animator.StringToHash("HideText");
    int goToRightTreeHash = Animator.StringToHash("RightTree");
    int spiralHash = Animator.StringToHash("Spiral");

    int rightTreeStateHash = Animator.StringToHash("Base Layer.RightTree");
    
    int goToLeftTreeHash = Animator.StringToHash("LeftTree");
    AnimatorStateInfo hayehStateInfo;
    bool onceAlready = false;

    public Text textBoxText;

    public GameObject leftHandModel;
    public GameObject rightHandModel;

    public GameObject leftHandScript;
    public GameObject rightHandScript;
    public GameObject leftToolTip;
    public GameObject rightToolTip;

    public GameObject treeLeft;
    public GameObject treeRight;

    public GameObject gameManager;

    public OvrAvatar avatar;

    public bool debug = false;

    void Start () {
        if(!debug)
            hayehStateInfo = hayehAnimator.GetCurrentAnimatorStateInfo(0);
        else
        {
            Debug();
        }
        //gameManager = GameObject.FindGameObjectWithTag("GM");
    }

    void Debug()
    {
        leftHandModel.SetActive(true);
        rightHandModel.SetActive(true);
        gameManager.SetActive(true);
        transform.parent.gameObject.SetActive(false);
        avatar.ShowControllers(false);
    }

    public void ShowIntroText()
    {
        if (!onceAlready)
        {
            StartCoroutine(IntroSecquence());
            onceAlready = true;

            //treeLeft.SetActive(false);
            //treeRight.SetActive(false);
        }
    }

    IEnumerator IntroSecquence()
    {
        textBoxAnimator.SetTrigger(showTextHash);
        yield return new WaitForSeconds(3f);

        textBoxAnimator.SetTrigger(hideTextHash);
        yield return new WaitForSeconds(2f);        
        textBoxText.text = "I'm going to help you get started!";
        textBoxAnimator.SetTrigger(showTextHash);
        yield return new WaitForSeconds(3f);
        textBoxAnimator.SetTrigger(hideTextHash);
        yield return new WaitForSeconds(2f);
        textBoxText.text = "Keep your eyes on me!";
        textBoxAnimator.SetTrigger(showTextHash);
        yield return new WaitForSeconds(2f);
        textBoxAnimator.SetTrigger(hideTextHash);
 
        hayehAnimator.SetTrigger(goToRightTreeHash);
    }

    public void RightTreeDelay()
    {
        StartCoroutine(DelayThenGoLeft());
    }
    
    IEnumerator DelayThenGoLeft()
    {
        //yield return new WaitForSeconds(2f);
        textBoxText.text = "Pretty cool tree wouldn't you say?";
        textBoxAnimator.SetTrigger(showTextHash);

        yield return new WaitForSeconds(3f);
        hayehAnimator.SetTrigger(goToLeftTreeHash);

        textBoxAnimator.SetTrigger(hideTextHash);
        yield return new WaitForSeconds(3f);
        textBoxText.text = "I made these trees all by myself! :)";
        textBoxAnimator.SetTrigger(showTextHash);

        yield return new WaitForSeconds(3f);
        textBoxAnimator.SetTrigger(hideTextHash);
        yield return new WaitForSeconds(3f);
        textBoxText.text = "Great job looking around!";
        textBoxAnimator.SetTrigger(showTextHash);
        yield return new WaitForSeconds(4f);

        hayehAnimator.SetTrigger(spiralHash);

        textBoxAnimator.SetTrigger(hideTextHash);
        yield return new WaitForSeconds(3f);
        textBoxText.text = "Look at the ground and center the ring with the white capsule.";
        textBoxAnimator.SetTrigger(showTextHash);
        yield return new WaitForSeconds(10f);

        textBoxAnimator.SetTrigger(hideTextHash);
        yield return new WaitForSeconds(3f);
        textBoxText.text = "Now look at your hands and move your fingers.";
        textBoxAnimator.SetTrigger(showTextHash);        

        avatar.ShowControllers(true);

        leftHandModel.SetActive(true);
        rightHandModel.SetActive(true);

        leftToolTip.SetActive(true);
        rightToolTip.SetActive(true);

        yield return new WaitForSeconds(10f);
        textBoxAnimator.SetTrigger(hideTextHash);

        yield return new WaitForSeconds(10f);

        if (leftHandScript.GetComponent<VRTK_Pointer>() != null ||
            rightHandScript.GetComponent<VRTK_Pointer>() != null)
        {
            leftHandScript.GetComponent<VRTK_Pointer>().enabled = true;
            rightHandScript.GetComponent<VRTK_Pointer>().enabled = true;

            textBoxText.text = "You will use the pointers connected to your hands to interact with the questions you will be asked. Use the marked button to select " +
                "and sort.";
        }
        else
        {
            textBoxText.text = "You will use your hands to interact with the questions you will be asked. Use the marked button to select " +
                "and sort.";
        }

        textBoxAnimator.SetTrigger(showTextHash);
        yield return new WaitForSeconds(20f);

        avatar.ShowControllers(false);
        leftToolTip.SetActive(false);
        rightToolTip.SetActive(false);

        textBoxAnimator.SetTrigger(hideTextHash);
        yield return new WaitForSeconds(3f);
        textBoxText.text = "Remember, you can use both of you hands to interact.";
        textBoxAnimator.SetTrigger(showTextHash);
        yield return new WaitForSeconds(5f);

        textBoxAnimator.SetTrigger(hideTextHash);
        yield return new WaitForSeconds(3f);
        textBoxText.text = "Let's start with some basic questions.";
        textBoxAnimator.SetTrigger(showTextHash);
        yield return new WaitForSeconds(5f);

        treeLeft.SetActive(false);
        treeRight.SetActive(false);

        gameManager.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
