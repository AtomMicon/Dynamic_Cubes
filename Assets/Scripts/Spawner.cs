using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Clicker _clicker;
    [SerializeField] private Cube _cube;
    [SerializeField] private float _xArea;
    [SerializeField] private float _zArea;
    [SerializeField] private float _yHeight;
    [SerializeField] private int _poolCapasity = 8;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => InstantiateCube(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapasity,
            maxSize: _poolMaxSize
            );
    }

    private void OnEnable()
    {
        _clicker.Clicked += SpawnCube;
        _cube.Hitted += ReturnCubeToPool;
    }

    private void OnDisable()
    {
        _clicker.Clicked -= SpawnCube;
        _cube.Hitted -= ReturnCubeToPool;
    }

    private void SpawnCube()
    {
        Debug.Log("SpawnCube");
        GameObject cube = _pool.Get();
        InstantiateCube(cube);
    }

    private void ReturnCubeToPool(GameObject cube)
    {
        cube.SetActive(false);
        _pool.Release(cube);
    }

    private void InstantiateCube(GameObject cube)
    {
        cube.transform.position = GetSpawnPosition();
    }

    private Vector3 GetSpawnPosition()
    {
        float xPosition = Random.Range(-_xArea, _xArea);
        float zPosition = Random.Range(-_zArea, _zArea);
        return new Vector3(xPosition, _yHeight, zPosition);
    }
}
