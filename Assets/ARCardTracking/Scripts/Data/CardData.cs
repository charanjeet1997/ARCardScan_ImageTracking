using System;
using System.Collections;
using System.Collections.Generic;
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
    
    [CreateAssetMenu(fileName = "CardDataContainer", menuName = "Data/Containers/CardDataContainer", order = 1)]
    public class CardDataContainer : ScriptableObject
    {
        public List<CardData> cardDataList = new List<CardData>();
    }
}

