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

    // attribution
    private VisualElement attributionVE;
    private Label attributionLbl;

    private void Start()
    {
        MakeBindings();

        SafeArea.ApplySafeArea(rootVE);
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
        DefineAttributionElements();
    }

    private void DefineStudioUIElements()
    {
        studioInfoVE = rootVE.Q<VisualElement>("studio_info_ve");
        studioSocialAccountsVE = studioInfoVE.Q<VisualElement>("social_accounts_ve");

        studioFbButton = studioSocialAccountsVE.Q<Button>("facebook");
        studioInstaButton = studioSocialAccountsVE.Q<Button>("instagram");
        studioYoutubeButton = studioSocialAccountsVE.Q<Button>("youtube");

        studioFbButton.clicked += () => GoToStudioFbAccount();
        studioInstaButton.clicked += () => GoToStudioInstaAccount();
        studioYoutubeButton.clicked += () => GoToStudioYoutubeAccount();
    }

    private void DefineMemberOneUIElements()
    {
        memberOneVE = membersVE.Q<VisualElement>("member_one");
        memberOneSocialAccountsVE = memberOneVE.Q<VisualElement>("social_accounts_ve");

        memberOneFbButton = memberOneSocialAccountsVE.Q<Button>("facebook");
        memberOneInstaButton = memberOneSocialAccountsVE.Q<Button>("instagram");
        memberOneLinkedinButton = memberOneSocialAccountsVE.Q<Button>("linkedin");
        memberOneYoutubeButton = memberOneSocialAccountsVE.Q<Button>("youtube");

        memberOneFbButton.clicked += () => GoToMemberOneFbAccount();
        memberOneInstaButton.clicked += () => GoToMemberOneInstaAccount();
        memberOneLinkedinButton.clicked += () => GoToMemberOneLinkedinAccount();
        memberOneYoutubeButton.clicked += () => GoToMemberOneYoutubeAccount();
    }

    private void DefineMemberTwoUIElements()
    {
        memberTwoVE = membersVE.Q<VisualElement>("member_two");
        memberTwoSocialAccountsVE = memberTwoVE.Q<VisualElement>("social_accounts_ve");

        memberTwoFbButton = memberTwoSocialAccountsVE.Q<Button>("facebook");
        memberTwoInstaButton = memberTwoSocialAccountsVE.Q<Button>("instagram");
        memberTwoLinkedinButton = memberTwoSocialAccountsVE.Q<Button>("linkedin");
        memberTwoXButton = memberTwoSocialAccountsVE.Q<Button>("x");

        memberTwoFbButton.clicked += () => GoToMemberTwoFbAccount();
        memberTwoInstaButton.clicked += () => GoToMemberTwoInstaAccount();
        memberTwoLinkedinButton.clicked += () => GoToMemberTwoLinkedinAccount();
        memberTwoXButton.clicked += () => GoToMemberTwoXAccount();

        homeButton.clicked += () => stateChanger.ChangeStateToMainUIWithoutLoadPage();
    }

    private void DefineAttributionElements()
    {
        attributionVE = membersVE.Q<VisualElement>("attribution_ve");
        attributionLbl = attributionVE.Q<Label>("attribution_lbl");

        attributionLbl.RegisterCallback<ClickEvent>(ev =>
        {
            GoToFreepik();
        });
    }

    private void GoToStudioFbAccount()
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=61563914828343");
    }

    private void GoToStudioInstaAccount()
    {
        Application.OpenURL("https://www.instagram.com/massivedreamersstudio");
    }

    private void GoToStudioYoutubeAccount()
    {
        Application.OpenURL("/not_yet_initialized");
    }

    private void GoToMemberOneFbAccount()
    {
        Application.OpenURL("https://www.facebook.com/aqsins");
    }

    private void GoToMemberOneInstaAccount()
    {
        Application.OpenURL("https://www.instagram.com/aqsin.sulxayev/");
    }

    private void GoToMemberOneLinkedinAccount()
    {
        Application.OpenURL("https://www.linkedin.com/in/agshin-sulkhayev-427a27106");
    }

    private void GoToMemberOneYoutubeAccount()
    {
        Application.OpenURL("https://www.youtube.com/@aqsinsulxayev");
    }

    private void GoToMemberTwoFbAccount()
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=100018470349191");
    }

    private void GoToMemberTwoInstaAccount()
    {
        Application.OpenURL("https://www.instagram.com/turxan_d/");
    }

    private void GoToMemberTwoLinkedinAccount()
    {
        Application.OpenURL("https://www.linkedin.com/in/turxan-dunyamal%C4%B1yev-753339154/");
    }

    private void GoToMemberTwoXAccount()
    {
        Application.OpenURL("https://x.com/TurxanDunya");
    }

    private void GoToFreepik()
    {
        Application.OpenURL("https://www.freepik.com/");
    }

    public void SetDisplayFlex()
    {
        rootVE.style.display = DisplayStyle.Flex;
    }

    public void SetDisplayNone()
    {
        rootVE.style.display = DisplayStyle.None;
    }

    public bool IsOverUI()
    {
        return rootVE.style.display == DisplayStyle.Flex;
    }

}
