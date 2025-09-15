using UnityEngine;

public class MushroomField : MonoBehaviour
{
    private BoxCollider2D col;
    public Mushroom mushroom;
    public int amount = 50;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        Bounds bounds = col.bounds;
        for (int i = 0; i < amount; i++)
        {
            Vector2 position = Vector2.zero;
            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            Instantiate(mushroom, position, Quaternion.identity, transform);
        }
    }
}
