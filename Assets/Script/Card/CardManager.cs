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
public class TypeHierarchy
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
    [SerializeField] private List<TypeHierarchy> typeHierarchy = new();

    [Header("Sprite Datas")]
    [SerializeField] private List<CardView> cardSpriteDatas = new();

    [Header("Card Datas")]
    private List<CardData> cardDatas = new();
    [SerializeField] private int maxCardValue_EachType = 10;
    [SerializeField] private List<CardData> deckShuffle = new();

    private void OnEnable()
    {
        GameplayManager.instance.OnGameStart += InitCardDatas;
    }
    private void OnDisable()
    {
        GameplayManager.instance.OnGameStart -= InitCardDatas;
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
                //Debug.Log("KartuData");
            }
        }
        DeckShuffle();
    }


    // Shuffle the card when start the game
    private void DeckShuffle()
    {
        deckShuffle.Clear();

        int cardTotal = cardDatas.Count;
        int index = cardTotal;
        //Debug.Log($"Total Kartu = {cardTotal}");


        for (int i = 0; i < cardTotal; i++)
        {
            //Debug.Log("KartuShuffle");

            int shuffleIndex = UnityEngine.Random.Range(0, index);
            deckShuffle.Add(cardDatas[shuffleIndex]);
            cardDatas.RemoveAt(shuffleIndex);
            index--;
        }
    }

    public CardData GetCardData()
    {
        if(deckShuffle.Count <= 0)
        {
            GameManager.instance.SwitchState(GameState.GameOverState);
            return null;
        }

        CardData data = deckShuffle[0];
        RemoveTopCardFromDeck();
        return data;
    }

    private void RemoveTopCardFromDeck()
    {
        //Debug.Log("HapusKartu");
        deckShuffle.RemoveAt(0);
    } 


    public Sprite GetCardSprite(CardType type)
    {
        CardView view = cardSpriteDatas.Find(x => x.cardType == type);
        return view.typeSprite;
    }

    public bool CheckPlayerTypeHierarchy(CardType playerType, CardType tableType)
    {
        int playerHierarchy = typeHierarchy.FindIndex(x => x.cardType == playerType);
        int enemyHierarchy = typeHierarchy.FindIndex(x => x.cardType == tableType);

        return playerHierarchy < enemyHierarchy; // True berarti lebih besar playernya, False berarti lebih kecil playernya
    }
}
