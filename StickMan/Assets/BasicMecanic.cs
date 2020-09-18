using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMecanic : MonoBehaviour
{

    public GameObject ancors;
    public float gravityAir;
    public float gravityRope;
    public float factorX;
    public float factorY;

    private HingeJoint2D HJoint;
    private bool spieder = false;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        HJoint = gameObject.GetComponent<HingeJoint2D>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        int bestPosition = 0;
        float bestDistance = float.MaxValue;
        for(int i = 0; i < ancors.transform.childCount; ++i)
        {
            float actualDistance = Vector2.Distance(gameObject.transform.position, ancors.transform.GetChild(i).transform.position);
            if(actualDistance < bestDistance)
            {
                bestPosition = i;
                bestDistance = actualDistance;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!spieder)
            {
                HJoint.enabled = true;
                rigidBody.gravityScale = gravityRope;
                HJoint.connectedBody = ancors.transform.GetChild(bestPosition).gameObject.GetComponent<Rigidbody2D>();

            }
            else
            {
                rigidBody.velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x * factorX , gameObject.GetComponent<Rigidbody2D>().velocity.y + factorY) ;
                rigidBody.gravityScale = gravityAir;
                HJoint.enabled = false;
            }
            spieder = !spieder;
        }
    }
}
