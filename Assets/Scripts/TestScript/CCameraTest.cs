using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraTest : MonoBehaviour
{
    private Camera _camera = null;

    [SerializeField]
    private GameObject _prefab = null;

    [Range(0.0f, 150.0f)]
    public float farZ = 1.0f;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 createPosition = _camera.ScreenToWorldPoint(Input.mousePosition - (Vector3.forward * (_camera.transform.position.z)) + (Vector3.forward * farZ));
            Instantiate(_prefab, createPosition, Quaternion.identity);
        }
    }
}
