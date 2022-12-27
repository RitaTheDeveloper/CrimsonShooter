using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPooledObject 
{
    ObjectPooler.ObjectInfo.ObjectType Type { get; }
}
