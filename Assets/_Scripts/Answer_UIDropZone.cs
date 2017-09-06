using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

public class Answer_UIDropZone : VRTK_UIDropZone {
    public GameManager2D gameManager;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
        {
            var dragItem = eventData.pointerDrag.GetComponent<VRTK_UIDraggableItem>();
            if (dragItem)
            {
                dragItem.validDropZone = gameObject;
                droppableItem = dragItem;
            }
        }
    }
}
