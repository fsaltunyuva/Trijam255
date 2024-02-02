using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    [SerializeField] private LightPlacement lightPlacementInstance;
    private float _health = 100;
    [SerializeField] private float _healthDepletionRate;
    
    private Vector2 movement;
    private bool pauseDepletion = false;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI statusText;
    
    [SerializeField] private GameObject winScreen, loseScreen;
    
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject restartButton;
    
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update() 
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        FlipSprite();
        
        if (lightPlacementInstance.gameStart)
        {
            if(!pauseDepletion)
                _health -= _healthDepletionRate * Time.deltaTime;
            else
            {
                _health += _healthDepletionRate * Time.deltaTime;
                if (_health > 100)
                    _health = 100;
            }
        }

        if (_health <= 0)
        {
            loseScreen.SetActive(true);
            lightPlacementInstance.gameStart = false;
            statusText.text = "";
            restartButton.SetActive(false);
        }
            
    }
    
    void FixedUpdate() 
    {
        healthBar.value = _health;
        if (lightPlacementInstance.gameStart)
        {
            rb.velocity = movement * speed * Time.fixedDeltaTime;
            playerAnimator.SetBool("Run", movement != Vector2.zero);
        }
            
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("LightRadius"))
    //     {
    //         if (lightPlacementInstance.gameStart)
    //         {
    //             Debug.Log("Player is in light radius!");
    //             pauseDepletion = true;
    //             statusText.text = "You are regenerating health!";
    //             statusText.color = Color.green;
    //         }
    //     }
    //     else if (other.CompareTag("Finish"))
    //     {
    //         lightPlacementInstance.gameStart = false;
    //         winScreen.SetActive(true);
    //         statusText.text = "";
    //         restartButton.SetActive(false);
    //     }
    //     else
    //     {
    //         Debug.Log("Player is out of light radius!");
    //         pauseDepletion = false;
    //         statusText.text = "You are losing health!";
    //         statusText.color = Color.red;
    //     }
    // }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("LightRadius"))
        {
            if (lightPlacementInstance.gameStart)
            {
                Debug.Log("Player is in light radius!");
                pauseDepletion = true;
                statusText.text = "You are regenerating health!";
                statusText.color = Color.green;
            }
        }
        else if (other.CompareTag("Finish"))
        {
            lightPlacementInstance.gameStart = false;
            winScreen.SetActive(true);
            statusText.text = "";
            restartButton.SetActive(false);
        }
        else
        {
            Debug.Log("Player is out of light radius!");
            pauseDepletion = false;
            statusText.text = "You are losing health!";
            statusText.color = Color.red;
        }
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("LightRadius") && lightPlacementInstance.gameStart)
    //     {
    //
    //     }
    // }
    
    private void FlipSprite()
    {
        if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
