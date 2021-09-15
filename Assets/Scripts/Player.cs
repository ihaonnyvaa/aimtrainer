using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 70f;
    public GameObject Camera;
    private Vector2 rotPos;

    public Rigidbody rb;
    public float jumpThrust = 20f;
    private bool isGrounded = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Hiiri pois näkyvistä
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Sivuttain liike (wasd)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(x, 0, z) * movementSpeed * Time.deltaTime);

        //Kääntyvyys (hiiri)
        rotPos.y += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        //Rajaa kääntymisasteen. (Ei mene ympäri asti)
        if(rotPos.y < -50f)
        {
            rotPos.y = -50f;
        } else if(rotPos.y > 50f)
        {
            rotPos.y = 50f;
        }

        rotPos.x += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

        //Pelaaja kääntyy sivuttain
        transform.localRotation = Quaternion.Euler(0, rotPos.x, 0);

        //Vain kamera kääntyy ylös ja alas
        Camera.transform.localRotation = Quaternion.Euler(-rotPos.y, 0, 0);

        //Hyppy ylös
        if(Input.GetKeyDown("space"))
        {
            if(isGrounded)
            {
                //Vain jos collision maan kanssa on tapahtunut
                isGrounded = false;
                rb.AddForce(transform.up * jumpThrust);
            }
        }
    }

    void OnCollisionEnter(Collision info)
    {
        if(info.collider.tag == "Ground")
        {
            //Voi taas hypätä
            isGrounded = true;
        }
    }
}
