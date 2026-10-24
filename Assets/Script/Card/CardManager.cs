using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum CardType
{
    Keriting,
    Wajik,
    Hati,
    Sekop
}

[System.Serializable]
public class TypeValue
{
    public CardType cardType;
}

[System.Serializable]
public class CardView
{
    public CardType cardType;
    public Sprite typeSprite;
}

[System.Serializable]
public class CardData
{
    public CardType cardType;
    public int cardValue;

    public CardData(CardType type, int x)
    {
        cardType = type;
        cardValue = x;
    }
}

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [Header("Type Datas")]
    [SerializeField] private List<TypeValue> typeOrder;

    [Header("Sprite Datas")]
    [SerializeField] private List<CardView> cardSpriteDatas;

    [Header("Card Datas")]
    private List<CardData> cardDatas = new();
    [SerializeField] private int maxCardValue_EachType = 10;
    [SerializeField] private List<CardData> deckShuffle = new();

    private void Start()
    {
        InitCardDatas();
    }

    // Make the Datas for all card
    private void InitCardDatas()
    {
        cardDatas.Clear();
        foreach (CardType type in Enum.GetValues(typeof(CardType)))
        {
            for (int i = 1; i <= maxCardValue_EachType; i++)
            {
                cardDatas.Add(new CardData(type, i));
            }
        }
        DeckShuffle();
    }


    // Shuffle the card when start the game
    private void DeckShuffle()
    {
        int index = cardDatas.Count;

        for(int i = 0; i < index; i++)
        {
            int shuffleIndex = UnityEngine.Random.Range(0, index);
            deckShuffle.Add(cardDatas[shuffleIndex]);
            cardDatas.RemoveAt(shuffleIndex);
            index--;
        }
    }
}
