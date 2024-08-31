using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] float lifeTime = 0f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
