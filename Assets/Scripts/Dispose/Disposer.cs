using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dispose
{
    public class Disposer : MonoBehaviour
    {
        private List<IDisposable> _disposeItems = new List<IDisposable>();

        public void Add(IDisposable item)
        {
            _disposeItems.Add(item);
        }

        private void OnDestroy()
        {
            for (int i = 0, len = _disposeItems.Count; i < len; ++i)
            {
                _disposeItems[i].Dispose();
            }
        }
    }
}