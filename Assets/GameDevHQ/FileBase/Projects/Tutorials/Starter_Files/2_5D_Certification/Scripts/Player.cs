using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravity = 1.0f;

    private Vector3 _direction;
    private CharacterController _controller;
    private Animator _anim;
    private bool _jumping = false;
    private bool _onLedge = false;

    private Ledge _activeLedge;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (_onLedge)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("ClimbUp");
            }
        }
    }

    #endregion

    #region Supporting Functions

    private void CalculateMovement()
    {
        if (_controller.isGrounded)
        {
            if (_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jumping", _jumping);
            }

            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, horizontalMovement) * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(horizontalMovement));

            if (horizontalMovement > 0)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else if (horizontalMovement < 0)
                transform.eulerAngles = new Vector3(0, 180, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jumping", _jumping);
            }
        }

        _direction.y -= _gravity * Time.deltaTime;
        _controller.Move(_direction * Time.deltaTime);
    }

    /// <summary>
    /// Grab a ledge
    /// </summary>
    public void GrabLedge(Ledge currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetBool("Jumping", false);
        _anim.SetFloat("Speed", 0.0f);
        transform.position = currentLedge.LedgePosition;
        _onLedge = true;
        _activeLedge = currentLedge;
    }

    /// <summary>
    /// Climb up
    /// </summary>
    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.StandPosition;
        _anim.SetBool("GrabLedge", false);
        _controller.enabled = true;
    }

    #endregion
}
