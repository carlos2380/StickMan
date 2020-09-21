using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCamera : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float MaxOffset;

    [HideInInspector]
    public float offset;

    private Rigidbody2D playerRigidbody;

    private void Start()
    {
        offset = 0;
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z); ;
    }
    void Update()
    {
         
        if(playerRigidbody.velocity.x > 0)
        {
            offset += speed*Time.deltaTime;
            if(offset > MaxOffset)
            {
                offset = MaxOffset;
            }
        }
        else if (playerRigidbody.velocity.x < 0)
        {
            offset -= speed * Time.deltaTime;
            if (offset < -MaxOffset)
            {
                offset = -MaxOffset;
            }
        }
        gameObject.transform.position = new Vector3(player.transform.position.x + offset, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
