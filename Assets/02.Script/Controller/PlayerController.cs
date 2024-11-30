using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using Input = UnityEngine.Input;

public class PlayerController : SingletonMonobehaviour<PlayerController>
{
    [SerializeField]
    float _speed = 5f;

    public float _searchRange = 5f;
    public float _viewAngle = 45f;
    public int _resolution = 36;
    Rigidbody _rigid;

    Transform _target;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    void Init()
    {
        _rigid = GetComponent<Rigidbody>();
    }
  
    void Update()
    {
        Vector3 startPosition = transform.position;
        DrawCircle(startPosition, _searchRange);
        DrawFieldOfView(startPosition);

        KeyboardInput();
    }

    void KeyboardInput()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            }
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
           EventManager.Instance.CallSpawnMonsterEvent();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(CoEndPlay());
        }
    }
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 move = new Vector3(horizontal * _speed, 0, vertical * _speed) * Time.deltaTime;
        _rigid.MovePosition(_rigid.position + move);

        Vector3 point = transform.position;  
        Vector3 size = new Vector3(_searchRange, _searchRange, _searchRange);
        
    }

    IEnumerator CoEndPlay()
    {
        EventManager.Instance.CallGameOverEvent();
        yield return new WaitForSeconds(0.3f);

        SceneManagerEx.Instance.LoadScene(Define.Scene.Scene3_End);

    }
    #region Debug

    private void DrawCircle(Vector3 center, float radius)
    {
        int resolution = 36;
        for (int i = 0; i < resolution; i++)
        {
            float angleA = (360f / resolution) * i;
            float angleB = (360f / resolution) * (i + 1);

            Vector3 pointA = center + DirFromAngle(angleA, true) * radius;
            Vector3 pointB = center + DirFromAngle(angleB, true) * radius;

            Debug.DrawLine(pointA, pointB, Color.green);
        }
    }

    private void DrawFieldOfView(Vector3 startPosition)
    {
        Vector3 viewAngleA = DirFromAngle(-_viewAngle / 2, false) * _searchRange;
        Vector3 viewAngleB = DirFromAngle(_viewAngle / 2, false) * _searchRange;

        Debug.DrawLine(startPosition, startPosition + viewAngleA, Color.white);
        Debug.DrawLine(startPosition, startPosition + viewAngleB, Color.white);

        int resolution = 20;
        for (int i = 0; i < resolution; i++)
        {
            float angleA = -_viewAngle / 2 + (_viewAngle / resolution) * i;
            float angleB = -_viewAngle / 2 + (_viewAngle / resolution) * (i + 1);

            Vector3 pointA = startPosition + DirFromAngle(angleA, false) * _searchRange;
            Vector3 pointB = startPosition + DirFromAngle(angleB, false) * _searchRange;

            Debug.DrawLine(pointA, pointB, Color.cyan);
        }
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool isGlobal)
    {
        if (!isGlobal)
        {
            angleInDegrees += transform.eulerAngles.y; // Y축 기준 회전
        }
        return new Vector3(
            Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            0,
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)
        );
    }
    #endregion
}
