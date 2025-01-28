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
        studioFbButton.UnregisterCallback<PointerDownEvent>(GoToStudioFbAccount);
        studioFbButton.UnregisterCallback<PointerUpEvent>(LeaveStudioFbAccount);

        studioInstaButton.UnregisterCallback<PointerDownEvent>(GoToStudioInstaAccount);
        studioInstaButton.UnregisterCallback<PointerUpEvent>(LeaveStudioInstaAccount);

        studioYoutubeButton.UnregisterCallback<PointerDownEvent>(GoToStudioYoutubeAccount);
        studioYoutubeButton.UnregisterCallback<PointerUpEvent>(LeaveStudioYoutubeAccount);

        memberOneFbButton.RegisterCallback<PointerDownEvent>(GoToMemberOneFbAccount);
        memberOneFbButton.RegisterCallback<PointerUpEvent>(LeaveMemberOneFbAccount);

        memberOneInstaButton.UnregisterCallback<PointerDownEvent>(GoToMemberOneInstaAccount);
        memberOneInstaButton.UnregisterCallback<PointerUpEvent>(LeaveMemberOneInstaAccount);

        memberOneLinkedinButton.UnregisterCallback<PointerDownEvent>(GoToMemberOneLinkedinAccount);
        memberOneLinkedinButton.UnregisterCallback<PointerUpEvent>(LeaveMemberOneLinkedinAccount);

        memberOneYoutubeButton.UnregisterCallback<PointerDownEvent>(GoToMemberOneYoutubeAccount);
        memberOneYoutubeButton.UnregisterCallback<PointerUpEvent>(LeaveMemberOneYoutubeAccount);

        memberTwoFbButton.UnregisterCallback<PointerDownEvent>(GoToMemberTwoFbAccount);
        memberTwoFbButton.UnregisterCallback<PointerUpEvent>(LeaveMemberTwoFbAccount);

        memberTwoInstaButton.UnregisterCallback<PointerDownEvent>(GoToMemberTwoInstaAccount);
        memberTwoInstaButton.UnregisterCallback<PointerUpEvent>(LeaveMemberTwoInstaAccount);

        memberTwoLinkedinButton.UnregisterCallback<PointerDownEvent>(GoToMemberTwoLinkedinAccount);
        memberTwoLinkedinButton.UnregisterCallback<PointerUpEvent>(LeaveMemberTwoLinkedinAccount);

        memberTwoXButton.UnregisterCallback<PointerDownEvent>(GoToMemberTwoXAccount);
        memberTwoXButton.UnregisterCallback<PointerUpEvent>(LeaveMemberTwoXAccount);

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

        homeButton.RegisterCallback<PointerDownEvent>(ReturnHomePagePress);
        homeButton.RegisterCallback<PointerUpEvent>(ReturnHomePageRelease);
    }

    private void DefineStudioUIElements()
    {
        studioInfoVE = rootVE.Q<VisualElement>("studio_info_ve");
        studioSocialAccountsVE = studioInfoVE.Q<VisualElement>("social_accounts_ve");

        studioFbButton = studioSocialAccountsVE.Q<Button>("facebook");
        studioInstaButton = studioSocialAccountsVE.Q<Button>("instagram");
        studioYoutubeButton = studioSocialAccountsVE.Q<Button>("youtube");

        studioFbButton.RegisterCallback<PointerDownEvent>(GoToStudioFbAccount);
        studioFbButton.RegisterCallback<PointerUpEvent>(LeaveStudioFbAccount);

        studioInstaButton.RegisterCallback<PointerDownEvent>(GoToStudioInstaAccount);
        studioInstaButton.RegisterCallback<PointerUpEvent>(LeaveStudioInstaAccount);

        studioYoutubeButton.RegisterCallback<PointerDownEvent>(GoToStudioYoutubeAccount);
        studioYoutubeButton.RegisterCallback<PointerUpEvent>(LeaveStudioYoutubeAccount);
    }

    private void DefineMemberOneUIElements()
    {
        memberOneVE = membersVE.Q<VisualElement>("member_one");
        memberOneSocialAccountsVE = memberOneVE.Q<VisualElement>("social_accounts_ve");

        memberOneFbButton = memberOneSocialAccountsVE.Q<Button>("facebook");
        memberOneInstaButton = memberOneSocialAccountsVE.Q<Button>("instagram");
        memberOneLinkedinButton = memberOneSocialAccountsVE.Q<Button>("linkedin");
        memberOneYoutubeButton = memberOneSocialAccountsVE.Q<Button>("youtube");

        memberOneFbButton.RegisterCallback<PointerDownEvent>(GoToMemberOneFbAccount);
        memberOneFbButton.RegisterCallback<PointerUpEvent>(LeaveMemberOneFbAccount);

        memberOneInstaButton.RegisterCallback<PointerDownEvent>(GoToMemberOneInstaAccount);
        memberOneInstaButton.RegisterCallback<PointerUpEvent>(LeaveMemberOneInstaAccount);

        memberOneLinkedinButton.RegisterCallback<PointerDownEvent>(GoToMemberOneLinkedinAccount);
        memberOneLinkedinButton.RegisterCallback<PointerUpEvent>(LeaveMemberOneLinkedinAccount);

        memberOneYoutubeButton.RegisterCallback<PointerDownEvent>(GoToMemberOneYoutubeAccount);
        memberOneYoutubeButton.RegisterCallback<PointerUpEvent>(LeaveMemberOneYoutubeAccount);
    }

    private void DefineMemberTwoUIElements()
    {
        memberTwoVE = membersVE.Q<VisualElement>("member_two");
        memberTwoSocialAccountsVE = memberTwoVE.Q<VisualElement>("social_accounts_ve");

        memberTwoFbButton = memberTwoSocialAccountsVE.Q<Button>("facebook");
        memberTwoInstaButton = memberTwoSocialAccountsVE.Q<Button>("instagram");
        memberTwoLinkedinButton = memberTwoSocialAccountsVE.Q<Button>("linkedin");
        memberTwoXButton = memberTwoSocialAccountsVE.Q<Button>("x");

        memberTwoFbButton.RegisterCallback<PointerDownEvent>(GoToMemberTwoFbAccount);
        memberTwoFbButton.RegisterCallback<PointerUpEvent>(LeaveMemberTwoFbAccount);

        memberTwoInstaButton.RegisterCallback<PointerDownEvent>(GoToMemberTwoInstaAccount);
        memberTwoInstaButton.RegisterCallback<PointerUpEvent>(LeaveMemberTwoInstaAccount);

        memberTwoLinkedinButton.RegisterCallback<PointerDownEvent>(GoToMemberTwoLinkedinAccount);
        memberTwoLinkedinButton.RegisterCallback<PointerUpEvent>(LeaveMemberTwoLinkedinAccount);

        memberTwoXButton.RegisterCallback<PointerDownEvent>(GoToMemberTwoXAccount);
        memberTwoXButton.RegisterCallback<PointerUpEvent>(LeaveMemberTwoXAccount);
    }

    private void GoToStudioFbAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveStudioFbAccount(PointerUpEvent ev)
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=61563914828343");
        InputManager.isOverUI = false;
    }

    private void GoToStudioInstaAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveStudioInstaAccount(PointerUpEvent ev)
    {
        Application.OpenURL("https://www.instagram.com/massivedreamersstudio");
        InputManager.isOverUI = false;
    }

    private void GoToStudioYoutubeAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveStudioYoutubeAccount(PointerUpEvent ev)
    {
        Application.OpenURL("/not_yet_initialized");
        InputManager.isOverUI = false;
    }

    private void GoToMemberOneFbAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberOneFbAccount(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.facebook.com/aqsins");
    }

    private void GoToMemberOneInstaAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberOneInstaAccount(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.instagram.com/aqsin.sulxayev/");
    }

    private void GoToMemberOneLinkedinAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberOneLinkedinAccount(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.linkedin.com/in/agshin-sulkhayev-427a27106");
    }

    private void GoToMemberOneYoutubeAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberOneYoutubeAccount(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.youtube.com/@aqsinsulxayev");
    }

    private void GoToMemberTwoFbAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberTwoFbAccount(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.facebook.com/profile.php?id=100018470349191");
    }

    private void GoToMemberTwoInstaAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberTwoInstaAccount(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.instagram.com/turxan_d/");
    }

    private void GoToMemberTwoLinkedinAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberTwoLinkedinAccount(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://www.linkedin.com/in/turxan-dunyamal%C4%B1yev-753339154/");
    }

    private void GoToMemberTwoXAccount(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void LeaveMemberTwoXAccount(PointerUpEvent ev)
    {
        InputManager.isOverUI = false;
        Application.OpenURL("https://x.com/TurxanDunya");
    }

    private void ReturnHomePagePress(PointerDownEvent ev)
    {
        InputManager.isOverUI = true;
    }

    private void ReturnHomePageRelease(PointerUpEvent ev)
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
