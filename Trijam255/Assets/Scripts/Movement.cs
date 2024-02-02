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
    
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update() 
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
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
        }
            
    }
    
    void FixedUpdate() 
    {
        healthBar.value = _health;
        if(lightPlacementInstance.gameStart)
            rb.velocity = movement * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LightRadius") && lightPlacementInstance.gameStart)
        {
            pauseDepletion = true;
            statusText.text = "You are regenerating health!";
            statusText.color = Color.green;
        }
        else if (other.CompareTag("Finish"))
        {
            lightPlacementInstance.gameStart = false;
            winScreen.SetActive(true);
            statusText.text = "";
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LightRadius") && lightPlacementInstance.gameStart)
        {
            pauseDepletion = false;
            statusText.text = "You are losing health!";
            statusText.color = Color.red;
        }
    }
}
