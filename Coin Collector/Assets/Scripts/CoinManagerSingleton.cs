using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManagerSingleton : MonoBehaviour
{
    public static CoinManagerSingleton Instance;

    int _coinsOwned = 10000; //Initial coins owned value

    public int CoinsOwned
    {
        get
        {
            return _coinsOwned;
        }
        set
        {
            _coinsOwned = value;
            PlayerPrefs.SetInt("CoinsOwned", _coinsOwned); //Save the coins owned to PlayerPrefs
            CoinsOwnedChanged?.Invoke(_coinsOwned); //Raise an event to notify other scripts that the coins owned has changed
        }
    }

    public event System.Action<int> CoinsOwnedChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            _coinsOwned = PlayerPrefs.GetInt("CoinsOwned", 10000); //Retrieve the coins owned from PlayerPrefs
        }
        else
            Destroy(this.gameObject);
    }
}
