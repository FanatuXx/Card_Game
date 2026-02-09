using System.Collections.Generic;
using TarotProject;
using Unity.VisualScripting;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //Assign card prefab in inspector
    public Transform handTransform; //Root of the hand position

    // Visual layout settings for the card fan
    public float fanSpread = 7.5f;
    public float cardSpacing = -150f;
    public float verticalSpacing = 75f;

    public int maxHandSize = 13;

    public bool isHumanPlayer = false; // Determines if this is a human player (true) or AI player (false)

    public List<GameObject> cardsInHand = new List<GameObject>(); //Hold a list of the card objects in players' hand


    // Adds a card to this player's hand and updates visual layout
    public void AddCardToHand(Card cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform); // Create new card GameObject from prefab
        cardsInHand.Add(newCard); // Add the new card to our list

        // Get the CardDisplay component to set card data
        CardDisplay cardDisplay = newCard.GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            cardDisplay.SetCardData(cardData, isHumanPlayer); // Apply the card data to display
        }
        
        CardMovement cardMovement = newCard.GetComponent<CardMovement>();
        if (cardMovement != null)
        {
            cardMovement.ownerHandManager = this;
        }

        //UpdateHandVisuals();
    }

    public void UpdateHandVisuals()
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