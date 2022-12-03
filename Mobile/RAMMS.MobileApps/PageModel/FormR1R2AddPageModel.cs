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
    
        public class FormR1R2AddPageModel : FreshBasePageModel
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
            private int? _selectedSeverity = -1;
        
            private int? _selectedParkingPosition = -1;
            private int? _selectedAccesibility = -1;
            private int? _selectedPotentialHazard = -1;

            private int _selectedVerIndex = -1;
            private int _selectedInspIndex = -1;

            private bool isModify;
            private bool isDelete;
            private bool isView;
            SignaturePadView InspPadView;
            SignaturePadView VerPadView;

            Grid MainGrid;
            public ObservableCollection<FormRImagesDTO> DetailImageList { get; set; }
            Image image { get; set; }
            Label label { get; set; }
            public bool IsAdd { get; set; }
            public bool IsHeaderEnable { get; set; } = true;
            public bool CanSave { get; set; } = false;
            public string SelectedRoadName { get; set; }
            public string SelectedSectionName { get; set; }
            public string SmartSearch { get; set; }
            public DateTime? SelectedDate { get; set; } = null;
            public decimal? RoadLength { get; set; }
            public string SelectedRefNo { get; set; }
            public bool IsVerNameEnabled { get; set; } = false;
            public bool IsInspNameEnabled { get; set; } = false;
            public string InspName { get; set; }
            public string VerName { get; set; }
            public string InspDesignation { get; set; }
            public string VerDesignation { get; set; }
            public ImageSource InspSign { get; set; }
            public ImageSource VerSign { get; set; }
            public DateTime? InspDate { get; set; } = null;
            public DateTime? VerDate { get; set; } = null;
            public int RoadID { get; set; }
            public string PartBServiceProvider { get; set; }
            public string PartCServiceProvider { get; set; }
            public string PartDServiceProvider { get; set; }
            public string PartBConsultant { get; set; }
            public string PartCConsultant { get; set; }
            public string PartDConsultant { get; set; }

            


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

            public ObservableCollection<DDListItems> DDCulvertMarkerSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDWaterwaySeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDEmbankmentSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDHeadwallInletSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDWingwallInletSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDApronInletSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDRiprapInletSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDHeadwallOutletSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDWingwallOutletSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDApronOutletSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDRiprapOutletSeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDCulvertApproachSeverityListItems { get; set; }

            public ObservableCollection<DDListItems> DDCulvertMarkerDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDWaterwayDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDEmbankmentDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDHeadwallInletDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDWingwallInletDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDApronInletDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDRiprapInletDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDHeadwallOutletDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDWingwallOutletDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDApronOutletDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDRiprapOutletDistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDCulvertApproachDistressListItems { get; set; }




            public ObservableCollection<DDListItems> DDBarrel1SeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel2SeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel3SeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel4SeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel5SeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel6SeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel7SeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel8SeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel9SeverityListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel10SeverityListItems { get; set; }



            public ObservableCollection<DDListItems> DDCulvertUnitSeverityListItems { get; set; }

            public ObservableCollection<DDListItems> DDCulvertUnitDistressListItems { get; set; }


            public ObservableCollection<DDListItems> DDBarrel1DistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel2DistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel3DistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel4DistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel5DistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel6DistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel7DistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel8DistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel9DistressListItems { get; set; }
            public ObservableCollection<DDListItems> DDBarrel10DistressListItems { get; set; }


            public ObservableCollection<DDListItems> DDCulvertUnitsDistressListIems { get; set; }
            public FormB1B2DetailRequestDTO SelecteddtlEditItem { get; set; }
            public List<String> SiteRef_multiSelect { get; set; }
            // SiteRef_multiSelect = new List<String>();


            public bool IsCulvertMarkerOthersVisible { get; set; } = false;
            public bool IsWaterwayOthersVisible { get; set; } = false;
            public bool IsEmbankmentOthersVisible { get; set; } = false;
            public bool IsHeadwallInletOthersVisible { get; set; } = false;
            public bool IsWingwallInletOthersVisible { get; set; } = false;
            public bool IsApronInletOthersVisible { get; set; } = false;
            public bool IsRiprapInletOthersVisible { get; set; } = false;
            public bool IsHeadwallOutletOthersVisible { get; set; } = false;
            public bool IsWingwallOutletOthersVisible { get; set; } = false;
            public bool IsApronOutletOthersVisible { get; set; } = false;
            public bool IsRiprapOutletOthersVisible { get; set; } = false;
            public bool IsCulvertApproachOthersVisible { get; set; } = false;



            public bool IsBarrel1DistressOthersVisible { get; set; } = false;
            public bool IsBarrel2DistressOthersVisible { get; set; } = false;
            public bool IsBarrel3DistressOthersVisible { get; set; } = false;
            public bool IsBarrel4DistressOthersVisible { get; set; } = false;
            public bool IsBarrel5DistressOthersVisible { get; set; } = false;
            public bool IsBarrel6DistressOthersVisible { get; set; } = false;
            public bool IsBarrel7DistressOthersVisible { get; set; } = false;
            public bool IsBarrel8DistressOthersVisible { get; set; } = false;
            public bool IsBarrel9DistressOthersVisible { get; set; } = false;
            public bool IsBarrel10DistressOthersVisible { get; set; } = false;

















            private int _selectedCulvertMarkerDistress = -1;

        private int? _selectedWallFunction = -1;
        private int? _selectedWallMembers = -1;
        private int? _selectedFacingType = -1;

        public int? SelectedWallFunction
        {
            get => _selectedWallFunction;
            set
            {
                _selectedWallFunction = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedWallMembers
        {
            get => _selectedWallMembers;
            set
            {
                _selectedWallMembers = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedFacingType
        {
            get => _selectedFacingType;
            set
            {
                _selectedFacingType = value;
                RaisePropertyChanged();
            }
        }















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

                        GetAssetList();
                        SetRefNumber();
                    }
                    RaisePropertyChanged();
                }
            }

            private void SetRefNumber()
            {
                if (SelectedRoadCode != null && SelectedYear > 0)
                    SelectedRefNo = "CI/Form R1/R2/" + DDAssetListItems[SelectedAsset].Text + "/" + DDYearListItems[SelectedYear].Value;
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


        public int SelectedCulvertMarkerDistress
        {
            get { return _selectedCulvertMarkerDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedCulvertMarkerDistress = value;

                    var userprp = DDCulvertMarkerDistressListItems[_selectedCulvertMarkerDistress].Text.Split('-')[1];
                    //if (userprp.ToLower() == "others")
                    //{
                    //    IsCulvertMarkerOthersVisible = true;
                    //}
                    //else
                    //{
                    //    IsCulvertMarkerOthersVisible = false;
                    //}

                }
            }
        }

        public int? SelectedSeverity
        {
            get => _selectedSeverity;
            set
            {
                _selectedSeverity = value;
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


        public int PkRefNo { get; set; }
        public string RefNo { get; set; }
        public int? AiPkRefNo { get; set; }
        public string AssetId { get; set; }
        public string DivCode { get; set; }
        public string RmuCode { get; set; }
        public string RmuName { get; set; }
        public string RdCode { get; set; }
        public string RdName { get; set; }
        public int? LocChKm { get; set; }
        public int? LocChM { get; set; }
        public string StrucCode { get; set; }
        public decimal? GpsEasting { get; set; }
        public decimal? GpsNorthing { get; set; }
        public int? YearOfInsp { get; set; }
        public DateTime? DtOfInsp { get; set; }
        public string WallFunction { get; set; }
        public string WallMember { get; set; }
        public string FacingType { get; set; }
        public int? RecordNo { get; set; }
        public string DistressObserved1 { get; set; }
        public string DistressObserved2 { get; set; }
        public string DistressObserved3 { get; set; }
        public int? Severity { get; set; }
        public int? InspectedBy { get; set; }
        public string InspectedName { get; set; }
        public string InspectedDesig { get; set; }
        public DateTime? InspectedDt { get; set; }
        public bool InspectedSign { get; set; }
        public int? CondRating { get; set; }
        public bool? IssuesFound { get; set; }
        public int? AuditedBy { get; set; }
        public string AuditedName { get; set; }
        public string AuditedDesig { get; set; }
        public DateTime? AuditedDt { get; set; }
        public bool AuditedSign { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
        public FormR2DTO FormG2 { get; set; }
      

        public bool IsView { get; set; } = false;
           // public string RmuCode { get; set; }
            public string SectionCode { get; set; }
            public string SectionName { get; set; }
            //public string Status { get; set; }
            public string DisplayAssetId { get; set; }

            #endregion

            public FormR1R2AddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
            {
                _userDialogs = userDialogs;
                _restApi = restApi;
                _localDatabase = localDatabase;
            }
            public override void Init(object initData)
            {
                base.Init(initData);

               // DDCulvertUnitsDistressListIems = new ObservableCollection<DDListItems>();

                //SelectedFacingType = 0;


               //SelectedWallFunction = 0;

           // SelectedWallMembers = 0;



            IsPhotoTabVisible = false;

                isView = Model.Security.IsView(ModuleNameList.Condition_Inspection);
                IsAdd = isModify = Model.Security.IsModify(ModuleNameList.Condition_Inspection);
                isDelete = Model.Security.IsDelete(ModuleNameList.Condition_Inspection);

                if (App.ReturnType == "Add")
                {
                    FromAdd = true;
                    App.HeaderCode = 0;
                }
            }

            protected async override void ViewIsAppearing(object sender, EventArgs e)
            {
                base.ViewIsAppearing(sender, e);
                await GetLandingDropDownList();
                await GetDDListDetails("division");
                await GetDDListDetails("Year");
                await GetDistressObservListDetails("RWG");
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
                    await GetR1R2ById(App.HeaderCode);
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
                        var hdrresponse = await _restApi.GetR1R2Images(App.HeaderCode);

                        if (hdrresponse.success == true)
                        {
                            var list = hdrresponse.data;
                            DetailImageList = new ObservableCollection<FormRImagesDTO>(hdrresponse.data);

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
                        //_userDialogs.Alert(ex.Message);
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
                                    var response = await _restApi.DeleteR1R2Image(App.HeaderCode, imageID);

                                    if (response.success)
                                    {
                                        await UserDialogs.Instance.AlertAsync("Image deleted successfully.", "RAMS", "0K");

                                        string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

                                        //GetImageList(strDetailCode);
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
                        await CoreMethods.PushPageModel<FormR1R2CameraPopupPageModel>();
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

                            if (SelectedAsset == -1)
                            {
                                await UserDialogs.Instance.AlertAsync("Please select Asset ID", "RAMS", "OK");
                                return;
                            }

                            if (SelectedYear == -1)
                            {
                                await UserDialogs.Instance.AlertAsync("Please select Year Of Inspection", "RAMS", "OK");
                                return;
                            }



                            var response = SaveFormR1R2Header();

                            FromAdd = false;
                        }
                        catch
                        {
                        }


                    });
                }
            }

            private async Task<ObservableCollection<FormR1DTO>> SaveFormR1R2Header()
            {
                try
                {
                    _userDialogs.ShowLoading("Loading");
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        FormR1DTO GridItems = new FormR1DTO()
                        {
                            PkRefNo = App.HeaderCode,
                            AidPkRefNo = Convert.ToInt32(DDAssetListItems[SelectedAsset]?.Value),
                            AssetId = DDAssetListItems[SelectedAsset]?.Text,
                            //AiDivCode = DDDivisionListItems[SelectedDivision]?.Value ?? null,
                            DivCode = SelectedDivision == -1 ? null : DDDivisionListItems[SelectedDivision]?.Value,
                            RmuName = SelectedRMU?.Text,
                            //SectionCode = SelectedSectionCode?.Code,
                            //SectionName = SelectedSectionName,
                            RdCode = SelectedRoadCode?.Value,
                            RdName = SelectedRoadName,
                            YearOfInsp = Convert.ToInt32(DDYearListItems[SelectedYear]?.Value),
                            DtOfInsp = SelectedDate.HasValue ? SelectedDate.Value : (DateTime?)null
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                        var response = await _restApi.SaveR1R2Hdr(GridItems);

                        if (response.success)
                        {
                            IsHeaderEnable = false;
                            //CanSave = true;
                            if (response.data.SubmitSts)
                                CanSave = false;
                            else
                                CanSave = true;
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

            private async Task<int> GetR1R2ById(int headerCode)
            {
                try
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        var response = await _restApi.GetR1R2ById(headerCode);

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

        private void SetViewData(ResponseBaseObject<FormR1DTO> response)
        {
            SelectedRefNo = response.data.RefNo;
            App.InspReferenceNo = response.data.RefNo;
            AssetId = response.data.AssetId;
            YearOfInsp = response.data.YearOfInsp;
            SelectedYear = DDYearListItems.ToList().FindIndex(x => x.Value == response.data.YearOfInsp.ToString());
            SelectedDate = response.data.DtOfInsp.HasValue ? response.data.DtOfInsp.Value : (DateTime?)null;
            DivCode = response.data.DivCode;
            RmuName = response.data.RmuName;
            RdCode = response.data.RdCode;
            RdName = response.data.RdName;
            StrucCode = response.data.StrucCode;
            LocChKm = response.data.LocChKm;
            LocChM = response.data.LocChM;
            GpsEasting = response.data.GpsEasting;
            GpsNorthing = response.data.GpsNorthing;
            SelectedSeverity = response.data.Severity != null ? Convert.ToInt32(response.data.Severity) : (int?)null; //Convert.ToInt32(response.data.Severity);
            SelectedWallFunction = response.data.WallFunction != null ? Convert.ToInt32(response.data.WallFunction) : (int?)null; //Convert.ToInt32(WallFunction); 
            SelectedWallMembers = response.data.WallMember != null ? Convert.ToInt32(response.data.WallMember) : (int?)null; //Convert.ToInt32(WallMember);
            SelectedFacingType = response.data.FacingType != null ? Convert.ToInt32(response.data.FacingType) : (int?)null;//Convert.ToInt32(FacingType);
            InspectedBy = response.data.InspectedBy;
            InspectedName = response.data.InspectedName;
            InspectedDesig = response.data.InspectedDesig;
            InspectedDt = response.data.InspectedDt;
            InspectedSign = true;//response.data.InspectedSign;
         
            AuditedBy = response.data.AuditedBy;
            AuditedName = response.data.AuditedName;
            AuditedDesig = response.data.AuditedDesig;
            AuditedDt = response.data.AuditedDt;
            AuditedSign = true; //response.data.AuditedSign;
            PartBServiceProvider = response.data.FormR2.DistressSp;
            PartBConsultant = response.data.FormR2.DistressEc;
            PartCServiceProvider = response.data.FormR2.GeneralSp;
            PartCConsultant = response.data.FormR2.GeneralEc;
            PartDServiceProvider = response.data.FormR2.FeedbackSp;
            PartDConsultant = response.data.FormR2.FeedbackEc;

            ModBy = response.data.ModBy;
            ModDt = response.data.ModDt;
            CrBy = response.data.CrBy;
            CrDt = response.data.CrDt;
            SubmitSts = response.data.SubmitSts;
            ActiveYn = response.data.ActiveYn;
            Status = response.data.Status;
            AuditLog = response.data.AuditLog;

            SelectedCulvertMarkerDistress = DDCulvertMarkerDistressListItems.ToList().FindIndex(x => x.Value == response.data.DistressObserved1.ToString());

            SelectedConditionRating = response.data.CondRating != null ? Convert.ToInt32(response.data.CondRating) : (int?)null;//response.data.CondRating - 1;

            SelectedFutureInvestigation = response.data.IssuesFound != null ? (response.data.IssuesFound == true ? 0 : 1) : (int?)null;

            int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.InspectedBy);
            SelectedInspIndex = inspindex;
            InspName = response.data.InspectedName;
            InspDesignation = response.data.InspectedDesig;
            InspDate = response.data.InspectedDt.HasValue ? response.data.InspectedDt.Value : (DateTime?)null;
            InspSign = ImageSource.FromStream(
               () => new MemoryStream(Convert.FromBase64String(response.data.InspectedSignature)));

            int verindex = DDVerUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.AuditedBy);
            SelectedVerIndex = verindex;
            VerName = response.data.AuditedName;
            VerDesignation = response.data.AuditedDesig;
            VerDate = response.data.AuditedDt.HasValue ? response.data.AuditedDt.Value : (DateTime?)null;
            VerSign = ImageSource.FromStream(
                () => new MemoryStream(Convert.FromBase64String(response.data.AuditedSignature)));


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
                            GrpCode = "GR"
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



        private async Task<int> GetDistressObservListDetails(string ddtype)
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
                    var response = await _restApi.GetDistressobserved(ddlist);

                    if (response.success)
                    {
                        if (ddtype == "RWG")
                            DDCulvertMarkerDistressListItems = new ObservableCollection<DDListItems>(response.data);
                       
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

                try
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        var HeaderItemResponse = await _restApi.GetR1R2ById(App.HeaderCode);

                        if (HeaderItemResponse.success)
                        {
                            HeaderItemResponse.data.PkRefNo = App.HeaderCode;

                            HeaderItemResponse.data.DtOfInsp = SelectedDate;

                            if (SelectedSeverity != null && SelectedSeverity != -1)
                                HeaderItemResponse.data.Severity = SelectedSeverity;
                            if (SelectedWallFunction != null && SelectedWallFunction != -1)
                                HeaderItemResponse.data.WallFunction = Convert.ToString(SelectedWallFunction);
                            if (SelectedWallMembers != null && SelectedWallMembers != -1)
                                HeaderItemResponse.data.WallMember = Convert.ToString(SelectedWallMembers);
                            if (SelectedFacingType != null && SelectedFacingType != -1)
                                HeaderItemResponse.data.FacingType = Convert.ToString(SelectedFacingType);
                            if (SelectedCulvertMarkerDistress != null && SelectedCulvertMarkerDistress != -1)
                                HeaderItemResponse.data.DistressObserved1 = SelectedCulvertMarkerDistress.ToString() == "-1" ? null : DDCulvertMarkerDistressListItems[SelectedCulvertMarkerDistress]?.Value;

                            HeaderItemResponse.data.AuditedBy = SelectedVerIndex != -1 ? Convert.ToInt32(DDVerUserListListItems[SelectedVerIndex].Value) : (int?)null;
                            HeaderItemResponse.data.AuditedName = VerName;
                            HeaderItemResponse.data.AuditedDesig = VerDesignation;
                            HeaderItemResponse.data.AuditedDt = VerDate;
                            HeaderItemResponse.data.AuditedSign = true;    //inspSign ?? HeaderItemResponse.data.AuditedSign ?? null;
                            HeaderItemResponse.data.AuditedSignature = verSign ?? HeaderItemResponse.data.AuditedSignature ?? null;

                            HeaderItemResponse.data.InspectedBy = SelectedInspIndex != -1 ? Convert.ToInt32(DDInspUserListListItems[SelectedInspIndex]?.Value) : (int?)null;
                            HeaderItemResponse.data.InspectedName = InspName;
                            HeaderItemResponse.data.InspectedDesig = InspDesignation;
                            HeaderItemResponse.data.InspectedDt = InspDate;
                            HeaderItemResponse.data.InspectedSign = true;  //verSign ?? HeaderItemResponse.data.InspectedSign ?? null;
                            HeaderItemResponse.data.InspectedSignature = inspSign ?? HeaderItemResponse.data.InspectedSignature ?? null;

                            HeaderItemResponse.data.SubmitSts = type == "save" ? false : true;
                            
                            if (SelectedFutureInvestigation != null && SelectedFutureInvestigation != -1)
                                HeaderItemResponse.data.IssuesFound = SelectedFutureInvestigation == 0 ? true : false;

                            if (SelectedConditionRating != null && SelectedConditionRating != -1)
                                HeaderItemResponse.data.CondRating = SelectedConditionRating + 1;

                            HeaderItemResponse.data.FormR2.FR1hPkRefNo = App.HeaderCode;

                            HeaderItemResponse.data.FormR2.DistressSp = PartBServiceProvider;
                            HeaderItemResponse.data.FormR2.DistressEc = PartBConsultant;
                            HeaderItemResponse.data.FormR2.GeneralSp = PartCServiceProvider;
                            HeaderItemResponse.data.FormR2.GeneralEc = PartCConsultant;
                            HeaderItemResponse.data.FormR2.FeedbackSp = PartDServiceProvider;
                            HeaderItemResponse.data.FormR2.FeedbackEc = PartDConsultant;
                           
                            if (type == "submit") { HeaderItemResponse.data.SubmitSts = true; HeaderItemResponse.data.Status = "Submitted"; }

                            var response = await _restApi.UpdateR1R2(HeaderItemResponse.data);

                            if (response.success)
                            {
                                if (type == "save")
                                    await UserDialogs.Instance.AlertAsync("Data Saved Successfully.", "RAMS", "OK");
                                else
                                    await UserDialogs.Instance.AlertAsync("Data Submitted Successfully.", "RAMS", "OK");

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

                        if (SelectedConditionRating == -1 || SelectedConditionRating == null)
                        {
                            _userDialogs.Alert("Overall Condition Rating field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedFutureInvestigation == -1 || SelectedFutureInvestigation == null)
                        {
                            _userDialogs.Alert("Further Investigation field is required. Please choose from the dropdown list.", "RAMS", "Ok");
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

                        if (SelectedWallFunction == -1 || SelectedWallFunction == null)
                        {
                            _userDialogs.Alert("Wall Function field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedWallMembers == -1 || SelectedWallMembers == null)
                        {
                            _userDialogs.Alert("Wall Members field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedFacingType == -1 || SelectedFacingType == null)
                        {
                            _userDialogs.Alert("Facing Type field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }

                        if (SelectedDate == null)
                        {
                            _userDialogs.Alert("Date of Inspection is required.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedCulvertMarkerDistress == -1 || SelectedCulvertMarkerDistress == null)
                        {
                            _userDialogs.Alert("Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }

                        //if (SelectedCulvertMarkerSeverity == -1 || SelectedCulvertMarkerSeverity == null)
                        //{
                        //    _userDialogs.Alert("Culver Marker (1A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}



                        //if (SelectedWaterwayDistress == -1 || SelectedWaterwayDistress == null)
                        //{
                        //    _userDialogs.Alert("Waterway (2A) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedWaterwaySeverity == -1 || SelectedWaterwaySeverity == null)
                        //{
                        //    _userDialogs.Alert("Waterway (2A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}


                        //if (SelectedEmbankmentDistress == -1 || SelectedEmbankmentDistress == null)
                        //{
                        //    _userDialogs.Alert("Embankment (Revetment) (2B) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedEmbankmentSeverity == -1 || SelectedEmbankmentSeverity == null)
                        //{
                        //    _userDialogs.Alert("Embankment (Revetment) (2B) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}


                        //if (SelectedHeadwallInletDistress == -1 || SelectedHeadwallInletDistress == null)
                        //{
                        //    _userDialogs.Alert("Headwall (Inlet) (3A) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedHeadwallInletSeverity == -1 || SelectedHeadwallInletSeverity == null)
                        //{
                        //    _userDialogs.Alert("Headwall (Inlet) (3A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedWingwallInletDistress == -1 || SelectedWingwallInletDistress == null)
                        //{
                        //    _userDialogs.Alert("Wingwall (Inlet) (3B) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedWingwallInletSeverity == -1 || SelectedWingwallInletSeverity == null)
                        //{
                        //    _userDialogs.Alert("Wingwall (Inlet) (3B) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}



                        //if (SelectedApronInletDistress == -1 || SelectedApronInletDistress == null)
                        //{
                        //    _userDialogs.Alert("Apron (Inlet) (3C) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedApronInletSeverity == -1 || SelectedApronInletSeverity == null)
                        //{
                        //    _userDialogs.Alert("Apron (Inlet) (3C) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}



                        //if (SelectedRiprapInletDistress == -1 || SelectedRiprapInletDistress == null)
                        //{
                        //    _userDialogs.Alert("Riprap (Inlet) (3D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedRiprapInletSeverity == -1 || SelectedRiprapInletSeverity == null)
                        //{
                        //    _userDialogs.Alert("Riprap (Inlet) (3D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedHeadwallOutletDistress == -1 || SelectedHeadwallOutletDistress == null)
                        //{
                        //    _userDialogs.Alert("Headwall (Outlet) (3E) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedHeadwallOutletSeverity == -1 || SelectedHeadwallOutletSeverity == null)
                        //{
                        //    _userDialogs.Alert("Headwall (Outlet) (3E) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}


                        //if (SelectedWingwallOutletDistress == -1 || SelectedWingwallOutletDistress == null)
                        //{
                        //    _userDialogs.Alert("Wingwall(Outlet) (3F) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedWingwallOutletSeverity == -1 || SelectedWingwallOutletSeverity == null)
                        //{
                        //    _userDialogs.Alert("Wingwall(Outlet) (3F) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}






                        //if (SelectedApronOutletDistress == -1 || SelectedApronOutletDistress == null)
                        //{
                        //    _userDialogs.Alert("Apron (Outlet) (3G) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedApronOutletSeverity == -1 || SelectedApronOutletSeverity == null)
                        //{
                        //    _userDialogs.Alert("Apron (Outlet) (3G) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedRiprapOutletDistress == -1 || SelectedRiprapOutletDistress == null)
                        //{
                        //    _userDialogs.Alert("Riprap (Outlet) (3H) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedRiprapOutletSeverity == -1 || SelectedRiprapOutletSeverity == null)
                        //{
                        //    _userDialogs.Alert("Riprap (Outlet) (3H) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}









                        //if (SelectedBarrel1DistressDistress == -1 || SelectedBarrel1DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #1 (4A) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel1SeveritySeverity == -1 || SelectedBarrel1SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #1 (4A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel2DistressDistress == -1 || SelectedBarrel2DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #2 (4B) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel2SeveritySeverity == -1 || SelectedBarrel2SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #2 (4B) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel3DistressDistress == -1 || SelectedBarrel3DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #3 (4C) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel3SeveritySeverity == -1 || SelectedBarrel3SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #3 (4C) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}


                        //if (SelectedBarrel4DistressDistress == -1 || SelectedBarrel4DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #4 (4D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel4SeveritySeverity == -1 || SelectedBarrel4SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #4 (4D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}


                        //if (SelectedBarrel5DistressDistress == -1 || SelectedBarrel5DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #5 (5D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel5SeveritySeverity == -1 || SelectedBarrel5SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #5 (5D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}



                        //if (SelectedBarrel6DistressDistress == -1 || SelectedBarrel6DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #6 (6D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel6SeveritySeverity == -1 || SelectedBarrel6SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #6 (6D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}


                        //if (SelectedBarrel7DistressDistress == -1 || SelectedBarrel7DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #7 (7D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel7SeveritySeverity == -1 || SelectedBarrel7SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #7 (7D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}


                        //if (SelectedBarrel8DistressDistress == -1 || SelectedBarrel8DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #8 (8D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel8SeveritySeverity == -1 || SelectedBarrel8SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #8 (8D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}


                        //if (SelectedBarrel9DistressDistress == -1 || SelectedBarrel9DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #9 (9D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel9SeveritySeverity == -1 || SelectedBarrel9SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #9 (9D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}



                        //if (SelectedBarrel10DistressDistress == -1 || SelectedBarrel10DistressDistress == null)
                        //{
                        //    _userDialogs.Alert("Barrel #10 (10D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedBarrel10SeveritySeverity == -1 || SelectedBarrel10SeveritySeverity == null)
                        //{
                        //    _userDialogs.Alert("Barrel #10 (10D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}





                        //if (SelectedCulvertApproachDistress == -1 || SelectedCulvertApproachDistress == null)
                        //{
                        //    _userDialogs.Alert("Culvert Approach Condition (5A) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        //if (SelectedCulvertApproachSeverity == -1 || SelectedCulvertApproachSeverity == null)
                        //{
                        //    _userDialogs.Alert("Culvert Approach Condition (5A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        if (SelectedInspIndex == -1 || SelectedInspIndex == null)
                        {
                            _userDialogs.Alert("Inspected By User Id field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }

                        if (InspDate == null)
                        {
                            _userDialogs.Alert("Inspected by date is required.", "RAMS", "Ok");
                            return;
                        }

                        if (SelectedConditionRating == -1 || SelectedConditionRating == null)
                        {
                            _userDialogs.Alert("Overall Condition Rating field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedFutureInvestigation == -1 || SelectedFutureInvestigation == null)
                        {
                            _userDialogs.Alert("Further Investigation field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        SaveSignature("submit");
                    });
                }
            }



        }

        //public class CulvertListData : FreshBasePageModel
        //{
        //    public int SelectedCulvertUnitSeverity { get; set; } = -1;
        //    private int _selectedCulvertUnitDistress = -1;
        //    public bool IsCulvertUnitOthersVisible { get; set; } = false;
        //    public int SelectedCulvertUnitDistress
        //    {
        //        get { return _selectedCulvertUnitDistress; }
        //        set
        //        {
        //            if (value != -1)
        //            {
        //                _selectedCulvertUnitDistress = value;

        //                var userprp = CulvertUnitDistressList[_selectedCulvertUnitDistress].Text.Split('-')[1];
        //                if (userprp.ToLower() == "others")
        //                {
        //                    IsCulvertUnitOthersVisible = true;
        //                }
        //                else
        //                {
        //                    IsCulvertUnitOthersVisible = false;
        //                }

        //            }
        //        }
        //    }

        //    public string CulvertName { get; set; }
        //    public ObservableCollection<DDListItems> CulvertUnitDistressList { get; set; }
        //    public ObservableCollection<DDListItems> CulvertUnitSeverityList { get; set; }


        //    public ICommand LocationSelectionCommand
        //    {
        //        get
        //        {
        //            return new Command((obj) =>
        //            {

        //                // CurrentPage.Navigation.PushPopupAsync(new FormB1B2CulvertUnit(CulvertUnitDistressList));
        //            });
        //        }
        //    }
        //}
    
}
