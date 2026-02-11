using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TarotProject;
using UnityEngine.EventSystems;

public class TarotCardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TarotCard cardData;
    public Image cardImage;
    public Image cardBack;
    public Image backgroundUI;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardProverbText;
    public TextMeshProUGUI cardDescriptionText;


    void Start()
    {
        HideCardDetails();
    }
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

    void HideCardDetails()
    {
        if (backgroundUI != null)
        {
            backgroundUI.gameObject.SetActive(false);
        }
        if (cardNameText != null)
        {
            cardNameText.gameObject.SetActive(false);
        }
        if (cardDescriptionText != null)
        {
            cardDescriptionText.gameObject.SetActive(false);
        }
        if (cardProverbText != null)
        {
            cardProverbText.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardData != null)
        {

            // Update the UI elements with card details
            if (backgroundUI != null)
            {
                backgroundUI.gameObject.SetActive(true);
            }
            if (cardNameText != null)
            {
                cardNameText.gameObject.SetActive(true);
                cardNameText.text = cardData.cardName;
            }
            if (cardDescriptionText != null)
            {
                cardDescriptionText.gameObject.SetActive(true);
                cardDescriptionText.text = cardData.effectDescription;
            }
            if (cardProverbText != null)
            {
                cardProverbText.gameObject.SetActive(true);
                cardProverbText.text = cardData.cardProverb;
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        HideCardDetails();
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