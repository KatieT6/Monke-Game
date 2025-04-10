using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    Vector2 checkpointPos;

    void Start()
    {
        checkpointPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Hurting")
        {
            Die();
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;   
    }

    void Die()
    {
        StartCoroutine(Respawn(0.1f));
    }

    IEnumerator Respawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        rb.linearVelocity = Vector2.zero;
    }

}

