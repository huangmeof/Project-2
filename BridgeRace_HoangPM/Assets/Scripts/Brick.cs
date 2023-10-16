using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour, IPooledObject
{
    public Color cubeColor;

    public int id;

    public float time;

    public float timer;

    private void Awake()
    {
        cubeColor = transform.GetComponent<MeshRenderer>().material.color;
    }
    public void OnObjectSpawn()
    {
        
    }

    public void SetTime()
    {
        timer += Time.deltaTime;
    }
    
    public void SetPos(Vector3 _pos)
    {
        this.transform.position = _pos;
    }
}
