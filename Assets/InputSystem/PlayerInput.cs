// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player_Controls"",
            ""id"": ""dd659f3b-257d-4d4c-85ae-684645020ec3"",
            ""actions"": [
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""27eb9ba8-7602-4b49-9599-03e4333c4610"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Gamepad"",
                    ""type"": ""Value"",
                    ""id"": ""bf1a5ebd-ef30-44f0-9ffb-2455aaf8c916"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e56e2c14-6563-4d68-92b4-308d02c50839"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gamepad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ea664f2-62a3-4e96-b20e-21c55c10eb96"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player_Controls
        m_Player_Controls = asset.FindActionMap("Player_Controls", throwIfNotFound: true);
        m_Player_Controls_Mouse = m_Player_Controls.FindAction("Mouse", throwIfNotFound: true);
        m_Player_Controls_Gamepad = m_Player_Controls.FindAction("Gamepad", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player_Controls
    private readonly InputActionMap m_Player_Controls;
    private IPlayer_ControlsActions m_Player_ControlsActionsCallbackInterface;
    private readonly InputAction m_Player_Controls_Mouse;
    private readonly InputAction m_Player_Controls_Gamepad;
    public struct Player_ControlsActions
    {
        private @PlayerInput m_Wrapper;
        public Player_ControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Mouse => m_Wrapper.m_Player_Controls_Mouse;
        public InputAction @Gamepad => m_Wrapper.m_Player_Controls_Gamepad;
        public InputActionMap Get() { return m_Wrapper.m_Player_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayer_ControlsActions instance)
        {
            if (m_Wrapper.m_Player_ControlsActionsCallbackInterface != null)
            {
                @Mouse.started -= m_Wrapper.m_Player_ControlsActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_Player_ControlsActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_Player_ControlsActionsCallbackInterface.OnMouse;
                @Gamepad.started -= m_Wrapper.m_Player_ControlsActionsCallbackInterface.OnGamepad;
                @Gamepad.performed -= m_Wrapper.m_Player_ControlsActionsCallbackInterface.OnGamepad;
                @Gamepad.canceled -= m_Wrapper.m_Player_ControlsActionsCallbackInterface.OnGamepad;
            }
            m_Wrapper.m_Player_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @Gamepad.started += instance.OnGamepad;
                @Gamepad.performed += instance.OnGamepad;
                @Gamepad.canceled += instance.OnGamepad;
            }
        }
    }
    public Player_ControlsActions @Player_Controls => new Player_ControlsActions(this);
    public interface IPlayer_ControlsActions
    {
        void OnMouse(InputAction.CallbackContext context);
        void OnGamepad(InputAction.CallbackContext context);
    }
}
