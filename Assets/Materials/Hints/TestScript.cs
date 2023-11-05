using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class ViewProcessor : InputProcessor<Vector2>
{
#if UNITY_EDITOR
    static ViewProcessor()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<ViewProcessor>();
    }



    [Tooltip("Scale down MouseDelta.")]
    public float valueShift = 0;

    public override Vector2 Process(Vector2 value, InputControl control)
    {
        return new Vector2(value.x * valueShift, value.y * valueShift);
    }
}