using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Vector3Event : UnityEvent<Vector3> {}
[Serializable]
public class StringEvent : UnityEvent<string> {}
[Serializable]
public class FloatEvent : UnityEvent<float> {}
[Serializable]
public class BoolEvent : UnityEvent<bool> {}