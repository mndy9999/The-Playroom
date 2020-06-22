using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 4.0f;

    private Quaternion standingRotation;

    public ParticleSystem particles;

    private Animator animator;

    public Vector2 moveDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        standingRotation = transform.rotation;
        standingRotation.y = 90;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!falling)
            rb.velocity = moveDirection * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.layer)
        {
            case 16: //Interactible
                var col = collision.GetComponent<Interactable>();
                if (col.IsInstant)
                    col.Interact(transform);
                break;
            case 11: //Views
                DestroyImmediate(gameObject);
                break;
        }
    }

    public void Disappear()
    {
        var newPos = transform.position;
        newPos.z = -2;
        var part = Instantiate(particles, newPos, Quaternion.identity);
        part.Play();
        Destroy(gameObject);
    }

    public void Frown()
    {
        animator.SetBool("isFrowning", true);
    }

    public void Smile()
    {
        animator.SetBool("isFrowning", false);
    }

    private bool falling;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            falling = true;
            animator.SetBool("isSquished", true);
            rb.velocity = new Vector2(0, -5);
        }
        if (collision.gameObject.layer == 19)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetComponent<EnablePortal>().EnableFinalPortal();
            AudioManager.Instance.Play("Crash");
            Disappear();

        }
    }
}
