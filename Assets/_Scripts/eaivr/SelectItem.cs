using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    public abstract class SelectItem : Item
    {
        public Color selectColor;
        public abstract void SetSelectedAnswer(GameObject selectedObject);
        public abstract void InsertItemData(SelectItemData selectItemData);
        public abstract string GetSelectedAnswer();
    }
}

