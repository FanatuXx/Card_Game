using UnityEngine;
using System.Collections.Generic;

namespace TarotProject
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public int health;
        public int damageMin;
        public int damageMax;
        public List<DamageType> damageType;
        public string cardDescription;
        public int cardValue;
        public Sprite cardSprite;


        public enum CardType
        {
            Wands,
            Swords,
            Cups,
            Pentacles,
            Trumps
        }

        public enum DamageType
        {
            Fire,
            Air,
            Water,
            Earth,
            Soul
        }
    }
}