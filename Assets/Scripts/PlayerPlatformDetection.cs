using UnityEngine;

public class PlayerPlatformDetection : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "FallingPlatform")
        {
            if (player.rb.linearVelocityY <= 0)
            {
                col.gameObject.GetComponent<FallingPlatform>().StartCoroutine("Drop");
            }
        }
    }
}
