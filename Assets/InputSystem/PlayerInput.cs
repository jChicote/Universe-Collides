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
            ""name"": ""Fighter_Controls"",
            ""id"": ""dd659f3b-257d-4d4c-85ae-684645020ec3"",
            ""actions"": [
                {
                    ""name"": ""StickRotation"",
                    ""type"": ""Value"",
                    ""id"": ""8b0e2bcd-2098-4d50-b28a-90883f43774b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throttle"",
                    ""type"": ""Value"",
                    ""id"": ""a3aafca2-67bc-40bc-8a54-82790fc51818"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Locking"",
                    ""type"": ""Button"",
                    ""id"": ""6d9f16b7-9983-4034-ac8d-209d8a51803d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""PrimaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""3b360df2-a516-4944-a396-28bda5e19d5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""590d656d-480a-4ba5-908c-c36c28fe327f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Commands"",
                    ""type"": ""Button"",
                    ""id"": ""f36f20d3-f49b-4a77-be02-af035c33388c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseRotation"",
                    ""type"": ""Value"",
                    ""id"": ""63cb09aa-dffd-4dfd-a15f-1c1d41d0f827"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryFire"",
                    ""type"": ""Button"",
                    ""id"": ""f7163110-08fc-48fa-b3f6-0485c2b6a92a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Test_Damage"",
                    ""type"": ""Button"",
                    ""id"": ""e7f9b41c-3c91-4c27-a2e3-fab760fb0f4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Test_Death"",
                    ""type"": ""Button"",
                    ""id"": ""d0499cdd-4108-45fa-ad3e-d38350422fc8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Ability1"",
                    ""type"": ""Button"",
                    ""id"": ""d1bbc666-124b-4255-8506-bd9cdafcf584"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability2"",
                    ""type"": ""Button"",
                    ""id"": ""ba076ddd-8fde-40e8-a675-7af44d2202c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ability3"",
                    ""type"": ""Button"",
                    ""id"": ""2ce304fb-d819-4af8-8c8a-8544a98c67e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5bdc5dde-3204-4017-a074-320e6bc9919f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.25)"",
                    ""groups"": """",
                    ""action"": ""StickRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b91371fe-3d69-49c8-9364-ce2cba71110d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.25),NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""16a495d5-ee00-4ef0-8df5-b920953411a7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1f464ea8-cd53-4878-96a3-845ddca88a13"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""68a3809e-0ffd-467a-88cc-af593854ba03"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e7c766a2-c57a-46b7-9932-914d838aea04"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7117322c-5394-445b-8822-7b616bbeb4fc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throttle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""74ba4b88-c82e-4fa0-b579-3fc0041a38d2"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2e707ca-beba-45d7-baa6-931fdf0e1f9f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Locking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d48e8f7f-f14a-4995-bcad-077ec450a5eb"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2339aa8-d014-443f-960b-96674fd9f8ee"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a2e3775-350b-4f58-aaa4-952242ad9fc7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c479ca9-10d7-4f58-b611-e1f26f191946"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""GamePadCommand"",
                    ""id"": ""4c18c34e-0040-45f8-babe-c695f4a0e8c6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Commands"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""45b73834-a67c-45e3-adb7-89a88f25ccca"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Commands"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cd8d4a46-b6b9-4f01-a516-db68bbd38cb8"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Commands"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""68433e5a-d9fd-4da4-b197-59a5086b47a3"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Commands"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3bfe1081-02f9-48ce-bd80-9a429e822e42"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Commands"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6805443b-ad2a-4207-9610-766bbbb32d5b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e84337cf-71bb-4ca9-9207-512fe089858f"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51ba5eef-2a8b-42ad-8c22-16b11e89ada6"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_Damage"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd24f3e1-6422-4348-9a6b-2ff8d4e99898"",
                    ""path"": ""<Keyboard>/o"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test_Death"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2f1b9d98-aa62-4d01-b239-85b1c6cea4d0"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af8fc9d2-f87c-4a93-9429-f41df50c448a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2ad98e2-d768-4e0c-a33f-f4c923223447"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6a8f641-47e1-4731-988f-b58da2468698"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e11a5eef-680d-468a-b3a3-3a61e2112caa"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f6c24e4-cfee-4dda-8d95-d723a6d30860"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ability3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Turrent_Controls"",
            ""id"": ""9300bc6c-4686-4452-9152-32cc06d1f571"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""dfad2a4a-8f67-46c6-802d-a0aa9c7a10ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""de3dd14a-4304-4f4f-be35-854de97860be"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Starship_Controls"",
            ""id"": ""c02b7a53-519b-436a-ae5f-b261269773f5"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""7d6fe1cf-3261-4bf8-a69e-b4e9098a4eda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0005b493-d4c7-419d-ac4a-6bdbec68c8fb"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Fighter_Controls
        m_Fighter_Controls = asset.FindActionMap("Fighter_Controls", throwIfNotFound: true);
        m_Fighter_Controls_StickRotation = m_Fighter_Controls.FindAction("StickRotation", throwIfNotFound: true);
        m_Fighter_Controls_Throttle = m_Fighter_Controls.FindAction("Throttle", throwIfNotFound: true);
        m_Fighter_Controls_Locking = m_Fighter_Controls.FindAction("Locking", throwIfNotFound: true);
        m_Fighter_Controls_PrimaryFire = m_Fighter_Controls.FindAction("PrimaryFire", throwIfNotFound: true);
        m_Fighter_Controls_Pause = m_Fighter_Controls.FindAction("Pause", throwIfNotFound: true);
        m_Fighter_Controls_Commands = m_Fighter_Controls.FindAction("Commands", throwIfNotFound: true);
        m_Fighter_Controls_MouseRotation = m_Fighter_Controls.FindAction("MouseRotation", throwIfNotFound: true);
        m_Fighter_Controls_SecondaryFire = m_Fighter_Controls.FindAction("SecondaryFire", throwIfNotFound: true);
        m_Fighter_Controls_Test_Damage = m_Fighter_Controls.FindAction("Test_Damage", throwIfNotFound: true);
        m_Fighter_Controls_Test_Death = m_Fighter_Controls.FindAction("Test_Death", throwIfNotFound: true);
        m_Fighter_Controls_Ability1 = m_Fighter_Controls.FindAction("Ability1", throwIfNotFound: true);
        m_Fighter_Controls_Ability2 = m_Fighter_Controls.FindAction("Ability2", throwIfNotFound: true);
        m_Fighter_Controls_Ability3 = m_Fighter_Controls.FindAction("Ability3", throwIfNotFound: true);
        // Turrent_Controls
        m_Turrent_Controls = asset.FindActionMap("Turrent_Controls", throwIfNotFound: true);
        m_Turrent_Controls_Newaction = m_Turrent_Controls.FindAction("New action", throwIfNotFound: true);
        // Starship_Controls
        m_Starship_Controls = asset.FindActionMap("Starship_Controls", throwIfNotFound: true);
        m_Starship_Controls_Newaction = m_Starship_Controls.FindAction("New action", throwIfNotFound: true);
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

    // Fighter_Controls
    private readonly InputActionMap m_Fighter_Controls;
    private IFighter_ControlsActions m_Fighter_ControlsActionsCallbackInterface;
    private readonly InputAction m_Fighter_Controls_StickRotation;
    private readonly InputAction m_Fighter_Controls_Throttle;
    private readonly InputAction m_Fighter_Controls_Locking;
    private readonly InputAction m_Fighter_Controls_PrimaryFire;
    private readonly InputAction m_Fighter_Controls_Pause;
    private readonly InputAction m_Fighter_Controls_Commands;
    private readonly InputAction m_Fighter_Controls_MouseRotation;
    private readonly InputAction m_Fighter_Controls_SecondaryFire;
    private readonly InputAction m_Fighter_Controls_Test_Damage;
    private readonly InputAction m_Fighter_Controls_Test_Death;
    private readonly InputAction m_Fighter_Controls_Ability1;
    private readonly InputAction m_Fighter_Controls_Ability2;
    private readonly InputAction m_Fighter_Controls_Ability3;
    public struct Fighter_ControlsActions
    {
        private @PlayerInput m_Wrapper;
        public Fighter_ControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @StickRotation => m_Wrapper.m_Fighter_Controls_StickRotation;
        public InputAction @Throttle => m_Wrapper.m_Fighter_Controls_Throttle;
        public InputAction @Locking => m_Wrapper.m_Fighter_Controls_Locking;
        public InputAction @PrimaryFire => m_Wrapper.m_Fighter_Controls_PrimaryFire;
        public InputAction @Pause => m_Wrapper.m_Fighter_Controls_Pause;
        public InputAction @Commands => m_Wrapper.m_Fighter_Controls_Commands;
        public InputAction @MouseRotation => m_Wrapper.m_Fighter_Controls_MouseRotation;
        public InputAction @SecondaryFire => m_Wrapper.m_Fighter_Controls_SecondaryFire;
        public InputAction @Test_Damage => m_Wrapper.m_Fighter_Controls_Test_Damage;
        public InputAction @Test_Death => m_Wrapper.m_Fighter_Controls_Test_Death;
        public InputAction @Ability1 => m_Wrapper.m_Fighter_Controls_Ability1;
        public InputAction @Ability2 => m_Wrapper.m_Fighter_Controls_Ability2;
        public InputAction @Ability3 => m_Wrapper.m_Fighter_Controls_Ability3;
        public InputActionMap Get() { return m_Wrapper.m_Fighter_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Fighter_ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IFighter_ControlsActions instance)
        {
            if (m_Wrapper.m_Fighter_ControlsActionsCallbackInterface != null)
            {
                @StickRotation.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnStickRotation;
                @StickRotation.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnStickRotation;
                @StickRotation.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnStickRotation;
                @Throttle.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnThrottle;
                @Throttle.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnThrottle;
                @Throttle.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnThrottle;
                @Locking.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnLocking;
                @Locking.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnLocking;
                @Locking.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnLocking;
                @PrimaryFire.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnPrimaryFire;
                @PrimaryFire.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnPrimaryFire;
                @Pause.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnPause;
                @Commands.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnCommands;
                @Commands.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnCommands;
                @Commands.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnCommands;
                @MouseRotation.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnMouseRotation;
                @MouseRotation.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnMouseRotation;
                @MouseRotation.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnMouseRotation;
                @SecondaryFire.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnSecondaryFire;
                @SecondaryFire.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnSecondaryFire;
                @SecondaryFire.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnSecondaryFire;
                @Test_Damage.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnTest_Damage;
                @Test_Damage.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnTest_Damage;
                @Test_Damage.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnTest_Damage;
                @Test_Death.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnTest_Death;
                @Test_Death.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnTest_Death;
                @Test_Death.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnTest_Death;
                @Ability1.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnAbility1;
                @Ability1.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnAbility1;
                @Ability1.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnAbility1;
                @Ability2.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnAbility2;
                @Ability2.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnAbility2;
                @Ability2.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnAbility2;
                @Ability3.started -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnAbility3;
                @Ability3.performed -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnAbility3;
                @Ability3.canceled -= m_Wrapper.m_Fighter_ControlsActionsCallbackInterface.OnAbility3;
            }
            m_Wrapper.m_Fighter_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @StickRotation.started += instance.OnStickRotation;
                @StickRotation.performed += instance.OnStickRotation;
                @StickRotation.canceled += instance.OnStickRotation;
                @Throttle.started += instance.OnThrottle;
                @Throttle.performed += instance.OnThrottle;
                @Throttle.canceled += instance.OnThrottle;
                @Locking.started += instance.OnLocking;
                @Locking.performed += instance.OnLocking;
                @Locking.canceled += instance.OnLocking;
                @PrimaryFire.started += instance.OnPrimaryFire;
                @PrimaryFire.performed += instance.OnPrimaryFire;
                @PrimaryFire.canceled += instance.OnPrimaryFire;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Commands.started += instance.OnCommands;
                @Commands.performed += instance.OnCommands;
                @Commands.canceled += instance.OnCommands;
                @MouseRotation.started += instance.OnMouseRotation;
                @MouseRotation.performed += instance.OnMouseRotation;
                @MouseRotation.canceled += instance.OnMouseRotation;
                @SecondaryFire.started += instance.OnSecondaryFire;
                @SecondaryFire.performed += instance.OnSecondaryFire;
                @SecondaryFire.canceled += instance.OnSecondaryFire;
                @Test_Damage.started += instance.OnTest_Damage;
                @Test_Damage.performed += instance.OnTest_Damage;
                @Test_Damage.canceled += instance.OnTest_Damage;
                @Test_Death.started += instance.OnTest_Death;
                @Test_Death.performed += instance.OnTest_Death;
                @Test_Death.canceled += instance.OnTest_Death;
                @Ability1.started += instance.OnAbility1;
                @Ability1.performed += instance.OnAbility1;
                @Ability1.canceled += instance.OnAbility1;
                @Ability2.started += instance.OnAbility2;
                @Ability2.performed += instance.OnAbility2;
                @Ability2.canceled += instance.OnAbility2;
                @Ability3.started += instance.OnAbility3;
                @Ability3.performed += instance.OnAbility3;
                @Ability3.canceled += instance.OnAbility3;
            }
        }
    }
    public Fighter_ControlsActions @Fighter_Controls => new Fighter_ControlsActions(this);

    // Turrent_Controls
    private readonly InputActionMap m_Turrent_Controls;
    private ITurrent_ControlsActions m_Turrent_ControlsActionsCallbackInterface;
    private readonly InputAction m_Turrent_Controls_Newaction;
    public struct Turrent_ControlsActions
    {
        private @PlayerInput m_Wrapper;
        public Turrent_ControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Turrent_Controls_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Turrent_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Turrent_ControlsActions set) { return set.Get(); }
        public void SetCallbacks(ITurrent_ControlsActions instance)
        {
            if (m_Wrapper.m_Turrent_ControlsActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_Turrent_ControlsActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_Turrent_ControlsActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_Turrent_ControlsActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_Turrent_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public Turrent_ControlsActions @Turrent_Controls => new Turrent_ControlsActions(this);

    // Starship_Controls
    private readonly InputActionMap m_Starship_Controls;
    private IStarship_ControlsActions m_Starship_ControlsActionsCallbackInterface;
    private readonly InputAction m_Starship_Controls_Newaction;
    public struct Starship_ControlsActions
    {
        private @PlayerInput m_Wrapper;
        public Starship_ControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_Starship_Controls_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_Starship_Controls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Starship_ControlsActions set) { return set.Get(); }
        public void SetCallbacks(IStarship_ControlsActions instance)
        {
            if (m_Wrapper.m_Starship_ControlsActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_Starship_ControlsActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_Starship_ControlsActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_Starship_ControlsActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_Starship_ControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public Starship_ControlsActions @Starship_Controls => new Starship_ControlsActions(this);
    public interface IFighter_ControlsActions
    {
        void OnStickRotation(InputAction.CallbackContext context);
        void OnThrottle(InputAction.CallbackContext context);
        void OnLocking(InputAction.CallbackContext context);
        void OnPrimaryFire(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnCommands(InputAction.CallbackContext context);
        void OnMouseRotation(InputAction.CallbackContext context);
        void OnSecondaryFire(InputAction.CallbackContext context);
        void OnTest_Damage(InputAction.CallbackContext context);
        void OnTest_Death(InputAction.CallbackContext context);
        void OnAbility1(InputAction.CallbackContext context);
        void OnAbility2(InputAction.CallbackContext context);
        void OnAbility3(InputAction.CallbackContext context);
    }
    public interface ITurrent_ControlsActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IStarship_ControlsActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
