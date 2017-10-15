using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr{
    public abstract class SortAnswer : Answer
    {
        public int correctOrder;
        public int currentOrder;
        public bool grabbed;
        public SortAnswerGroup sortAnswerGroup;
        public SortItem sortItem;
        
        public void IncrementInteraction()
        {
            if (!sortItem.firstInteraction)
            {
                sortItem.firstInteraction = true;
                sortItem.timeToFirstInteraction = Time.time;
            }

            ++sortItem.numOfInteractions;
        }
    }
}

