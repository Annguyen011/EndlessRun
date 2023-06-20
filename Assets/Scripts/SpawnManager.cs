using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemPool
{
    public Item prefab;
    public List<Item> active;
    public List<Item> inactive;
    public Item Spawn(Vector3 position, Transform parent)
    {
        if (inactive.Count == 0)
        {
            Item newObj = GameObject.Instantiate(prefab);
            newObj.gameObject.SetActive(false);
            newObj.transform.parent = parent;
            inactive.Add(newObj);
            return newObj;
        }
        else
        {
            Item oldItem = inactive[0];
            oldItem.gameObject.SetActive(true);
            oldItem.transform.parent = parent;
            oldItem.transform.position = position;
            inactive.RemoveAt(0);
            active.Add(oldItem);
            return oldItem;
        }
    }
    public void Release(Item obj)
    {
        if (active.Contains(obj))
        {
            obj.gameObject.SetActive(false);
            active.Remove(obj);
            inactive.Add(obj);
        }
    }
    public void Reset()
    {
        if (active.Count > 0)
        {
            foreach (Item olditem in active)
            {
                GameObject.Destroy(olditem.gameObject);
            }
        }
        active.Clear();
        inactive.Clear();
    }
}
public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpawnManager>();
            }
            return instance;
        }
    }

    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private ItemPool[] itemPool;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private float timer;
    private float curTime;
    private int ranInt;
    private int ranInt2;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        curTime = timeBetweenSpawn;
    }
    private void FixedUpdate()
    {
        SpawnBox();
    }
    public void SpawnBox()
    {
        timer += Time.fixedDeltaTime;
        ranInt = UnityEngine.Random.Range(0, spawnPos.Length);
        ranInt2 = UnityEngine.Random.Range(0, itemPool.Length);
        if (timer <= timeBetweenSpawn) return;
        timer = 0;
        float upLevelSpeed = curTime - 0.05f;
        curTime = upLevelSpeed;
        if (curTime <= 0.5)
        {
            curTime = 0.5f;
        }

        itemPool[ranInt2].Spawn(spawnPos[ranInt].position, transform);

    }
    public void ResetItem()
    {
        itemPool[ranInt2].Reset();
    }
    public void ReleaseItem(Item obj)
    {
        itemPool[ranInt2].Release(obj);
        curTime = timeBetweenSpawn;
    }
}
