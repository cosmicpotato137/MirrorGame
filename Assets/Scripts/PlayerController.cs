using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float jumpHeight = 10;
    public float rotationSpeed = 20;

    bool moving;
    Vector2 target;
    BlockState currentBlockState;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    Vector3 velocity = new Vector3(0, rb.velocity.y, 0);
    //    bool grounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f,
    //        LayerMask.GetMask(new string[] { "LightInteractable", "Default" }));
    //    Debug.DrawLine(transform.position, transform.position + Vector3.down * .6f, Color.red);
    //    if (Input.GetKeyDown(KeyCode.W) && grounded)
    //    {
    //        velocity += Vector3.up * speed;
    //    }
    //    else if (Input.GetKey(KeyCode.S))
    //    {
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        velocity += Vector3.left * speed;
    //    }
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        velocity += Vector3.right * speed;
    //    }

    //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector3 perpendicular = mousePos - transform.position;
    //    transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

    //    rb.velocity = Vector3.Normalize(velocity);
    //}

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (moving)
        {
            float spd = currentBlockState == BlockState.Pushable ? speed / 2 : speed;
            transform.position = Vector2.Lerp(transform.position, target, spd * Time.deltaTime);
            if (Mathf.Abs(Vector2.Distance(transform.position, target)) < .001)
            {
                transform.position = target;
                moving = false;
            }
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            currentBlockState = CheckBlock(Vector2.up);
            if (currentBlockState == BlockState.Blocked)
                return;

            target = (Vector2)transform.position + Vector2.up;
            moving = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentBlockState = CheckBlock(Vector2.down);
            if (currentBlockState == BlockState.Blocked)
                return;
            target = (Vector2)transform.position + Vector2.down;
            moving = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentBlockState = CheckBlock(Vector2.left);
            if (currentBlockState == BlockState.Blocked)
                return;
            target = (Vector2)transform.position + Vector2.left;
            moving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentBlockState = CheckBlock(Vector2.right);
            if (currentBlockState == BlockState.Blocked)
                return;
            target = (Vector2)transform.position + Vector2.right;
            moving = true;
        }
        else
            moving = false;
    }

    private enum BlockState
    {
        Clear, Blocked, Pushable
    }

    private BlockState CheckBlock(Vector3 direction)
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + direction, direction, 0);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + direction * 2, direction, 0);

        if (!hit1)
            return BlockState.Clear;
        else if (hit1.collider.gameObject.tag == "Pushable")
        {
            if (!hit2)
            {
                hit1.collider.gameObject.GetComponent<PushableObject>().OnPush(direction, speed / 2);
                return BlockState.Pushable;
            }
            else
                return BlockState.Blocked;
        }
        else if (hit1.collider.gameObject.tag == "Wall")
            return BlockState.Blocked;
        else
            return BlockState.Blocked;
    }
}
