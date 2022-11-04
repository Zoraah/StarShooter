using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarShooter.Interfaces
{
    public interface ICollisionObject
    {
        public void DoCollisionOperation(GameObject gameObject);
    }
}