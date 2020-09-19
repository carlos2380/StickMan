using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBehaviour : MonoBehaviour
{

    public Sprite spriteUnsticked;
    public Sprite spriteSticked;

    [Header("Setting animation selected")]
    public float timeAnim;
    public AnimationCurve scaleCurve;

    private SpriteRenderer spriteRenderer;
    private GameObject lineSelector;
    private bool sticked = false;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        lineSelector = gameObject.transform.GetChild(1).gameObject;
    }

  
    public void setSticked()
    {
        spriteRenderer.sprite = spriteSticked;
        sticked = true;
    }

    public void setUnsticked()
    {
        spriteRenderer.sprite = spriteUnsticked;
        sticked = false;
        unselected();
    }

    public void selected()
    {
        if(!sticked)
        {
            StartCoroutine(selectingJoint());
        }
    }

    public void unselected()
    {
        StopCoroutine(selectingJoint());
        lineSelector.transform.localScale = Vector3.zero;
    }

    IEnumerator selectingJoint()
    {
        float time = 0f;
        Vector3 startScale = Vector3.zero; 
        Vector3 endScale = new Vector3(1.2f, 1.2f, 1f);
        while (time <= timeAnim)
        {
            time += Time.deltaTime;
            lineSelector.transform.localScale = Vector3.Lerp(startScale, endScale, scaleCurve.Evaluate(time));
            yield return null;
        }
    }
}
