using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _coinText;

    float _currentTime;
    float _timeBeforeDestroy = 2.5f;


    enum CoinValue
    {
        One = 1,
        Ten = 10,
        Hundred = 100
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _timeBeforeDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;


        if (_currentTime <= 0f)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    public void HandleCoinText(int value)
    {
        CoinValue coinText = CoinValue.One;


        if (value == 1)
            coinText = CoinValue.One;

        if (value == 10)
            coinText = CoinValue.Ten;

        if (value == 100)
            coinText = CoinValue.Hundred;

        Debug.Log(coinText);

        switch (coinText)
        {
            case CoinValue.One:
                _coinText.text = "+1";
                break;
            case CoinValue.Ten:
                _coinText.text = "+10";
                break;
            case CoinValue.Hundred:
                _coinText.text = "+100";
                break;
            default:
                break;
        }
    }
}
