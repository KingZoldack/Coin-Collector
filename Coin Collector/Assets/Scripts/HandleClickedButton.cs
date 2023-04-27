using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleClickedButton : MonoBehaviour
{
    [SerializeField]
    GameObject _defaulButton;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (this.gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(0.5f);
            this.gameObject.SetActive(false);
            _defaulButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
