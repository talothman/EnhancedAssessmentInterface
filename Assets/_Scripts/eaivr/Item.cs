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
        public GameObject submitGameObject;
        public abstract void CheckSelectedAnswers();
        public virtual void NextQuestion()
        {
            timeEnded = Time.time;
            gameManager.Next();
        }

        public bool firstInteraction = false;

        public bool answeredCorreclty;
        public string questionID;
        public float timeStart;
        public float timeToFirstInteraction;
        public float timeEnded;
        public int numOfInteractions;
    }
}

