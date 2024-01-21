# `DiEventLib (C# Library)`
**_C# library for Sonic Frontiers (.dvscene)_**
## 📜 Description 📜
A library with simple reading and writing function for the .dvscene file from Sonic Frontiers 
</br>
</br>
<b>Huge credit goes to <a href="https://github.com/ik-01">ik-01</a>, who did the research of the .dvscene files, without his research this library wouldn't exist.</b>

## 🗂️ Projects 🗂️

- DiEventLib - The actual C# library itself.

- DiEventTest - A testing sandbox for the C# library.

## 🗃 Dependencies 🗃

|                      Name                       |   Use   |
| :---------------------------------------------: | :------:|
|     [HedgeLib](https://github.com/Radfordhound/HedgeLib/tree/master)     | Used for its upgraded and better binary reader and writer |

## 📝 Documentation 📝
### Reading
```csharp
Reader reader = new Reader();
DiEvent diEvent = reader.ReadDvScene("file-path-to-the-dvscene");
```

### Writing
```csharp
Writer writer = new Writer();
writer.WriteDvScene("file-path-where-to-writer", your-diEvent);
```
