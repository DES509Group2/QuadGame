using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    private Vector3 startScale;
    [SerializeField]
    private float scaleDuration;
    [SerializeField]
    private Vector3 endScale;

    void Start()
    {
        startScale = transform.localScale;
    }
    void OnEnable()
    {
        PlayerMovement.scaleChange += scaleUp;
    }

    private void OnDisable()
    {
        PlayerMovement.scaleChange -= scaleUp;
    }
    void scaleUp()
    {
        transform.LeanScale(endScale, scaleDuration);
        StartCoroutine(delay());

    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
        transform.LeanScale(startScale, scaleDuration);
    }
}
