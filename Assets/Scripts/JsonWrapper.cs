﻿using Newtonsoft.Json;
using System.IO;
using UnityEngine;


namespace SubalternGames
{
    /// <summary>
    /// Convenient wrapper for Newtonsoft.Json.
    /// Usage:
    /// 
    /// MyClass o = JsonWrapper.DeserializeFromPath<MyClass>("C:/save.json");
    /// JsonWrapper.Serialize(o, "C:/save.json");
    /// </summary>
    public static class JsonWrapper
    {
        /// <summary>
        /// JSON serializer. Never call this directly!
        /// </summary>
        private static JsonSerializer serializer;
        /// <summary>
        /// JSON serializer;
        /// </summary>
        private static JsonSerializer Serializer
        {
            get
            {
                if (serializer == null)
                {
                    JsonSerializerSettings jss = new JsonSerializerSettings();
                    // Ignore loops of type references.
                    jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    // Handle typing so we can de-serialize properly.
                    jss.TypeNameHandling = TypeNameHandling.Objects;
                    // Make the output look nice.
                    jss.Formatting = Formatting.Indented;
                    serializer = JsonSerializer.Create(jss);
                }
                return serializer;
            }
        }


        /// <summary>
        /// Serialize an object of type T to a file.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="o">The object.</param>
        /// <param name="path">The absolute path to the file (including the file extension).</param>
        public static void Serialize<T>(T o, string path)
        {
            // Create the directory if needed.
            FileInfo fi = new FileInfo(path);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            // Serialize the object and write it to disk.
            using (StreamWriter sw = new StreamWriter(path))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    Serializer.Serialize(writer, o);
                }
            }
        }


        /// <summary>
        /// Deserialize a serialized object of type T.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="o">The serialized object.</param>
        public static T Deserialize<T>(string o)
        {
            using (StringReader sr = new StringReader(o))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    return Serializer.Deserialize<T>(reader);
                }
            }
        }


        /// <summary>
        /// Deserialize an object of type T stored as a text file.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="path">The absolute path to the text file (including the file extension).</param>
        public static T DeserializeFromPath<T>(string path)
        {
            return Deserialize<T>(File.ReadAllText(path));
        }


        /// <summary>
        /// Deserialize an object of type T stored as a text file in Resources.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="path">The path to the file in Resources (minus the file extension).</param>
        public static T DeserializeFromResources<T>(string path)
        {
            return Deserialize<T>(Resources.Load<TextAsset>(path).text);
        }
    }
}