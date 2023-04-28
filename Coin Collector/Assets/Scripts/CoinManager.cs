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
    TextMeshProUGUI _clicksLeftText;
    [SerializeField]
    TextMeshProUGUI _coinsOwnedText;

    int _clicksToBonus = 10;
    int _coinsEarned = 0;

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
        HandleBonus(1, 10);
    }

    void HandleBonus(int oneCoin, int tenCoins)
    {
        _clicksToBonus--;
        if (_clicksToBonus < 1)
        {
            AddCoins(tenCoins);
            _clicksToBonus = 10;
        }

        if (_clicksToBonus >= 1)
        {
            AddCoins(oneCoin);
        }

        _clicksLeftText.text = _clicksToBonus.ToString();
    }

    void AddCoins(int coinsToAdd)
    {
        if (coinsToAdd == 1)
        {
            _coinsEarned++;
        }
        else if (coinsToAdd == 10)
        {
            _coinsEarned += 10;
        }

        _coinsOwnedText.text = _coinsEarned.ToString();
    }
}
