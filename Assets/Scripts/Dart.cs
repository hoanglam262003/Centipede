using UnityEngine;
using UnityEngine.InputSystem;

public class Dart : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private Transform parent;

    public float speed = 50f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        col = GetComponent<Collider2D>();
        col.enabled = false;

        parent = transform.parent;
    }

    private void Update()
    {
        if ((rb.bodyType == RigidbodyType2D.Kinematic) && (Keyboard.current.spaceKey.isPressed || Mouse.current.leftButton.isPressed))
        {
            transform.SetParent(null);
            rb.bodyType = RigidbodyType2D.Dynamic;
            col.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (rb.bodyType != RigidbodyType2D.Kinematic)
        {
            Vector2 position = rb.position;
            position += Vector2.up * speed * Time.fixedDeltaTime;
            rb.MovePosition(position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.enabled = false;
    }
}
