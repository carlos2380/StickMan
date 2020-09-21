using UnityEngine;

public class CtrlShadow : MonoBehaviour
{
    public Material ShadowMaterial;
    public Vector2 ShadowOffset;
    

    protected SpriteRenderer spriteRenderer;
    protected GameObject shadowGameobject;
    protected SpriteRenderer shadowSpriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shadowGameobject = new GameObject("Shadow2D");

        shadowSpriteRenderer = shadowGameobject.AddComponent<SpriteRenderer>();
        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
        shadowSpriteRenderer.material = ShadowMaterial;
        shadowSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;

        shadowGameobject.transform.parent = gameObject.transform;
        shadowGameobject.transform.localScale = Vector3.one;
        shadowGameobject.transform.position = gameObject.transform.position + (Vector3)ShadowOffset;


    }
}

