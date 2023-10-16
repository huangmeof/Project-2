using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    [SerializeField] protected float speed;
    public Color playerColor;
    public int id;
    protected float x = 0;
    public GameObject StackPoint;
    public List<GameObject> bricks;

    public virtual void OnInit()
    {
        bricks = new List<GameObject>();
    }

    public virtual void Control()
    {
        
    }
}