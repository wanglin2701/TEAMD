
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHighlighter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = Color.cyan;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plate") && spriteRenderer != null)
            spriteRenderer.color = highlightColor;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plate") && spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }
}
