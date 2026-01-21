using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace TarotProject
{
    [CreateAssetMenu(fileName = "New Tarot Card", menuName = "Tarot Card")]
    public class TarotCard : ScriptableObject
    {
        public string cardName;
        public Sprite cardSprite;
        public Sprite cardBack;
        public string effectDescription;
        public string cardProverb;
    }
}