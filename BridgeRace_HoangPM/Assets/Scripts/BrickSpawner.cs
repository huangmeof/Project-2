using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : GameManager
{
    public Brick[] brickPrefabs;
    public int poolSize;
    private List<Brick> brickPool = new List<Brick>();
    private float brickSpacing = 2.0f;
    private List<Vector3> positions = new List<Vector3>();
    private int bricksPerRow = 10;
    private int currentLayer = 0;

    public Vector3 spawnAreaCenter;

    public override void OnInit()
    {
        base.OnInit();
        positions = new List<Vector3>(); // Khởi tạo danh sách positions ở đây
        float offsetX = spawnAreaCenter.x - brickSpacing * (bricksPerRow - 1) / 2.0f;
        float offsetZ = spawnAreaCenter.z - brickSpacing * (poolSize / bricksPerRow - 1) / 2.0f;
        for (int i = 0; i < poolSize; i++)
        {
            positions.Add(new Vector3((i % bricksPerRow) * brickSpacing + offsetX, spawnAreaCenter.y + 0.5f + currentLayer, (i / bricksPerRow) * brickSpacing + offsetZ));
        }

        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(0, positions.Count);
            Vector3 randomPosition = positions[randomIndex];
            positions.RemoveAt(randomIndex);
            Brick brick = brickPrefabs[i % brickPrefabs.Length];
            Brick _newBrick = SpawnBrick(brick, randomPosition);
        }
    }

    private void Update()
    {
        RespawnBrick();
    }

    public Brick SpawnBrick(Brick _brick, Vector3 position)
    {
        if (brickPool.Count > 0)
        {
            foreach (Brick brick in brickPool)
            {
                if (brick.id == _brick.id && !brick.gameObject.activeSelf)
                {
                    return brick;
                }
            }
        }
        Brick instance = Instantiate(_brick, position, Quaternion.identity);
        instance.gameObject.SetActive(true);
        brickPool.Add(instance);
        return instance;
    }

    public void RespawnBrick()
    {
        foreach (Brick brick in brickPool)
        {
            if (!brick.gameObject.activeSelf)
            {
                brick.SetTime();
                if (brick.timer >= brick.time)
                {
                    brick.gameObject.SetActive(true);
                    if (positions.Count > 0)
                    {
                        int randomIndex = Random.Range(0, positions.Count);
                        Vector3 randomPosition = positions[randomIndex];
                        brick.SetPos(randomPosition);
                        positions.RemoveAt(randomIndex);
                    }
                    brick.timer = 0;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentLayer++;
            OnInit();
        }
    }
}
