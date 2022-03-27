using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BocciaPlayer
{
    public class PlayerMoveState : IPlayerState
    {
        private const float SPEED = 0.6f;
        private NetworkSendManagerScript netSendManager = null;
        private PlayerMoveButton moveButton = null;
        private PlayerMoveScript playerMove = null;
        private Transform playerTransform = null;
        private Vector3 moveSpeed = Vector3.zero;       //移動速度。
        override public void Init(PlayerController controller)
        {
            m_player = controller;
            moveButton = PlayerMoveButton.GetInstance();
            if (m_player)
            {
                playerMove = m_player.GetPlayerMoveScript();
                playerMove.SetIsMove(false);
                playerTransform = m_player.gameObject.transform;
                netSendManager = m_player.GetNetSendManager();
            }
        }
        override public void Enter()
        {
            playerMove.SetIsMove(true);
        }
        override public void Leave()
        {
            playerMove.SetIsMove(false);
            playerMove.PlayerMove(Vector3.zero);
        }
        override public void Execute()
        {
            moveSpeed = Vector3.zero;
            if(moveButton.IsPressAnyButton() && moveButton.IsActive())
            {
                playerMove.SetIsMove(true);
                Vector3 zMoveSpeed = Vector3.zero;
                Vector3 xMoveSpeed = Vector3.zero;
                if(moveButton.IsPressForward())
                {
                    zMoveSpeed += playerTransform.forward;
                }
                if(moveButton.IsPressBack())
                {
                    zMoveSpeed -= playerTransform.forward;
                }
                if(moveButton.IsPressRight())
                {
                    xMoveSpeed += playerTransform.right;
                }
                if(moveButton.IsPressLeft())
                {
                    xMoveSpeed -= playerTransform.right;
                }
                //y成分削除。
                zMoveSpeed.y = 0.0f;
                xMoveSpeed.y = 0.0f;

                //移動速度計算。
                moveSpeed = zMoveSpeed.normalized + xMoveSpeed.normalized;
                moveSpeed *= SPEED;
            }
            else
            {
                //待機ステートに変更。
                m_player.ChangeState(EnPlayerState.enIdle);
            }
            playerMove.PlayerMove(moveSpeed);
        }

        //有効になっているか？
        override public bool IsEnable()
        {
            return true;
        }
        //処理を止めているか？
        override public bool IsStop()
        {
            return false;
        }

    }
}
