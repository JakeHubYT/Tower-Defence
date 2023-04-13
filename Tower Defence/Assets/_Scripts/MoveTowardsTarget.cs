using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;

    private void Update()
    {
      
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
