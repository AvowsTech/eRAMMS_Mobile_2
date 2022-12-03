using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Model.ResponseModel;
using RAMMS.MobileApps.Page;
using Rg.Plugins.Popup.Extensions;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    
    public class FormMAddPageModel : FreshBasePageModel
    {
        //private bool _fromAdd;

        //private bool _isPhotoTabVisible;

        //public bool FromAdd
        //{
        //    get
        //    {
        //        return _fromAdd;
        //    }
        //    set
        //    {
        //        _fromAdd = value;
        //        RaisePropertyChanged("FromAdd");
        //    }
        //}
        //public override async void Init(object initData)
        //{
        //    base.Init(initData);
        //    IsPhotoTabVisible = false;



        //    if (App.ReturnType == "Add")
        //    {
        //        FromAdd = true;
        //        App.HeaderCode = 0;
        //    }

        //}
        //public bool IsPhotoTabVisible
        //{
        //    get
        //    {
        //        return _isPhotoTabVisible;
        //    }
        //    set
        //    {
        //        _isPhotoTabVisible = value;
        //        RaisePropertyChanged("IsPhotoTabVisible");
        //    }
        //}

        //public ICommand AddButtonCommand
        //{
        //    get
        //    {
        //        return new Command((obj) =>
        //        {
        //            IsPhotoTabVisible = false;
        //        });
        //    }
        //}

        //public ICommand PhotoButtonCommand
        //{
        //    get
        //    {
        //        return new Command((obj) =>
        //        {
        //            IsPhotoTabVisible = true;
        //        });
        //    }
        //}
        //public ICommand ToggleCommand
        //{
        //    get
        //    {
        //        return new Command(ToggleBarTapped);
        //    }
        //}

        //private void ToggleBarTapped(object obj)
        //{
        //    Frame layout = obj as Frame;

        //    if (layout != null)
        //    {
        //        if (layout.IsVisible)
        //        {
        //            layout.IsVisible = false;
        //        }
        //        else
        //        {
        //            layout.IsVisible = true;
        //        }
        //    }
        //    else
        //    {
        //        Image image = obj as Image;
        //        string imgsrc = (image?.Source as FileImageSource).File;
        //        if (String.Equals(imgsrc, "RoundedAddIcon.png"))
        //        {
        //            image.Source = "minusicon.png";
        //        }
        //        else
        //        {
        //            image.Source = "RoundedAddIcon.png";
        //        }
        //    }
        //}

        //public ICommand AddImage
        //{
        //    get
        //    {
        //        return new FreshCommand(async (obj) =>
        //        {

        //            await CoreMethods.PushPageModel<FormC1C2CameraPopupPageModel>();
        //        });
        //    }
        //}



        private bool _isPhotoTabVisible;
        private bool _fromAdd;
        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private AssetDDLResponseDTO.DropDown _selectedRMU;
        private AssetDDLResponseDTO.DropDown _selectedRoadCode;
        private AssetDDLResponseDTO.DropDown _selectedSectionCode;
        private int _selectedDivision = -1;
        private int _selectedYear = -1;
        private int _selectedAsset = -1;
        // private int _selectedSectionCode = -1;
        private int? _selectedConditionRating = -1;
        private int? _selectedFutureInvestigation = -1;
        private int? _selectedParkingPosition = -1;
        private int? _selectedAccesibility = -1;
        private int? _selectedPotentialHazard = -1;
        public string SelectedActivityName { get; set; }

        private int _selectedVerIndex = -1;
        private int _selectedVerIndex1 = -1;
        private int _selectedInspIndex = -1;
        private DDListItems _selectedActivity;
        private bool responseflag = false;

        TimePicker tAuditTime, fAuditTime;

        private bool isModify;
        private bool isDelete;
        private bool isView;
        SignaturePadView InspPadView;
        SignaturePadView VerPadView;
        SignaturePadView VerPadView1;

        Grid MainGrid;
        public ObservableCollection<FormC1C2ImgRequestDTO> DetailImageList { get; set; }
        Image image { get; set; }
        Label label { get; set; }
        public bool IsAdd { get; set; }
        public bool IsHeaderEnable { get; set; } = true;
        public bool CanSave { get; set; } = false;
        public string SelectedRoadName { get; set; }
        public string SelectedSectionName { get; set; }

        




        public string SelectedType { get; set; }
        public string SelectedMethod { get; set; }
        public string SelectedAuditplan { get; set; }
        public string SelectedAuditBy { get; set; }

        public string SmartSearch { get; set; }
        public DateTime? SelectedDate { get; set; } = null;
        public decimal? RoadLength { get; set; }
        public string SelectedRefNo { get; set; }
        public bool IsVerNameEnabled { get; set; } = false;
        public bool IsCrewVerNameEnabled { get; set; } = false;
        public bool IsInspNameEnabled { get; set; } = false;
        public string InspName { get; set; }
        public string VerName { get; set; }
        public string VerName1 { get; set; }
        public string InspDesignation { get; set; }
        public string CrewInspUser { get; set; }
        public string VerDesignation { get; set; }
        public string VerDesignation1 { get; set; }
        public ImageSource InspSign { get; set; }
        public ImageSource VerSign { get; set; }
        public ImageSource VerSign1 { get; set; }
        public DateTime? InspDate { get; set; } = null;
        public DateTime? VerDate { get; set; } = null;
        public DateTime? VerDate1 { get; set; } = null;
        public DateTime? AuditDate { get; set; } = null;
        public int RoadID { get; set; }
        public string PartBServiceProvider { get; set; }
        public string PartCServiceProvider { get; set; }
        public string PartDServiceProvider { get; set; }
        public string PartBConsultant { get; set; }
        public string PartCConsultant { get; set; }
        public string PartDConsultant { get; set; }
        public string SignatureAudit { get; set; }
        public string SignatureWit { get; set; }
        public string SignatureWitone { get; set; }
        public string strDescStatus { get; set; }


        public FilteredPagingDefinition<FormB1B2DetailRequestDTO> SearchCriteriaItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }
        public ObservableCollection<DDListItems> DDYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDDivisionListItems { get; set; }
        public ObservableCollection<DDListItems> DDAssetListItems { get; set; }
        public ObservableCollection<DDListItems> DDRatingListItems { get; set; }
        public ObservableCollection<DDListItems> DDInspUserListListItems { get; set; }
        public ObservableCollection<DDListItems> DDVerUserListListItems { get; set; }
        public ObservableCollection<DDListItems> DDVerUserListListItems1 { get; set; }

        public ObservableCollection<DDListItems> DDActivityListItems { get; set; }

        public ObservableCollection<DDListItems> DDCrewUserListListItems { get; set; }

        public ObservableCollection<DDListItems> DDCulvertUnitsDistressListIems { get; set; }
        public FormB1B2DetailRequestDTO SelecteddtlEditItem { get; set; }
        public List<String> SiteRef_multiSelect { get; set; }
        // SiteRef_multiSelect = new List<String>();

        public int SelectedBarrel1SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel2SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel3SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel4SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel5SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel6SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel7SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel8SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel9SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel10SeveritySeverity { get; set; } = -1;






        public int SelectedCulvertMarkerSeverity { get; set; } = -1;
        public int SelectedWaterwaySeverity { get; set; } = -1;
        public int SelectedEmbankmentSeverity { get; set; } = -1;
        public int SelectedHeadwallInletSeverity { get; set; } = -1;
        public int SelectedWingwallInletSeverity { get; set; } = -1;
        public int SelectedApronInletSeverity { get; set; } = -1;
        public int SelectedRiprapInletSeverity { get; set; } = -1;
        public int SelectedHeadwallOutletSeverity { get; set; } = -1;
        public int SelectedWingwallOutletSeverity { get; set; } = -1;
        public int SelectedApronOutletSeverity { get; set; } = -1;
        public int SelectedRiprapOutletSeverity { get; set; } = -1;
        public int SelectedCulvertApproachSeverity { get; set; } = -1;
        public ObservableCollection<CulvertListData> CulvertUnitItemsSource { get; set; }

        public bool FromAdd
        {
            get
            {
                return _fromAdd;
            }
            set
            {
                _fromAdd = value;
                RaisePropertyChanged("FromAdd");
            }
        }

        public bool IsPhotoTabVisible
        {
            get
            {
                return _isPhotoTabVisible;
            }
            set
            {
                _isPhotoTabVisible = value;
                RaisePropertyChanged("IsPhotoTabVisible");
            }
        }
        public int SelectedVerIndex
        {
            get
            {
                return _selectedVerIndex;
            }
            set
            {
                _selectedVerIndex = value;
                if (_selectedVerIndex != -1)
                {
                    var selectedItem = Convert.ToInt32(DDVerUserListListItems?[_selectedVerIndex]?.Value.ToString());

                    GetUserDetilsList("veruser", selectedItem);
                }
            }
        }

        public int SelectedVerIndex1
        {
            get
            {
                return _selectedVerIndex1;
            }
            set
            {
                _selectedVerIndex1 = value;
                if (_selectedVerIndex1 != -1)
                {
                    var selectedItem = Convert.ToInt32(DDVerUserListListItems1?[_selectedVerIndex1]?.Value.ToString());

                    GetUserDetilsList("veruser1", selectedItem);
                }
            }
        }

        public int SelectedCrewIndex
        {
            get
            {
                return _selectedVerIndex;
            }
            set
            {
                _selectedVerIndex = value;
                if (_selectedVerIndex != -1)
                {
                    var selectedItem = Convert.ToInt32(DDCrewUserListListItems?[_selectedVerIndex]?.Value.ToString());

                    GetUserDetilsList("crewuser", selectedItem);
                }
            }
        }


        public DDListItems SelectedActivity
        {
            get => _selectedActivity;
            set
            {
                _selectedActivity = value;
                if (_selectedActivity != null) {
                    SelectedActivityName = _selectedActivity.Text.Split('-')[1].ToString(); if (_selectedActivity != null) ;
                    //DDDistressCodeListItems[distresscode.SelectedIndex].Text.ToString().Split('-')[1].ToString()
                    SetRefNumber();
            }
            }
        }


        public int SelectedInspIndex
        {
            get
            {
                return _selectedInspIndex;
            }
            set
            {
                _selectedInspIndex = value;

                if (_selectedInspIndex != -1)
                {
                    var selectedItem = Convert.ToInt32(DDInspUserListListItems?[_selectedInspIndex].Value.ToString());

                    GetUserDetilsList("inspuser", selectedItem);
                }
            }
        }
        public int SelectedDivision
        {
            get => _selectedDivision;
            set
            {
                _selectedDivision = value;
                RaisePropertyChanged();
            }
        }

        private TimeSpan _SavefTime;
        public TimeSpan SavefTime
        {
            get { return _SavefTime; }
            set { _SavefTime = value; }
        }

        private TimeSpan _SavetTime;
        public TimeSpan SavetTime
        {
            get { return _SavetTime; }
            set { _SavetTime = value; }
        }




        public AssetDDLResponseDTO.DropDown SelectedRMU
        {
            get => _selectedRMU;
            set
            {
                
                    _selectedRMU = value;

                    SelectedSectionCode = SelectedRoadCode = null;
                    SelectedSectionName = SelectedRoadName = "";
                    RoadLength = null;
                    RMUSelectionChanged();
                    RaisePropertyChanged();
                
            }
        }

        private async void RMUSelectionChanged()
        { 
           
                await GetLandingDropDownList();
                await GetAssetList();
            
        }

        public AssetDDLResponseDTO.DropDown SelectedRoadCode
        {
            get => _selectedRoadCode;
            set
            {
               
                    _selectedRoadCode = value;
                    if (_selectedRoadCode != null)
                    {
                        SelectedRoadName = _selectedRoadCode.Item1;

                        //GetAssetList();
                        SetRefNumber();
                    }
               
                RaisePropertyChanged();
            }
        }

        private void SetRefNumber()
        {
            if (SelectedRoadCode != null && SelectedYear > 0)
                SelectedRefNo = "CI/Form M" + DDRodeCodeListItems[SelectedRoadCode.PKId].Text + "/" + SelectedActivity.Value + "/"+ AuditDate?.ToString("yyyyMMdd"); 
        }


        private async Task<int> GetActivityDetails(string ddtype)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype

                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    var response = await _restApi.GetActivityCode(ddlist);

                    if (response.success)
                    {
                        if (ddtype == "ACT-QA1")
                            DDActivityListItems = new ObservableCollection<DDListItems>(response.data);

                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
                }
                else
                {
                    _userDialogs.Alert("Please check your Internet Connection !");
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
            }
            return 1;
        }



        public AssetDDLResponseDTO.DropDown SelectedSectionCode
        {
            
            get => _selectedSectionCode;
            set
            {
               
                    _selectedSectionCode = value;
                    if (_selectedSectionCode != null)
                    {
                        SelectedRoadCode = null;
                        SelectedRoadName = "";
                        GetLandingDropDownList();
                        GetAssetList();
                        SelectedSectionName = _selectedSectionCode.Text.Split('-')[1].ToString();
                    }
                
                RaisePropertyChanged();
            
          }
        }
        public DateTime? MinimumDate { get; set; } = null;
        public DateTime? MaximumDate { get; set; } = null;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                SetRefNumber();
                RaisePropertyChanged();

                if (SelectedYear != -1)
                {
                    var year = DDYearListItems[SelectedYear].Text;
                    MinimumDate = Convert.ToDateTime("1-1-" + year);
                    MaximumDate = Convert.ToDateTime("12-31-" + year);
                    SelectedDate = null;
                }
            }
        }
        public int SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                _selectedAsset = value;
                RaisePropertyChanged();
            }
        }

        public int? SelectedConditionRating
        {
            get => _selectedConditionRating;
            set
            {
                _selectedConditionRating = value;
                RaisePropertyChanged();
            }
        }

        public int? SelectedFutureInvestigation
        {
            get => _selectedFutureInvestigation;
            set
            {
                _selectedFutureInvestigation = value;
                RaisePropertyChanged();
            }
        }

        public int? SelectedParkingPosition
        {
            get => _selectedParkingPosition;
            set
            {
                _selectedParkingPosition = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedAccesibility
        {
            get => _selectedAccesibility;
            set
            {
                _selectedAccesibility = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedPotentialHazard
        {
            get => _selectedPotentialHazard;
            set
            {
                _selectedPotentialHazard = value;
                RaisePropertyChanged();
            }
        }

        #region


        //public int PkRefNo { get; set; }
        //public int? AiPkRefNo { get; set; }
        //public string AiAssetId { get; set; }
        //public string AiDivCode { get; set; }
        //public string AiRmuName { get; set; }
        //public string AiRdCode { get; set; }
        //public string AiRdName { get; set; }
        //public int? AiLocChKm { get; set; }
        //public string AiLocChM { get; set; }
        //public double? AiFinRdLevel { get; set; }
        //public string AiStrucCode { get; set; }
        //public double? AiCatchArea { get; set; }
        //public double? AiSkew { get; set; }
        //public double? AiDesignFlow { get; set; }
        //public double? AiLength { get; set; }
        //public string AiPrecastSitu { get; set; }
        //public string AiGrpType { get; set; }
        //public int? AiBarrelNo { get; set; }
        //public double? AiGpsEasting { get; set; }
        //public double? AiGpsNorthing { get; set; }
        //public string AiMaterial { get; set; }
        //public double? AiIntelLevel { get; set; }
        //public double? AiOutletLevel { get; set; }
        //public string AiIntelStruc { get; set; }
        //public string AiOutletStruc { get; set; }
        //public string CInspRefNo { get; set; }
        //public int? YearOfInsp { get; set; }
        //public DateTime? DtOfInsp { get; set; }
        //public int? RecordNo { get; set; }
        //public bool? PrkPosition { get; set; }
        //public bool? Accessibility { get; set; }
        //public bool? PotentialHazards { get; set; }
        //public string SerProviderDefObs { get; set; }
        //public string AuthDefObs { get; set; }
        //public string SerProviderDefGenCom { get; set; }
        //public string AuthDefGenCom { get; set; }
        //public string SerProviderDefFeedback { get; set; }
        //public string AuthDefFeedback { get; set; }
        //public int? SerProviderUserId { get; set; }
        //public string SerProviderUserName { get; set; }
        //public string SerProviderUserDesignation { get; set; }
        //public DateTime? SerProviderInsDt { get; set; }
        //public string SignpathSerProvider { get; set; }
        //public int? UserIdAud { get; set; }
        //public string UserNameAud { get; set; }
        //public string UserDesignationAud { get; set; }
        //public DateTime? DtAud { get; set; }
        //public string SignpathAud { get; set; }
        //public int? CulvertConditionRat { get; set; }
        //public bool? ReqFurtherInv { get; set; }
        //public int? ModBy { get; set; }
        //public DateTime? ModDt { get; set; }
        //public int? CrBy { get; set; }
        //public DateTime? CrDt { get; set; }
        //public bool SubmitSts { get; set; }
        //public bool? ActiveYn { get; set; }
        //public FormB1B2DetailRequestDTO Detail { get; set; }
        public bool IsView { get; set; } = false;
        //public string RmuCode { get; set; }
        //public string SectionCode { get; set; }
        //public string SectionName { get; set; }
        //public string Status { get; set; }
        //public string DisplayAssetId { get; set; }

        public int PkRefNo { get; set; }
        public string RefId { get; set; }
        public string Dist { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public string DivCode { get; set; }
        public string RmuCode { get; set; }
        public string RmuName { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string RdCode { get; set; }
        public string RdName { get; set; }
        public string ActCode { get; set; }
        public string ActName { get; set; }
        public string CrewSup { get; set; }
        public string Desc { get; set; }
        public int? LocChKm { get; set; }
        public int? LocChM { get; set; }
        public DateTime? AuditedDate { get; set; }
        public string AuditTimeFrm { get; set; }
        public string AuditTimeTo { get; set; }
        public string AuditedBy { get; set; }
        public string AuditType { get; set; }
        public string SrProvider { get; set; }
        public int? FinalResult { get; set; }
        public string FinalResultValue { get; set; }
        public string Findings { get; set; }
        public string CorrectiveActions { get; set; }
        public bool? SignAudit { get; set; }
        public int? UseridAudit { get; set; }
        public string UsernameAudit { get; set; }
        public string DesignationAudit { get; set; }
        public DateTime? DateAudit { get; set; }
        public string OfAudit { get; set; }
        public bool? SignWit { get; set; }
        public int? UseridWit { get; set; }
        public string UsernameWit { get; set; }
        public string DesignationWit { get; set; }
        public DateTime? DateWit { get; set; }
        public string OfWit { get; set; }

        public bool? SignWitone { get; set; }
        public int? UseridWitone { get; set; }
        public string UsernameWitone { get; set; }
        public string DesignationWitone { get; set; }
        public DateTime? DateWitone { get; set; }
        public string OfWitone { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string status { get; set; }
        public string AuditLog { get; set; }

        public FormMAuditDetailsDTO FormMAD { get; set; }

        public int? Category { get; set; }
        public string ActivityCode { get; set; }
        public string ActivityName { get; set; }
        public string ActivityFor { get; set; }
        public bool? IsEditable { get; set; }
        public int? TallyBox { get; set; }
        public int? Weightage { get; set; }
        public int? Total { get; set; }
        public int? A1tallyBox { get; set; }
        public int? A1total { get; set; }
        public int? A2tallyBox { get; set; }
        public int? A2total { get; set; }
        public int? A3tallyBox { get; set; }
        public int? A3total { get; set; }
        public int? A4tallyBox { get; set; }
        public int? A4total { get; set; }
        public int? A5tallyBox { get; set; }
        public int? A5total { get; set; }
        public int? A6tallyBox { get; set; }
        public int? A6total { get; set; }
        public int? A7tallyBox { get; set; }
        public int? A7total { get; set; }
        public int? A8tallyBox { get; set; }
        public int? A8total { get; set; }
        public int? B1tallyBox { get; set; }
        public int? B1total { get; set; }
        public int? B2tallyBox { get; set; }
        public int? B2total { get; set; }
        public int? B3tallyBox { get; set; }
        public int? B3total { get; set; }
        public int? B4tallyBox { get; set; }
        public int? B4total { get; set; }
        public int? B5tallyBox { get; set; }
        public int? B5total { get; set; }
        public int? B6tallyBox { get; set; }
        public int? B6total { get; set; }
        public int? B7tallyBox { get; set; }
        public int? B7total { get; set; }
        public int? B8tallyBox { get; set; }
        public int? B8total { get; set; }
        public int? B9tallyBox { get; set; }
        public int? B9total { get; set; }
        public int? C1tallyBox { get; set; }
        public int? C1total { get; set; }
        public int? C2tallyBox { get; set; }
        public int? C2total { get; set; }
        public int? D1tallyBox { get; set; }
        public int? D1total { get; set; }
        public int? D2tallyBox { get; set; }
        public int? D2total { get; set; }
        public int? D3tallyBox { get; set; }
        public int? D3total { get; set; }
        public int? D4tallyBox { get; set; }
        public int? D4total { get; set; }
        public int? D5tallyBox { get; set; }
        public int? D5total { get; set; }
        public int? D6tallyBox { get; set; }
        public int? D6total { get; set; }
        public int? D7tallyBox { get; set; }
        public int? D7total { get; set; }
        public int? D8tallyBox { get; set; }
        public int? D8total { get; set; }
        public int? E1tallyBox { get; set; }
        public int? E1total { get; set; }
        public int? E2tallyBox { get; set; }
        public int? E2total { get; set; }
        public int? F1tallyBox { get; set; }
        public int? F1total { get; set; }
        public int? F2tallyBox { get; set; }
        public int? F2total { get; set; }
        public int? F3tallyBox { get; set; }
        public int? F3total { get; set; }
        public int? F4tallyBox { get; set; }
        public int? F4total { get; set; }
        public int? F5tallyBox { get; set; }
        public int? F5total { get; set; }
        public int? F6tallyBox { get; set; }
        public int? F6total { get; set; }
        public int? F7tallyBox { get; set; }
        public int? F7total { get; set; }
        public int? G1tallyBox { get; set; }
        public int? G1total { get; set; }
        public int? G2tallyBox { get; set; }
        public int? G2total { get; set; }
        public int? G3tallyBox { get; set; }
        public int? G3total { get; set; }
        public int? G4tallyBox { get; set; }
        public int? G4total { get; set; }
        public int? G5tallyBox { get; set; }
        public int? G5total { get; set; }
        public int? G6tallyBox { get; set; }
        public int? G6total { get; set; }
        public int? G7tallyBox { get; set; }
        public int? G7total { get; set; }
        public int? G8tallyBox { get; set; }
        public int? G8total { get; set; }
        public int? G9tallyBox { get; set; }
        public int? G9total { get; set; }
        public int? G10tallyBox { get; set; }
        public int? G10total { get; set; }





        #endregion

        public FormMAddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }
        public override void Init(object initData)
        {
            base.Init(initData);

            tAuditTime = CurrentPage.FindByName<TimePicker>("tAuditftime");

            fAuditTime = CurrentPage.FindByName<TimePicker>("fAuditftime");


            DDCulvertUnitsDistressListIems = new ObservableCollection<DDListItems>();

            IsPhotoTabVisible = false;

            FormMAD = new FormMAuditDetailsDTO();

            isView = Model.Security.IsView(ModuleNameList.Condition_Inspection);
            IsAdd = isModify = Model.Security.IsModify(ModuleNameList.Condition_Inspection);
            isDelete = Model.Security.IsDelete(ModuleNameList.Condition_Inspection);

            if (App.ReturnType == "Add")
            {
                FromAdd = true;
                App.HeaderCode = 0;
            }
            else
            {
                FromAdd = true;
            }
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await GetLandingDropDownList();
            await GetDDListDetails("division");
            await GetDDListDetails("Year");
            await GetActivityDetails("ACT-QA1");
            //await GetDDLDescValueContact("C1C2_Severity");
            //await GetDDLDescValueContact("C1C2_Culvert Marker");
            //await GetDDLDescValueContact("C1C2_Waterways");
            //await GetDDLDescValueContact("C1C2_Walls and Aprons");
            //await GetDDLDescValueContact("C1C2_Culvert Approach Condition");
            //await GetDDLDescValueContact("C1C2_Culvert Units");


            await GetAssetList();
            await GetUserList();

            if (DDInspUserListListItems != null)
            {
                int inspindex = DDInspUserListListItems.ToList().FindIndex(a => a.Text.Substring(2) == App.username);
                SelectedInspIndex = inspindex;
                GetUserDetilsList("inspuser", SelectedInspIndex);
                InspDate = DateTime.Now.Date;
            }

            CanSave = App.ReturnType == "Edit" ? true : false;

            if (App.ReturnType == "Edit" || App.ReturnType == "View")
            {


                //if (SelecteddtlEditItem.SiteRef_multiSelect.Count > 0)
                //{
                //    string s = "";

                //    int index = 0;
                //    SelectedLocationList = SelecteddtlEditItem.SiteRef_multiSelect;
                //    foreach (var model in SelectedLocationList)
                //    {
                //        s = s + model;
                //        if (index < SelectedLocationList.Count - 1)
                //        {
                //            s = s + ",";
                //        }
                //        index++;
                //        DDLocationListItems.FirstOrDefault(x => x.Value == model).IsChecked = true;
                //    }
                //    btnlocation.Text = s;

                //}
                //else
                //{
                //    btnlocation.Text = "Select Location";
                //}







                
                IsHeaderEnable = false;
                IsView = App.ReturnType == "View" ? true : false;
                await GetC1C2ById(App.HeaderCode);
            }

            string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

            GetImageList(strDetailCode);


            MessagingCenter.Unsubscribe<object, string>(this, "Uploaded");

            MessagingCenter.Subscribe<object, string>(this, "Uploaded", (obj, s) =>
            {
                GetImageList(strDetailCode);
            });
        }

        private async void GetImageList(string AssetID)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        var hdrresponse = await _restApi.GetC1C2Images(App.HeaderCode);

                        if (hdrresponse.success == true)
                        {
                            var list = hdrresponse.data;
                            DetailImageList = new ObservableCollection<FormC1C2ImgRequestDTO>(hdrresponse.data);

                            int i = 0;
                            foreach (var listdata in DetailImageList)
                            {
                                listdata.ImageSrno = i + 1;
                                i++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _userDialogs.Alert(ex.Message);
                    }
                }
            }
            catch { }
        }

        public ICommand DeleteImageCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        var actionResult = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete?", "RAMS", "Yes", "No");
                        if (actionResult)
                        {
                            _userDialogs.ShowLoading("Loading");
                            if (CrossConnectivity.Current.IsConnected)
                            {
                                var imageID = (obj as FormC1C2ImgRequestDTO).PkRefNo;
                                var response = await _restApi.DeleteC1C2Image(App.HeaderCode, imageID);

                                if (response.success)
                                {
                                    await UserDialogs.Instance.AlertAsync("Image deleted successfully.", "RAMS", "0K");

                                    string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

                                    GetImageList(strDetailCode);
                                }
                                else
                                    _userDialogs.Toast(response.errorMessage);
                            }
                            else
                                UserDialogs.Instance.Alert("Please check your Internet Connection !");
                        }
                    }
                    catch (Exception ex)
                    {
                        _userDialogs.Alert(ex.Message);
                    }
                    finally
                    {
                        _userDialogs.HideLoading();
                    }
                });
            }

        }

       
        public ICommand AddImage
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormC1C2CameraPopupPageModel>();
                });
            }
        }

        public ICommand AddButtonCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    IsPhotoTabVisible = false;
                });
            }
        }

        public ICommand PhotoButtonCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    IsPhotoTabVisible = true;
                });
            }
        }
        public ICommand ToggleCommand
        {
            get
            {
                return new Command(ToggleBarTapped);
            }
        }

        public ICommand OKCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {



                        if (SelectedRoadCode == null)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select Road Code", "RAMS", "OK");
                            return;
                        }

                        if (SelectedActivity == null)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select Activity", "RAMS", "OK");
                            return;
                        }


                        if (LocChKm == null)
                        {
                            await UserDialogs.Instance.AlertAsync("Please enter Location", "RAMS", "OK");
                            return;
                        }

                        if (LocChM == null)
                        {
                            await UserDialogs.Instance.AlertAsync("Please enter Location", "RAMS", "OK");
                            return;
                        }

                        if (AuditedDate == null)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select audit date", "RAMS", "OK");
                            return;
                        }

                        //if (Dis == null)
                        //{
                        //    await UserDialogs.Instance.AlertAsync("Please select audit From", "RAMS", "OK");
                        //    return;
                        //}

                        //if (SavetTime == null)
                        //{
                        //    await UserDialogs.Instance.AlertAsync("Please select audit To", "RAMS", "OK");
                        //    return;
                        //}

                        /*if (SelectedYear == -1)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select Year Of Inspection", "RAMS", "OK");
                            return;
                        }*/



                        var response = SaveFormMHeader();

                        FromAdd = false;
                    }
                    catch
                    {
                    }


                });
            }
        }

        private async Task<ObservableCollection<FormMDTO>> SaveFormMHeader()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    SetRefNumber();
                    RaisePropertyChanged();

                    TimeSpan timefspan = new TimeSpan(SavefTime.Hours, SavefTime.Minutes, SavefTime.Seconds);
                    DateTime ftime = DateTime.Today.Add(timefspan);
                    string displayfTime = ftime.ToString("hh:mm tt");

                    TimeSpan timetspan = new TimeSpan(SavetTime.Hours, SavetTime.Minutes, SavetTime.Seconds);
                    DateTime ttime = DateTime.Today.Add(timetspan);
                    string displaytTime = ttime.ToString("hh:mm tt");

                    FormMDTO GridItems = new FormMDTO()
                    {
                        PkRefNo = App.HeaderCode,
                        //PkRefNo = Convert.ToInt32(DDAssetListItems[SelectedAsset]?.Value),
                        // AssetId = DDAssetListItems[SelectedAsset]?.Text,
                        //AiDivCode = DDDivisionListItems[SelectedDivision]?.Value ?? null,
                        DivCode = SelectedDivision == -1 ? null : DDDivisionListItems[SelectedDivision]?.Value,
                        RmuName = SelectedRMU?.Text,
                        RmuCode = SelectedRMU?.Value,
                        SecCode = SelectedSectionCode?.Code,
                        SecName = SelectedSectionName,
                        RdCode = SelectedRoadCode?.Value,
                        RdName = SelectedRoadName,
                        ActCode = SelectedActivity?.Value,
                        ActName = SelectedActivityName,
                        AuditedDate = AuditedDate.HasValue ? AuditedDate.Value : (DateTime?)null,
                        LocChKm = LocChKm.HasValue ? LocChKm.Value : (int?)null,
                        LocChM = LocChM.HasValue ? LocChM.Value : (int?)null,

                        AuditTimeFrm = displayfTime,//Convert.ToString(SavefTime.ToString()),
                        AuditTimeTo = displaytTime //Convert.ToString(SavetTime.ToString())
                    
                        // YearOfInsp = Convert.ToInt32(DDYearListItems[SelectedYear]?.Value),
                        //DtOfInsp = SelectedDate.HasValue ? SelectedDate.Value : (DateTime?)null
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.SaveFMHdr(GridItems);

                    if (response.success)
                    {
                        IsHeaderEnable = false;
                        CanSave = true;
                        if (response.data.SubmitSts)
                            CanSave = false;
                        else
                        { 
                             CanSave = true;
                            
                         }
                        App.HeaderCode = response.data.PkRefNo;
                        SetViewData(response);
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex) { return null; }
            finally
            {
                _userDialogs.HideLoading();
            }
        }

        private async Task<int> GetC1C2ById(int headerCode)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetFMById(headerCode);

                    if (response.success)
                    {
                        SetViewData(response);
                    }
                    else
                    {
                        _userDialogs.Toast(response.errorMessage);
                    }
                }
                else
                    _userDialogs.Alert("Please check your Internet Connection !");
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
            }
            return 1;
        }

        private void SetViewData(ResponseBaseObject<FormMDTO> response)
        {
            Total = (int?)null;

            SelectedRefNo = response.data.RefId;
            App.InspReferenceNo = response.data.RefId;
            DivCode = response.data.DivCode;

            int devIndex = DDDivisionListItems.ToList().FindIndex(a => a.Value == response.data.DivCode);
            SelectedDivision = devIndex;

            RmuName = response.data.RmuName;
            SelectedRMU = DDRMUListItems.ToList().Find(a => a.Value == response.data.RmuCode);
            

            RmuCode = response.data.RmuCode;


            


            SecCode = response.data.SecCode;
            SelectedSectionCode = DDSectionListItems.ToList().Find(a => a.Value == response.data.SecCode);
            

            SecName = response.data.SecName;
            SelectedSectionName = SecName;


            RdCode = response.data.RdCode;
            SelectedRoadCode = DDRodeCodeListItems.ToList().Find(a => a.Value == response.data.RdCode);


            RdName = response.data.RdName;
            SelectedRoadName = response.data.RdName;

            ActCode = response.data.ActCode;
            SelectedActivity  = DDActivityListItems.ToList().Find(a => a.Value == response.data.ActCode);
            
            

            ActName = response.data.ActName;
            SelectedActivityName = ActName;

            LocChKm = response.data.LocChKm;
            LocChM = response.data.LocChM;

            RmuName = response.data.RmuName;
            AuditDate = response.data.AuditedDate.HasValue ? response.data.AuditedDate.Value : (DateTime?)null;
            AuditedDate = AuditDate;
            AuditTimeFrm = response.data.AuditTimeFrm;
            AuditTimeTo = response.data.AuditTimeTo;
            SelectedAuditBy = response.data.AuditedBy;
            SelectedAuditplan = response.data.AuditType;
            SelectedMethod = response.data.Method;
            SelectedType = response.data.Type;
            Findings = response.data.Findings;
            CorrectiveActions = response.data.CorrectiveActions;
            int cindex = DDCrewUserListListItems.ToList().FindIndex(a => Convert.ToString(a.Value) == response.data.CrewSup);
            SelectedCrewIndex = cindex;
            try
            {
                if (response.data.FormMAD != null)
                {
                    //response.data.IssuesFound != null ? (response.data.IssuesFound == true ? 0 : 1) : (int?)null;
                    A1tallyBox = response.data.FormMAD.A1tallyBox != null ? response.data.FormMAD.A1tallyBox : (int?)null;
                    
                    A2tallyBox = response.data.FormMAD.A2tallyBox != null ? response.data.FormMAD.A2tallyBox : (int?)null;
                    
                    A3tallyBox = response.data.FormMAD.A3tallyBox != null ? response.data.FormMAD.A2tallyBox.Value : (int?)null;
                    
                    A4tallyBox = response.data.FormMAD.A4tallyBox != null ? response.data.FormMAD.A2tallyBox.Value : (int?)null;
                    
                    A5tallyBox = response.data.FormMAD.A5tallyBox != null ? response.data.FormMAD.A2tallyBox.Value : (int?)null;
                    
                    A6tallyBox = response.data.FormMAD.A6tallyBox != null ? response.data.FormMAD.A2tallyBox.Value : (int?)null;
                    
                    A7tallyBox = response.data.FormMAD.A7tallyBox != null ? response.data.FormMAD.A2tallyBox.Value : (int?)null;
                    
                    A8tallyBox = response.data.FormMAD.A8tallyBox != null ? response.data.FormMAD.A2tallyBox.Value : (int?)null;

                    ActivityFor = response.data.FormMAD.ActivityFor != null ? response.data.FormMAD.ActivityFor : null;

                    A1total = response.data.FormMAD.A1total != null ? response.data.FormMAD.A1total.Value : (int?)null;
                    
                    A2total = response.data.FormMAD.A2total != null ? response.data.FormMAD.A2total.Value : (int?)null;
                   
                    A3total = response.data.FormMAD.A3total != null ? response.data.FormMAD.A3total.Value : (int?)null;
                    
                    A4total = response.data.FormMAD.A4total != null ? response.data.FormMAD.A4total.Value : (int?)null;
                    
                    A5total = response.data.FormMAD.A5total != null ? response.data.FormMAD.A5total.Value : (int?)null;
                    
                    A6total = response.data.FormMAD.A6total != null ? response.data.FormMAD.A6total.Value : (int?)null;
                    
                    A7total = response.data.FormMAD.A7total != null ? response.data.FormMAD.A7total.Value : (int?)null;
                    
                    A8total = response.data.FormMAD.A8total != null ? response.data.FormMAD.A8total.Value : (int?)null;
                    
                    B1tallyBox = response.data.FormMAD.B1tallyBox != null ? response.data.FormMAD.B1tallyBox.Value : (int?)null;
                        
                    B2tallyBox = response.data.FormMAD.B2tallyBox != null ? response.data.FormMAD.B2tallyBox.Value : (int?)null;
                    
                    B3tallyBox = response.data.FormMAD.B3tallyBox != null ? response.data.FormMAD.B3tallyBox.Value : (int?)null;
                    
                    B4tallyBox = response.data.FormMAD.B4tallyBox != null ? response.data.FormMAD.B4tallyBox.Value : (int?)null;
                    
                    B5tallyBox = response.data.FormMAD.B5tallyBox != null ? response.data.FormMAD.B5tallyBox.Value : (int?)null;
                    
                    B6tallyBox = response.data.FormMAD.B6tallyBox != null ? response.data.FormMAD.B6tallyBox.Value : (int?)null;
                    
                    B7tallyBox = response.data.FormMAD.B7tallyBox != null ? response.data.FormMAD.B7tallyBox.Value : (int?)null;
                    
                    B8tallyBox = response.data.FormMAD.B8tallyBox != null ? response.data.FormMAD.B8tallyBox.Value : (int?)null;
                    
                    B9tallyBox = response.data.FormMAD.B9tallyBox != null ? response.data.FormMAD.B9tallyBox.Value : (int?)null;
                    
                    B1total = response.data.FormMAD.B1total != null ? response.data.FormMAD.B1total.Value : (int?)null;
                    
                    B2total = response.data.FormMAD.B2total != null ? response.data.FormMAD.B2total.Value : (int?)null;
                    
                    B3total = response.data.FormMAD.B3total != null ? response.data.FormMAD.B3total.Value : (int?)null;
                    
                    B4total = response.data.FormMAD.B4total != null ? response.data.FormMAD.B4total.Value : (int?)null;
                    
                    B5total = response.data.FormMAD.B5total != null ? response.data.FormMAD.B5total.Value : (int?)null;
                    
                    B6total = response.data.FormMAD.B6total != null ? response.data.FormMAD.B6total.Value : (int?)null;
                    
                    B7total = response.data.FormMAD.B7total != null ? response.data.FormMAD.B7total.Value : (int?)null;
                    
                    B8total = response.data.FormMAD.B8total != null ? response.data.FormMAD.B8total.Value : (int?)null;
                   
                    B9total = response.data.FormMAD.B9total != null ? response.data.FormMAD.B9total.Value : (int?)null;
                    
                    C1tallyBox = response.data.FormMAD.C1tallyBox != null ? response.data.FormMAD.C1tallyBox.Value : (int?)null;
                   
                    C2tallyBox = response.data.FormMAD.C2tallyBox != null ? response.data.FormMAD.C2tallyBox.Value : (int?)null;
                    
                    C1total = response.data.FormMAD.C1total != null ? response.data.FormMAD.C1total.Value : (int?)null;
                    
                    C2total = response.data.FormMAD.C2total != null ? response.data.FormMAD.C2total.Value : (int?)null;
                    
                    D1tallyBox = response.data.FormMAD.D1tallyBox != null ? response.data.FormMAD.D1tallyBox.Value : (int?)null;
                    
                    D2tallyBox = response.data.FormMAD.D2tallyBox != null ? response.data.FormMAD.D2tallyBox.Value : (int?)null;
                    
                    D3tallyBox = response.data.FormMAD.D3tallyBox != null ? response.data.FormMAD.D3tallyBox.Value : (int?)null;
                    
                    D4tallyBox = response.data.FormMAD.D4tallyBox != null ? response.data.FormMAD.D4tallyBox.Value : (int?)null;
                   
                    D5tallyBox = response.data.FormMAD.D5tallyBox != null ? response.data.FormMAD.D5tallyBox.Value : (int?)null;
                    
                    D6tallyBox = response.data.FormMAD.D6tallyBox != null ? response.data.FormMAD.D6tallyBox.Value : (int?)null;
                    
                    D7tallyBox = response.data.FormMAD.D7tallyBox != null ? response.data.FormMAD.D7tallyBox.Value : (int?)null;
                    
                    D8tallyBox = response.data.FormMAD.D8tallyBox != null ? response.data.FormMAD.D8tallyBox.Value : (int?)null;
                    
                    D1total = response.data.FormMAD.D1total != null ? response.data.FormMAD.D1total.Value : (int?)null;
                    
                    D2total = response.data.FormMAD.D2total != null ? response.data.FormMAD.D2total.Value : (int?)null;
                    
                    D3total = response.data.FormMAD.D3total != null ? response.data.FormMAD.D3total.Value : (int?)null;
                    
                    D4total = response.data.FormMAD.D4total != null ? response.data.FormMAD.D4total.Value : (int?)null;
                    
                    D5total = response.data.FormMAD.D5total != null ? response.data.FormMAD.D5total.Value : (int?)null;
                    
                    D6total = response.data.FormMAD.D6total != null ? response.data.FormMAD.D6total.Value : (int?)null;
                    
                    D7total = response.data.FormMAD.D7total != null ? response.data.FormMAD.D7total.Value : (int?)null;
                    
                    D8total = response.data.FormMAD.D8total != null ? response.data.FormMAD.D8total.Value : (int?)null;
                    
                    E1tallyBox = response.data.FormMAD.E1tallyBox != null ? response.data.FormMAD.E1tallyBox.Value : (int?)null;
                   
                    E2tallyBox = response.data.FormMAD.E2tallyBox != null ? response.data.FormMAD.E2tallyBox.Value : (int?)null;
                   
                    E1total = response.data.FormMAD.E1total != null ? response.data.FormMAD.E1total.Value : (int?)null;
                    
                    E2total = response.data.FormMAD.E2total != null ? response.data.FormMAD.E2total.Value : (int?)null;
                    
                    F1tallyBox = response.data.FormMAD.F1tallyBox != null ? response.data.FormMAD.F1tallyBox.Value : (int?)null;
                    
                    F2tallyBox = response.data.FormMAD.F2tallyBox != null ? response.data.FormMAD.F2tallyBox.Value : (int?)null;
                    
                    F3tallyBox = response.data.FormMAD.F3tallyBox != null ? response.data.FormMAD.F3tallyBox.Value : (int?)null;
                    
                    F4tallyBox = response.data.FormMAD.F4tallyBox != null ? response.data.FormMAD.F4tallyBox.Value : (int?)null;
                    
                    F5tallyBox = response.data.FormMAD.F5tallyBox != null ? response.data.FormMAD.F5tallyBox.Value : (int?)null;
                    
                    F6tallyBox = response.data.FormMAD.F6tallyBox != null ? response.data.FormMAD.F6tallyBox.Value : (int?)null;
                    
                    F7tallyBox = response.data.FormMAD.F7tallyBox != null ? response.data.FormMAD.F7tallyBox.Value : (int?)null;
                    

                    F1total = response.data.FormMAD.F1total != null ? response.data.FormMAD.F1total.Value : (int?)null;
                   
                    F2total = response.data.FormMAD.F2total != null ? response.data.FormMAD.F2total.Value : (int?)null;
                    
                    F3total = response.data.FormMAD.F3total != null ? response.data.FormMAD.F3total.Value : (int?)null;
                   
                    F4total = response.data.FormMAD.F4total != null ? response.data.FormMAD.F4total.Value : (int?)null;
                    
                    F5total = response.data.FormMAD.F5total != null ? response.data.FormMAD.F5total.Value : (int?)null;
                   
                    F6total = response.data.FormMAD.F6total != null ? response.data.FormMAD.F6total.Value : (int?)null;
                    
                    F7total = response.data.FormMAD.F7total != null ? response.data.FormMAD.F7total.Value : (int?)null;
                    
                    G1tallyBox = response.data.FormMAD.G1tallyBox != null ? response.data.FormMAD.G1tallyBox.Value : (int?)null;
                    
                    G2tallyBox = response.data.FormMAD.G2tallyBox != null ? response.data.FormMAD.G2tallyBox.Value : (int?)null;
                    
                    G3tallyBox = response.data.FormMAD.G3tallyBox != null ? response.data.FormMAD.G3tallyBox.Value : (int?)null;
                    
                    G4tallyBox = response.data.FormMAD.G4tallyBox != null ? response.data.FormMAD.G4tallyBox.Value : (int?)null;
                    
                    G5tallyBox = response.data.FormMAD.G5tallyBox != null ? response.data.FormMAD.G5tallyBox.Value : (int?)null;
                    
                    G6tallyBox = response.data.FormMAD.G6tallyBox != null ? response.data.FormMAD.G6tallyBox.Value : (int?)null;
                    
                    G7tallyBox = response.data.FormMAD.G7tallyBox != null ? response.data.FormMAD.G7tallyBox.Value : (int?)null;
                    
                    G8tallyBox = response.data.FormMAD.G8tallyBox != null ? response.data.FormMAD.G8tallyBox.Value : (int?)null;
                    
                    G9tallyBox = response.data.FormMAD.G9tallyBox != null ? response.data.FormMAD.G9tallyBox.Value : (int?)null;
                   
                    G10tallyBox = response.data.FormMAD.G10tallyBox != null ? response.data.FormMAD.G10tallyBox.Value : (int?)null;
                    

                    G1total = response.data.FormMAD.G1total != null ? response.data.FormMAD.G1total.Value : (int?)null;
                    
                    G2total = response.data.FormMAD.G2total != null ? response.data.FormMAD.G2total.Value : (int?)null;
                    
                    G3total = response.data.FormMAD.G3total != null ? response.data.FormMAD.G3total.Value : (int?)null;
                    
                    G4total = response.data.FormMAD.G4total != null ? response.data.FormMAD.G4total.Value : (int?)null;
                    
                    G5total = response.data.FormMAD.G5total != null ? response.data.FormMAD.G5total.Value : (int?)null;
                    
                    G6total = response.data.FormMAD.G6total != null ? response.data.FormMAD.G6total.Value : (int?)null;
                    
                    G7total = response.data.FormMAD.G7total != null ? response.data.FormMAD.G7total.Value : (int?)null;
                    
                    G8total = response.data.FormMAD.G8total != null ? response.data.FormMAD.G8total.Value : (int?)null;
                    
                    G9total = response.data.FormMAD.G9total != null ? response.data.FormMAD.G9total.Value : (int?)null;
                    
                    G10total = response.data.FormMAD.G10total != null ? response.data.FormMAD.G10total.Value : (int?)null;

                    ActivityName = response.data.FormMAD.ActivityName != null ? response.data.FormMAD.ActivityName : null;

                    Total = response.data.FormMAD.Total != null ? response.data.FormMAD.Total.Value : (int?)null;

                    GetTotalDesc(Total.ToString());

                }
            }
            catch (Exception ex) { }

            int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UseridAudit);
            SelectedInspIndex = inspindex;
            InspName = response.data.UsernameAudit;
            InspDesignation = response.data.DesignationAudit;
            InspDate = response.data.DateAudit.HasValue ? response.data.DateAudit.Value : (DateTime?)null;
            
            InspSign = ImageSource.FromStream(
                () => new MemoryStream(Convert.FromBase64String(response.data.SignatureAudit)));

            int verindex = DDVerUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UseridWit);
            SelectedVerIndex = verindex;
            VerName = response.data.UsernameWit;
            VerDesignation = response.data.DesignationWit;
            VerDate = response.data.DateWit.HasValue ? response.data.DateWit.Value : (DateTime?)null;
            VerSign = ImageSource.FromStream(
                () => new MemoryStream(Convert.FromBase64String(response.data.SignatureWit)));


            int verindex1 = DDVerUserListListItems1.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UseridWitone);
            SelectedVerIndex1 = verindex1;
            VerName1 = response.data.UsernameWitone;
            VerDesignation1 = response.data.DesignationWitone;
            VerDate1 = response.data.DateWitone.HasValue ? response.data.DateWitone.Value : (DateTime?)null;
            VerSign1 = ImageSource.FromStream(
                () => new MemoryStream(Convert.FromBase64String(response.data.SignatureWitone)));

        }

        private void ToggleBarTapped(object obj)
        {
            Frame layout = obj as Frame;

            if (layout != null)
            {
                if (layout.IsVisible)
                {
                    layout.IsVisible = false;
                }
                else
                {
                    layout.IsVisible = true;
                }
            }
            else
            {
                Image image = obj as Image;
                string imgsrc = (image?.Source as FileImageSource).File;
                if (String.Equals(imgsrc, "RoundedAddIcon.png"))
                {
                    image.Source = "minusicon.png";
                }
                else
                {
                    image.Source = "RoundedAddIcon.png";
                }
            }
        }

        public async Task<int> GetLandingDropDownList(string propName = null)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var strQuery = new AssetDDLRequestDTO
                    {
                        RMU = SelectedRMU?.Value,
                        RdCode = SelectedRoadCode?.Value,
                        SectionCode = Convert.ToInt32(SelectedSectionCode?.Code),
                        GrpCode = "CV"
                    };

                    var response = await _restApi.GetF2LandingDropDown(strQuery);

                    if (response.success)
                    {
                        if (response.data.RMU != null && SelectedRMU == null)
                        {
                            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.RMU);
                        }
                        if (propName != "Section" && response.data.Section?.Count > 0)
                        {
                            DDSectionListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.Section);
                        }
                        if (response.data.RdCode != null)
                        {
                            DDRodeCodeListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.RdCode);
                        }
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
            return 1;
        }

        private async Task<int> GetAssetList()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new AssetDDLRequestDTO()
                    {
                        RMU = SelectedRMU?.Value,
                        SectionCode = Convert.ToInt32(SelectedSectionCode?.Code),
                        RdCode = SelectedRoadCode?.Value
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    var response = await _restApi.GetDDAssetList(ddlist);

                    if (response.success)
                    {
                        DDAssetListItems = new ObservableCollection<DDListItems>(response.data);
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
                }
                else
                {
                    _userDialogs.Alert("Please check your Internet Connection !");
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {

            }
            return 1;
        }
        private async Task<int> GetDDListDetails(string ddtype)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    var response = await _restApi.GetDDList(ddlist);

                    if (response.success)
                    {
                        if (ddtype == "Year")
                            DDYearListItems = new ObservableCollection<DDListItems>(response.data);
                        else if (ddtype == "division")
                            DDDivisionListItems = new ObservableCollection<DDListItems>(response.data);
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
                }
                else
                {
                    _userDialogs.Alert("Please check your Internet Connection !");
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
            }
            return 1;
        }


        private async Task<string> GetTotalDesc(string strTotalDesc)
        {

            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    if (strTotalDesc != "")
                    {
                        if (Convert.ToInt32(strTotalDesc) < 20)
                        {
                            strTotalDesc = "Satisfactory";
                        }
                        else if (Convert.ToInt32(strTotalDesc) >= 20 && Convert.ToInt32(strTotalDesc) < 45)
                        {
                            strTotalDesc = "Needs Improvement";
                        }
                        else if (Convert.ToInt32(strTotalDesc) > 45)
                        {
                            strTotalDesc = "Dangerous";
                        }
                        else
                            _userDialogs.Toast("Invalid Total Desccription");
                    }
                }
                else
                {
                    _userDialogs.Alert("Please check your Internet Connection !");
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
            }
            return strTotalDesc;
        }

        public async Task<int> GetUserList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        DDInspUserListListItems = new ObservableCollection<DDListItems>(response.data);
                        DDVerUserListListItems = new ObservableCollection<DDListItems>(response.data);
                        DDVerUserListListItems1 = new ObservableCollection<DDListItems>(response.data);
                        DDCrewUserListListItems = new ObservableCollection<DDListItems>(response.data);
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
            return 1;
        }

        public async void GetUserDetilsList(string usertype, int iUser)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var objUser = new UserRequestDTO()
                    {
                        UserId = iUser
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(objUser);
                    var response = await _restApi.UserDtl(objUser);

                    if (response.success)
                    {
                        try
                        {
                            if (usertype == "inspuser")
                            {
                                var userprp = DDInspUserListListItems[SelectedInspIndex].Text.Split('-')[1];
                                if (userprp.ToLower() == "others")
                                {
                                    IsInspNameEnabled = true;
                                }
                                else
                                {
                                    IsInspNameEnabled = false;
                                }

                                InspName = response.data.UserName;
                                InspDesignation = response.data.Position;
                            }
                            else if (usertype == "veruser")
                            {
                                var userprp = DDVerUserListListItems[SelectedVerIndex].Text.Split('-')[1];
                                if (userprp.ToLower() == "others")
                                {
                                    IsVerNameEnabled = true;
                                }
                                else
                                {
                                    IsVerNameEnabled = false;
                                }

                                VerName = response.data.UserName;
                                VerDesignation = response.data.Position;
                            }
                            else if (usertype == "veruser1")
                            {
                                var userprp = DDVerUserListListItems1[SelectedVerIndex1].Text.Split('-')[1];
                                if (userprp.ToLower() == "others")
                                {
                                    IsVerNameEnabled = true;
                                }
                                else
                                {
                                    IsVerNameEnabled = false;
                                }

                                VerName1 = response.data.UserName;
                                VerDesignation1 = response.data.Position;
                            }
                            else if (usertype == "crewuser")
                            {
                                CrewInspUser = DDCrewUserListListItems[SelectedVerIndex].Text.Split('-')[1];
                              
                            }

                        }
                        catch (Exception ex)
                        {
                            _userDialogs.HideLoading();
                        }
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
        }

        private async void SaveSignature(string type)
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                string inspSign = "";
                string verSign = "";
                string verSign1 = "";
                InspPadView = CurrentPage.FindByName<SignaturePadView>("InspPadView");
                Stream image = await InspPadView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                if (!InspPadView.IsBlank)
                {
                    using (BinaryReader binaryReader = new BinaryReader(image))
                    {
                        binaryReader.BaseStream.Position = 0;
                        byte[] Signature = binaryReader.ReadBytes((int)image.Length);
                    }
                    var signatureMemoryStream = image as System.IO.MemoryStream;
                    var byteArray = signatureMemoryStream.ToArray();
                    string base64String = Convert.ToBase64String(byteArray);
                    inspSign = base64String;
                }
                else
                    inspSign = null;

                VerPadView = CurrentPage.FindByName<SignaturePadView>("VerPadView");
                Stream image1 = await VerPadView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                if (!VerPadView.IsBlank)
                {
                    using (BinaryReader binaryReader = new BinaryReader(image1))
                    {
                        binaryReader.BaseStream.Position = 0;
                        byte[] Signature = binaryReader.ReadBytes((int)image1.Length);
                    }
                    var signatureMemoryStream = image1 as System.IO.MemoryStream;
                    var byteArray = signatureMemoryStream.ToArray();
                    string base64String = Convert.ToBase64String(byteArray);
                    verSign = base64String;
                }
                else
                    verSign = null;


                VerPadView1 = CurrentPage.FindByName<SignaturePadView>("VerPadView1");
                Stream image2 = await VerPadView1.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                if (!VerPadView1.IsBlank)
                {
                    using (BinaryReader binaryReader = new BinaryReader(image2))
                    {
                        binaryReader.BaseStream.Position = 0;
                        byte[] Signature = binaryReader.ReadBytes((int)image2.Length);
                    }
                    var signatureMemoryStream = image2 as System.IO.MemoryStream;
                    var byteArray = signatureMemoryStream.ToArray();
                    string base64String = Convert.ToBase64String(byteArray);
                    verSign1 = base64String;
                }
                else
                    verSign1 = null;

                try
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        var HeaderItemResponse = await _restApi.GetFMById(App.HeaderCode);

                        if (HeaderItemResponse.success)
                        {
                            HeaderItemResponse.data.PkRefNo = App.HeaderCode;
                            HeaderItemResponse.data.AuditedDate = AuditedDate;

                            HeaderItemResponse.data.UseridAudit = SelectedInspIndex != -1 ? Convert.ToInt32(DDInspUserListListItems[SelectedInspIndex]?.Value) : (int?)null;
                            HeaderItemResponse.data.UsernameAudit = InspName;
                            HeaderItemResponse.data.DesignationAudit = InspDesignation;
                            HeaderItemResponse.data.DateAudit = InspDate;
                            HeaderItemResponse.data.SignatureAudit = inspSign ?? HeaderItemResponse.data.SignatureAudit ?? null;

                            HeaderItemResponse.data.UseridWit = SelectedVerIndex != -1 ? Convert.ToInt32(DDVerUserListListItems[SelectedVerIndex].Value) : (int?)null;
                            HeaderItemResponse.data.UsernameWit = VerName;
                            HeaderItemResponse.data.DesignationWit = VerDesignation;
                            HeaderItemResponse.data.DateWit = VerDate;
                            HeaderItemResponse.data.SignatureWit = verSign ?? HeaderItemResponse.data.SignatureWit ?? null;

                            HeaderItemResponse.data.UseridWitone = SelectedVerIndex1 != -1 ? Convert.ToInt32(DDVerUserListListItems1[SelectedVerIndex1].Value) : (int?)null;
                            HeaderItemResponse.data.UsernameWitone = VerName1;
                            HeaderItemResponse.data.DesignationWitone = VerDesignation1;
                            HeaderItemResponse.data.DateWitone = VerDate1;
                            HeaderItemResponse.data.SignatureWitone = verSign1 ?? HeaderItemResponse.data.SignatureWitone ?? null;

                            HeaderItemResponse.data.CrewSup = Convert.ToString(SelectedCrewIndex != -1 ? Convert.ToInt32(DDCrewUserListListItems[SelectedCrewIndex]?.Value) : (int?)null);
                            HeaderItemResponse.data.Findings = Findings;
                            HeaderItemResponse.data.FinalResult = FinalResult;
                            HeaderItemResponse.data.FinalResultValue = FinalResultValue;
                            HeaderItemResponse.data.CorrectiveActions = CorrectiveActions;
                            

                            HeaderItemResponse.data.AuditedBy = SelectedAuditBy;
                            HeaderItemResponse.data.AuditType = SelectedAuditplan;
                            HeaderItemResponse.data.Method = SelectedMethod;
                            HeaderItemResponse.data.Type = SelectedType;

                            HeaderItemResponse.data.FormMAD.FmhPkRefNo = App.HeaderCode;
                            HeaderItemResponse.data.FormMAD.A1tallyBox = A1tallyBox;
                            HeaderItemResponse.data.FormMAD.A2tallyBox = A2tallyBox;
                            HeaderItemResponse.data.FormMAD.A3tallyBox = A3tallyBox;
                            HeaderItemResponse.data.FormMAD.A4tallyBox = A4tallyBox;
                            HeaderItemResponse.data.FormMAD.A5tallyBox = A5tallyBox;
                            HeaderItemResponse.data.FormMAD.A6tallyBox = A6tallyBox;
                            HeaderItemResponse.data.FormMAD.A7tallyBox = A7tallyBox;
                            HeaderItemResponse.data.FormMAD.A8tallyBox = A8tallyBox;
                            HeaderItemResponse.data.FormMAD.ActivityFor = ActivityFor;

                            HeaderItemResponse.data.FormMAD.A1total = A1total;
                            HeaderItemResponse.data.FormMAD.A2total = A2total;
                            HeaderItemResponse.data.FormMAD.A3total = A3total;
                            HeaderItemResponse.data.FormMAD.A4total = A4total;
                            HeaderItemResponse.data.FormMAD.A5total = A5total;
                            HeaderItemResponse.data.FormMAD.A6total = A6total;
                            HeaderItemResponse.data.FormMAD.A7total = A7total;
                            HeaderItemResponse.data.FormMAD.A8total = A8total;

                            HeaderItemResponse.data.FormMAD.B1tallyBox = B1tallyBox;
                            HeaderItemResponse.data.FormMAD.B2tallyBox = B2tallyBox;
                            HeaderItemResponse.data.FormMAD.B3tallyBox = B3tallyBox;
                            HeaderItemResponse.data.FormMAD.B4tallyBox = B4tallyBox;
                            HeaderItemResponse.data.FormMAD.B5tallyBox = B5tallyBox;
                            HeaderItemResponse.data.FormMAD.B6tallyBox = B6tallyBox;
                            HeaderItemResponse.data.FormMAD.B7tallyBox = B7tallyBox;
                            HeaderItemResponse.data.FormMAD.B8tallyBox = B8tallyBox;
                            HeaderItemResponse.data.FormMAD.B9tallyBox = B9tallyBox;

                            HeaderItemResponse.data.FormMAD.B1total = B1total;
                            HeaderItemResponse.data.FormMAD.B2total = B2total;
                            HeaderItemResponse.data.FormMAD.B3total = B3total;
                            HeaderItemResponse.data.FormMAD.B4total = B4total;
                            HeaderItemResponse.data.FormMAD.B5total = B5total;
                            HeaderItemResponse.data.FormMAD.B6total = B6total;
                            HeaderItemResponse.data.FormMAD.B7total = B7total;
                            HeaderItemResponse.data.FormMAD.B8total = B8total;
                            HeaderItemResponse.data.FormMAD.B9total = B9total;

                            HeaderItemResponse.data.FormMAD.C1tallyBox = C1tallyBox;
                            HeaderItemResponse.data.FormMAD.C2tallyBox = C2tallyBox;
                            HeaderItemResponse.data.FormMAD.C1total = C1total;
                            HeaderItemResponse.data.FormMAD.C2total = C2total;

                            HeaderItemResponse.data.FormMAD.D1tallyBox = D1tallyBox;
                            HeaderItemResponse.data.FormMAD.D2tallyBox = D2tallyBox;
                            HeaderItemResponse.data.FormMAD.D3tallyBox = D3tallyBox;
                            HeaderItemResponse.data.FormMAD.D4tallyBox = D4tallyBox;
                            HeaderItemResponse.data.FormMAD.D5tallyBox = D5tallyBox;
                            HeaderItemResponse.data.FormMAD.D6tallyBox = D6tallyBox;
                            HeaderItemResponse.data.FormMAD.D7tallyBox = D7tallyBox;
                            HeaderItemResponse.data.FormMAD.D8tallyBox = D8tallyBox;

                            HeaderItemResponse.data.FormMAD.D1total = D1total;
                            HeaderItemResponse.data.FormMAD.D2total = D2total;
                            HeaderItemResponse.data.FormMAD.D3total = D3total;
                            HeaderItemResponse.data.FormMAD.D4total = D4total;
                            HeaderItemResponse.data.FormMAD.D5total = D5total;
                            HeaderItemResponse.data.FormMAD.D6total = D6total;
                            HeaderItemResponse.data.FormMAD.D7total = D7total;
                            HeaderItemResponse.data.FormMAD.D8total = D8total;

                            HeaderItemResponse.data.FormMAD.E1tallyBox = E1tallyBox;
                            HeaderItemResponse.data.FormMAD.E1tallyBox = E1tallyBox;

                            HeaderItemResponse.data.FormMAD.E1total = E1total;
                            HeaderItemResponse.data.FormMAD.E2total = E2total;

                            HeaderItemResponse.data.FormMAD.F1tallyBox = F1tallyBox;
                            HeaderItemResponse.data.FormMAD.F2tallyBox = F2tallyBox;
                            HeaderItemResponse.data.FormMAD.F3tallyBox = F3tallyBox;
                            HeaderItemResponse.data.FormMAD.F4tallyBox = F4tallyBox;
                            HeaderItemResponse.data.FormMAD.F5tallyBox = F5tallyBox;
                            HeaderItemResponse.data.FormMAD.F6tallyBox = F6tallyBox;
                            HeaderItemResponse.data.FormMAD.F7tallyBox = F7tallyBox;


                            HeaderItemResponse.data.FormMAD.F1total = F1total;
                            HeaderItemResponse.data.FormMAD.F2total = F2total;
                            HeaderItemResponse.data.FormMAD.F3total = F3total;
                            HeaderItemResponse.data.FormMAD.F4total = F4total;
                            HeaderItemResponse.data.FormMAD.F5total = F5total;
                            HeaderItemResponse.data.FormMAD.F6total = F6total;
                            HeaderItemResponse.data.FormMAD.F7total = F7total;

                            HeaderItemResponse.data.FormMAD.G1tallyBox = G1tallyBox;
                            HeaderItemResponse.data.FormMAD.G2tallyBox = G2tallyBox;
                            HeaderItemResponse.data.FormMAD.G3tallyBox = G3tallyBox;
                            HeaderItemResponse.data.FormMAD.G4tallyBox = G4tallyBox;
                            HeaderItemResponse.data.FormMAD.G5tallyBox = G5tallyBox;
                            HeaderItemResponse.data.FormMAD.G6tallyBox = G6tallyBox;
                            HeaderItemResponse.data.FormMAD.G7tallyBox = G7tallyBox;
                            HeaderItemResponse.data.FormMAD.G8tallyBox = G8tallyBox;
                            HeaderItemResponse.data.FormMAD.G9tallyBox = G9tallyBox;
                            HeaderItemResponse.data.FormMAD.G10tallyBox = G10tallyBox;

                            HeaderItemResponse.data.FormMAD.G1total = G1total;
                            HeaderItemResponse.data.FormMAD.G2total = G2total;
                            HeaderItemResponse.data.FormMAD.G3total = G3total;
                            HeaderItemResponse.data.FormMAD.G4total = G4total;
                            HeaderItemResponse.data.FormMAD.G5total = G5total;
                            HeaderItemResponse.data.FormMAD.G6total = G6total;
                            HeaderItemResponse.data.FormMAD.G7total = G7total;
                            HeaderItemResponse.data.FormMAD.G8total = G8total;
                            HeaderItemResponse.data.FormMAD.G9total = G9total;
                            HeaderItemResponse.data.FormMAD.G9total = G9total;
                            HeaderItemResponse.data.FormMAD.G10total = G10total;
                            HeaderItemResponse.data.FormMAD.ActivityName = ActivityName;
                            Total = 0;
                            if(A1tallyBox != null)
                                Total += A1tallyBox;
                            if (A2tallyBox != null)
                                Total += A2tallyBox;
                            if (A3tallyBox != null)
                                Total += A3tallyBox;
                            if (A4tallyBox != null)
                                Total += A4tallyBox;
                            if (A5tallyBox != null)
                                Total += A5tallyBox;
                            if (A6tallyBox != null)
                                Total += A6tallyBox;
                            if (A7tallyBox != null)
                                Total += A7tallyBox;
                            if (A8tallyBox != null)
                                Total += A8tallyBox;
                            if (A1total != null)
                                Total += A1total;
                            if (A2total != null)
                                Total += A2total;
                            if (A3total != null)
                                Total += A3total;
                            if (A4total != null)
                                Total += A4total;
                            if (A5total != null)
                                Total += A5total;
                            if (A6total != null)
                                Total += A6total;
                            if (A7total != null)
                                Total += A7total;
                            if (A8total != null)
                                Total += A8total;
                            if (B1tallyBox != null)
                                Total += B1tallyBox;
                            if (B2tallyBox != null)
                                Total += B2tallyBox;
                            if (B3tallyBox != null)
                                Total += B3tallyBox;
                            if (B4tallyBox != null)
                                Total += B4tallyBox;
                            if (B5tallyBox != null)
                                Total += B5tallyBox;
                            if (B6tallyBox != null)
                                Total += B6tallyBox;
                            if (B7tallyBox != null)
                                Total += B7tallyBox;
                            if (B8tallyBox != null)
                                Total += B8tallyBox;
                            if (B9tallyBox != null)
                                Total += B9tallyBox;
                            if (B1total != null)
                                Total += B1total;
                            if (B2total != null)
                                Total += B2total;
                            if (B3total != null)
                                Total += B3total;
                            if (B4total != null)
                                Total += B4total;
                            if (B5total != null)
                                Total += B5total;
                            if (B6total != null)
                                Total += B6total;
                            if (B7total != null)
                                Total += B7total;
                            if (B8total != null)
                                Total += B8total;
                            if (B9total != null)
                                Total += B9total;
                            if (C1tallyBox != null)
                                Total += C1tallyBox;
                            if (C2tallyBox != null)
                                Total += C2tallyBox;
                            if (C1total != null)
                                Total += C1total;
                            if (C2total != null)
                                Total += C2total;
                            if (D1tallyBox != null)
                                Total += D1tallyBox;
                            if (D2tallyBox != null)
                                Total += D2tallyBox;
                            if (D3tallyBox != null)
                                Total += D3tallyBox;
                            if (D4tallyBox != null)
                                Total += D4tallyBox;
                            if (D5tallyBox != null)
                                Total += D5tallyBox;
                            if (D6tallyBox != null)
                                Total += D6tallyBox;
                            if (D7tallyBox != null)
                                Total += D7tallyBox;
                            if (D8tallyBox != null)
                                Total += D8tallyBox;
                            if (D1total != null)
                                Total += D1total;
                            if (D2total != null)
                                Total += D2total;
                            if (D3total != null)
                                Total += D3total;
                            if (D4total != null)
                                Total += D4total;
                            if (D5total != null)
                                Total += D5total;
                            if (D6total != null)
                                Total += D6total;
                            if (D7total != null)
                                Total += D7total;
                            if (D8total != null)
                                Total += D8total;
                            if (E1tallyBox != null)
                                Total += E1tallyBox;
                            if (E2tallyBox != null)
                                Total += E2tallyBox;
                            if (E1total != null)
                                Total += E1total;
                            if (E2total != null)
                                Total += E2total;
                            if (F1tallyBox != null)
                                Total += F1tallyBox;
                            if (F2tallyBox != null)
                                Total += F2tallyBox;
                            if (F3tallyBox != null)
                                Total += F3tallyBox;
                            if (F4tallyBox != null)
                                Total += F4tallyBox;
                            if (F5tallyBox != null)
                                Total += F5tallyBox;
                            if (F6tallyBox != null)
                                Total += F6tallyBox;
                            if (F7tallyBox != null)
                                Total += F7tallyBox;
                            if (F1total != null)
                                Total += F1total;
                            if (F2total != null)
                                Total += F2total;
                            if (F3total != null)
                                Total += F3total;
                            if (F4total != null)
                                Total += F4total;
                            if (F5total != null)
                                Total += F5total;
                            if (F6total != null)
                                Total += F6total;
                            if (F7total != null)
                                Total += F7total;
                            if (G1tallyBox != null)
                                Total += G1tallyBox;
                            if (G2tallyBox != null)
                                Total += G2tallyBox;
                            if (G3tallyBox != null)
                                Total += G3tallyBox;
                            if (G4tallyBox != null)
                                Total += G4tallyBox;
                            if (G5tallyBox != null)
                                Total += G5tallyBox;
                            if (G6tallyBox != null)
                                Total += G6tallyBox;
                            if (G7tallyBox != null)
                                Total += G7tallyBox;
                            if (G8tallyBox != null)
                                Total += G8tallyBox;
                            if (G9tallyBox != null)
                                Total += G9tallyBox;
                            if (G10tallyBox != null)
                                Total += G10tallyBox;
                            if (A2tallyBox != null)
                                Total += G1total;
                            if (G2total != null)
                                Total += G2total;
                            if (G3total != null)
                                Total += G3total;
                            if (G4total != null)
                                Total += G4total;
                            if (G5total != null)
                                Total += G5total;
                            if (G6total != null)
                                Total += G6total;
                            if (G7total != null)
                                Total += G7total;
                            if (G8total != null)
                                Total += G8total;
                            if (G9total != null)
                                Total += G9total;
                            if (G10total != null)
                                Total += G10total;
                            HeaderItemResponse.data.FormMAD.Total = Total;
                            GetTotalDesc(Total.ToString());

                            HeaderItemResponse.data.SubmitSts = type == "save" ? false : true;
                            if (type == "submit") {
                                HeaderItemResponse.data.SubmitSts = true;
                                HeaderItemResponse.data.status = "Submitted"; }
                            var response = await _restApi.UpdateFM(HeaderItemResponse.data);
                            if (response.success)
                            {
                                if (type == "save")
                                    await UserDialogs.Instance.AlertAsync("Data Saved Successfully.", "RAMS", "OK");
                                else
                                {
                             
                                    await UserDialogs.Instance.AlertAsync("Data Submitted Successfully.", "RAMS", "OK");
                                }
                                await CoreMethods.PopPageModel();
                            }
                            else
                            {
                                _userDialogs.Alert(response.errorMessage, "RAMS", "OK");
                            }
                        }
                        else
                        {
                            _userDialogs.Toast(HeaderItemResponse.errorMessage);
                        }
                    }
                    else
                        _userDialogs.Alert("Please check your Internet Connection !");
                }
                catch (Exception ex)
                {
                    _userDialogs.Alert(ex.Message);
                }
                finally
                {
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
        }

        public ICommand SaveAction
        {
            get
            {

                return new Command(async (obj) =>
                {

                    if (string.IsNullOrEmpty(SelectedAuditplan) || SelectedAuditplan == null)
                    {
                        _userDialogs.Alert("Audit plan field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (string.IsNullOrEmpty(SelectedType) || SelectedType == null)
                    {
                        _userDialogs.Alert("Type field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (string.IsNullOrEmpty(SelectedMethod) || SelectedMethod == null)
                    {
                        _userDialogs.Alert("Method field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (string.IsNullOrEmpty(SelectedAuditBy) || SelectedAuditBy == null)
                    {
                        _userDialogs.Alert("Audit By field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    
                    SaveSignature("save");
                });
            }
        }

        public ICommand CancelAction
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (App.ReturnType == "View")
                    {
                        await CoreMethods.PopPageModel();
                    }
                    else
                    {
                        var actionResult = await UserDialogs.Instance.ConfirmAsync(" Unsaved changes will be lost. Are you sure want to cancel?", "RAMS", "Yes", "No");
                        if (actionResult)
                            await CoreMethods.PopPageModel();
                    }
                });
            }
        }

        public ICommand SubmitAction
        {
            get
            {
                return new Command(async (obj) =>
                {

                    if (string.IsNullOrEmpty(SelectedType))
                    {
                        _userDialogs.Alert("Type field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (string.IsNullOrEmpty(SelectedMethod))
                    {
                        _userDialogs.Alert("Method field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (string.IsNullOrEmpty(SelectedAuditplan))
                    {
                        _userDialogs.Alert("AuditPlan field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (string.IsNullOrEmpty(SelectedAuditBy))
                    {
                        _userDialogs.Alert("Audit By field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }


                    if (SelectedInspIndex == -1 || SelectedInspIndex == null)
                    {
                        _userDialogs.Alert("Inspected By User Id field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                   
                    SaveSignature("submit");
                });
            }
        }



    }
}
