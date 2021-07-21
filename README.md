# JSON Unity Wrapper

## Example usage

```c#
namespace SubalternGames.Json
{
    public class TestObject
    {
        public string name;
        public int hp;


        public TestObject(string name, int hp)
        {
            this.name = name;
            this.hp = hp;
        }
    }
}
```

```c#
using UnityEngine;
using System.IO;
using UnityEditor;


namespace SubalternGames.Json
{
    /// <summary>
    /// Tests for the JSON wrapper. Remember to put this script in an Editor/ folder.
    /// </summary>
    public static class JsonTest
    {
        [MenuItem("Subaltern Games/JSON Test")]
        public static void Serialize()
        {
            TestObject a = new TestObject("abcd", 1);
            string path = Path.Combine(Application.dataPath, "test.json");
            
            // Serialize the object to a file path.
            JsonWrapper.Serialize(a, path);
            Debug.Log(File.ReadAllText(path));
            
            // Deserialize the object from the file path.
            TestObject b = JsonWrapper.DeserializeFromPath<TestObject>(path);
            
            // Assert that the object deserialized correctly.
            Debug.Assert(b.name == "abcd");
            Debug.Assert(b.hp == 1);
        }
    }
}
```

## Installation

### Step 1: Set your Unity project to .NET 4

**Open Project Settings:**

![](doc/images/0_project_settings.png)

**Open the Player tab and the Other Settings tab:**

![](doc/images/1_other_settings.png)

**Scroll down and set .NET to 4.x:**

![](doc/images/2_net_4.png)

### Step 2: Copy [this folder] to the `Assets/` directory of your Unity project

Make sure that the filepath is: `Assets/Plugins/JSON/Newtonsoft.Json.dll`

### Step 3: Copy [this script] to the `Assets/` directory of your Unity project

The script can be copied anywhere in Assets. For the sake of organizing your project, consider: `Assets/Scripts/SubalternGames/JsonWrapper.cs`

## API

### `Serialize<T>(T o, string path)`

Serialize an object of type `T` to a file.

| Parameter | Type     | Description                                                  |
| --------- | -------- | ------------------------------------------------------------ |
| `o`       | `T`      | The object.                                                  |
| `path`    | `string` | The absolute path to the file (including the file extension). |

```c#
using SubalternGames;


public class TestObject
{
    public int hp;


    public TestObject(int hp)
    {
        this.hp = hp;
    }


    public void Serialize(string path)
    {
        JsonWrapper.Serialize(this, path);
    }
}
```

***

### `Deserialize<T>(string o)`

Deserialize a serialized object of type `T`.

*Return:* An object of type `T`.

| Parameter | Type | Description            |
| --------- | ---- | ---------------------- |
| `o`       | `T`  | The serialized object. |

```c#
using SubalternGames;


public class TestObject
{
    public int hp;


    public TestObject(int hp)
    {
        this.hp = hp;
    }


    public static TestObject Deserialize(string serialized)
    {
        return JsonWrapper.Deserialize<TestObject>(serialized);
    }
}
```

***

### `DeserializeFromPath<T>(string path)`

Deserialize an object of type `T` stored as a text file.

*Return:* An object of type `T`.

| Parameter | Type     | Description                                                  |
| --------- | -------- | ------------------------------------------------------------ |
| `path`    | `string` | The absolute path to the text file (including the file extension). |

```c#
using SubalternGames;


public class TestObject
{
    public int hp;


    public TestObject(int hp)
    {
        this.hp = hp;
    }


    public static TestObject DeserializeFromPath(string path)
    {
        return JsonWrapper.DeserializeFromPath<TestObject>(path);
    }
}
```

***

### `DeserializeFromResources<T>(string path)`

Deserialize an object of type T stored as a text file in Resources.

*Return:* An object of type `T`.

| Parameter | Type     | Description                                                  |
| --------- | -------- | ------------------------------------------------------------ |
| `path`    | `string` | The path to the file in Resources (*minus* the file extension). |

```c#
using SubalternGames;


public class TestObject
{
    public int hp;


    public TestObject(int hp)
    {
        this.hp = hp;
    }


    public static TestObject DeserializeFromResources(string path)
    {
        return JsonWrapper.DeserializeFromResources<TestObject>(path);
    }
}
```

