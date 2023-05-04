using UnityEngine;

public class StringReferences : MonoBehaviour
{
    public static StringReferences Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    //PlayerPrefs
    string coinsEarnedPref = "CoinsEarned";

    public string CoinsEarnedPref => coinsEarnedPref;
}
