using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common;

namespace Jit64
{
    public class InstructionReader : IDisposable
    {
        private readonly Stream _source;
        private bool _disposed = false;
        private readonly bool _leaveOpen;
        private readonly StreamReader _parser;

        public Stream BaseStream => _parser.BaseStream; // is it ok to allow access?

        public InstructionReader(Stream input)
            : this(input, Encoding.UTF8) { }

        public InstructionReader(Stream input, Encoding encoding, bool leaveOpen = false)
        {
            _source = input;
            _leaveOpen = leaveOpen;
            _parser = new StreamReader(input, encoding, false, 1024, leaveOpen);
        }
        
        private void EnsureNotDisposed() 
            => ThrowHelper.ThrowDisposed(_disposed);

        public virtual string ReadInstruction()
        {
            EnsureNotDisposed();
            return _parser.ReadLine();
        }

        public void Dispose()
        {
            if (_disposed || _leaveOpen) return;

            _source?.Dispose();
            _disposed = true;
        }
    }
}
