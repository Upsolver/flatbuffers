using System;
using System.Collections;
using System.Collections.Generic;

namespace FlatBuffers
{
    using ObjectType = String;

    public class FlatBufferStringEnumerable : IEnumerable<ObjectType>, IEnumerator<ObjectType>
    {
        private int _pos;
        private readonly int _end;
        private readonly ByteBuffer _buffer;
        private const int Size = sizeof(int);

        public FlatBufferStringEnumerable(int pos, int length, ByteBuffer buffer)
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
            int stringPos = _pos + _buffer.GetInt(_pos);
            var len = _buffer.GetInt(stringPos);
            Current = Table.StringEncoding.GetString(_buffer.Data, stringPos + sizeof(int), len);
            _pos += Size;
            return true;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }
}
