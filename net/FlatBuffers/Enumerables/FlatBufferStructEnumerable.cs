using System;
using System.Collections;
using System.Collections.Generic;

namespace FlatBuffers
{
    public class FlatBufferStructEnumerable<T> : IEnumerable<T>, IEnumerator<T>
        where T : Struct, new()
    {
        private int _pos;
        private readonly int _end;
        private readonly int _size;
        private readonly ByteBuffer _buffer;

        public FlatBufferStructEnumerable(int pos, int length, int size, ByteBuffer buffer)
        {
            _pos = pos;
            _end = pos + size * length;
            _size = size;
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
            _pos += _size;
            return true;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }
}
