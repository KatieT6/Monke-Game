using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float lifeTime;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check layers
        Destroy(gameObject);
    }
}
