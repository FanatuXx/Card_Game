using System.Collections.Generic;
using TarotProject;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    
    private const int CARDS_PER_PLAYER = 13; // Number of cards each player receives at game start
    private List<Card> deck = new List<Card>(); // All available cards in the deck
    private List<HandManager> playerHandManagers = new List<HandManager>(); // References to all player hand managers in the game

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

    // Draws a single card from the deck and gives it to specified player
    public void DrawCard(HandManager handManager)
    {
        // Check if deck is empty
        if (deck.Count == 0)
        {
            Debug.LogWarning("No cards left in deck to draw!");
            return;
        }

        // Check if player hand is full
        if (handManager.cardsInHand.Count >= handManager.maxHandSize)
        {
            Debug.LogWarning("Hand is full, cannot draw more cards!");
            return;
        }

        // Get the top card from the deck
        Card topCard = deck[0];

        // Remove the card from the deck
        deck.RemoveAt(0);

        // Add the card to player's hand
        handManager.AddCardToHand(topCard);
    }
}

   /* public int startingHandSize = 6;

    private int currentIndex = 0;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager handManager;

    void Start()
    {
        //Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to the allCards list
        allCards.AddRange(cards);

        handManager = FindFirstObjectByType<HandManager>();
        maxHandSize = handManager.maxHandSize;
        for (int i = 0; i < startingHandSize; i++)
        {
            Debug.Log($"Drawing Card");
            DrawCard(handManager);
        }
    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
            return;

        if (currentHandSize < maxHandSize)
        {
            Card nextCard = allCards[currentIndex];
            handManager.AddCardToHand(nextCard);
            currentIndex = (currentIndex + 1) % allCards.Count;
        }
    }
   
} */
