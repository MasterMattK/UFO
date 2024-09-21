// Matthew Kellen, 9/21/2024
// This script controls the movement of the player and the UI elements on screen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float timer;
    public Text timerText;
    public Text winText;
    public Text loseText;
    public Button restartButton;

    private Rigidbody2D rb2d;
    private bool hasLost;
    private bool hasWon;

    // Start is called before the first frame update
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        int seconds = (int)timer % 60;
        timerText.text = "Timer: " + seconds.ToString() + " s"; // set timer text to 60s
        winText.text = "";
        loseText.text = "";
        restartButton.gameObject.SetActive(false); // hide button
        hasLost = false;
        hasWon = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // we don't need to keep updating the timer after they've won or lost
        if (hasLost || hasWon) return; 

        timer -= Time.deltaTime;

        // once the timer has hit 0 seconds, we don't need to keep subtracting
        if (timer >= 0.0f)
        {
            int seconds = (int)timer % 60;
            timerText.text = "Timer: " + seconds.ToString() + "s";
        } else
        {
            // tell the player they've won
            if (!hasLost)
            {
                winText.text = "You Win!";
                hasWon = true;
            }

            timerText.text = "Timer: 0s";
            restartButton.gameObject.SetActive(true);
        }
    }

    // FixedUpdate is in sync with physics engine
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.velocity = movement * speed;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if the player has won or lost, we don't need to check collisions
        if (hasWon || hasLost) return;

        // tell the player they've lost and allow them to reset
        if (other.gameObject.CompareTag("PickUp"))
        {
            hasLost = true;
            loseText.text = "Game Over";
            restartButton.gameObject.SetActive(true);
        }
    }

    public void OnRestartButtonPress()
    {
        SceneManager.LoadScene("SampleScene"); // restart the game
    }
}
