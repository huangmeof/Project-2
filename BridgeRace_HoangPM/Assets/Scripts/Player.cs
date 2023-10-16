using UnityEngine;
using System.Collections.Generic;

public class Player : Character
{
    [SerializeField] protected FixedJoystick joystick;
    [SerializeField] protected Rigidbody rb;

    private void Awake()
    {
        playerColor = transform.GetComponent<MeshRenderer>().material.color;
    }

    public override void OnInit()
    {
        bricks = new List<GameObject>();
    }

    private void Update()
    {
        Control();
    }

    public override void Control()
    {
        rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            if (other.transform.GetComponent<Brick>().cubeColor == playerColor)
            {
                AddBrick(other.gameObject);
                other.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("BridgeTile"))
        {
            if (other.gameObject.GetComponent<MeshRenderer>().material.color != playerColor)
            {
                if (bricks.Count > 0)
                {
                    DropBrick(other.gameObject);
                }
                else
                {
                    other.GetComponent<Collider>().isTrigger = false;
                }
            }
        }
        if (other.gameObject.CompareTag("BridgeEnd"))
        {
            x = 0;
            foreach (GameObject brick in bricks)
            {
                Destroy(brick);
            }
            bricks.Clear();
        }
        if (other.gameObject.CompareTag("BridgeWall"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BridgeTile"))
        {
            if (collision.gameObject.GetComponent<MeshRenderer>().material.color != playerColor)
            {
                DropBrick(collision.gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<Collider>().isTrigger = true;
            }
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            UIManager.Instance.OnVictory();
        }
    }

    private void AddBrick(GameObject brick)
    {
        GameObject go = Instantiate(brick, StackPoint.transform.position + new Vector3(0, x, 0), transform.rotation);
        go.GetComponent<Collider>().enabled = false;
        go.transform.SetParent(StackPoint.transform);
        bricks.Add(go);
        x += 0.3f;
    }

    private void DropBrick(GameObject obj)
    {
        if (bricks.Count > 0)
        {
            GameObject lastBrick = bricks[bricks.Count - 1];
            Destroy(lastBrick);
            bricks.RemoveAt(bricks.Count - 1);
            x -= 0.3f;
            obj.GetComponent<MeshRenderer>().material.color = playerColor;
            obj.GetComponent<MeshRenderer>().enabled = true;
            obj.GetComponent<Collider>().isTrigger = true;
        }
    }
}