using System;
using System.Collections.Generic;
using UnityEngine;

namespace _01_Scripts.Entity.EntityState
{
    public class EntityStateMachine : MonoBehaviour
    {
        public EntityStateType CurrentState => _currentEntityState.StateType;
        
        [SerializeField] private StateSo[] stateSo;
        [SerializeField] private StateSo InitEntityState;
        [SerializeField] private Entity entity;
        
        private EntityState _currentEntityState;

        private Dictionary<EntityStateType, EntityState> _states;

        private void Awake()
        {
            _states = new Dictionary<EntityStateType, EntityState>();
            
            foreach (var state in stateSo)
            {
                Type type = Type.GetType(state.className);
                
                Debug.Assert(type != null, $"{state.className} is not found");
                
                EntityState entityState = Activator.CreateInstance(type, entity, state.stateType, state.animationHash) as EntityState;
                _states[state.stateType] = entityState;
            }
            
            _currentEntityState = GetState(InitEntityState.stateType);
            _currentEntityState.Enter();
        }

        private void Update()
        {
            if (entity.IsDeath)
            {
                ChangeState(InitEntityState.stateType);
                return;
            }

            if (_currentEntityState != null)
                _currentEntityState.StateUpdate();
            else
                ChangeState(InitEntityState.stateType);
        }

        public void ChangeState(EntityStateType entityState)
        {
            _currentEntityState.Exit();
            _currentEntityState = GetState(entityState);
            _currentEntityState.Enter();
        }
        
        private EntityState GetState(EntityStateType stateType)
        {
            foreach (var state in _states)
            {
                if (state.Key == stateType)
                {
                    return state.Value;
                }
            }
            
            return null;
        }
    }
}