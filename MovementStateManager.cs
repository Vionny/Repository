using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{

    public float moveSpeed = 2;
    [HideInInspector] public Vector3 direction;
    float hInput, vInput;
    CharacterController controller;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity = -9.18f;
    new Animator animation;
    Vector3 velocity;
    Vector3 spherePos;
    //public Rigidbody rbPlayer;
    bool jump;
    // Start is called before the first frame update
    void Start()
    {
        //rbPlayer = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        animation = GetComponent<Animator>();
    }
    
    void DirectionandMove()
    {
        /*
        jump = Input.GetButtonDown("Jump");
        animation.SetBool("Jump", false);
        if (jump==true)
        {
            rbPlayer.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            animation.SetBool("Jump", true);
           // transform.Translate(new Vector3(0,0, 0));
           // transform.Translate(new Vector3(0, 1, 0));
           // transform.Translate(new Vector3(0, 1.5, 0));
           // transform.Translate(new Vector3(0, 2, 0));
        }*/
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        //direction = transform.forward * vInput + transform.right * hInput;
        //controller.Move(direction * moveSpeed * Time.deltaTime);
        // Debug.Log(vInput);
        animation.SetFloat("hInput", hInput);
        animation.SetFloat("vInput", vInput);
        

    }
    void Gravity()
    {

        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }
    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        else return false;
    }
    // Update is called once per frame
    void Update()
    {
        DirectionandMove();
        //Debug.Log(Input.GetKeyDown(KeyCode.Space));
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(3 * -2 * gravity);
            animation.SetBool("Jump", true);
           // rbPlayer.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }else if(!(IsGrounded() && Input.GetKeyDown(KeyCode.Space)))
        {
            animation.SetBool("Jump", false);
            Gravity();
        }

        

        //GetPos(hInput, vInput);
    }
}
