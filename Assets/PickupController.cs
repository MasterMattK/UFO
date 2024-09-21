// Matthew Kellen, 9/21/2024
// This script controls the movement and visibility of the pickup objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public float speed;
    public float waitTime; // this is the time before this pickup spawns

    private Rigidbody2D rb2d;
    private float time;
    private bool isActive;

    // Start is called before the first frame update
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        time = 0.0f;

        // make the pickup inactive while still allowing the script to run
        isActive = false;
        gameObject.GetComponent<Renderer>().enabled = false; // makes it invisible
        gameObject.GetComponent<Collider2D>().enabled = false; // disables its collision
    }

    // Update is called once per frame
    private void Update()
    {
        // update the time until the object is activated
        if (!isActive)
        {
            time += Time.deltaTime;
            if (time >= waitTime)
            {
                gameObject.GetComponent<Renderer>().enabled = true; // makes it visible
                gameObject.GetComponent<Collider2D>().enabled = true; // enables its collision
                isActive = true;
            }
            return;
        }

        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    // FixedUpdate is in sync with physics engine
    private void FixedUpdate()
    {
        if (!isActive)
            return;

        Vector2 movement = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        rb2d.AddForce(movement * speed);
    }
}
