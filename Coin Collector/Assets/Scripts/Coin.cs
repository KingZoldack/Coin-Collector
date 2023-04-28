using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    float _currentTime;
    float _timeBeforeDestroy = 2.5f;


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
}
