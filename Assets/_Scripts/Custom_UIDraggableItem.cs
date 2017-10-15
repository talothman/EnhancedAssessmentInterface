using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

namespace eaivr
{
    public class Custom_UIDraggableItem : VRTK_UIDraggableItem
    {
        public override void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            dragTransform = null;
            transform.position += (transform.forward * forwardOffset);
            bool validDragEnd = true;


            if (validDropZone != null)
            {
                transform.SetParent(validDropZone.transform);
                //UpdateSortState();
            }
            else
            {
                //ResetElement();
                validDragEnd = false;
            }

            Canvas destinationCanvas = (eventData.pointerEnter != null ? eventData.pointerEnter.GetComponentInParent<Canvas>() : null);
            if (restrictToOriginalCanvas)
            {
                if (destinationCanvas != null && destinationCanvas != startCanvas)
                {
                    //ResetElement();
                    validDragEnd = false;
                }
            }

            if (destinationCanvas == null)
            {
                //We've been dropped off of a canvas
                //ResetElement();
                validDragEnd = false;
            }

            if (validDragEnd)
            {
                VRTK_UIPointer pointer = GetPointer(eventData);
                if (pointer != null)
                {
                    pointer.OnUIPointerElementDragEnd(pointer.SetUIPointerEvent(pointer.pointerEventData.pointerPressRaycast, gameObject));
                }
                OnDraggableItemDropped(SetEventPayload(validDropZone));
                UpdateSortState();
                
            }

            UpdateSortState();
            
            validDropZone = null;
            startParent = null;
            startCanvas = null;

            GetComponent<SortAnswer2D>().grabbed = false;
        }

        public void UpdateSortState()
        {
            (GetComponent<SortAnswer2D>().sortAnswerGroup as SortAnswerGroup2D).UpdateSortState();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            if (!GetComponent<SortAnswer2D>().sortItem.submitGameObject.activeInHierarchy)
                GetComponent<SortAnswer2D>().sortItem.submitGameObject.SetActive(true);

            GetComponent<SortAnswer2D>().grabbed = true;
            GetComponent<SortAnswer2D>().IncrementInteraction();
        }

    }
}

