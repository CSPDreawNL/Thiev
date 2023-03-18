using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject Target;

    private void LateUpdate()
    {
        transform.position = Target.transform.position;
    }
}