using UnityEngine;
using System.IO;
using UnityEditor;


namespace SubalternGames.Json
{
    /// <summary>
    /// Tests for the JSON wrapper.
    /// </summary>
    public static class JsonTest
    {
        [MenuItem("Subaltern Games/JSON Test")]
        public static void Serialize()
        {
            TestObject a = new TestObject("abcd", 1);
            string path = Path.Combine(Application.dataPath, "test.json");
            JsonWrapper.Serialize(a, path);
            Debug.Log(File.ReadAllText(path));
            TestObject b = JsonWrapper.DeserializeFromPath<TestObject>(path);
            Debug.Assert(b.name == "abcd");
            Debug.Assert(b.hp == 1);
        }
    }
}