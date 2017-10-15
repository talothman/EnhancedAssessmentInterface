using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

namespace eaivr
{
    public class SortAnswer3D : SortAnswer
    {
        VRTK_InteractableObject interactableObject;
        
        void Start()
        {
            interactableObject = GetComponent<VRTK_InteractableObject>();
            sortAnswerGroup = GetComponentInParent<SortAnswerGroup3D>();
            sortItem = GetComponentInParent<SortItem3D>();

            interactableObject.InteractableObjectGrabbed += OnGrabbed;
            interactableObject.InteractableObjectUngrabbed += OnUngrabbed;
        }

        protected void OnGrabbed(object sender, InteractableObjectEventArgs e)
        {
            grabbed = true;

            IncrementInteraction();

            if (!sortItem.submitGameObject.activeInHierarchy)
                sortItem.submitGameObject.SetActive(true);
        }

        protected void OnUngrabbed(object sender, InteractableObjectEventArgs e)
        {
            grabbed = false;
        }

        private void Update()
        {
            if(grabbed)
                (sortAnswerGroup as SortAnswerGroup3D).UpdateSortState();
        }

    }
}

