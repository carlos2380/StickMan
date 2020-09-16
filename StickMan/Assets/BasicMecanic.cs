using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMecanic : MonoBehaviour
{

    public Rigidbody2D pivot1;
    public Rigidbody2D pivot2;

    // Start is called before the first frame update
    void  Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space))
        {
            if (gameObject.GetComponent<HingeJoint2D>().connectedBody.Equals(pivot1))
            {
                gameObject.GetComponent<HingeJoint2D>().connectedBody = pivot2;
            }
            else
            {
                gameObject.GetComponent<HingeJoint2D>().connectedBody = pivot1;
            }
        }
    }
}
