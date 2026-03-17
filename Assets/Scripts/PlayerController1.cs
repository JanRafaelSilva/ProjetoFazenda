using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController1 : MonoBehaviour
{
    public float speed = 20f;
    public float xRange = 15f;
    public GameObject projectilePrefab;
    private float horizontalInput;

    public InputActionAsset InputActions;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction pausaActionPlayer;
    private InputAction pausaActionUI; 

    private InputAction GhostAction;
    public GameObject painel;

    // Update is called once per frame  
    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable(); 
    }
    
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Jump");
        pausaActionPlayer = InputSystem.actions.FindAction("Pausa");
        pausaActionUI = InputSystem.actions.FindAction("Despausa");
        GhostAction = InputSystem.actions.FindAction("Ghost");
    }
    void Update()
    {
        float horizontalInput = moveAction.ReadValue<Vector2>().x;
        // movimenta o player para esquerda e direita a partir da entrada do usu�rio
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput);
        // mant�m o player dentro dos limites do jogo (eixo x)
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.y);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.y);
        }
        if(fireAction.WasReleasedThisFrame())
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);

        }
        if (GhostAction.IsPressed())
        {
            this.gameObject.SetActive(false);
            //if(GhostAction.h)

        }else if (GhostAction.WasReleasedThisFrame())
        {
            this.gameObject.SetActive(true);
        }
        PauseGame();
       
}
void PauseGame()
    {
         if (pausaActionPlayer.WasPressedThisFrame())
        {
            painel.SetActive(true);
            InputActions.FindActionMap("Player").Disable(); 
            InputActions.FindActionMap("UI").Enable(); 
        }
        if (pausaActionUI.WasPressedThisFrame())
        {
            painel.SetActive(false);
            InputActions.FindActionMap("Player").Enable(); 
            InputActions.FindActionMap("UI").Disable(); 
        }
    }
    }