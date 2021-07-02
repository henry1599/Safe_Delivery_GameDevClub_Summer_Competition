using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCorona : MonoBehaviour
{
    public Vector2 posLeft;
    public Vector2 posRight;
    public GameObject corona;
    private static SpawnCorona instance;
    public static SpawnCorona Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
    }
    public void Spawn()
    {
        float x = Random.Range(posLeft.x, posRight.x);
        float y = posRight.y;
        Instantiate(corona, new Vector2(x, y), Quaternion.identity);
    }

    public IEnumerator SpawnCoroutine()
    {
        Spawn();
        yield return new WaitForSecondsRealtime(1f);
    }
}
