using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CtrlShadow : MonoBehaviour
{
    public Material ShadowMaterial;
    public Vector2 ShadowOffset;
    public bool isDynamic;
    [HideInInspector]
    public float maxOffset;

    private SpriteRenderer spriteRenderer;
    private GameObject shadowGameobject;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shadowGameobject = new GameObject("Shadow2D");

        SpriteRenderer shadowSpriteRenderer = shadowGameobject.AddComponent<SpriteRenderer>();
        shadowSpriteRenderer.sprite = spriteRenderer.sprite;
        shadowSpriteRenderer.material = ShadowMaterial;
        shadowSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;

        shadowGameobject.transform.parent = gameObject.transform;
        shadowGameobject.transform.localPosition = gameObject.transform.localPosition;
        shadowGameobject.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        shadowGameobject.transform.localPosition = Vector3.zero + (Vector3)ShadowOffset;
        shadowGameobject.transform.localRotation = gameObject.transform.localRotation;
    }


}

[CustomEditor(typeof(CtrlShadow))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myScript = target as CtrlShadow;
        
        if (myScript.isDynamic)
        {
            myScript.maxOffset = EditorGUILayout.FloatField("Max Offset", myScript.maxOffset); 
        }
    }
}