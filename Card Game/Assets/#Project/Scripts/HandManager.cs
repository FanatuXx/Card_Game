using System.Collections.Generic;
using UnityEngine;
using TarotProject;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //Assign card prefab in inspector
    public Transform handTransform; //Root of the hand position

    // Visual layout settings for the card fan
    public float fanSpread = 7.5f;
    public float cardSpacing = -150f;
    public float verticalSpacing = 75f;

    public int maxHandSize = 13;

    public List<GameObject> cardsInHand = new List<GameObject>(); //Hold a list of the card objects in players' hand

    /*
    private List<Card> allCards;

    void Start()
    {
        LoadAllCards();
        DrawStartingHand();
    }

    private void LoadAllCards()
    {
        allCards = new List<Card>();

        string[] cardTypes = { "Clubs", "Diamonds", "Hearts", "Spades" };

        foreach (string cardType in cardTypes)
        {
            Card[] cards = Resources.LoadAll<Card>($"Cards/{cardType}");
            allCards.AddRange(cards);
        }
    }

    private void DrawStartingHand()
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            AddRandomCardToHand();
        }
    }

    public void AddRandomCardToHand()
    {
        if (allCards.Count == 0)
        {
            Debug.LogWarning("No cards available to draw!");
            return;
        }

        Card randomCard = allCards[UnityEngine.Random.Range(0, allCards.Count)];
        AddCardToHand(randomCard);
    }
    */

    // Adds a card to this player's hand and updates visual layout
    public void AddCardToHand(Card cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform); // Create new card GameObject from prefab
        cardsInHand.Add(newCard); // Add the new card to our list

        // Get the CardDisplay component to set card data
        CardDisplay cardDisplay = newCard.GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            cardDisplay.SetCardData(cardData); // Apply the card data to display
        }

        UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        // Get total number of cards in hand
        int cardCount = cardsInHand.Count;

        // Special case: single card centered with no rotation
        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        // Position each card in a fan pattern
        for (int i = 0; i < cardCount; i++)
        {
            // Calculate rotation angle for fan spread
            float rotationAngle = fanSpread * (i - (cardCount - 1) / 2f);
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            // Calculate horizontal spacing
            float horizontalOffset = cardSpacing * (i - (cardCount - 1) / 2f);

            // Calculate vertical offset for arc effect
            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);

            // Apply final position
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}

/* void Start()
 {

 }

 public void AddCardToHand(Card cardData)
 {

     //Instantiate the card
     GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
     cardsInHand.Add(newCard);

     CardDisplay cardDisplay = newCard.GetComponent<CardDisplay>();
     if (cardDisplay != null)
     {
         cardDisplay.SetCardData(cardData);
     }

     UpdateHandVisuals();
 }

 void Update()
 {
     //UpdateHandVisuals();
 }

 private void UpdateHandVisuals()
 {
     int cardCount = cardsInHand.Count;

     if (cardCount == 1)
     {
         cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
         cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
         return;
     }

     for (int i = 0; i < cardCount; i++)
     {
         float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
         cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

         float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

         float normalizedPosition = (2f * i / (cardCount - 1) - 1f); //Normalize card position between -1, 1
         float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);

         //Set card position
         cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
     }
 } 
}*/
