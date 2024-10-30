using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundChecker : MonoBehaviour
{
    public string groundTag = "Ground";
    private readonly Dictionary<int, Collider2D> grounds = new();
    public bool IsOnGround => grounds.Count > 0;

    private Collider2D groundCheckCollider;

    private void TryAddGround(Collider2D collision)
    {
        if (!collision.CompareTag(groundTag)) return;

        if (!grounds.ContainsKey(collision.gameObject.GetInstanceID()))
        {
            grounds.Add(collision.gameObject.GetInstanceID(), collision);
        }
    }

    private void TryRemoveGround(Collider2D collision)
    {
        if (!collision.CompareTag(groundTag)) return;

        if (grounds.ContainsKey(collision.gameObject.GetInstanceID()))
        {
            grounds.Remove(collision.gameObject.GetInstanceID());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryAddGround(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TryRemoveGround(collision);
    }

    private void Awake()
    {
        // Getting Collider of Ground Checker
        groundCheckCollider = GetComponent<Collider2D>();

        // Check if there is already collision with ground
        List<Collider2D> colliders = new();
        if (Physics2D.OverlapCollider(groundCheckCollider, colliders) != 0)
        {
            foreach (Collider2D collider2 in colliders)
            {
                TryAddGround(collider2);
            }
        }
    }

    void Update()
    {
        // Check if there is magicaly no collision with ground
        /*List<Collider2D> toRemove = new(); // To avoid messing up with foreach
        foreach (var ground in grounds)
        {
            List<ContactPoint2D> contactPoints = new();
            if (Physics2D.GetContacts(groundCheckCollider, ground.Value, new ContactFilter2D(), contactPoints) == 0)
            {
                toRemove.Add(ground.Value);
            }
        }
        // Removing Grounds
        foreach (var ground in toRemove)
        {
            TryRemoveGround(ground);
        }*/
    }
}
