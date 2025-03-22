using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BTNHoverSound : MonoBehaviour, IPointerEnterHandler
{
    SoundManager soundManaager;

    // Start is called before the first frame update
    void Start()
    {
        soundManaager = GameObject.Find("SFXManager").GetComponent<SoundManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        soundManaager.PlaySound("BTNHover");
    }
}
