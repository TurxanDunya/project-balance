using UnityEngine;
using UnityEngine.UIElements;

public class AboutUsController : MonoBehaviour, IControllerTemplate
{
    [SerializeField] private StateChanger stateChanger;

    private VisualElement rootVE;
    private VisualElement topVE;
    private Button homeButton;

    private VisualElement membersVE;

    // studio
    private VisualElement studioInfoVE;
    private VisualElement studioSocialAccountsVE;
    private Button studioFbButton;
    private Button studioInstaButton;
    private Button studioYoutubeButton;

    // member one
    private VisualElement memberOneVE;
    private VisualElement memberOneSocialAccountsVE;
    private Button memberOneFbButton;
    private Button memberOneInstaButton;
    private Button memberOneLinkedinButton;
    private Button memberOneYoutubeButton;

    // member two
    private VisualElement memberTwoVE;
    private VisualElement memberTwoSocialAccountsVE;
    private Button memberTwoFbButton;
    private Button memberTwoInstaButton;
    private Button memberTwoLinkedinButton;
    private Button memberTwoXButton;

    private void Start()
    {
        MakeBindings();
        SafeArea.ApplySafeArea(rootVE);
    }

    private void OnDisable()
    {
        studioFbButton.UnregisterCallback<PointerEnterEvent>(GoToStudioFbAccount);
        studioFbButton.UnregisterCallback<PointerLeaveEvent>(LeaveStudioFbAccount);

        studioInstaButton.UnregisterCallback<PointerEnterEvent>(GoToStudioInstaAccount);
        studioInstaButton.UnregisterCallback<PointerLeaveEvent>(LeaveStudioInstaAccount);

        studioYoutubeButton.UnregisterCallback<PointerEnterEvent>(GoToStudioYoutubeAccount);
        studioYoutubeButton.UnregisterCallback<PointerLeaveEvent>(LeaveStudioYoutubeAccount);

        memberOneFbButton.RegisterCallback<PointerEnterEvent>(GoToMemberOneFbAccount);
        memberOneFbButton.RegisterCallback<PointerLeaveEvent>(LeaveMemberOneFbAccount);

        memberOneInstaButton.UnregisterCallback<PointerEnterEvent>(GoToMemberOneInstaAccount);
        memberOneInstaButton.UnregisterCallback<PointerLeaveEvent>(LeaveMemberOneInstaAccount);

        memberOneLinkedinButton.UnregisterCallback<PointerEnterEvent>(GoToMemberOneLinkedinAccount);
        memberOneLinkedinButton.UnregisterCallback<PointerLeaveEvent>(LeaveMemberOneLinkedinAccount);

        memberOneYoutubeButton.UnregisterCallback<PointerEnterEvent>(GoToMemberOneYoutubeAccount);
        memberOneYoutubeButton.UnregisterCallback<PointerLeaveEvent>(LeaveMemberOneYoutubeAccount);

        memberTwoFbButton.UnregisterCallback<PointerEnterEvent>(GoToMemberTwoFbAccount);
        memberTwoFbButton.UnregisterCallback<PointerLeaveEvent>(LeaveMemberTwoFbAccount);

        memberTwoInstaButton.UnregisterCallback<PointerEnterEvent>(GoToMemberTwoInstaAccount);
        memberTwoInstaButton.UnregisterCallback<PointerLeaveEvent>(LeaveMemberTwoInstaAccount);

        memberTwoLinkedinButton.UnregisterCallback<PointerEnterEvent>(GoToMemberTwoLinkedinAccount);
        memberTwoLinkedinButton.UnregisterCallback<PointerLeaveEvent>(LeaveMemberTwoLinkedinAccount);

        memberTwoXButton.UnregisterCallback<PointerEnterEvent>(GoToMemberTwoXAccount);
        memberTwoXButton.UnregisterCallback<PointerLeaveEvent>(LeaveMemberTwoXAccount);

        homeButton.clicked -= stateChanger.ChangeStateToMainUIWithoutLoadPage;
    }

    public void MakeBindings()
    {
        rootVE = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("rootVE");

        topVE = rootVE.Q<VisualElement>("top_ve");
        homeButton = topVE.Q<Button>("home_btn");

        membersVE = rootVE.Q<VisualElement>("members_ve");

        DefineStudioUIElements();
        DefineMemberOneUIElements();
        DefineMemberTwoUIElements();

        homeButton.RegisterCallback<PointerEnterEvent>(ReturnHomePagePress);
        homeButton.RegisterCallback<PointerLeaveEvent>(ReturnHomePageRelease);
    }

    private void DefineStudioUIElements()
    {
        studioInfoVE = rootVE.Q<VisualElement>("studio_info_ve");
        studioSocialAccountsVE = studioInfoVE.Q<VisualElement>("social_accounts_ve");

        studioFbButton = studioSocialAccountsVE.Q<Button>("facebook");
        studioInstaButton = studioSocialAccountsVE.Q<Button>("instagram");
        studioYoutubeButton = studioSocialAccountsVE.Q<Button>("youtube");

        studioFbButton.RegisterCallback<PointerEnterEvent>(GoToStudioFbAccount);
        studioFbButton.RegisterCallback<PointerLeaveEvent>(LeaveStudioFbAccount);

        studioInstaButton.RegisterCallback<PointerEnterEvent>(GoToStudioInstaAccount);
        studioInstaButton.RegisterCallback<PointerLeaveEvent>(LeaveStudioInstaAccount);

        studioYoutubeButton.RegisterCallback<PointerEnterEvent>(GoToStudioYoutubeAccount);
        studioYoutubeButton.RegisterCallback<PointerLeaveEvent>(LeaveStudioYoutubeAccount);
    }

    private void DefineMemberOneUIElements()
    {
        memberOneVE = membersVE.Q<VisualElement>("member_one");
        memberOneSocialAccountsVE = memberOneVE.Q<VisualElement>("social_accounts_ve");

        memberOneFbButton = memberOneSocialAccountsVE.Q<Button>("facebook");
        memberOneInstaButton = memberOneSocialAccountsVE.Q<Button>("instagram");
        memberOneLinkedinButton = memberOneSocialAccountsVE.Q<Button>("linkedin");
        memberOneYoutubeButton = memberOneSocialAccountsVE.Q<Button>("youtube");

        memberOneFbButton.RegisterCallback<PointerEnterEvent>(GoToMemberOneFbAccount);
        memberOneFbButton.RegisterCallback<PointerLeaveEvent>(LeaveMemberOneFbAccount);

        memberOneInstaButton.RegisterCallback<PointerEnterEvent>(GoToMemberOneInstaAccount);
        memberOneInstaButton.RegisterCallback<PointerLeaveEvent>(LeaveMemberOneInstaAccount);

        memberOneLinkedinButton.RegisterCallback<PointerEnterEvent>(GoToMemberOneLinkedinAccount);
        memberOneLinkedinButton.RegisterCallback<PointerLeaveEvent>(LeaveMemberOneLinkedinAccount);

        memberOneYoutubeButton.RegisterCallback<PointerEnterEvent>(GoToMemberOneYoutubeAccount);
        memberOneYoutubeButton.RegisterCallback<PointerLeaveEvent>(LeaveMemberOneYoutubeAccount);
    }

    private void DefineMemberTwoUIElements()
    {
        memberTwoVE = membersVE.Q<VisualElement>("member_two");
        memberTwoSocialAccountsVE = memberTwoVE.Q<VisualElement>("social_accounts_ve");

        memberTwoFbButton = memberTwoSocialAccountsVE.Q<Button>("facebook");
        memberTwoInstaButton = memberTwoSocialAccountsVE.Q<Button>("instagram");
        memberTwoLinkedinButton = memberTwoSocialAccountsVE.Q<Button>("linkedin");
        memberTwoXButton = memberTwoSocialAccountsVE.Q<Button>("x");

        memberTwoFbButton.RegisterCallback<PointerEnterEvent>(GoToMemberTwoFbAccount);
        memberTwoFbButton.RegisterCallback<PointerLeaveEvent>(LeaveMemberTwoFbAccount);

        memberTwoInstaButton.RegisterCallback<PointerEnterEvent>(GoToMemberTwoInstaAccount);
        memberTwoInstaButton.RegisterCallback<PointerLeaveEvent>(LeaveMemberTwoInstaAccount);

        memberTwoLinkedinButton.RegisterCallback<PointerEnterEvent>(GoToMemberTwoLinkedinAccount);
        memberTwoLinkedinButton.RegisterCallback<PointerLeaveEvent>(LeaveMemberTwoLinkedinAccount);

        memberTwoXButton.RegisterCallback<PointerEnterEvent>(GoToMemberTwoXAccount);
        memberTwoXButton.RegisterCallback<PointerLeaveEvent>(LeaveMemberTwoXAccount);
    }

    private void GoToStudioFbAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveStudioFbAccount(PointerLeaveEvent ev)
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=61563914828343");
        InputManager.isOverUI = false;
    }

    private void GoToStudioInstaAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveStudioInstaAccount(PointerLeaveEvent ev)
    {
        Application.OpenURL("https://www.instagram.com/massivedreamersstudio");
        InputManager.isOverUI = false;
    }

    private void GoToStudioYoutubeAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveStudioYoutubeAccount(PointerLeaveEvent ev)
    {
        Application.OpenURL("/not_yet_initialized");
        InputManager.isOverUI = false;
    }

    private void GoToMemberOneFbAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberOneFbAccount(PointerLeaveEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.facebook.com/aqsins");
    }

    private void GoToMemberOneInstaAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberOneInstaAccount(PointerLeaveEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.instagram.com/aqsin.sulxayev/");
    }

    private void GoToMemberOneLinkedinAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberOneLinkedinAccount(PointerLeaveEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.linkedin.com/in/agshin-sulkhayev-427a27106");
    }

    private void GoToMemberOneYoutubeAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberOneYoutubeAccount(PointerLeaveEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.youtube.com/@aqsinsulxayev");
    }

    private void GoToMemberTwoFbAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberTwoFbAccount(PointerLeaveEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.facebook.com/profile.php?id=100018470349191");
    }

    private void GoToMemberTwoInstaAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberTwoInstaAccount(PointerLeaveEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.instagram.com/turxan_d/");
    }

    private void GoToMemberTwoLinkedinAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberTwoLinkedinAccount(PointerLeaveEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.linkedin.com/in/turxan-dunyamal%C4%B1yev-753339154/");
    }

    private void GoToMemberTwoXAccount(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberTwoXAccount(PointerLeaveEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://x.com/TurxanDunya");
    }

    private void ReturnHomePagePress(PointerEnterEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ReturnHomePageRelease(PointerLeaveEvent ev)
    {
        stateChanger.ChangeStateToMainUIWithoutLoadPage();
        InputManager.isOverUI = false;
    }

    public void SetDisplayFlex()
    {
        InputManager.isOverUI = true;
        rootVE.style.display = DisplayStyle.Flex;
    }

    public void SetDisplayNone()
    {
        InputManager.isOverUI = true;
        rootVE.style.display = DisplayStyle.None;
    }

}
