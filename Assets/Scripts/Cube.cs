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

    public event Action<Cube> Hitted;

    private float _lifeTime;
    private Renderer _renderer;
    private bool _isHitted = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        _lifeTime = GetLifeTime();
        _renderer.material.color = Color.white;
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
        _isHitted = false;
        _renderer.material.color = Color.white;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void HitOnPlane()
    {
        ChangeColor();

        Debug.Log("HitOnPlane");
        
        StartCoroutine(WaitRoutine());

        Debug.Log("Hitted event invoked");
    }

    IEnumerator WaitRoutine()
    {
        Debug.Log("WaitRoutine started: " + _lifeTime);
        yield return new WaitForSeconds(_lifeTime);
        Debug.Log("WaitRoutine ended");

        Hitted?.Invoke(this);
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
