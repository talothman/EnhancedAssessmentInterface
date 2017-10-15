using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace eaivr
{
    public class SortAnswer2D : SortAnswer
    {
        Custom_UIDraggableItem uiDraggable;
        
        void Start()
        {
            sortItem = GetComponentInParent<SortItem>();
            sortAnswerGroup = GetComponentInParent<SortAnswerGroup2D>();

            uiDraggable = GetComponent<Custom_UIDraggableItem>();
            uiDraggable.DraggableItemDropped += IncrementInteractionOnDrop; ;
        }

        private void IncrementInteractionOnDrop(object sender, VRTK.UIDraggableItemEventArgs e)
        {
            IncrementInteraction();
        }

        private void Update()
        {
            if(grabbed)
                (sortAnswerGroup as SortAnswerGroup2D).UpdateSortState();
        }

        
    }
}

