using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBehaviour : MonoBehaviour
{

    public Sprite spriteUnsticked;
    public Sprite spriteSticked;
    private SpriteRenderer spriteRenderer;

    
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

  
    public void setSticked()
    {
        spriteRenderer.sprite = spriteSticked;
    }

    public void setUnsticked()
    {
        spriteRenderer.sprite = spriteUnsticked;
    }
}
