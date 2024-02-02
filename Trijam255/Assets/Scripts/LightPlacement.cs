using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightPlacement : MonoBehaviour
{
    [SerializeField] private Camera lightPlacementCamera;
    [SerializeField] private GameObject followCamera, mainCamera, lightPlacementCameraGameObject, light2D;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private int maxLights = 3;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TextMeshProUGUI placableLightsText;
    [SerializeField] private GameObject placableLightsTextGameObject;
    [SerializeField] private GameObject warningText;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject startText;
    
    [HideInInspector]
    public bool gameStart = false;
    private bool isTutorialPanelActive = true;
    private bool startButtonPressed = false;
    
    private int _currentlyPlacedLights = 0;
    
    private void Start()
    {
        placableLightsText.text = maxLights - _currentlyPlacedLights + " / " + maxLights;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStart)
        {
            if (_currentlyPlacedLights < maxLights)
            {
                if (!isTutorialPanelActive)
                {
                    Vector3 pos = lightPlacementCamera.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 offset = new Vector3(0, 0, 10);
                    Instantiate(light2D, pos + offset, Quaternion.identity);
                    AudioSource.PlayClipAtPoint(light2D.GetComponent<AudioSource>().clip, pos);
                    _currentlyPlacedLights++;
                    placableLightsText.text = (maxLights - _currentlyPlacedLights) + " / " + maxLights;
                }
            }
            else
            {
                if (!startButtonPressed)
                {
                    warningText.SetActive(true);
                    Invoke("DestroyWarningText", 1f);
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        globalLight.intensity = 0f;
        lightPlacementCameraGameObject.SetActive(false);
        mainCamera.SetActive(true);
        followCamera.SetActive(true);
        startButton.SetActive(false);
        gameStart = true;
        restartButton.SetActive(true);
        placableLightsTextGameObject.SetActive(false);
        statusText.transform.gameObject.SetActive(true);
        healthBar.transform.gameObject.SetActive(true);
        startText.SetActive(false);
    }
    
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
    public void DestroyWarningText()
    {
        warningText.SetActive(false);
    }

    public void DisableTutorialPanel()
    {
        tutorialPanel.SetActive(false);
        isTutorialPanelActive = false;
    }
    
}
