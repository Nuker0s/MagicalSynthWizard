using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{
    public PlayerInput pinput;
    public Rigidbody rb;
    public Transform currentcheckpoint;
    public float hp=100;
    public float moveforce=1000f;
    public float maxspeed = 8;
    public float maxhp=100;
   
    public Vector2 forwardandrot;
    private InputAction move;
    private InputAction keyboard;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
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
        if (transform.position.y < - 10)
        {
            rb.velocity = Vector3.zero;
            transform.position = currentcheckpoint.position;
        }
        if (hp<=0)
        {
            transform.position = currentcheckpoint.position;
            hp = maxhp;
        }

    }
    private void FixedUpdate()
    {
        if (move.IsPressed())
        {
            Vector2 dir = move.ReadValue<Vector2>();
            rb.AddForce(dir.y*transform.forward * moveforce);
            if (rb.velocity.magnitude>maxspeed)
            {
                rb.velocity = rb.velocity.normalized * maxspeed;
            }
            
        }
    }
}
