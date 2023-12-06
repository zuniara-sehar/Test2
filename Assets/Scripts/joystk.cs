using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class joystk : MonoBehaviour
{
    public Joystick js;
    private float xinput, yinput;
    public Rigidbody2D rb;
    public LeaderBoard leaderboard;
    public Text scoreText;
    private int score = 0;
    private bool gameIsOver = false;
    public GameObject Menu;
    public GameObject GameUI;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("IncreaseScore", 1f, 1f);
        GameUI.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(gameObject.CompareTag("GameObject"))
        if (collision.gameObject.CompareTag("GameObject"))
        {
            this.gameObject.SetActive(false);
            //Destroy(gameObject);
            GameOver();
            
        }
    }
    public void HideTheMenu()
    {
        if (Menu != null)
        {
            Menu.SetActive(false);
            GameUI.SetActive(true);
            this.gameObject.SetActive(true);
            //IncreaseScore();
        }
        
    }
     private void GameOver()
    {
        gameIsOver = true;
        Menu.SetActive(true);
       
        // Display "Game Over" text or perform any other game over actions.
        Debug.Log("Game Over");
        
        // You can also update the score here if needed.
        // For now, let's just stop updating the score.
        CancelInvoke("IncreaseScore");
    }

     public void IncreaseScore()
    {
        if (!gameIsOver)
        {
            score++;
            scoreText.text = "Score: " + score;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        xinput = js.Horizontal;
        yinput = js.Vertical;

        rb.velocity = new Vector2(xinput*3,  yinput*3);
    }
}
