using System;
using Fusion;
using UnityEngine;



public class PlayerAnimator : NetworkBehaviour
{

    public delegate void PlayerAnimatorDelegate(int index, int weight);
    public static PlayerAnimatorDelegate        OnSetLayerMaskWeight;

    [SerializeField] private PlayerManager      _playerManager;

    [SerializeField] private Animator           _animator;

    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");

    #region Monobehaviour callbacks
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        OnSetLayerMaskWeight += SetLayerMaskWeight;
    }

    private void OnDisable()
    {
        OnSetLayerMaskWeight -= SetLayerMaskWeight;
    }
    #endregion

    #region Network callbacks
    public override void Render()
    {
        if(!Object.HasInputAuthority && !Object.HasStateAuthority)
            return;
        MoveAnimation(_playerManager._currentDirection);
    }
    #endregion

    void MoveAnimation(Vector2 moveDirection){
         
        _animator.SetFloat(X, moveDirection.x);
        _animator.SetFloat(Y, moveDirection.y);
        
    }

    void SetLayerMaskWeight(int index, int weight)
    {
        _animator.SetLayerWeight(index, weight);
        Debug.Log($"{nameof(SetLayerMaskWeight)} index {index} weight {weight}");
    }
}
