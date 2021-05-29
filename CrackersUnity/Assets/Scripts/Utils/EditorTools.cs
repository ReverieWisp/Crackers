using System;
using UnityEngine;

#if UNITY_EDITOR
using System.Reflection;
#endif

public static class EditorTools
{
    /// <summary>
    /// Using reflection, wipes the content from the Unity Editor console as if 'Clear' was pressed manually.
    /// This function only does something while running in the unity editor!
    /// </summary>
    public static void ClearEditorLog()
    {
#if UNITY_EDITOR
        Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        Type type = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo method = type.GetMethod("Clear");

        method.Invoke(new object (), null);
#endif
    }
}
