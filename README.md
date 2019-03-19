PrefsUGUI
===


## Description
The library that auto creation GUI elements by doing variable declaration.  

*Inspired by [fuqunaga/PrefsGUI](https://github.com/fuqunaga/PrefsGUI)*  
PrefsGUI is a so useful library. But, PrefsGUI is using the OnGUI system, So I have some problem by itself.
- In high resolution display, GUI window is showing like so small.
- OnGUI system don't have kind and useful visual.
- I heard that OnGUI system to make a spike sometimes.
- OnGUI system can't validate about input values.

By uGUI system, I can solve those problems like easy.

## Usage
Sample code
```` csharp
using PrefsUGUI;

public class Example : MonoBehaviour
{
    private PrefsVector2 vec2 = new PrefsVector2("vec2");
}
````
If you want to view more details, Let's check Example codes.  
[Example codes](Assets/PrefsUGUI/Examples/)

## Behaviour
- Using the [XmlStorage](https://github.com/a3geek/XmlStorage) library for saving and loading data.
<br />

- I generate and use a dedicated Canvas.
<br />

- You can move uGUI window by mouse moving.
<br />

- If you pressed discard button, back values to last saved.
<br />

## Implemented type
- PrefsBool
- PrefsButton
- PrefsColor
- PrefsColorSlider
- PrefsEnum
- PrefsFloat
- PrefsFloatSlider
- PrefsInt
- PrefsIntSlider
- PrefsString
- PrefsVector2
- PrefsVector2Int
- PrefsVector3
- PrefsVector3Int
- PrefsVector4

## References
[fuqunaga/PrefsGUI](https://github.com/fuqunaga/PrefsGUI)  
[XmlStorage](https://github.com/a3geek/XmlStorage)  
