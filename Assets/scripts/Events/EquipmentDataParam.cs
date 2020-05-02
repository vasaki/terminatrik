using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct EquipmentDataParam : EventParameter
{
    public EquipmentData equipmentData;
    public EquipmentStatus previousStatus;
    public EquipmentDataParam(EquipmentData equipmentData, EquipmentStatus previousStatus)
    {
        this.equipmentData = equipmentData;
        this.previousStatus = previousStatus;
    }
}
