using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using UnityEngine;

public abstract class MovingObject1 : MonoBehaviour
{
    public float moveTime = 0.1f;
    public LayerMask blockingLayer;

    // private objects
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
    {
        // transform.position is a Vector3, but we are casting it as a; Vector2, which discards the z axis data
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        // disable box collider so we don't hit our own collider with the Ray
        boxCollider.enabled = false;
        // check collision on Blocking Layer
        hit = Physics2D.Linecast(start, end, blockingLayer);
        // now re-enable the box collider
        boxCollider.enabled = true;

        // now check to see if anything was hit
        if(hit.transform == null)
        {
            StartCoroutine(SmoothMovement (end));
            return true;
        }

        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        // using sqrMagnitude is computationally cheaper than Magnitude
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        // while loop to check that our square remaining distance is greater
        while(sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            // calculate that remaining distance now that we've moved
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            // wait for a frame before evaluating the condition of the loop
            yield return null;
        }
    }
    
    protected virtual void AttemptMove <T> (int xDir, int yDir)
        where T: Component
    {
        RaycastHit2D hit;
        bool CanMove = Move (xDir, yDir, out hit);

        if (hit.transform == null)
        {
            return;
        }

        T hitComponent = hit.transform.GetComponent<T>();

        if(!CanMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    protected abstract void OnCantMove<T>(T Component)
        where T : Component;
}
