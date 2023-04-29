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

    int _coinsEarned = 0; //Initial coins owned value
    int _clicksToBonus = 101, _maxBonusValue = 101; //Clicks left to bonus initially and after each subsequent bonus
    int _minRangeForBonus = 0, _maxRangeForBonus = 10; //Range for random chance
    int _oneCoinValue = 1, _tenCoinValue = 10, _hundredCoinValue = 100; //Coin values

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
        GameObject spawnedCoin = Instantiate(_coinPrefab, _mainCanvas.transform); //Spawn Coin
        spawnedCoin.transform.SetParent(_coinHolder.transform); //Sets parent to object on the Canvas

        TextMeshProUGUI coinText = spawnedCoin.GetComponentInChildren<TextMeshProUGUI>();//Gets child text for updating
        _coinScript.SetCoinText(coinText); //Updates text

        HandleBonus(_oneCoinValue, _tenCoinValue, _hundredCoinValue);
    }

    void HandleBonus(int oneCoin, int tenCoins, int oneHundredCoins)
    {
        _clicksToBonus--; //Tracks bonus countdown

        if (_clicksToBonus <= 0) //Assignes bonus
        {
            int randomRange = Random.Range(_minRangeForBonus, _maxRangeForBonus);

            if (randomRange == _minRangeForBonus) //1 in 10 chance to get 100 bonus coins
                AddCoins(oneHundredCoins);
            else //9 in 10 chance to get 10 bonus coins
                AddCoins(tenCoins);

            _clicksToBonus = _maxBonusValue; //Resets Bonus Value
        }
        else //Adds 1 coin for every click that is not a bonus round
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
        }
        else if (coinsToAdd == _tenCoinValue)
        {
            _coinsEarned += _tenCoinValue;
        }
        else if (coinsToAdd == _hundredCoinValue)
        {
            _coinsEarned += _hundredCoinValue;
        }

        _coinsOwnedText.text = _coinsEarned.ToString();

        _coinScript.HandleCoinText(coinsToAdd); //Calls HandleCoinText after adding coins
    }
}
