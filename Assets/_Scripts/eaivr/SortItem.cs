using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eaivr
{
    public abstract class SortItem : Item
    {
        public abstract void InsertItemData(SortItemData sorItemData);
        public abstract string[] GetOrderedItems();
    }
}

