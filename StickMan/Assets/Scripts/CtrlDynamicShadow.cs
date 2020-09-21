using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CtrlDynamicShadow : CtrlShadow
{

    public bool isDynamic;
    [HideInInspector]
    public float maxOffset;
    public CtrlCamera ctrlCamera;

    void LateUpdate()
    {
        if (isDynamic)
        {
            shadowSpriteRenderer.sprite = spriteRenderer.sprite;
            shadowSpriteRenderer.flipX = spriteRenderer.flipX;
            float offset = 0;
            if (ctrlCamera.offset != 0)
            {
                offset = (ctrlCamera.offset / ctrlCamera.MaxOffset) * (maxOffset * 100);
                offset /= 100;
            }
            shadowGameobject.transform.position = gameObject.transform.position + (Vector3)ShadowOffset + new Vector3(-offset, 0f, 0f);
        }
    }
}

[CustomEditor(typeof(CtrlDynamicShadow))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myScript = target as CtrlDynamicShadow;

        if (myScript.isDynamic)
        {
            myScript.maxOffset = EditorGUILayout.FloatField("Max Offset", myScript.maxOffset);
        }
    }
}