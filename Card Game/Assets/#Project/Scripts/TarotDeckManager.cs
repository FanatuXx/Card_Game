using System.Collections.Generic;
using TarotProject;
using Unity.VisualScripting;
using UnityEngine;

public class TarotDeckManager : MonoBehaviour
{

    private List<TarotCard> tarotDeck = new List<TarotCard>(); // All available cards in the deck

    public GameObject tarotCardObject;
    public GameObject tarotDeckPileObject;
    public GameObject tarotCardPrefab;
    public GameObject tarotDeckPrefab;
    public Sprite tarotDeckBackSprite;
    public Transform tarotCardPosition;

    void Start()
    {
        // Find the TarotCardPosition if not assigned
        if (tarotCardPosition == null)
        {
            GameObject tarotCardPositionObject = GameObject.Find("TarotCardPosition");
            if (tarotCardPositionObject != null)
            {
                tarotCardPosition = tarotCardPositionObject.transform;
            }
            else
            {
                Debug.LogError("TrumpCardPosition not found in scene!");
                return;
            }
        }

        // Initialize the deck with all available cards
        InitializeTarotDeck();

        // Shuffle the deck to randomize card order
        ShuffleTarotDeck();

        InstantiateTarotDeckPileVisual();
    }

    // Loads all card assets from Resources folder into the deck
    private void InitializeTarotDeck()
    {
        // Load all card assets from the Resources/Cards folder
        TarotCard[] tarotCards = Resources.LoadAll<TarotCard>("TarotCards");

        // Add the loaded cards to the deck list
        tarotDeck.AddRange(tarotCards);

        Debug.Log($"Tarot Deck initialized with {tarotDeck.Count} cards");
    }

    // Randomizes the order of cards in the deck using Fisher-Yates shuffle
    private void ShuffleTarotDeck()
    {
        // Loop through deck from last to first
        for (int i = tarotDeck.Count - 1; i > 0; i--)
        {
            // Pick random index from 0 to i
            int randomIndex = Random.Range(0, i + 1);

            // Swap current card with random card
            TarotCard temp = tarotDeck[i];
            tarotDeck[i] = tarotDeck[randomIndex];
            tarotDeck[randomIndex] = temp;
        }

        Debug.Log("Deck shuffled");
    }

    public void SetTarotCard()
    {
        // Check if there are cards left in the deck
        if (tarotDeck.Count == 0)
        {
            Debug.LogWarning("No cards left in deck to set Tarot card!");
            return;
        }
        // The next card in the deck is the Tarot card
        TarotCard tarotCard = tarotDeck[0];
        tarotDeck.RemoveAt(0);

        InstantiateTarotCardVisual(tarotCard);
    }

    private void InstantiateTarotCardVisual(TarotCard tarotCard)
    {
        if (tarotCardPrefab == null || tarotCardPosition == null)
        {
            Debug.LogWarning("Tarot card prefab or Tarot card position not assigned!");
            return;
        }

        tarotCardObject = Instantiate(tarotCardPrefab, tarotCardPosition.position, Quaternion.identity, tarotCardPosition);
        tarotCardObject.name = "TarotCard";

        TarotCardDisplay tarotCardDisplay = tarotCardObject.GetComponent<TarotCardDisplay>();
        if (tarotCardDisplay != null)
        {
            tarotCardDisplay.SetTarotCardData(tarotCard, true);
        }
    }

    private void InstantiateTarotDeckPileVisual()
    {
        if (tarotDeckPrefab == null || tarotCardPosition == null || tarotDeckBackSprite == null)
        {
            Debug.LogWarning("Required references not assigned for Tarot deck pile!");
            return;
        }

        Vector3 tarotDeckPosition = tarotCardPosition.position + new Vector3(+200f, 0f, 0f);

        tarotDeckPileObject = Instantiate(tarotDeckPrefab, tarotDeckPosition, Quaternion.identity, tarotCardPosition.parent);
        tarotDeckPileObject.name = "TarotDeck";

        TarotCardDisplay tarotDeckDisplay = tarotDeckPileObject.GetComponent<TarotCardDisplay>();
        if (tarotDeckDisplay != null && tarotDeckDisplay.cardImage != null)
        {
            tarotDeckDisplay.cardImage.sprite = tarotDeckBackSprite;
        }
    }
}
