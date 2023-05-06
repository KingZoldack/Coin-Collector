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
    bool _isMaxLevel;
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

    //[SerializeField]
    //CoinManager _coinManager;

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
       CoinManagerSingleton.Instance.CoinsOwnedChanged += OnCoinsOwnedChanger;
    }

    private void OnDisable()
    {
        CoinManagerSingleton.Instance.CoinsOwnedChanged -= OnCoinsOwnedChanger;
    }

    private void Start()
    {
        _itemPriceText = GetComponentInChildren<TextMeshProUGUI>();
        _coinsOwnedText.text = CoinManagerSingleton.Instance.CoinsOwned.ToString();

        _itemLevel = PlayerPrefs.GetInt(_itemName + " Item Level");
        if (_itemLevel == 6)
        {
            _isMaxLevel = true;
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
        if (!_isMaxLevel)
            _itemPriceText.text = _itemPrice[_itemLevel].ToString();

        else
            _itemPriceText.text = "MAX";
    }

    public void BuyItem()
    {
        int clampedLevel = Math.Clamp(_itemLevel, 0, _itemPrice.Length - 1);
        int cost = _itemPrice[clampedLevel];

        if (_isMaxLevel)
        {
            ShowAlertText("MAX LEVEL!");
        }
        else if (CoinManagerSingleton.Instance.CoinsOwned >= cost)
        {
            CoinManagerSingleton.Instance.CoinsOwned -= cost;
            _itemLevel++;
            PlayerPrefs.SetInt(_itemName + " Item Level", _itemLevel);
            if (_itemLevel == _itemPrice.Length)
            {
                _isMaxLevel = true;
            }

            UpdateItemSprite();
            UpdatePriceText();
            _coinsOwnedText.text = CoinManagerSingleton.Instance.CoinsOwned.ToString();
        }
        else
            ShowAlertText("NOT ENOUGH COINS!");

        _isFirstSprite = false;
    }

    private void ShowAlertText(string message)
    {
        GameObject alertObject = Instantiate(_alertTextGameObject, _mainCanvasTransform);
        alertObject.SetActive(true);
        _alertText = alertObject.GetComponent<TextMeshProUGUI>();
        _alertText.text = message;
    }

    void OnCoinsOwnedChanger(int coinsOwned)
    {
        UpdateItemSprite();
        UpdatePriceText();
        //_currentCoins = coinsOwned; //Sets current coins to coins owned
    }

    public void OpenUpgradePanel()
    {
        if (_itemLevel == 0)
        {
            _unlockPanel.SetActive(true);
        }
    }
}
