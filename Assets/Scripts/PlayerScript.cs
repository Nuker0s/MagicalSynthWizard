using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    public PlayerInput pinput;
    public Rigidbody rb;
    public Vector2 forwardandrot;
    private InputAction move;
    private InputAction keyboard;
    // Start is called before the first frame update
    void Start()
    {
        move = pinput.actions.FindAction("Move");
        keyboard = pinput.actions.FindAction("Keyboard");
    }

    // Update is called once per frame
    void Update()
    {
        if (move.IsPressed())
        {
            Vector2 dir = move.ReadValue<Vector2>();
            
            transform.Rotate(0, dir.x * forwardandrot.y * Time.deltaTime, 0);
        }

    }
    private void FixedUpdate()
    {
        if (move.IsPressed())
        {
            Vector2 dir = move.ReadValue<Vector2>();
            rb.velocity = transform.forward * dir.y * forwardandrot.x; 
            
        }
    }
}
