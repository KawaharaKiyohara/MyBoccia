using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BocciaPlayer
{
    public abstract class IPlayerState
    {
        protected PlayerController m_player;

        abstract public void Init(PlayerController controller);
        abstract public void Enter();
        abstract public void Leave();
        abstract public void Execute();

        //有効になっているか？
        abstract public bool IsEnable();
        //処理を止めているか？
        abstract public bool IsStop();

    }
}