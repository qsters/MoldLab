using System.Runtime.InteropServices;
using UnityEngine;

namespace Helpers
{
    public class ComputeHelper
    {
        public static void Dispatch(ComputeShader cs, int numIterationsX, int numIterationsY = 1,
            int numIterationsZ = 1, int kernelIndex = 0)
        {
            var threadGroupSizes = GetThreadGroupSizes(cs, kernelIndex);
            var numGroupsX = Mathf.CeilToInt(numIterationsX / (float)threadGroupSizes.x);
            var numGroupsY = Mathf.CeilToInt(numIterationsY / (float)threadGroupSizes.y);
            var numGroupsZ = Mathf.CeilToInt(numIterationsZ / (float)threadGroupSizes.y);
            cs.Dispatch(kernelIndex, numGroupsX, numGroupsY, numGroupsZ);
        }

        public static Vector3Int GetThreadGroupSizes(ComputeShader compute, int kernelIndex = 0)
        {
            uint x, y, z;
            compute.GetKernelThreadGroupSizes(kernelIndex, out x, out y, out z);
            return new Vector3Int((int)x, (int)y, (int)z);
        }

        public static void CreateAndSetBuffer<T>(ref ComputeBuffer buffer, T[] data, ComputeShader cs, string nameID,
            int kernelIndex = 0)
        {
            var stride = Marshal.SizeOf(typeof(T));
            CreateStructuredBuffer<T>(ref buffer, data.Length);
            buffer.SetData(data);
            cs.SetBuffer(kernelIndex, nameID, buffer);
        }

        public static void CreateStructuredBuffer<T>(ref ComputeBuffer buffer, T[] data)
        {
            CreateStructuredBuffer<T>(ref buffer, data.Length);
            buffer.SetData(data);
        }

        public static void CreateStructuredBuffer<T>(ref ComputeBuffer buffer, int count)
        {
            var stride = Marshal.SizeOf(typeof(T));
            var createNewBuffer =
                buffer == null || !buffer.IsValid() || buffer.count != count || buffer.stride != stride;
            if (createNewBuffer)
            {
                Release(buffer);
                buffer = new ComputeBuffer(count, stride);
            }
        }

        /// Releases supplied buffer/s if not null
        public static void Release(params ComputeBuffer[] buffers)
        {
            for (var i = 0; i < buffers.Length; i++)
                if (buffers[i] != null)
                {
                    buffers[i].Release();
                }
        }
    }
}