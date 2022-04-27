// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/CharacterControllerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CharacterControllerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterControllerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterControllerInput"",
    ""maps"": [
        {
            ""name"": ""CharacterController"",
            ""id"": ""d5db0970-16ae-403a-8b28-8926789d6b7d"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""99278592-bfc8-4617-8213-e5abdb98002f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FireAction"",
                    ""type"": ""Button"",
                    ""id"": ""c9ee5421-72c7-4755-bd28-f8d4047e31d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FireValue"",
                    ""type"": ""PassThrough"",
                    ""id"": ""384114bd-8c85-4dfd-a45b-3fb4a0147288"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action2"",
                    ""type"": ""Button"",
                    ""id"": ""05701d9d-972c-49dd-a588-ef4f224145a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action2Value"",
                    ""type"": ""Value"",
                    ""id"": ""7aff3be8-9fc9-49da-a37e-962160804df0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Action3"",
                    ""type"": ""Button"",
                    ""id"": ""cbed1b4c-f233-4f38-95b1-0dd47b6f6c57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b9ff392c-ef3d-4397-9780-92c4041235f9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""5f464fa0-9c64-405e-afde-daafe82e5739"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e69f245c-67bb-4d18-a0d8-66a6fd38c09d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c0132939-2144-417a-8d56-acc8c0c0b119"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4ba4f2d8-b140-4b02-8996-8738bfc72d64"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d635ca08-a717-4e23-b8a1-6fcf15b472a3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bd31822b-0f8f-40fb-86d4-da84f5345ce4"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireValue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70bbaab2-0e10-4335-bffe-e7a9f3c772f4"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bcef1042-c95c-4658-9181-61ff45f2fdfa"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Action2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de7a05e5-ad29-459b-8a3b-5002a675cd3c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65aace6b-3500-47b6-befa-7a8a4d65a8fd"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Action3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff6bc3f4-c6eb-48c6-8dcd-9b88d6f3341f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""FireAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd389dea-f191-4c0c-aeb1-6463a8c7edfb"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FireAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a5c77f1-1808-4a55-8918-dedfff854caa"",
                    ""path"": ""<AndroidJoystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action2Value"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CharacterController
        m_CharacterController = asset.FindActionMap("CharacterController", throwIfNotFound: true);
        m_CharacterController_Movement = m_CharacterController.FindAction("Movement", throwIfNotFound: true);
        m_CharacterController_FireAction = m_CharacterController.FindAction("FireAction", throwIfNotFound: true);
        m_CharacterController_FireValue = m_CharacterController.FindAction("FireValue", throwIfNotFound: true);
        m_CharacterController_Action2 = m_CharacterController.FindAction("Action2", throwIfNotFound: true);
        m_CharacterController_Action2Value = m_CharacterController.FindAction("Action2Value", throwIfNotFound: true);
        m_CharacterController_Action3 = m_CharacterController.FindAction("Action3", throwIfNotFound: true);
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

    // CharacterController
    private readonly InputActionMap m_CharacterController;
    private ICharacterControllerActions m_CharacterControllerActionsCallbackInterface;
    private readonly InputAction m_CharacterController_Movement;
    private readonly InputAction m_CharacterController_FireAction;
    private readonly InputAction m_CharacterController_FireValue;
    private readonly InputAction m_CharacterController_Action2;
    private readonly InputAction m_CharacterController_Action2Value;
    private readonly InputAction m_CharacterController_Action3;
    public struct CharacterControllerActions
    {
        private @CharacterControllerInput m_Wrapper;
        public CharacterControllerActions(@CharacterControllerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CharacterController_Movement;
        public InputAction @FireAction => m_Wrapper.m_CharacterController_FireAction;
        public InputAction @FireValue => m_Wrapper.m_CharacterController_FireValue;
        public InputAction @Action2 => m_Wrapper.m_CharacterController_Action2;
        public InputAction @Action2Value => m_Wrapper.m_CharacterController_Action2Value;
        public InputAction @Action3 => m_Wrapper.m_CharacterController_Action3;
        public InputActionMap Get() { return m_Wrapper.m_CharacterController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControllerActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControllerActions instance)
        {
            if (m_Wrapper.m_CharacterControllerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnMovement;
                @FireAction.started -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnFireAction;
                @FireAction.performed -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnFireAction;
                @FireAction.canceled -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnFireAction;
                @FireValue.started -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnFireValue;
                @FireValue.performed -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnFireValue;
                @FireValue.canceled -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnFireValue;
                @Action2.started -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnAction2;
                @Action2.performed -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnAction2;
                @Action2.canceled -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnAction2;
                @Action2Value.started -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnAction2Value;
                @Action2Value.performed -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnAction2Value;
                @Action2Value.canceled -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnAction2Value;
                @Action3.started -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnAction3;
                @Action3.performed -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnAction3;
                @Action3.canceled -= m_Wrapper.m_CharacterControllerActionsCallbackInterface.OnAction3;
            }
            m_Wrapper.m_CharacterControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @FireAction.started += instance.OnFireAction;
                @FireAction.performed += instance.OnFireAction;
                @FireAction.canceled += instance.OnFireAction;
                @FireValue.started += instance.OnFireValue;
                @FireValue.performed += instance.OnFireValue;
                @FireValue.canceled += instance.OnFireValue;
                @Action2.started += instance.OnAction2;
                @Action2.performed += instance.OnAction2;
                @Action2.canceled += instance.OnAction2;
                @Action2Value.started += instance.OnAction2Value;
                @Action2Value.performed += instance.OnAction2Value;
                @Action2Value.canceled += instance.OnAction2Value;
                @Action3.started += instance.OnAction3;
                @Action3.performed += instance.OnAction3;
                @Action3.canceled += instance.OnAction3;
            }
        }
    }
    public CharacterControllerActions @CharacterController => new CharacterControllerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface ICharacterControllerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnFireAction(InputAction.CallbackContext context);
        void OnFireValue(InputAction.CallbackContext context);
        void OnAction2(InputAction.CallbackContext context);
        void OnAction2Value(InputAction.CallbackContext context);
        void OnAction3(InputAction.CallbackContext context);
    }
}
