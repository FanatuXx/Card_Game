using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TarotProject;
using UnityEngine.EventSystems;

public class TarotCardDisplay : MonoBehaviour, IPointerEnterHandler
{

    public TarotCard cardData;
    public Image cardImage;
    public Image cardBack;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI cardDescription;


    public void SetTarotCardData(TarotCard data, bool showCardFace)
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardName = cardName.GetComponent<TextMeshProUGUI>();
        cardDescription = cardDescription.GetComponent<TextMeshProUGUI>();

        if (cardData != null)
        {
            cardName.enabled = true;
            cardDescription.enabled = true;

            // Update the UI elements with card details
            if (cardName != null)
            {
                cardName = cardData.cardName;
            }
            if (cardDescription != null)
            {
                cardDescription = cardData.effectDescription;
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

    public void HideCard()
    {
        if (cardData != null && cardImage != null && cardData.cardBack != null)
        {
            cardImage.sprite = cardData.cardBack;
        }
    }
}