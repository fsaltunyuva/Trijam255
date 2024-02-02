using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightPlacement : MonoBehaviour
{
    [SerializeField] private Camera lightPlacementCamera;
    [SerializeField] private GameObject followCamera, mainCamera, lightPlacementCameraGameObject, light2D;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private int maxLights = 3;
    [SerializeField] private GameObject startButton;
    
    [HideInInspector]
    public bool gameStart = false;
    
    private int _currentlyPlacedLights = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_currentlyPlacedLights < maxLights)
            {
                Vector3 pos = lightPlacementCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 offset = new Vector3(0, 0, 10);
                Instantiate(light2D, pos + offset, Quaternion.identity);
                _currentlyPlacedLights++;
            }
            else
            {
                //TODO: Add a message to the player that they can't place any more lights
            }
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
    }
    
}
