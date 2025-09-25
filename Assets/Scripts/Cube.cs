using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private float _cubeMinLifeTime = 2f;
    [SerializeField] private float _cubeMaxLifeTime = 5f;
    [SerializeField] private int _hitMaxCount = 1;

    public event Action<GameObject> Hitted;

    private float _lifeTime;
    private Renderer _renderer;
    private int _hitCount;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        _lifeTime = GetLifeTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hitCount < _hitMaxCount)
        {
            HitOnPlane();
            _hitCount++;
        }
    }

    private void HitOnPlane()
    {
        ChangeColor();

        Debug.Log("HitOnPlane");
        
        StartCoroutine(WaitRoutine());
        Hitted?.Invoke(this.gameObject);

        Debug.Log("Hitted event invoked");
    }

    IEnumerator WaitRoutine()
    {
        Debug.Log("WaitRoutine started: " + _lifeTime);
        yield return new WaitForSeconds(_lifeTime);
        Debug.Log("WaitRoutine ended");
    }

    private float GetLifeTime()
    {
        return UnityEngine.Random.Range(_cubeMinLifeTime, _cubeMaxLifeTime);
    }

    private void ChangeColor()
    {
        Color color = UnityEngine.Random.ColorHSV();
        _renderer.material.color = color;
    }
}
