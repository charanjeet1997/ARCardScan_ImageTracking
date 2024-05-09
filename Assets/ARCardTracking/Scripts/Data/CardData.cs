using System;
using System.Collections;
using UnityEngine;

namespace Game.ARCardTracking
{
    [Serializable]
    public class CardData
    {
        public string cardName;
        public GameObject objectToShow;
        public float objectScale = 1f;
    }
}

