﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace AdbClient
{
    internal static class StreamExtensions
    {
        internal static async Task ReadExact(this Stream stream, Memory<byte> memory, CancellationToken cancellationToken)
        {
            for (int i = 0; i < memory.Length;)
            {
                i += await stream.ReadAsync(memory.Slice(i), cancellationToken);
            }
        }

        internal static async Task<uint> ReadUInt32(this Stream stream, CancellationToken cancellationToken)
        {
            var buffer = new byte[4];
            await stream.ReadExact(buffer.AsMemory(), cancellationToken);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(buffer);
            }
            return BitConverter.ToUInt32(buffer);
        }
        internal static async Task<ulong> ReadUInt64(this Stream stream, CancellationToken cancellationToken)
        {
            var buffer = new byte[8];
            await stream.ReadExact(buffer.AsMemory(), cancellationToken);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(buffer);
            }
            return BitConverter.ToUInt64(buffer);
        }
        internal static async Task<long> ReadInt64(this Stream stream, CancellationToken cancellationToken)
        {
            var buffer = new byte[8];
            await stream.ReadExact(buffer.AsMemory(), cancellationToken);
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(buffer);
            }
            return BitConverter.ToInt64(buffer);
        }
    }
}
