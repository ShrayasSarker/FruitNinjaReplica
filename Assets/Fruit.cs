using UnityEditor.Callbacks;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject fruitSlicedPrefab; // Prefab for the sliced fruit
    public float startForce = 13f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up*startForce,ForceMode2D.Impulse); // Apply an initial force to the fruit in the upward direction
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Blade")
        {
            Vector3 direction = (col.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            //Quaternion.Euler(0, 0, 180); // Rotate the sliced fruit to face downwards
            GameObject slicedFruit = Instantiate(fruitSlicedPrefab,transform.position, rotation); // Instantiate the sliced fruit prefab at the fruit's position and rotation
            Score.instance.AddScore(1);
            Destroy(slicedFruit, 3f); // Destroy the sliced fruit after 2 seconds
            Destroy(gameObject);
        }
    }
}
