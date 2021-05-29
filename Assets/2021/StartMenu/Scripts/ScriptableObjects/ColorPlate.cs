using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Color Plate", menuName = "Color Plate")]
public class ColorPlate : ScriptableObject
{
    public List<Color> colors;
}
