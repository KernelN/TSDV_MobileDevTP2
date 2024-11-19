using UnityEngine;
using UnityEngine.InputSystem;
using Universal.Singletons;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;
using Accelerometer = UnityEngine.InputSystem.Accelerometer;

public class InputManager : MonoBehaviourSingletonInScene<InputManager>
{
    public enum InputType { Gyro, Accel, VStick }

    [Header("Set Values")] 
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject Joystick;
    //[Header("Runtime Values")]
    Vector3 sensorVec = Vector3.zero;

    public Vector2 Axis { get; private set; } = Vector2.zero;
    public InputType inputType { get; private set; } = InputType.Gyro;
    
    void Start()
    {
        if (playerInput.currentControlScheme.Contains(InputType.Gyro.ToString()))
        {
            inputType = InputType.Gyro;
            if(Gyroscope.current != null)
                InputSystem.EnableDevice(Gyroscope.current);
        }
        else if (playerInput.currentControlScheme.Contains(InputType.Accel.ToString()))
        {
            inputType = InputType.Accel;
            if(Accelerometer.current != null)
                InputSystem.EnableDevice(Accelerometer.current);
        }
        else
        {
            inputType = InputType.VStick;
            Joystick.SetActive(true);
        }

        InputAction move = playerInput.actions["Move"];
        move.performed += UpdateInput;
        move.canceled += UpdateInput;
    }

    public void RecalibrateSensors()
    {
        sensorVec = Vector3.zero;
        Axis = Vector2.zero;
    }
    void UpdateInput(InputAction.CallbackContext value)
    {
        if (inputType == InputType.VStick)
            Axis = value.ReadValue<Vector2>();
        else
        {
            sensorVec += value.ReadValue<Vector3>();
            Axis = sensorVec;
        }
        
        Axis = Axis.normalized;
    }
}