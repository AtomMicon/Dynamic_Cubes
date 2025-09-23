using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _xArea;
    [SerializeField] private float _zArea;
    [SerializeField] private float _yHeight;
    [SerializeField] private float _cubeMinLifeTime = 2f;
    [SerializeField] private float _cubeMaxLifeTime = 5f;
    [SerializeField] private int _poolCapasity = 8;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => SpawnCube(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapasity,
            maxSize: _poolMaxSize
            );
    }


    private void SpawnCube(GameObject cube)
    {
        cube.transform.position = GetSpawnPosition();
        InstantiateComponents(cube);
    }

    private Vector3 GetSpawnPosition()
    {
        float xPosition = Random.Range(-_xArea, _xArea);
        float zPosition = Random.Range(-_zArea, _zArea);
        return new Vector3(xPosition, _yHeight, zPosition);
    }

    private float GetLifeTime()
    {
        return Random.Range(_cubeMinLifeTime, _cubeMaxLifeTime);
    }

    private void InstantiateComponents(GameObject cube)
    {
        cube.AddComponent<Rigidbody>();
        cube.AddComponent<BoxCollider>();
        cube.AddComponent<MeshRenderer>();
    }
}
