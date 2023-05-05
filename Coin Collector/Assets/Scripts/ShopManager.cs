using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    bool _maxLevel;
    bool _isFirstSprite = true;

    int[] _levelPrices;

    [SerializeField]
    GameObject _alertTextGameObject;

    //[SerializeField]
    GameObject _alertHolder;

    [SerializeField]
    GameObject _unlockPanel;

    [SerializeField]
    Transform _mainCanvasTransform;

    [SerializeField]
    Sprite[] itemImage;

    [SerializeField]
    CoinManager _coinManager;

    [SerializeField]
    Button _itemButton;

    [SerializeField]
    TextMeshProUGUI _coinsOwnedText;

    TextMeshProUGUI _itemPriceText;
    TextMeshProUGUI _alertText;

    int _currentCoins;

    public Action<int> OnBuyItem;

    private void OnEnable()
    {
        _coinManager.CoinsOwnedChanged += OnCoinsOwnedChanger;
    }

    private void OnDisable()
    {
        _coinManager.CoinsOwnedChanged -= OnCoinsOwnedChanger;
    }

    private void Start()
    {
        _currentCoins = _coinManager.CoinsOwened();
        _itemPriceText = GetComponentInChildren<TextMeshProUGUI>();

        _itemLevel = PlayerPrefs.GetInt(_itemName + " Item Level");
        if (_itemLevel == 6)
        {
            _maxLevel = true;
            _isFirstSprite = false;
        }
    }

    private void Update()
    {
        UpdateItemSprite();
        UpdatePriceText();
    }

    private void UpdateItemSprite()
    {
        if (_isFirstSprite && _itemLevel != 6)
            _itemButton.image.sprite = itemImage[_itemLevel]; //Sprite stays the same on first click (at 0 cost)
        
        else
        {
            int spriteIndex = Mathf.Min(_itemLevel - 1, itemImage.Length - 1); //Clamp to last sprite index
            _itemButton.image.sprite = itemImage[spriteIndex];
        }
    }

    private void UpdatePriceText()
    {
        if (!_maxLevel)
            _itemPriceText.text = _itemPrice[_itemLevel].ToString();
        
        else
            _itemPriceText.text = "MAX";
    }

    public void BuyItem()
    {
        Debug.Log(_currentCoins.ToString());

        if(_maxLevel)
        {
            ShowAlertText("MAX LEVEL!");
        }

        else if (_currentCoins >= _itemPrice[_itemLevel] && !_maxLevel)
        {
            HandleTransactions();

            _itemLevel++;
            PlayerPrefs.SetInt(_itemName + " Item Level", _itemLevel);
            if (_itemLevel > 5)
                MaxLevelReached();

            _coinManager.SetCoinsOwned(_currentCoins);
            _coinsOwnedText.text = _currentCoins.ToString();
        }

        else if (_currentCoins < _itemPrice[_itemLevel])
        {
            ShowAlertText("NOT ENOUGH COINS!");
        }

        _isFirstSprite = false;
    }

    private void ShowAlertText(string message)
    {
        GameObject alertObject = Instantiate(_alertTextGameObject, _mainCanvasTransform);
        alertObject.SetActive(true);
        _alertText = alertObject.GetComponent<TextMeshProUGUI>();
        _alertText.text = message;
    }

    private void HandleTransactions()
    {
        _currentCoins -= _itemPrice[_itemLevel]; //Deduct price based on item level from coins owned
        OnBuyItem?.Invoke(_itemPrice[_itemLevel]); //Sets param to item price
        _coinManager.OnBuyItem(_itemPrice[_itemLevel]); //Updates player coins in CoinManager
    }

    private void MaxLevelReached()
    {
            _maxLevel = true;
            _itemLevel = 6;
    }

    void OnCoinsOwnedChanger(int coinsOwned) => _currentCoins = coinsOwned; //Sets current coins to coins owned

    public void OpenUpgradePanel()
    {
        if (_itemLevel == 0)
        {
            _unlockPanel.SetActive(true);
        }
    }
}
