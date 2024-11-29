using UnityEngine;
using Input = UnityEngine.Input;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 5f;

    public float _searchRange = 5f;   
    public float _viewAngle = 45f;    
    Rigidbody _rigid;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Move();
        Jump();

        View();
    }
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 move = new Vector3(horizontal * _speed, 0, vertical * _speed) * Time.deltaTime;
        _rigid.MovePosition(_rigid.position + move);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    #region Debug

    void View()
    {

        Vector3 position = transform.position;

        Vector3 viewAngleA = DirFromAngle(-_viewAngle / 2, true);  // ���� �þ߰�
        Vector3 viewAngleB = DirFromAngle(_viewAngle / 2, true);   // ������ �þ߰�

        Debug.DrawLine(position, position + viewAngleA * _searchRange, Color.yellow);
        Debug.DrawLine(position, position + viewAngleB * _searchRange, Color.yellow);
    }

    private bool IsInFieldOfView(Vector3 directionToTarget)
    {
        float angleToTarget = Vector3.Angle(transform.right, directionToTarget);
        return angleToTarget <= _viewAngle / 2;
    }
    private Vector3 DirFromAngle(float angleInDegrees, bool isGlobal)
    {
        if (!isGlobal)
        {
            angleInDegrees += transform.eulerAngles.z; // ������Ʈ�� ���� ȸ���� �ݿ� (2D ȯ��)
        }
        float radian = angleInDegrees * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0); // 2D ����
    }

    #endregion
}
