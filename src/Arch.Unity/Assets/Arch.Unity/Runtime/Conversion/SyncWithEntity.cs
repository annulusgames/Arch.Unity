using System.Runtime.CompilerServices;
using UnityEngine;
using Arch.Core;

namespace Arch.Unity.Conversion
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    public sealed class SyncWithEntity : MonoBehaviour
    {
        internal World World { get; set; }
        internal Entity Entity { get; set; }
        internal bool UseDisabledComponent { get; set; }

        void OnEnable()
        {
            if (!IsEntityAlive()) return;

            if (UseDisabledComponent && World.Has<GameObjectDisabled>(Entity))
            {
                World.Remove<GameObjectDisabled>(Entity);
            }
        }

        void OnDisable()
        {
            if (!IsEntityAlive()) return;

            if (UseDisabledComponent && !World.Has<GameObjectDisabled>(Entity))
            {
                World.Add<GameObjectDisabled>(Entity);
            }
        }

        void OnDestroy()
        {
            if (IsEntityAlive())
            {
                World.Destroy(Entity);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity GetEntityReference()
        {
            return Entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEntityAlive()
        {
            if (World == null) return false;
            return World.IsAlive(Entity);
        }
    }
}