using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveObjectScript : MonoBehaviour
{   public Vector3 targetPosition;

// Move the object to the target position
public void MoveObject()
{
    transform.position = targetPosition;
}
}
