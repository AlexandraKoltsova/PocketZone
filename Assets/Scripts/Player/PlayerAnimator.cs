using System;
using Logic.Animation;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Move = Animator.StringToHash("Run");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int Win = Animator.StringToHash("Win");

        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _attackStateHash = Animator.StringToHash("attack");
        private readonly int _runningStateHash = Animator.StringToHash("run");
        private readonly int _deathStateHash = Animator.StringToHash("die");

        private Animator _animator;
        private Rigidbody2D _rb;
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }

        public void Init(Animator animator, Rigidbody2D rb)
        {
            _animator = animator;
            _rb = rb;
        }
        
        public void Tick()
        {
            _animator.SetFloat(Move, _rb.velocity.magnitude, 0.1f, Time.deltaTime);
        }
        
        public void PlayHit() => _animator.SetTrigger(Hit);
        public void PlayDeath() => _animator.SetTrigger(Die);
        public void PlayWin() => _animator.SetTrigger(Win);
        public void PlayAttack() => _animator.SetTrigger(Attack);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _runningStateHash)
                state = AnimatorState.Run;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}