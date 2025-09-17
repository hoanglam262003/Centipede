using UnityEngine;
using UnityEngine.InputSystem;

public class Blaster : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 spawnPosition;
    private bool hasCollidedThisFrame = false;

    public float speed = 20f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = transform.position;
    }

    private void Update()
    {
        direction = Vector2.zero;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            direction.y = 1;
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            direction.y = -1;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            direction.x = 1;
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            direction.x = -1;

        direction = direction.normalized;

        hasCollidedThisFrame = false;
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position += direction.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(position);
    }

    public void Respawn()
    {
        transform.position = spawnPosition;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollidedThisFrame && collision.gameObject.layer == LayerMask.NameToLayer("Centipede"))
        {
            hasCollidedThisFrame = true;
            GameManager.instance.ResetRound();
        }
    }
}
