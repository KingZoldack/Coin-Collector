using System.Collections;
using UnityEngine;

public class HandleClickedButton : MonoBehaviour
{
    [SerializeField]
    GameObject _defaulButton;

    private void OnEnable()
    {
        StartCoroutine(StartTurnOffRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator StartTurnOffRoutine()
    {
        while (this.gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(0.05f);
            this.gameObject.SetActive(false);
            _defaulButton.SetActive(true);
        }
    }
}
