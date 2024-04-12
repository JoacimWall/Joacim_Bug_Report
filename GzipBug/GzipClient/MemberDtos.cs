using System;
namespace GzipBug;


public class ForgotPasswordRequest
{
    public string Email { get; set; }
}
public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class CompleteRegisterRequest
{
    public CompleteRegisterRequest()
    {
        Childs = new List<AddChildRequest>();
    }
    public List<AddChildRequest> Childs { get; set; }
    public DateTime? PregnencyDate { get; set; }
    public bool ICarryTheBaby { get; set; }
}
public class SingleSignOnResponse
{
    #region api propertys
    public string RedirectUrl { get; set; }
    #endregion


    #region non-api propertys
    #endregion

}

public class IsReconsentRequiredResponse
{
    #region api properties
    public bool IsReconsentRequired { get; set; }
    public string Message { get; set; }
    public string ReconsentUrl { get; set; }
    #endregion
}
public class Authentication
{
    #region api properties
    public string Logintoken { get; set; } //"3dd982bf-1c87-4d8a-98a2-1a5c99930733"
    public string LogintokenExpiration { get; set; } // "2018-05-18T14:17:15.00Z"
    #endregion
}
public class LoginResponse
{
    #region api properties
    public Authentication Authentication { get; set; }
    public UserDetails UserDetails { get; set; }
    public bool BisnodeFailure { get; set; }
    #endregion
}

public class UserDetails
{
    public UserDetails()
    {
        Children = new List<Children>();

    }
    #region api properties
    public int GbwUserId { get; set; }
    public string Firstname { get; set; }
    public string MembershipNumber { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public int FamilyMemberProfileId { get; set; }
    public int UserStatus { get; set; } //Use property BdpUserStatus below


    public DateTime? PregnancyDueDate { get; set; }
    public string ProfileImageUrl { get; set; }
    public bool IsOnboarded { get; set; }
    //removed for backend 2 to speed up backend 
    //public bool IsReconsentRequired { get; set; }
    public string ReconsentMessage { get; set; }

    public string CountryIsoCode { get; set; }

    public string ReconsentUrl { get; set; }

    public bool IsAccountInactivated { get; set; }

    public FamilyDetails FamilyDetails { get; set; }
    public List<Children> Children { get; set; }
    public AddressForm AddressForm { get; set; }

    public List<FamilyInviteDetails> FamilyInviteDetails { get; set; }
    public CoParentDetails CoParentDetails { get; set; }
    public List<StoryTags> Tags { get; set; }
    #endregion
    #region non-api properties
    // Liberoclub
    public BdpUserStatus BdpUserStatus
    {
        get
        {
            return (BdpUserStatus)(UserStatus);
        }
    }
    // Libero Light
    public string UserLanguage { get; set; }

    /// ///////
    /// 
    public bool IsFamilyOwner()
    {
        return GbwUserId == FamilyDetails.CreatedByGbwUserId;

    }
    public bool HasPartner()
    {
        return CoParentDetails != null
               && !string.IsNullOrEmpty(CoParentDetails.Email);
    }

    public bool HasPregnancyAndChildLessThanFourYears(UserDetails userDetails)
    {
        return userDetails.Children != null
               && (userDetails.Children.Any(x => x.BirthDate > DateTime.Now.AddYears(-4))
               || userDetails.Children.Any(x => x.BirthDate > DateTime.Now));
    }

    public bool HasPregnancyDateLessThanEightWeeksAway(UserDetails userDetails)
    {

        return userDetails.PregnancyDueDate != null
               && (userDetails.PregnancyDueDate < DateTime.Now.AddDays(56));
    }
    public bool HasPregnancyDateLessThanFourWeeksAway(UserDetails userDetails)
    {

        return userDetails.PregnancyDueDate != null
               && (userDetails.PregnancyDueDate < DateTime.Now.AddDays(28));
    }
    public bool HasOnlyPregnancy(UserDetails userDetails)
    {
        var res = false;

        if (userDetails.PregnancyDueDate != null)
        {
            if (Children != null && Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    if (child.BirthDate == userDetails.PregnancyDueDate)
                    {
                        res = true;
                    }
                    else
                    {
                        if (!child.Hidden)
                        {
                            res = false;
                            break;
                        }
                    }
                }
            }
        }


        return res;
    }

    public bool HasNoActiveChildrenOrPregnancy()
    {
        if (Children == null)
            return true;
        if (Children.Count == 0)
            return true;

        if (Children.Count > 0)
        {
            bool onlyHiddenChildren = true;
            foreach (var child in Children)
            {
                if (child.BirthDate > DateTime.Now)
                {
                    onlyHiddenChildren = false; // We have at least a pregnancy
                    break;
                }
                if (!child.Hidden)
                {
                    onlyHiddenChildren = false; //We have at least one active child
                    break;
                }
            }
            return onlyHiddenChildren;
        }
        return false;
    }

    public bool HasActiveChildren()
    {
        if (Children == null)
            return false;
        if (Children.Count == 0)
            return false;

        var hasAtleastOneActiveChild = false;

        if (Children.Count > 0)
        {
            foreach (var child in Children)
            {
                if (!child.Hidden)
                {
                    hasAtleastOneActiveChild = true; //We have at least one active child
                    break;
                }
            }

        }
        return hasAtleastOneActiveChild;
    }
    #endregion
}
public enum BdpUserStatus
{
    Registered,
    Confirmed,
    Onboarded,
    Locked,
    Frozen,
    Quarantine,
    PreOnboarded,
    Migrated,
    UserDataInvalid
}
public class FamilyInviteDetails
{
    #region api properties
    public int Id { get; set; }
    public string ToEmail { get; set; }
    public int InviteType { get; set; }
    public string FamilyName { get; set; }

    #endregion
}
public class CoParentDetails
{
    #region api properties
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string ProfileImageUrl { get; set; }
    #endregion
}
public class FamilyDetails
{
    #region api properties
    public int FamilyId { get; set; }
    public string FamilyName { get; set; }
    public string FamilyImageUrl { get; set; }
    public int CreatedByGbwUserId { get; set; }

    #endregion
}
public class Children
{
    #region api properties
    public int ChildId { get; set; }
    public int FamilyMemberProfileId { get; set; }
    public string ChildName { get; set; }
    public DateTime BirthDate { get; set; }
    public bool Hidden { get; set; }
    public string ProfileImageUrl { get; set; }
    public string ChildAge { get; set; }
    public bool BirthdayVerified { get; set; }
    #endregion
    #region Non Api propertys
    public string BirthDateCaption { get { return $"{BirthDate:d MMMM, yyyy}"; } }
    public EventActionEnum EventAction { get; set; }


    public enum EventActionEnum
    {
        Remove = 1,
        RemovePregnancy = 2,
        DeleteAccount = 3
    }
    #endregion
}
public class AddressForm
{
    #region api properties
    public string CoAddress { get; set; }
    public string StreetAddress1 { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string CellPhone { get; set; }
    #endregion
}
public class StoryTags
{
    #region api properties
    public string Title { get; set; }
    public int Id { get; set; }
    public string ProfileImageUrl { get; set; }
    #endregion
}
public class InviteRequest
{
    //public string UserId { get; set; }
    public string InviteEmail { get; set; }
    public string InvitePhoneNumber { get; set; }

}

public class AddChildRequest
{
    public AddChildRequest()
    {
        ChildName = "";
        BirthDate = DateTime.Now;
        IsPregnancy = false;
        Image = string.Empty;
    }
    public string ChildName { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsPregnancy { get; set; }
    public bool IsPartnerPregnant { get; set; }

    public string Image { get; set; }

    //Non API propertys
    public DateTime DateToDay { get { return DateTime.Now; } }
    public bool ShowRemove { get; set; }
    public int Id { get; set; }
}
public class AdditionalInfoRequest
{
    public string StreetAddress { get; set; }
    public string CoAddress { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public Boolean Skip { get; set; }
    public string CountryIsoCode { get; set; }

    //Non API propertys


}

public class RegisterRequest
{

    //public string SSN { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string CoAddress { get; set; }
    public string StreetAddress1 { get; set; }

    public string PostalCode { get; set; }
    public string City { get; set; }
    public string CellPhone { get; set; }

    public Boolean AgreeToTerms { get; set; }

    //Non API propertys


}

public class Media
{
    public string Image { get; set; }
}
public class UpdateFamilyProfile
{
    public int FamilyProfileId { get; set; }
    public DateTime BirthDate { get; set; }
    public string Name { get; set; }
    public Media Media { get; set; }
    public bool IsPregnancy { get; set; }
    public bool RemoveProfileImage { get; set; }
}

public class ContactDetails
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string CoAddress { get; set; }
    public string StreetAddress1 { get; set; }
    public string StreetAddress2 { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string CellPhone { get; set; }
    public string CountryIsoCode { get; set; }
}
public class UpdateEmailRequest
{
    public string CurrentEmail { get; set; }
    public string NewEmail { get; set; }
}
public class GamificationRequest
{
    public string Id { get; set; } = "capturepocket";
}
public class GamificationResponse
{
    //0 - Success
    //1 - AlreadyPlayed
    //2 - Error
    public int Status { get; set; }
    public string Message { get; set; } = "";
}


