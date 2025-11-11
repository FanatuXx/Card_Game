using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TarotProject;

public class CardDisplay : MonoBehaviour
{

    public Card cardData;

    public Image cardImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public TMP_Text valueText;
    public Image[] typeImages;

    public Image damageImage;

    private Color[] cardColors = 
    {
        new Color(0.70f, 0.20f, 0.13f), //Fire
        new Color(0.37f, 0.57f, 0.57f), //Air
        new Color(0f, 0.38f, 0.54f), //Water
        new Color(0.28f, 0.18f, 0.12f), //Earth
        new Color(0.37f, 0f, 0.68f) //Soul
    };

    private Color[] typeColors =
{
        new Color(0.55f, 0.20f, 0.13f), //Fire
        new Color(0.3f, 0.57f, 0.57f), //Air
        new Color(0f, 0.38f, 0.54f), //Water
        new Color(0.56f, 0.38f, 0.27f), //Earth
        new Color(0.37f, 0f, 0.68f) //Soul
    };

    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        //Update the main card image color based on the first card type
        cardImage.color = cardColors[(int)cardData.cardType[0]];

        damageImage.color = typeColors[(int)cardData.damageType[0]];

        nameText.text = cardData.cardName;
        healthText.text = cardData.health.ToString();
        damageText.text = $"{cardData.damageMin} - {cardData.damageMax}";
        descriptionText.text = cardData.cardDescription;
        valueText.text = cardData.cardValue.ToString();

        //Update type images
        for (int i = 0; i < typeImages.Length; i++)
        {
            if (i < cardData.cardType.Count)
            { 
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typeColors[(int)cardData.cardType[i]];
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }
    }
}
