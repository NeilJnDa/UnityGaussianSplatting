using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Scripting.Python;
using UnityEditor;


public class Hello
{
    [MenuItem("PythonScripts/Hello")]
    static void RunHello()
    {
        PythonRunner.RunFile($"{Application.dataPath}/Scripts/Python/HelloWorld.py");
    }
}


