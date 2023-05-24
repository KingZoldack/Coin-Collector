using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Badges : MonoBehaviour
{
    int _maxItemLevelCount;

    [SerializeField]
    GameObject[] _maxBadges;

    public Action<int> OnMaxedItem;

    public static Badges Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //_maxBadges[0].GetComponent<Image>().color = new Color(255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_maxItemLevelCount + "--");
        UnlockMaxBadges();
    }
    public void UpdateMaxItemLevelCount(int newLevelCount)
    {
        _maxItemLevelCount = newLevelCount;
        OnMaxedItem?.Invoke(_maxItemLevelCount); // Invoke the event when the _maxItemLevelCount is updated
    }

    void UnlockMaxBadges()
    {
        Color unlockedColor = new Color(255, 255, 255); //Change badge color from all black to actual color
        int[] badgeLevelCounts = { 3, 6, 9 }; //Requirement needed to unlock specific badge. 3 = broze, 6 = gold, 9 = red

        for (int i = 0; i < badgeLevelCounts.Length; i++)
        {
            if (_maxItemLevelCount >= badgeLevelCounts[i])
                _maxBadges[i].GetComponent<Image>().color = unlockedColor;
        }
    }
}
