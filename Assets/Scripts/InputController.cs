using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    public RingMenu m_ringMenu;

    public void Start()
    {
    }

    public void Update()
    {
        var pointer = Pointer.current;
        if (pointer.press.wasPressedThisFrame)
        {
            m_ringMenu.Show(pointer.position.ReadValue());
        }
        if (pointer.press.wasReleasedThisFrame)
        {
            m_ringMenu.Hide();
        }
    }

}
