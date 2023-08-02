using UnityEngine;

public class LayerMaskHelper  
{
    public static bool IsLayerInLayerMask(int _layer, LayerMask _layerMask)
    {
        return _layerMask == (_layerMask | (1 << _layer));
    }
}