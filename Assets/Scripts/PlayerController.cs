using UnityEditor.SearchService;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float inputX;
    private float inputY;
    public float speed = 200;
    public float lookSpeed = 100;
    Rigidbody rb;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        MyInput();
        Vector3 move = new Vector3(inputX, 0, inputY);
        if (move != new Vector3(0, 0, 0))
        {
            transform.forward = Vector3.Lerp(move, transform.forward, lookSpeed * Time.deltaTime);
            rb.AddForce(move * Time.deltaTime * speed);
            animator.SetInteger("moving", 1);
        }
        else
        {
            animator.SetInteger("moving", 0);
        }
    }
    void MyInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tomate"))
        {
            if(Input.GetKey(KeyCode.E))
            {

            }
            Debug.Log("entrado");
        }

        else if (other.CompareTag("abacaxi"))
        {
            Debug.Log("entrado");
        }

        else if (other.CompareTag("carne"))
        {
            Debug.Log("entrado");
        }

        else if (other.CompareTag("blueberry"))
        {
            Debug.Log("entrado");
        }

        else if (other.CompareTag("maca"))
        {
            Debug.Log("entrado");
        }

        else if (other.CompareTag("batata"))
        {
            Debug.Log("entrado");
        }
    }
}
