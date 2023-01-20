using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private Vector2 _mousePosition;
    public Vector2 MousePosition { get { return _mousePosition; } private set { _mousePosition = value; } }

    public int MousePositionX { get; private set; }
    public int MousePositionY { get; private set; }

    public UnityEvent OnClickMouseButton = new UnityEvent();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePosition = Input.mousePosition;
            _mousePosition = _camera.ScreenToWorldPoint(_mousePosition);

            MousePositionX = Mathf.RoundToInt(_mousePosition.x);
            MousePositionY = Mathf.RoundToInt(_mousePosition.y);

            OnClickMouseButton.Invoke();
            //Debug.Log($"X : {MousePositionX}, Y : {MousePositionY}");
        }
    }
}