using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotPlayer : Character
{
    private NavMeshAgent agent;

    private int brickCount = 0;
    private GameObject bridge;

    private void Awake()
    {
        playerColor = transform.GetComponent<MeshRenderer>().material.color;
        agent = GetComponent<NavMeshAgent>();
    }

    public override void OnInit()
    {
        base.OnInit();
        bricks = new List<GameObject>();
    }

    private void Update()
    {
        Control();
        if (brickCount == 0)
        {
            bridge = null;
        }
    }

    public GameObject FindClosestBrick()
    {
        GameObject[] bricks;
        bricks = GameObject.FindGameObjectsWithTag("Brick");
        GameObject closestSameColor = null;
        GameObject closestDifferentColor = null;
        float distanceSameColor = Mathf.Infinity;
        float distanceDifferentColor = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in bricks)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (go.GetComponent<Brick>().cubeColor == playerColor)
            {
                if (curDistance < distanceSameColor)
                {
                    closestSameColor = go;
                    distanceSameColor = curDistance;
                }
            }
            else
            {
                if (curDistance < distanceDifferentColor)
                {
                    closestDifferentColor = go;
                    distanceDifferentColor = curDistance;
                }
            }
        }
        return closestSameColor != null ? closestSameColor : closestDifferentColor;
    }

    public override void Control()
    {
        if (bridge == null)
        {
            GameObject closestBrick = FindClosestBrick();
            if (closestBrick != null)
            {
                agent.SetDestination(closestBrick.transform.position);
            }
        }
        else
        {
            // Lấy vị trí của điểm cuối cầu
            Vector3 endPoint = bridge.transform.GetChild(0).position;

            // Kiểm tra xem bot có đang ở gần cầu không
            float distanceToEndPoint = Vector3.Distance(transform.position, endPoint);
            if (distanceToEndPoint < 2f)
            {
                // Nếu bot đang ở gần cầu, hãy di chuyển theo hướng song song với cầu
                Vector3 directionParallelToBridge = Vector3.Cross(bridge.transform.up, Vector3.up).normalized;
                agent.SetDestination(transform.position + directionParallelToBridge);
            }
            else
            {
                // Nếu bot không ở gần cầu, hãy tiếp tục di chuyển đến điểm cuối cầu
                agent.SetDestination(endPoint);
            }
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
            brickCount = 0;
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
            UIManager.Instance.OnLose();
        }
    }

    private void AddBrick(GameObject brick)
    {
        GameObject go = Instantiate(brick, StackPoint.transform.position + new Vector3(0, x, 0), transform.rotation);
        go.GetComponent<Collider>().enabled = false;
        go.transform.SetParent(StackPoint.transform);
        bricks.Add(go);
        go.tag = "Player";
        x += 0.3f;

        brickCount++;
        if (brickCount == 7)
        {
            bridge = GameObject.FindGameObjectWithTag("Bridge");
        }
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
            brickCount--;
        }
    }
}
