using System;
using System.Collections;
using System.Collections.Generic;

namespace FlatBuffers
{
    using ObjectType = Int64;

    public class FlatBufferLongEnumerable : IEnumerable<ObjectType>, IEnumerator<ObjectType>
    {
        private int _pos;
        private readonly int _end;
        private readonly ByteBuffer _buffer;
        private const int Size = sizeof(ObjectType);

        public FlatBufferLongEnumerable(int pos, int length, ByteBuffer buffer)
        {
            _pos = pos;
            _end = pos + Size * length;
            _buffer = buffer;
        }

        public IEnumerator<ObjectType> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ObjectType Current { get; private set; }

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
            Current = _buffer.GetLong(_pos);
            _pos += Size;
            return true;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }
}
