# `DiEventLib (C# Library)`
**_C# library for Sonic Frontiers (.dvscene)_**
## 📜 Description 📜
A library with simple reading and writing function for the .dvscene file from Sonic Frontiers 
</br>
</br>
<b>Huge credit goes to <a href="https://github.com/ik-01">ik-01</a>, who did the research of the .dvscene files and who refactored the entirety of this library, without his research and help this library wouldn't exist.</b>

## 🗂️ Projects 🗂️

- DiEventLib - The actual C# library itself.

- DiEventTest - A testing sandbox for the C# library.

## 🗃 Dependencies 🗃

|                      Name                       |   Use   |
| :---------------------------------------------: | :------:|
|     [Amiticia.IO]([https://github.com/tge-was-taken/Amicitia.IO](https://github.com/tge-was-taken/Amicitia.IO))     | Used for its upgraded and better binary reader and writer |

## 📝 Documentation 📝
### Reading
```csharp
DvScene dvScene = new("file-path-to-the-dvscene");
```
### Writing
```csharp
dvScene.Save("file-path-to-your-new-dvscene");
```
