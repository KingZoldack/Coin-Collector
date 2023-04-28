using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CoinManager : MonoBehaviour
{
    [SerializeField]
    GameObject _coinPrefab;

    [SerializeField]
    GameObject _coinHolder;

    [SerializeField]
    Canvas _mainCanvas;

    [SerializeField]
    Coin _coinScript;

    [SerializeField]
    TextMeshProUGUI _clicksLeftText;
    [SerializeField]
    TextMeshProUGUI _coinsOwnedText;

    bool _bonusGiven;

    int _clicksToBonus = 10;
    int _coinsEarned = 0;
    int _oneCoinValue = 1;
    int _tenCoinValue = 10;
    int _hundredCoinValue = 100;

    int _minBonusValue = 1, _maxBonusValue = 10;
    int _minRangeForBonus = 1, _maxRangeForBonus = 11;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCoin()
    {
        GameObject spawnedCoin = Instantiate(_coinPrefab, _mainCanvas.transform);
        spawnedCoin.transform.SetParent(_coinHolder.transform);
        HandleBonus(_oneCoinValue, _tenCoinValue, _hundredCoinValue);
    }

    void HandleBonus(int oneCoin, int tenCoins, int oneHundredCoins)
    {
        _clicksToBonus--; //Tracks bonus countdown

        if (_clicksToBonus <= 0)
        {
            int randomRange = Random.Range(0, 10);

            if (randomRange == 0) //1 in 10 chance to get 100 bonus coins
                AddCoins(oneHundredCoins);
            else //9 in 10 chance to get 10 bonus coins
                AddCoins(tenCoins);

            _clicksToBonus = _maxBonusValue; //Resets Bonus Value
        }
        else
        {
            AddCoins(oneCoin);
        }

        _clicksLeftText.text = _clicksToBonus.ToString();
    }

    void AddCoins(int coinsToAdd)
    {
        if (coinsToAdd == _oneCoinValue)
        {
            _coinsEarned++;
            _coinScript.HandleCoinText(_oneCoinValue);
        }
        else if (coinsToAdd == _tenCoinValue)
        {
            _coinsEarned += _tenCoinValue;
            _coinScript.HandleCoinText(_tenCoinValue);
        }
        else if (coinsToAdd == _hundredCoinValue)
        {
            _coinsEarned += _hundredCoinValue;
            _coinScript.HandleCoinText(_hundredCoinValue);
        }

        _coinsOwnedText.text = _coinsEarned.ToString();
    }
}
