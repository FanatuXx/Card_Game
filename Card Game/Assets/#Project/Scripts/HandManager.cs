using UnityEngine;
using TarotProject;
using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using System;

public class HandManager : MonoBehaviour
{
    public DeckManager deckManager;
    public GameObject cardPrefab; //Assign card prefab in inspector
    public Transform handTransform; //Root of the hand position
    public float fanSpread = 7.5f; //How much the cards spread out

    public float cardSpacing = -150f; //Spacing between cards
    public float verticalSpacing = 75f;
    public List<GameObject> cardsInHand = new List<GameObject>(); //Hold a list of the cards objects in our hand

    void Start()
    {

    }

    public void AddCardToHand(Card cardData)
    {
        if (cardsInHand.Count >= 7)
        {
            return;
        }
        else
        {
            //Instantiate the card
            GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
            cardsInHand.Add(newCard);

            //Set the cardData of the instantiated card
            newCard.GetComponent<CardDisplay>().cardData = cardData;

            UpdateHandVisuals();
        }
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
            //cardsInHand[i].transform.localPosition = Vector3.zero;
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);
            //cardsInHand[i].transform.localPosition += new Vector3(0, Mathf.Abs(angle) * 0.1f, 0); // Slight vertical offset for better visibility

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = (2f * i) / (cardCount - 1) - 1f; // Normalize card position between -1 and 1
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition); // Parabolic vertical offset

            //Set card position
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
