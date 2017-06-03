using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{

    public float destroyAfterAmount;

    private void Start()
    {
        StartCoroutine(DestroyAfter(destroyAfterAmount));
    }

    private IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
