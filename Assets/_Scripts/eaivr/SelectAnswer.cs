using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    public abstract class SelectAnswer : Answer {

        public bool isKey, isSelected;
        protected SelectItem selectItemParent;

        public void IncrementInteraction()
        {
            ++selectItemParent.numOfInteractions;
        }
    }
}

