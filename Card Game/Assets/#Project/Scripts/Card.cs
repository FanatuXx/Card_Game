using UnityEngine;
using System.Collections.Generic;

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


        public enum CardType
        {
            Clubs,
            Spades,
            Hearts,
            Diamonds,
        }
    }
}