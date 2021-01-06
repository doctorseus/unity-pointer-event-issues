using UnityEngine;
using UnityEngine.UIElements;

public class RingMenu : MonoBehaviour
{
    public enum IconState
    {
        Selected,
        Active,
        Disabled,
        Hidden
    }

    public ScreenLogger m_Logger;
    public UIDocument m_UiRoot;

    public Sprite spriteBackgroundLarge;

    public Sprite spriteIcon0;
    public Sprite spriteIcon1;
    public Sprite spriteIcon2;

    private VisualElement m_Container;

    private VisualElement m_IconWrapper;
    private VisualElement m_IconSelected;
    private VisualElement m_IconActive;
    private VisualElement m_IconDisabled;

    public void SetCenterPosition(Vector2 screenPoint)
    {
        var flippedScreenPoint = new Vector2(screenPoint.x, Screen.height - screenPoint.y);
        var panelPoint = RuntimePanelUtils.ScreenToPanel(m_UiRoot.rootVisualElement.panel, flippedScreenPoint);
        m_Container.style.left = panelPoint.x;
        m_Container.style.top = panelPoint.y;
    }

    public void Show(Vector2 touchStartPos)
    {
        SetCenterPosition(touchStartPos);
        m_Container.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        m_Container.style.display = DisplayStyle.None;
    }

    public void SetIconState(IconState state)
    {
        m_IconSelected.style.visibility = Visibility.Hidden;
        m_IconActive.style.visibility = Visibility.Hidden;
        m_IconDisabled.style.visibility = Visibility.Hidden;
        switch (state)
        {
            case IconState.Selected:
                m_IconSelected.style.visibility = Visibility.Visible;
                break;
            case IconState.Active:
                m_IconActive.style.visibility = Visibility.Visible;
                break;
            case IconState.Disabled:
                m_IconDisabled.style.visibility = Visibility.Visible;
                break;
        }
    }

    void Start()
    {
            var uiRoot = m_UiRoot.rootVisualElement;

            // CONTAINER
            m_Container = new VisualElement();
            m_Container.name = "build-menu-container";
            m_Container.style.display = DisplayStyle.None;
            m_Container.style.position = Position.Absolute;
            m_Container.style.width = 398;
            m_Container.style.height = 398;

            // BACKGROUND
            var backgroundLarge = new VisualElement();
            backgroundLarge.style.visibility = Visibility.Visible;
            backgroundLarge.style.position = Position.Absolute;
            backgroundLarge.style.backgroundImage = new StyleBackground(spriteBackgroundLarge);
            backgroundLarge.style.width = Length.Percent(100);
            backgroundLarge.style.height = Length.Percent(100);
            backgroundLarge.style.left = Length.Percent(-50);
            backgroundLarge.style.top = Length.Percent(-50);

            m_Container.Add(backgroundLarge);

            // ICON
            {
                m_IconWrapper = new VisualElement();

                // CALLBACK HANDLERS
                m_IconWrapper.RegisterCallback<PointerEnterEvent>(e =>
                {
                    m_Logger.Log($"PointerEnterEvent ({e.localPosition})");
                    SetIconState(IconState.Selected);
                });
                m_IconWrapper.RegisterCallback<PointerMoveEvent>(e =>
                {
                    m_Logger.Log($"PointerMoveEvent ({e.localPosition})");

                });
                m_IconWrapper.RegisterCallback<PointerLeaveEvent>(e =>
                {
                    m_Logger.Log($"PointerLeaveEvent ({e.localPosition})");
                    SetIconState(IconState.Active);
                });



                // Set icon position
                var centerX = 100;
                var centerY = 0;
                m_IconWrapper.style.left = (-65 / 2) + centerX;
                m_IconWrapper.style.top = (-77 / 2) + centerY;

                m_IconSelected = new VisualElement();
                m_IconSelected.style.visibility = Visibility.Hidden;
                m_IconSelected.style.position = Position.Absolute;
                m_IconSelected.style.backgroundImage = new StyleBackground(spriteIcon2);
                m_IconSelected.style.width = 65;
                m_IconSelected.style.height = 77;
                m_IconWrapper.Add(m_IconSelected);

                m_IconActive = new VisualElement();
                m_IconActive.style.visibility = Visibility.Hidden;
                m_IconActive.style.position = Position.Absolute;
                m_IconActive.style.backgroundImage = new StyleBackground(spriteIcon1);
                m_IconActive.style.width = 65;
                m_IconActive.style.height = 77;

                m_IconWrapper.Add(m_IconActive);

                m_IconDisabled = new VisualElement();
                m_IconDisabled.style.visibility = Visibility.Hidden;
                m_IconDisabled.style.position = Position.Absolute;
                m_IconDisabled.style.backgroundImage = new StyleBackground(spriteIcon0);
                m_IconDisabled.style.width = 65;
                m_IconDisabled.style.height = 77;
                m_IconWrapper.Add(m_IconDisabled);

                m_Container.Add(m_IconWrapper);
            }

            // Add container to root
            uiRoot.Add(m_Container);

            SetIconState(IconState.Active);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
