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
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ColorChanger _colorChanger;

    private float _lifeTime;
    private bool _isHitted = false;

    public event Action<Cube> Hitted;

    private void Start()
    {
        _lifeTime = CalculateLifeTime();
        if (TryGetComponent<Renderer>(out var renderer))
        {
            _colorChanger.ResetColor(renderer);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isHitted == false)
        {
            HitOnPlane();
            _isHitted = true;
        }
    }

    public void ResetCube()
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            _colorChanger.ResetColor(renderer);
        }

        _lifeTime = CalculateLifeTime();
        _isHitted = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void HitOnPlane()
    {
        if (TryGetComponent<Renderer>(out var renderer))
        {
            _colorChanger.ChangeColor(renderer);
        }

        StartCoroutine(WaitRoutine());
    }

    private IEnumerator WaitRoutine()
    {
        yield return new WaitForSeconds(_lifeTime);
        Hitted?.Invoke(this);
    }

    private float CalculateLifeTime()
    {
        return UnityEngine.Random.Range(_cubeMinLifeTime, _cubeMaxLifeTime);
    }
}
