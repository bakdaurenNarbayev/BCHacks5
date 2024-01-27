using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformMovement : MonoBehaviour
{
    private int direction, count;
    private float baseSpeedMultiplier, currentPlatformSpeed;
    private float multiplier;
    private float temp;
    public GameObject tail, collider;
    private Vector3 position;
    private Quaternion rotation;

    private void Start()
    {
        baseSpeedMultiplier = Random.value;
        count = Random.Range(0, 240);
        direction = Random.Range(0, 2) * 2 - 1;

        position = tail.transform.localPosition;
        position.x = direction * 13.74f;
        position.y = 3.5f + direction * 0.5f;
        tail.transform.localPosition = position;
        collider.transform.localPosition = position;
        
        rotation = tail.transform.localRotation;
        rotation.z *= direction;
        tail.transform.localRotation = rotation;
        collider.transform.localRotation = rotation;
    }

    void Update()
    {
        if (GameManager.Instance.isSelected || GameManager.Instance.isActive) {
            return;
        }

        if(count < 60) {
            multiplier = count / 60;
        } else if(count >= 180) {
            temp = 240 - count;
            multiplier = temp / 60;
        } else {
            multiplier = 1;
        }
        
        currentPlatformSpeed = multiplier * (DatabaseManager.Instance.minPlatformSpeed + baseSpeedMultiplier * (DatabaseManager.Instance.maxPlatformSpeed - DatabaseManager.Instance.minPlatformSpeed));
        transform.position = new Vector3(transform.position.x + currentPlatformSpeed * direction, transform.position.y, transform.position.z);

        if(count >= 240)
        {
            count = 0;
            direction *= -1;

            position = tail.transform.localPosition;
            position.x = direction * 13.74f;
            position.y = 3.5f + direction * 0.5f;
            tail.transform.localPosition = position;
            collider.transform.localPosition = position;

            rotation = tail.transform.localRotation;
            rotation.z *= -1;
            tail.transform.localRotation = rotation;
            collider.transform.localRotation = rotation;
        } else
        {
            count++;
        }
    }
}