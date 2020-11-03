using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class TransformExtensions
    {
        public static void SetPosition(this Transform transform, Vector3 newPos) => transform.SetPositionAndRotation(newPos, Quaternion.identity);
    }
}
