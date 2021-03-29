using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MyPlayerController : NetworkBehaviour
{
    Rigidbody rb;
    public float speed = 5.0F;
    public float rotationSpeed = 100.0F;

    public Vector3 StartPos;
    public Quaternion StartRot;

    public GameObject SpeedFx;

    float rotation;
    public float translation;
    public bool isJump;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartPos = this.transform.position;
        StartRot = this.transform.rotation;
        SpeedFx = LocalGM.LGMinstanse.SpeedFx;
    }

    private void Update()
    {

        translation = Input.GetAxis("Vertical") * speed;

        if (translation >= 0.1f)
        {

            SpeedFx.SetActive(true);
            rotation = Input.GetAxis("Horizontal") * rotationSpeed;
            rotation *= Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }
        else if (translation <= 0.1f && translation != 0f)
        {
            SpeedFx.SetActive(false);
            rotation = -(Input.GetAxis("Horizontal") * rotationSpeed);
            rotation *= Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            SpeedFx.SetActive(false);
        }

        translation *= Time.deltaTime;
        transform.Translate(0, 0, translation);

        if (!isJump && LocalGM.LGMinstanse.JumpBar.GetComponent<Slider>().value >= 0.2f && (LocalGM.LGMinstanse.JumpBar.GetComponent<Slider>().value - 0.2f) >= 0f)
        {
            JumpSkill();
        }

    }

    void JumpSkill()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJump = true;
            Debug.Log("Jump Forward");
            rb.AddRelativeForce((Vector3.up * rb.mass * 8f), ForceMode.Impulse);
            rb.velocity = transform.forward * 7f;
            LocalGM.LGMinstanse.JumpBar.GetComponent<Slider>().value -= 0.2f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isJump = true;
            Debug.Log("Jump Backward");
            rb.AddRelativeForce((Vector3.up * rb.mass * 8f), ForceMode.Impulse);
            rb.velocity = transform.forward * -7f;
            LocalGM.LGMinstanse.JumpBar.GetComponent<Slider>().value -= 0.2f;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isJump = true;
            Debug.Log("Jump Left");
            rb.AddRelativeForce((Vector3.up * rb.mass * 8f), ForceMode.Impulse);
            rb.velocity = transform.right * -7f;
            LocalGM.LGMinstanse.JumpBar.GetComponent<Slider>().value -= 0.2f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isJump = true;
            Debug.Log("Jump Right");
            rb.AddRelativeForce((Vector3.up * rb.mass * 8f), ForceMode.Impulse);
            rb.velocity = transform.right * 7f;
            LocalGM.LGMinstanse.JumpBar.GetComponent<Slider>().value -= 0.2f;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Road"))
        {
            if (isJump)
            {
                isJump = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isLocalPlayer)
        {
            if (other.CompareTag("OutOfStartArea"))
            {
                LocalGM.LGMinstanse.WinCollider.SetActive(true);
            }
            if (other.CompareTag("Cheater") && !GetComponent<SetUpLocalPlayer>().isWin)
            {
                LocalGM.LGMinstanse.WinCollider.SetActive(false);
            }
        }
    }
}
