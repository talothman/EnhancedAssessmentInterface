using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eaivr
{
    public abstract class Item : MonoBehaviour
    {
        public Text canvasText;
        public GameManager gameManager;
        protected bool answeredCorreclty;
        public GameObject submitGameObject;
        //public abstract void InsertItemData(ItemData itemData);
        public abstract void CheckSelectedAnswer();
        public void NextQuestion()
        {
            gameManager.Next();
        }
    }
}

