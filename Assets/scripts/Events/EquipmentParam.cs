using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct EquipmentParam : EventParameter
{
    public Equipment equipment;

    public EquipmentParam(Equipment equipment)
    {
        this.equipment = equipment;
    }
}
