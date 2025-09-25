using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
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
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => InstantiateCube(cube),
            actionOnRelease: (cube) => OnRealize(cube),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapasity,
            maxSize: _poolMaxSize
            );
    }

    private void OnEnable()
    {
        _clicker.Clicked += GetCube;
    }

    private void OnDisable()
    {
        _clicker.Clicked -= GetCube;
    }

    private void GetCube()
    {
        Cube cube = _pool.Get();
        cube.Hitted += ReturnCube;
    }

    private void ReturnCube(Cube cube)
    {
        _pool.Release(cube);
        cube.Hitted -= ReturnCube;
    }

    private void OnRealize(Cube cube)
    {
        cube.ResetCube();
        cube.gameObject.SetActive(false);
    }

    private void InstantiateCube(Cube cube)
    {
        cube.transform.position = GetSpawnPosition();
        cube.gameObject.SetActive(true);
    }

    private Vector3 GetSpawnPosition()
    {
        float xPosition = Random.Range(-_xArea, _xArea);
        float zPosition = Random.Range(-_zArea, _zArea);
        return new Vector3(xPosition, _yHeight, zPosition);
    }
}
