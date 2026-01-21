using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace TarotProject
{
    [CreateAssetMenu(fileName = "New Tarot Card", menuName = "Tarot Card")]
    public class TarotCard : ScriptableObject
    {
        public TextMeshProUGUI cardName;
        public Sprite cardSprite;
        public Sprite cardBack;
        public TextMeshProUGUI effectDescription;
    }
}