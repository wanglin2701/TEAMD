using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundaryDestroy : MonoBehaviour
{
    private bool canDestroy = false;

    void Start()
    {
        StartCoroutine(DelayBeforeCheck());
    }

    IEnumerator DelayBeforeCheck()
    {
        yield return new WaitForSeconds(2f); // Wait 2 seconds before checking
        canDestroy = true;
    }

    void Update()
    {
        if (!canDestroy) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x < 0 || screenPos.x > Screen.width ||
            screenPos.y < 0 || screenPos.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }
}
