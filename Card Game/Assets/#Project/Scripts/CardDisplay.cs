using UnityEngine;
using UnityEngine.UI;
using TarotProject;

public class CardDisplay : MonoBehaviour
{

    public Card cardData;

    public Image cardImage;
    public Image cardBack;

    public void SetCardData(Card data, bool showCardFace)
    {
        cardData = data;

        // Check if we have valid data and image component
        if (cardData != null && cardImage != null)
        {
            // If this is a human player, show the card face
            if (showCardFace && cardData.cardSprite != null)
            {
                cardImage.sprite = cardData.cardSprite;
            }
            // If this is an AI player, show the card back
            else if (!showCardFace && cardData.cardBack != null)
            {
                cardImage.sprite = cardData.cardBack;
            }
        }
    }

    public void RevealCard()
    {
        if (cardData != null && cardImage != null && cardData.cardSprite != null)
        {
            cardImage.sprite = cardData.cardSprite;
        }
    }
}