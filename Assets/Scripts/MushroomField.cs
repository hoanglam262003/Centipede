using UnityEngine;

public class MushroomField : MonoBehaviour
{
    private BoxCollider2D col;
    public Mushroom mushroomPrefab;
    public int amount = 50;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    public void Generate()
    {
        Bounds bounds = col.bounds;
        for (int i = 0; i < amount; i++)
        {
            Vector2 position = Vector2.zero;
            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
            Instantiate(mushroomPrefab, position, Quaternion.identity, transform);
        }
    }

    public void Clear()
    {
        Mushroom[] mushrooms = FindObjectsByType<Mushroom>(FindObjectsSortMode.None);
        foreach (Mushroom mushroom in mushrooms)
        {
            Destroy(mushroom.gameObject);
        }
    }

    public void Heal()
    {
        Mushroom[] mushrooms = FindObjectsByType<Mushroom>(FindObjectsSortMode.None);
        foreach (Mushroom mushroom in mushrooms)
        {
            mushroom.GetHealth();
        }
    }
}
