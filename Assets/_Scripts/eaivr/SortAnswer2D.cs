using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    public class SortAnswer2D : SortAnswer
    {
        Custom_UIDraggableItem uiDraggable;
        // Use this for initialization
        void Start()
        {
            sortItem = GetComponentInParent<SortItem>();
            sortAnswerGroup = GetComponentInParent<SortAnswerGroup2D>();

            uiDraggable = GetComponent<Custom_UIDraggableItem>();
        }

        private void Update()
        {
            if(grabbed)
                (sortAnswerGroup as SortAnswerGroup2D).UpdateSortState();
        }
    }
}

