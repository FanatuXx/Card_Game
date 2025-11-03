using UnityEngine;

namespace TarotProject
{
    public class Card : ScriptableObject
    {
        public string cardName;
        public CardType cardType;
        public int health;
        public int damageMin;
        public int damageMax;
        public DamageType damageType;
        public string cardDescription;


        public enum CardType
        {

        }

    }
}