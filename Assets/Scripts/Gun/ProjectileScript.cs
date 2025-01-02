using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float lifeTime;

    [SerializeField] GameObject DestroyAnim;

    float ms = 0f;

    void Start()
    {

    }

    void Update()
    {


        Debug.Log(ms);

        while (ms <= lifeTime)
        {
            ms += Time.deltaTime;
            return;
        }
        BulletDestroy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check layers
        BulletDestroy();
    }

    void BulletDestroy()
    {
        GameObject AnimInstance = Instantiate(DestroyAnim, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
