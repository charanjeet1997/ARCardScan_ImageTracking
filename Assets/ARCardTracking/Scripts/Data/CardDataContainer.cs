using System.Collections.Generic;
using UnityEngine;

namespace Game.ARCardTracking
{
    [CreateAssetMenu(fileName = "CardDataContainer", menuName = "Data/Containers/CardDataContainer")]
    public class CardDataContainer : ScriptableObject
    {
        public List<CardData> cardDataList;
    }
}