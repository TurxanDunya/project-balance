using UnityEngine;
using UnityEngine.UIElements;

public class AboutUsController : MonoBehaviour
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

    private void Start()
    {
        rootVE = GetComponent<UIDocument>()
            .rootVisualElement.Q<VisualElement>("rootVE");

        topVE = rootVE.Q<VisualElement>("top_ve");
        homeButton = topVE.Q<Button>("home_btn");

        membersVE = rootVE.Q<VisualElement>("members_ve");

        DefineStudioUIElements();
        DefineMemberOneUIElements();
        DefineMemberTwoUIElements();
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

        memberTwoFbButton.clicked += () => GoToMemberTwoFbAccount();
        memberTwoInstaButton.clicked += () => GoToMemberTwoInstaAccount();
        memberTwoLinkedinButton.clicked += () => GoToMemberTwoLinkedinAccount();

        homeButton.clicked += () => stateChanger.ChangeStateToMainUIWithoutLoadPage();
    }

    private void GoToStudioFbAccount()
    {
        Application.OpenURL("/mock");
    }

    private void GoToStudioInstaAccount()
    {
        Application.OpenURL("/mock");
    }

    private void GoToStudioYoutubeAccount()
    {
        Application.OpenURL("/mock");
    }

    private void GoToMemberOneFbAccount()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=w7l5yU_ZQrE&list=LL&index=32");
    }

    private void GoToMemberOneInstaAccount()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=w7l5yU_ZQrE&list=LL&index=32");
    }

    private void GoToMemberOneLinkedinAccount()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=w7l5yU_ZQrE&list=LL&index=32");
    }

    private void GoToMemberOneYoutubeAccount()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=w7l5yU_ZQrE&list=LL&index=32");
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
        Application.OpenURL("https://www.youtube.com/watch?v=w7l5yU_ZQrE&list=LL&index=32");
    }

}
