using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region private�ϐ�
    /// <summary>
    /// ��������
    /// </summary>
    private readonly float _moveSpeed = 5f;

    /// <summary>
    /// ��]���x
    /// </summary>
    private readonly float _rotationSpeed = 3600f;

    private Rigidbody _bodyRigidbody;

    private IInputEventProviders _playerInput;

    /// <summary>
    /// ���ݎ����Ă���A�C�e��
    /// </summary>
    private GameObject _currentItem = null;

    /// <summary>
    /// �E���グ��ꂽ�A�C�e���̈ʒu
    /// </summary>
    private readonly Vector3 _caughtItemPosition = new Vector3(0f, 1f, 1f);
    #endregion private�ϐ�

    #region �萔
    /// <summary>
    /// Raycast��maxDistance
    /// </summary>
    private const float RAYCAST_MAX_DISTANCE = 2f;
    #endregion �萔
    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
        _bodyRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // �L�[�̓��͂��擾
        bool catchAndReleaseInput = _playerInput.GetCatchAndReleaseInput();

        CatchAndReleaseItem(catchAndReleaseInput);
    }

    private void FixedUpdate()
    {
        // �L�[�̓��͂��擾
        float horizontalInput = _playerInput.GetHorizontalInput();
        float verticalInput = _playerInput.GetVerticalInput();

        Move(horizontalInput, verticalInput);
    }

    /// <summary>
    /// �ړ��Ɖ�]
    /// </summary>
    /// <param name="horizontalInput"></param>
    /// <param name="verticalInput"></param>
    private void Move(float horizontalInput, float verticalInput)
    {
        // ���̓x�N�g���𐳋K��
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        // �ړ���
        Vector3 movement = movementDirection * _moveSpeed;
        _bodyRigidbody.velocity = movement;

        // ��]����
        if (movement == Vector3.zero) return;

        Quaternion toRotation = Quaternion.LookRotation(movement);
        _bodyRigidbody.rotation = Quaternion.RotateTowards(_bodyRigidbody.rotation, toRotation, _rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// �A�C�e�����E���E����
    /// </summary>
    /// <param name="catchAndReleaseInput"></param>
    private void CatchAndReleaseItem(bool catchAndReleaseInput)
    {
        if (catchAndReleaseInput == false) return;

        if(_currentItem == null)
        {
            CatchItem();
        }
        else
        {
            ReleaseItem();
        }
    }
    /// <summary>
    /// �A�C�e�����E��
    /// </summary>
    private void CatchItem()
    {
        // �߂��ɂ���A�C�e�����擾
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, RAYCAST_MAX_DISTANCE) == false) return;

        if (hit.collider.CompareTag("Item") == false) return;

        _currentItem = hit.collider.gameObject;
        _currentItem.transform.SetParent(transform);
        _currentItem.transform.localPosition = _caughtItemPosition;
        _currentItem.GetComponent<Rigidbody>().isKinematic = true;
        _currentItem.GetComponent<Collider>().enabled = false;
    }

    /// <summary>
    /// �A�C�e�������
    /// </summary>
    private void ReleaseItem()
    {
        // �A�C�e���𗣂�����
        _currentItem.transform.SetParent(null);
        _currentItem.GetComponent<Rigidbody>().isKinematic = false;
        _currentItem.GetComponent<Collider>().enabled = true;
        _currentItem = null;
    }
}
