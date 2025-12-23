using UnityEngine;
using UnityEngine.UI;
using TarotProject;

public class CardDisplay : MonoBehaviour
{

    public Card cardData;

    public Image cardImage;

    public void SetCardData(Card data)
    {
        cardData = data;

        if (cardData != null && cardImage != null && cardData.cardSprite != null)
        {
            cardImage.sprite = cardData.cardSprite;
        }
    }
}