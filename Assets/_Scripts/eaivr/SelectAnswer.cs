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
            if (!selectItemParent.firstInteraction)
            {
                selectItemParent.firstInteraction = true;
                selectItemParent.timeToFirstInteraction = Time.time;
            }

            ++selectItemParent.numOfInteractions;
        }
    }
}

