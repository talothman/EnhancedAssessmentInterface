using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public abstract class Item : MonoBehaviour
    {
        public Text canvasText;
        protected bool answeredCorreclty;
        public GameObject submitGameObject;
        public abstract void NextQuestion();
        //public abstract void InsertItemData(ItemData itemData);
        public abstract void CheckSelectedAnswer();
    }
}

