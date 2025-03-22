
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHighlighter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = new Color(1f, 1f, 0.3f);

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
    }

    void OnMouseEnter()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = highlightColor;
    }

    void OnMouseExit()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;
    }
}
