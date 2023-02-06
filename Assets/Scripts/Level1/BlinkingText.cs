using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    public GameObject blinkingText;
    [SerializeField]
    private int Count = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(blinkText());
    }

    IEnumerator blinkText()
    {
        for (int i = 0; i < 4; i++)
        {
            blinkingText.SetActive(false);
            yield return new WaitForSeconds(1);
            blinkingText.SetActive(true);
            yield return new WaitForSeconds(1);
        }
        blinkingText.SetActive(false);
        StopCoroutine(blinkText());
    }
}
