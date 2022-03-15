using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboUI : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(coroutine_Animation());
    }
    IEnumerator coroutine_Animation()
    {
        yield return StartCoroutine(coroutine_ScaleChange(0.1f));
        gameObject.SetActive(false);  
    }
    IEnumerator coroutine_ScaleChange(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        yield return new WaitForSeconds(timer * 2);
        gameObject.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(timer); 
    }
}
