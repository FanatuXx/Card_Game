using System.Collections.Generic;
using TarotProject;
using UnityEngine;

public class BacusEffect : MonoBehaviour
{

    private List<HandManager> GetAllPlayerHandManagers()
    {
        List<HandManager> handManagers = new List<HandManager>();

        GameObject playerHuman = GameObject.Find("PlayerHuman");
        if (playerHuman != null)
        {
            HandManager humanHand = playerHuman.GetComponent<HandManager>();
            if (humanHand != null)
            {
                handManagers.Add(humanHand);
            }
        }

        GameObject playerAI1 = GameObject.Find("PlayerAI1");
        if (playerAI1 != null)
        {
            HandManager ai1Hand = playerAI1.GetComponent<HandManager>();
            if (ai1Hand != null)
            {
                handManagers.Add(ai1Hand);
            }
        }

        GameObject playerAI2 = GameObject.Find("PlayerAI2");
        if (playerAI2 != null)
        {
            HandManager ai2Hand = playerAI2.GetComponent<HandManager>();
            if (ai2Hand != null)
            {
                handManagers.Add(ai2Hand);
            }
        }

        return handManagers;
    }

    private List<Card> CollectAllCardsFromPlayers(List<HandManager> handManagers)
    {
        List<Card> allCards = new List<Card>();

        foreach (HandManager handManager in handManagers)
        {
            foreach (GameObject cardObject in handManager.cardsInHand)
            {
                CardDisplay cardDisplay = cardObject.GetComponent<CardDisplay>();
                if (cardDisplay != null && cardDisplay.cardData != null)
                {
                    allCards.Add(cardDisplay.cardData);
                }
            }
        }

        return allCards;
    }

    private void ClearAllHands(List<HandManager> handManagers)
    {
        foreach (HandManager handManager in handManagers)
        {
            foreach (GameObject cardObject in handManager.cardsInHand)
            {
                Destroy(cardObject);
            }

            handManager.cardsInHand.Clear();
        }
    }

    private void ShuffleCards(List<Card> cards)
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Card temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    private void RedistributeCards(List<Card> shuffledCards, List<HandManager> handManagers)
    {
        int cardIndex = 0;
        int playerCount = handManagers.Count;

        while (cardIndex < shuffledCards.Count)
        {
            for (int playerIndex = 0; playerIndex < playerCount && cardIndex < shuffledCards.Count; playerIndex++)
            {
                handManagers[playerIndex].AddCardToHand(shuffledCards[cardIndex]);
                cardIndex++;
            }
        }
    }

    public void ExecuteBacusEffect()
    {
        List<HandManager> handManagers = GetAllPlayerHandManagers();

        if (handManagers.Count == 0)
        {
            Debug.LogWarning("No players found!");
            return;
        }

        List<Card> allCards = CollectAllCardsFromPlayers(handManagers);

        if (allCards.Count == 0)
        {
            Debug.LogWarning("No cards to shuffle!");
            return;
        }

        ClearAllHands(handManagers);

        ShuffleCards(allCards);

        RedistributeCards(allCards, handManagers);

        Debug.Log($"Bacus effect complete! Shuffled and redistributed {allCards.Count} cards to {handManagers.Count} players");
    }
}