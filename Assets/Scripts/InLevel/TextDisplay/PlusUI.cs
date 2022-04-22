using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusUI : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(coroutine_Animation()); 
    }

    IEnumerator coroutine_Animation()
    {
        yield return StartCoroutine(coroutine_PosChange(0.1f));
        gameObject.SetActive(false);
    }

    IEnumerator coroutine_PosChange(float timer)
    {
        yield return new WaitForSeconds(timer * 3);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z); 
        yield return new WaitForSeconds(timer);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
        yield return new WaitForSeconds(timer);
    }
}
