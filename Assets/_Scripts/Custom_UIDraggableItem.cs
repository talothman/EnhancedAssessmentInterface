using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

public class Custom_UIDraggableItem : VRTK_UIDraggableItem {
    public GameManager2D gameManager2D;

    public override void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        dragTransform = null;
        transform.position += (transform.forward * forwardOffset);
        bool validDragEnd = true;
        
        
        if (validDropZone != null)
        {
            transform.SetParent(validDropZone.transform);
            gameManager2D.CheckAnswer(gameObject);
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
        }

        validDropZone = null;
        startParent = null;
        startCanvas = null;
    }
}
