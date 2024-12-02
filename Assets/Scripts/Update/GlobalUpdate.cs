using System.Collections.Generic;
using UnityEngine;

namespace Update
{
    public class GlobalUpdate : MonoBehaviour
    {
        private List<IUpdatable> _updatableObjects = new List<IUpdatable>();

        public void AddUpdatableObject(IUpdatable updatableObj)
        {
            _updatableObjects.Add(updatableObj);
        }

        private void Update()
        {
            for (int i = 0, len = _updatableObjects.Count; i < len; ++i)
            {
                _updatableObjects[i].Update();
            }
        }
    }
}
