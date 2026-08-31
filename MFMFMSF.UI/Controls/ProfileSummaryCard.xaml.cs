using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Controls
{
    public partial class ProfileSummaryCard : UserControl
    {
        public ProfileSummaryCard()
        {
            InitializeComponent();
        }


        // =====================================================
        // PROFILE IMAGE
        // =====================================================

        public ImageSource ProfileImage
        {
            get => (ImageSource)GetValue(ProfileImageProperty);
            set => SetValue(ProfileImageProperty, value);
        }

        public static readonly DependencyProperty ProfileImageProperty =
            DependencyProperty.Register(
                nameof(ProfileImage),
                typeof(ImageSource),
                typeof(ProfileSummaryCard),
                new PropertyMetadata(null));


        // =====================================================
        // FULL NAME
        // =====================================================

        public string FullName
        {
            get => (string)GetValue(FullNameProperty);
            set => SetValue(FullNameProperty, value);
        }

        public static readonly DependencyProperty FullNameProperty =
            DependencyProperty.Register(
                nameof(FullName),
                typeof(string),
                typeof(ProfileSummaryCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // MEMBER ID
        // =====================================================

        public string MemberId
        {
            get => (string)GetValue(MemberIdProperty);
            set => SetValue(MemberIdProperty, value);
        }

        public static readonly DependencyProperty MemberIdProperty =
            DependencyProperty.Register(
                nameof(MemberId),
                typeof(string),
                typeof(ProfileSummaryCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // STATUS
        // =====================================================

        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register(
                nameof(Status),
                typeof(string),
                typeof(ProfileSummaryCard),
                new PropertyMetadata("Active Member"));


        // =====================================================
        // PHONE
        // =====================================================

        public string Phone
        {
            get => (string)GetValue(PhoneProperty);
            set => SetValue(PhoneProperty, value);
        }

        public static readonly DependencyProperty PhoneProperty =
            DependencyProperty.Register(
                nameof(Phone),
                typeof(string),
                typeof(ProfileSummaryCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // PHONE LABEL
        // =====================================================

        public string PhoneLabel
        {
            get => (string)GetValue(PhoneLabelProperty);
            set => SetValue(PhoneLabelProperty, value);
        }

        public static readonly DependencyProperty PhoneLabelProperty =
            DependencyProperty.Register(
                nameof(PhoneLabel),
                typeof(string),
                typeof(ProfileSummaryCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // EMAIL
        // =====================================================

        public string Email
        {
            get => (string)GetValue(EmailProperty);
            set => SetValue(EmailProperty, value);
        }

        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register(
                nameof(Email),
                typeof(string),
                typeof(ProfileSummaryCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // JOINED DATE
        // =====================================================

        public string JoinedDate
        {
            get => (string)GetValue(JoinedDateProperty);
            set => SetValue(JoinedDateProperty, value);
        }

        public static readonly DependencyProperty JoinedDateProperty =
            DependencyProperty.Register(
                nameof(JoinedDate),
                typeof(string),
                typeof(ProfileSummaryCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // ADDRESS
        // =====================================================

        public string Address
        {
            get => (string)GetValue(AddressProperty);
            set => SetValue(AddressProperty, value);
        }

        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register(
                nameof(Address),
                typeof(string),
                typeof(ProfileSummaryCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // MARITAL STATUS
        // =====================================================

        public string MaritalStatus
        {
            get => (string)GetValue(MaritalStatusProperty);
            set => SetValue(MaritalStatusProperty, value);
        }

        public static readonly DependencyProperty MaritalStatusProperty =
            DependencyProperty.Register(
                nameof(MaritalStatus),
                typeof(string),
                typeof(ProfileSummaryCard),
                new PropertyMetadata(string.Empty));
    }
}