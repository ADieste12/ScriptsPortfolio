using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class DetectGamepad : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            print("no colega");
        }
        if (gamepad is DualShockGamepad)
        {
            print("Playstation gamepad");
        }
        else
        {
            print("Otro");
        }
    }
}
