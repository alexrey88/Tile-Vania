using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] float moveAbsoluteSpeed = 2f;
    [SerializeField] float moveHalfDistance = 4f;

    Vector3 initialPosition;
    float moveDirection = 1;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveDirection * moveAbsoluteSpeed * Time.deltaTime);

        if (Vector3.Distance(initialPosition, transform.position) >= moveHalfDistance)
        {
            moveDirection *= -1;
        }
    }

    public float GetPlatformSpeed()
    {
        return moveDirection * moveAbsoluteSpeed;
    }
}
