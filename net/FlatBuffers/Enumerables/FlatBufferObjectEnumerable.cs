using System;
using System.Collections;
using System.Collections.Generic;

namespace FlatBuffers
{
    public class FlatBufferObjectEnumerable<T> : IEnumerable<T>, IEnumerator<T>
        where T : Table, new()
    {
        private int _pos;
        private readonly int _end;
        private readonly ByteBuffer _buffer;
        private const int Size = sizeof(int);

        public FlatBufferObjectEnumerable(int pos, int length, ByteBuffer buffer)
        {
            _pos = pos;
            _end = pos + Size * length;
            _buffer = buffer;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T Current { get; private set; }

        public void Dispose()
        {
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (_pos >= _end)
            {
                return false;
            }
            Current = new T();
            Current.Initialize(_pos, _buffer);
            _pos += Size;
            return true;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }
}
