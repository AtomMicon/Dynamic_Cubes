using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Clicker _clicker;
    [SerializeField] private float _xArea;
    [SerializeField] private float _zArea;
    [SerializeField] private float _yHeight;
    [SerializeField] private int _poolCapasity = 8;
    [SerializeField] private int _poolMaxSize = 10;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cube),
            actionOnGet: (@object) => InstantiateCube(@object),
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
        Cube cube = _pool.Get();
        InstantiateCube(cube);
    }

    private void ReturnCubeToPool(GameObject gameObject)
    {
        Cube cube = gameObject.GetComponent<Cube>();

        if (cube != null)
        {
            Debug.Log("ReturnCubeToPool");
            _pool.Release(cube);
        }
    }

    private void InstantiateCube(Cube cube)
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
