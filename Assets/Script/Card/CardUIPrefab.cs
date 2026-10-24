using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIPrefab : MonoBehaviour
{
    [SerializeField] private CardData cardData;

    [SerializeField] private Image cardIconImage;
    [SerializeField] private TMP_Text cardValueText;

    private void OnEnable()
    {
        GameplayManager.instance.OnResetAll += ResetCardUI;
        GameplayManager.instance.OnRoundFinish += ResetCardUI;
    }

    private void OnDisable()
    {
        GameplayManager.instance.OnResetAll -= ResetCardUI;
        GameplayManager.instance.OnRoundFinish -= ResetCardUI;
    }

    public void CardUISetUp(CardData data)
    {
        cardData = data;
        cardIconImage.sprite = CardManager.instance.GetCardSprite(cardData.cardType);
        cardValueText.text = cardData.cardValue.ToString();
    }
    public void ResetCardUI()
    {
        cardData = null;
        cardIconImage.sprite = null;
        cardValueText.text  = string.Empty;
    }
}
