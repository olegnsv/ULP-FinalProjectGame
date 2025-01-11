using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
