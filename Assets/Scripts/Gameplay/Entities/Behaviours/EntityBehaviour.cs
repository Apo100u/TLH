using UnityEngine;

namespace TLH.Gameplay.Entities.Behaviours
{
    public abstract class EntityBehaviour : MonoBehaviour
    {
        protected Entity entity;

        protected virtual void Awake()
        {
            entity = GetComponent<Entity>();
        }
    }
}