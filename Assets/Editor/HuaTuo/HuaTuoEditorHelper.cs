using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEngine;

namespace HuaTuo
{
    /// <summary>
    ///     这里仅仅是一个流程展示
    ///     简单说明如果你想将huatuo的dll做成自动化的简单实现
    /// </summary>
    public class HuaTuoEditorHelper
    {
        public static string DllBuildOutputDir => Path.GetFullPath($"{Application.streamingAssetsPath}");


        public static string AssetBundleOutputDir => Application.dataPath + "/HuaTuo/Output";

        [InitializeOnLoadMethod]
        private static void Setup()
        {
            PlayerSettings.SetAdditionalIl2CppArgs("--generate-cmake=il2cpp");
        }

        private static void CreateDirIfNotExists(string dirName)
        {
            if (!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);
        }

        private static void CompileDll(string buildDir, BuildTarget target)
        {
            var group = BuildPipeline.GetBuildTargetGroup(target);

            var scriptCompilationSettings = new ScriptCompilationSettings();
            scriptCompilationSettings.group = group;
            scriptCompilationSettings.target = target;
            CreateDirIfNotExists(buildDir);
            var scriptCompilationResult =
                PlayerBuildInterface.CompilePlayerScripts(scriptCompilationSettings, buildDir);
            foreach (var ass in scriptCompilationResult.assemblies) Debug.LogFormat("compile assemblies:{0}", ass);
        }

        public static string GetDllBuildOutputDirByTarget(BuildTarget target)
        {
            // return $"{DllBuildOutputDir}/{target}";
            return $"{DllBuildOutputDir}";
        }

        [MenuItem("HuaTuo/CompileDll/ActiveBuildTarget")]
        public static void CompileDllActiveBuildTarget()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/Win64")]
        public static void CompileDllWin64()
        {
            var target = BuildTarget.StandaloneWindows64;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/Linux64")]
        public static void CompileDllLinux()
        {
            var target = BuildTarget.StandaloneLinux64;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/OSX")]
        public static void CompileDllOSX()
        {
            var target = BuildTarget.StandaloneOSX;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/Android")]
        public static void CompileDllAndroid()
        {
            var target = BuildTarget.Android;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/IOS")]
        public static void CompileDllIOS()
        {
            //var target = EditorUserBuildSettings.activeBuildTarget;
            var target = BuildTarget.iOS;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/生成泛型共享值类型")]
        public static void GenSharedValueTypes()
        {
            var code = new StringBuilder();
            code.Append("namespace Huatuo");
            code.Append("{");
            code.Append(@"
    public struct _shared_size_0
    {
    }
    public struct _shared_size_1
    {
        private byte f1;
    }");

            for (var i = 2; i <= 512; i++)
                code.Append($@"
    public struct _shared_size_{i}
    {{
        private _shared_size_{i - 1} f1;
        private byte f2;
    }}");

            code.Append($@"
    public static class shared_value_types {{
        public static System.Collections.Generic.List<System.Type> types = new System.Collections.Generic.List<System.Type>
        {{
            {string.Join(",", Enumerable.Range(0, 513).Select(i => $"typeof(_shared_size_{i})"))}
        }};
    }}");

            code.Append("}");

            File.WriteAllText("Assets/Standard Assets/Huatuo/shared_value_types.cs", code.ToString());
        }
    }
}