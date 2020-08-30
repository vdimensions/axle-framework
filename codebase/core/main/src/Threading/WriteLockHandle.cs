﻿using System.Diagnostics;

namespace Axle.Threading
{
    internal struct WriteLockHandle : ILockHandle
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IReadWriteLock _readWriteLock;

        internal WriteLockHandle(IReadWriteLock readWriteLock)
        {
            _readWriteLock = readWriteLock;
            _readWriteLock.EnterWriteLock();
        }

        public void Dispose()
        {
            if (_readWriteLock == null)
            {
                return;
            }
            _readWriteLock.ExitWriteLock();
            _readWriteLock = null;
        }
    }
}