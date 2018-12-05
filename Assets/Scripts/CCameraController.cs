using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraController : MonoBehaviour
{
    // 줌인 줌아웃 제한
    // 마우스 휠 스크롤

    private Vector3 _mouseOriginPoint = Vector3.zero;
    //private Vector3 _offset = Vector3.zero;
    private Vector3 _delta = Vector3.zero;

    private bool _isDragging = false;

    private Camera _camera = null;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        if (_camera == null)
        {
            Debug.Log("####CCameraController::Awake : 카메라가 존재하지 않습니다.");
            _camera = Camera.main;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_camera == null)
        {
            return;
        }

        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") *
            (10.0f * _camera.orthographicSize * 0.1f), 2.5f, 50.0f);

        if (Input.GetMouseButtonDown(2))
        {
            _isDragging = true;
            // 드래그 시작시에 처음 클릭한 마우스의 월드 포인트 위치를 기억
            _mouseOriginPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

            //Debug.Log(_offset + " " + _mouseOriginPoint);
        }
        else if (Input.GetMouseButtonUp(2))
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            // 마우스의 월드 포인트 지점에서 카메라 까지의 거리를 구함
            //_offset = (_camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position);
            //_camera.transform.position = _mouseOriginPoint - _offset;

            // 마우스의 월드 포인트 변화값을 구하여 카메라를 이동
            _delta = (_mouseOriginPoint - _camera.ScreenToWorldPoint(Input.mousePosition));
            _camera.transform.position += _delta;
        }
    }
}
