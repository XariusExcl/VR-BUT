using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIModal : MonoBehaviour
{
    TMP_Text text;
    Animation fadeoutAnimation;
    public float lifetime;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        fadeoutAnimation = GetComponent<Animation>();
        lifetime = (lifetime < 0.5f) ? 0.5f : lifetime;
    }

    public void Show(string message)
    {
        text.text = message;
        StartCoroutine(ShowCoroutine());
    }

    IEnumerator ShowCoroutine()
    {
        /*
        yield return new WaitForSeconds(lifetime - 0.5f);
        fadeoutAnimation.Play();
        yield return new WaitForSeconds(0.5f);
        */
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

}
