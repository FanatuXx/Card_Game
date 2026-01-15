using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace TarotProject
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public int cardValue;
        public Sprite cardSprite;
        public Sprite cardBack;
        public int initialValue;


        public enum CardType
        {
            Clubs,
            Spades,
            Hearts,
            Diamonds,
        }

    }
}