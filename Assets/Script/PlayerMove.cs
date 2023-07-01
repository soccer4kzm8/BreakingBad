using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private Rigidbody _bodyRigidbody;
    #endregion SerializeField
    #region private�ϐ�
    /// <summary>
    /// ��������
    /// </summary>
    private float _moveSpeed = 5f;

    /// <summary>
    /// ��]���x
    /// </summary>
    private float _rotationSpeed = 360f;

    private IInputEventProviders _playerInput;

    private GameObject _currentItem;

    private bool _isHoldingItem;
    #endregion private�ϐ�

    private void Start()
    {
        _playerInput = new InputEventProviderImpl();
    }

    private void FixedUpdate()
    {
        // �L�[�̓��͂��擾
        float horizontalInput = _playerInput.GetHorizontalInput();
        float verticalInput = _playerInput.GetVerticalInput();
        bool catchAndReleaseInput = _playerInput.GetCatchAndReleaseInput();

        Move(horizontalInput, verticalInput);

        CatchAndReleaseItem(catchAndReleaseInput);
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

        if(_isHoldingItem == true)
        {
            ReleaseItem();
        }
        else
        {
            CatchItem();
        }
    }
    /// <summary>
    /// �A�C�e�����E��
    /// </summary>
    private void CatchItem()
    {
        // �߂��ɂ���A�C�e�����擾
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f) == false) return;

        if (hit.collider.CompareTag("Item") == false) return;

        //Debug.DrawRay(transform.position, transform.forward.normalized, Color.cyan);
        _isHoldingItem = true;
        _currentItem = hit.collider.gameObject;
        _currentItem.transform.SetParent(transform);
        _currentItem.transform.localPosition = new Vector3(0f, 1f, 1f);
        var rigidBody = _currentItem.GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.isKinematic = true;
    }

    /// <summary>
    /// �A�C�e�������
    /// </summary>
    private void ReleaseItem()
    {
        // �A�C�e���𗣂�����
        _isHoldingItem = false;
        _currentItem.transform.SetParent(null);
        var rigidBody = _currentItem.GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        rigidBody.isKinematic = false;
        _currentItem = null;
    }
}
