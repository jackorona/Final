using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject PickupShield;
    public bool activate;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;
    private Rigidbody rb;

    public AudioClip Laser;
    public AudioClip ShieldSound;
    public AudioClip PowerSound;
    public AudioSource Laseraudio;
    public AudioSource Pickupaudio;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Laseraudio.clip = Laser;
            Laseraudio.Play();
        }
        if (activate == true)
        {
            PickupShield.SetActive(true);
        }
        else
        {
            PickupShield.SetActive(false);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {
            activate = true;
            Destroy(other.gameObject);
            Pickupaudio.clip = ShieldSound;
            Pickupaudio.Play();
        }
        if (other.tag == "Enemy")
        {
            activate = false;
            Destroy(other.gameObject);
        }
        if (other.tag == "Firerate")
        {
            fireRate = (fireRate - .07f);
            Pickupaudio.clip = PowerSound;
            Pickupaudio.Play();

            Destroy(other.gameObject);
        }

    }
}
