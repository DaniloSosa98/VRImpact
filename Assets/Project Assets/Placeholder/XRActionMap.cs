// GENERATED AUTOMATICALLY FROM 'Assets/Project Assets/Placeholder/XRActionMap.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @XRActionMap : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @XRActionMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""XRActionMap"",
    ""maps"": [
        {
            ""name"": ""Custom Game Actions"",
            ""id"": ""1332ede4-365d-43fc-968f-75e0502d5e6c"",
            ""actions"": [
                {
                    ""name"": ""Menu Press"",
                    ""type"": ""Button"",
                    ""id"": ""945ffaac-71ab-4b3c-b34f-30eb756c02f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""298cf214-442e-4504-a717-092d3e8141ae"",
                    ""path"": ""<XRController>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a2d7180-5246-4b12-95f3-043fa6f1e93e"",
                    ""path"": ""*/{Menu}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dba2f100-8c2f-412a-9ac6-9b7837f8e435"",
                    ""path"": ""*/{MenuButton}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9474432-31b4-466f-86c3-5bf74813ae3c"",
                    ""path"": ""<XRController>{LeftHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""UI Press"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Custom Game Actions
        m_CustomGameActions = asset.FindActionMap("Custom Game Actions", throwIfNotFound: true);
        m_CustomGameActions_MenuPress = m_CustomGameActions.FindAction("Menu Press", throwIfNotFound: true);
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

    // Custom Game Actions
    private readonly InputActionMap m_CustomGameActions;
    private ICustomGameActionsActions m_CustomGameActionsActionsCallbackInterface;
    private readonly InputAction m_CustomGameActions_MenuPress;
    public struct CustomGameActionsActions
    {
        private @XRActionMap m_Wrapper;
        public CustomGameActionsActions(@XRActionMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @MenuPress => m_Wrapper.m_CustomGameActions_MenuPress;
        public InputActionMap Get() { return m_Wrapper.m_CustomGameActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CustomGameActionsActions set) { return set.Get(); }
        public void SetCallbacks(ICustomGameActionsActions instance)
        {
            if (m_Wrapper.m_CustomGameActionsActionsCallbackInterface != null)
            {
                @MenuPress.started -= m_Wrapper.m_CustomGameActionsActionsCallbackInterface.OnMenuPress;
                @MenuPress.performed -= m_Wrapper.m_CustomGameActionsActionsCallbackInterface.OnMenuPress;
                @MenuPress.canceled -= m_Wrapper.m_CustomGameActionsActionsCallbackInterface.OnMenuPress;
            }
            m_Wrapper.m_CustomGameActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MenuPress.started += instance.OnMenuPress;
                @MenuPress.performed += instance.OnMenuPress;
                @MenuPress.canceled += instance.OnMenuPress;
            }
        }
    }
    public CustomGameActionsActions @CustomGameActions => new CustomGameActionsActions(this);
    public interface ICustomGameActionsActions
    {
        void OnMenuPress(InputAction.CallbackContext context);
    }
}
