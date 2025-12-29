using System.Collections.Generic;
using TarotProject;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private const int CARDS_PER_PLAYER = 13; // Number of cards each player receives at game start
    private List<Card> deck = new List<Card>(); // All available cards in the deck
    private List<HandManager> playerHandManagers = new List<HandManager>(); // References to all player hand managers in the game

    public Card.CardType currentTrumpColor;
    public GameObject trumpCardObject;
    public GameObject deckPileObject;
    public GameObject cardPrefab;
    public Sprite deckBackSprite;
    public Transform trumpCardPosition;
    void Start()
    {
        // Initialize the deck with all available cards
        InitializeDeck();

        // Shuffle the deck to randomize card order
        ShuffleDeck();

        // Find all players in the scene
        FindAllPlayers();

        // Deal cards to all players
        DealCardsToAllPlayers();

        // Set the trump card (next card in deck) for the round
        SetTrumpCard();
    }

    // Loads all card assets from Resources folder into the deck
    private void InitializeDeck()
    {
        // Load all card assets from the Resources/Cards folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        // Add the loaded cards to the deck list
        deck.AddRange(cards);

        Debug.Log($"Deck initialized with {deck.Count} cards");
    }

    // Randomizes the order of cards in the deck using Fisher-Yates shuffle
    private void ShuffleDeck()
    {
        // Loop through deck from last to first
        for (int i = deck.Count - 1; i > 0; i--)
        {
            // Pick random index from 0 to i
            int randomIndex = Random.Range(0, i + 1);

            // Swap current card with random card
            Card temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        Debug.Log("Deck shuffled");
    }

    // Locates all HandManager components attached to player GameObjects
    private void FindAllPlayers()
    {
        // Find PlayerHuman and get its HandManager component
        GameObject playerHuman = GameObject.Find("PlayerHuman");
        if (playerHuman != null)
        {
            HandManager humanHand = playerHuman.GetComponent<HandManager>();
            if (humanHand != null)
            {
                playerHandManagers.Add(humanHand);
            }
        }

        // Find PlayerAI1 and get its HandManager component
        GameObject playerAI1 = GameObject.Find("PlayerAI1");
        if (playerAI1 != null)
        {
            HandManager ai1Hand = playerAI1.GetComponent<HandManager>();
            if (ai1Hand != null)
            {
                playerHandManagers.Add(ai1Hand);
            }
        }

        // Find PlayerAI2 and get its HandManager component
        GameObject playerAI2 = GameObject.Find("PlayerAI2");
        if (playerAI2 != null)
        {
            HandManager ai2Hand = playerAI2.GetComponent<HandManager>();
            if (ai2Hand != null)
            {
                playerHandManagers.Add(ai2Hand);
            }
        }

        Debug.Log($"Found {playerHandManagers.Count} players");
    }

    // Distributes cards from the deck to all players
    private void DealCardsToAllPlayers()
    {
        // Track current position in deck
        int deckIndex = 0;

        // Give each player their cards one at a time (round-robin distribution)
        for (int cardNumber = 0; cardNumber < CARDS_PER_PLAYER; cardNumber++)
        {
            // Loop through each player
            for (int playerIndex = 0; playerIndex < playerHandManagers.Count; playerIndex++)
            {
                // Check if we have cards left in the deck
                if (deckIndex >= deck.Count)
                {
                    Debug.LogWarning("Ran out of cards in deck!");
                    return;
                }

                // Get the next card from the deck
                Card cardToDeal = deck[deckIndex];

                // Give the card to the current player
                playerHandManagers[playerIndex].AddCardToHand(cardToDeal);

                // Move to next card in deck
                deckIndex++;
            }
        }

        Debug.Log($"Dealt {CARDS_PER_PLAYER} cards to {playerHandManagers.Count} players");
    }

    private void SetTrumpCard()
    {
        // Check if there are cards left in the deck
        if (deck.Count == 0)
        {
            Debug.LogWarning("No cards left in deck to set trump card!");
            return;
        }
        // The next card in the deck is the trump card
        Card trumpCard = deck[0];
        deck.RemoveAt(0);

        currentTrumpColor = trumpCard.cardType[0];

        InstantiateTrumpCardVisual(trumpCard);
        InstantiateDeckPileVisual();

        // Log the trump card information
        Debug.Log($"Trump color set to: {currentTrumpColor}");
    }

    private void InstantiateTrumpCardVisual(Card trumpCard)
    {
        if (cardPrefab == null || trumpCardPosition == null)
        {
            Debug.LogWarning("Card prefab or trump card position not assigned!");
            return;
        }

        trumpCardObject = Instantiate(cardPrefab, trumpCardPosition.position, Quaternion.identity, trumpCardPosition);

        CardDisplay cardDisplay = trumpCardObject.GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            cardDisplay.SetCardData(trumpCard, true);
        }

        CardMovement cardMovement = trumpCardObject.GetComponent<CardMovement>();
        if (cardMovement != null)
        {
            Destroy(cardMovement);
        }
    }

    private void InstantiateDeckPileVisual()
    {
        if (cardPrefab == null || trumpCardPosition == null || deckBackSprite == null)
        {
            Debug.LogWarning("Required references not assigned for deck pile!");
            return;
        }

        Vector3 deckPosition = trumpCardPosition.position + new Vector3(-150f, 0f, 0f);

        deckPileObject = Instantiate(cardPrefab, deckPosition, Quaternion.identity, trumpCardPosition.parent);

        CardDisplay deckDisplay = deckPileObject.GetComponent<CardDisplay>();
        if (deckDisplay != null && deckDisplay.cardImage != null)
        {
            deckDisplay.cardImage.sprite = deckBackSprite;
        }

        CardMovement cardMovement = deckPileObject.GetComponent<CardMovement>();
        if (cardMovement != null)
        {
            Destroy(cardMovement);
        }
    }
}
