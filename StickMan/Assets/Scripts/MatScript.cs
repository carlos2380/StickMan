using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatScript : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("Touched");
    }
}
