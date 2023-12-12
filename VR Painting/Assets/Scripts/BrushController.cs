using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushController : MonoBehaviour
{
    private float speed = 5f;
    private BrushCollisionController brushCollisionController;

    // Start is called before the first frame update
    void Start()
    {
        brushCollisionController = GetComponent<BrushCollisionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            float dirX = speed * Input.GetAxis("Horizontal") * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + dirX, transform.position.y, transform.position.z);
        }
        if (Input.GetButton("Vertical"))
        {
            float dirY = speed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + dirY, transform.position.z);
        }
        if (Input.GetKey(KeyCode.C))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (speed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (!brushCollisionController.hitPixel)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (speed * Time.deltaTime));
            }
        }
    }
}
