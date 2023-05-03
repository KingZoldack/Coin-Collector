using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    string _itemName;

    [SerializeField]
    int[] _itemPrice;

    int _currentPrice = 1;

    int _itemLevel = 0;

    int[] _levelPrices;

    [SerializeField]
    Sprite[] itemImage;

    [SerializeField]
    CoinManager _coinManager;

    [SerializeField]
    Button _itemButton;

    [SerializeField]
    TextMeshProUGUI _coinsOwnedText;

    int _currentCoins;

    public Action<int> OnBuyItem;

    private void OnDisable()
    {
        _coinManager.CoinsOwnedChanged -= OnCoinsOwnedChanger;
    }

    private void Start()
    {
        _currentCoins = _coinManager.CoinsOwened();
        _coinManager.CoinsOwnedChanged += OnCoinsOwnedChanger;

        _itemButton.image.sprite = itemImage[_itemLevel];
    }

    //Do you have enough coins?
    public void BuyItem()
    {
        if (_currentCoins >= _itemPrice[_currentPrice])
        {
            _currentCoins -= _itemPrice[_currentPrice];
            OnBuyItem?.Invoke(_itemPrice[_currentPrice]);
            _currentPrice = _itemPrice[_currentPrice + 1];
            _itemLevel++;
            Debug.Log(_currentCoins);
            _coinManager.SetCoinsOwned(_currentCoins);
            _coinsOwnedText.text = _currentCoins.ToString();
        }

    }

    void OnCoinsOwnedChanger(int coinsOwned) => _currentCoins = coinsOwned;

    void OnCoinsOwnedTextChanger() => _coinsOwnedText.text = _currentCoins.ToString();

    //Upgrade if bought
}
