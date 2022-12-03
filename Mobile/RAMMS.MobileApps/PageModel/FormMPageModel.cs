using Acr.UserDialogs;
using FreshMvvm;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    class FormMPageModel : FreshBasePageModel
    {
        #region Private fields

        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private AssetDDLResponseDTO.DropDown _selectedRMU;
        private AssetDDLResponseDTO.DropDown _selectedRoadCode;
        private AssetDDLResponseDTO.DropDown _selectedSectionCode;
        private DDListItems _selectedAssetType;
        private DDListItems _selectedActivity;
        private DDListItems _selectedBound;
        private DDListItems _selectedFromYear;
        private DDListItems _selectedToYear;
        private int iValueRet = 1;
        private int _selectedPageSize = 0;

        ExtendedPicker activitycode;


        private bool isModify;
        private bool isDelete;
        private bool isView;
        #endregion

        #region Public Properties
        public string[] ColumnName = { null, "RefID", "AuditDate", "RMUCode", "RMUDesc", "RoadCode", "RoadName", "ActivityCode", "ClosureType", "Method","Status", "AuditedBy", "WitnessedBy" };
        public List<Column> ColumnList { get; set; }
        public DateTime? SelectedToDate { get; set; } = null;
        public DateTime? SelectedFromDate { get; set; } = null;
        public string SelectedRMUName { get; set; }
        public string SelectedRoadName { get; set; }
        public string SelectedSectionName { get; set; }
        public string SelectedActivityName { get; set; }
        public DateTime? AuditedDate { get; set; }
        public string AuditTimeFrm { get; set; }
        public string AuditTimeTo { get; set; }
        public bool IsAdd { get; set; }
        public bool DetailSearchVisible { get; set; } = false;
        public string SmartSearch { get; set; }
        public string FromChKM { get; set; }
        public string FromChM { get; set; }
        public string ToChKM { get; set; }
        public string ToChM { get; set; }
        public string TotalRecords { get; set; } = "0";
        public string PageStart { get; set; } = "0";
        public string PageEnd { get; set; } = "0";
        public int PageSize { get; set; } = 10;
        public string SortOrder { get; set; } = "0";
        public bool IsRefNoAssending { get; set; } = false;
        public bool IsActivityCodeAssending { get; set; } = false;
        public bool IsTypeOfClosureAssending { get; set; } = false;
        public bool IsMethodAssending { get; set; } = false;
        public bool IsStatusAssending { get; set; } = false;
        public bool IsWitnessedByAssending { get; set; } = false;
        public bool IsYearOfInspectionAssending { get; set; } = false;
        public bool IsDateOfInspectAssending { get; set; } = false;
        public bool IsAssetIDAssending { get; set; } = false;
        public bool IsRMUAssending { get; set; } = false;
        public bool IsRMUNameAssending { get; set; } = false;
        public bool IsSectionCodeAssending { get; set; } = false;
        public bool IsSectionNameAssending { get; set; } = false;
        public bool IsRoadCodeAssending { get; set; } = false;
        public bool IsRoadNameAssending { get; set; } = false;
        public bool IsLocationChAssending { get; set; } = false;
        public bool IsCulvertDiameterAssending { get; set; } = false;
        public bool IsCulvertWidthAssending { get; set; } = false;
        public bool IsInspectedByAssending { get; set; } = false;
        public bool IsAuditedByAssending { get; set; } = false;
        public int ColumnIndex { get; set; }
        public SearchRequestDTO SearchCriteriaItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }
       // public ObservableCollection<DDListItems> DDActivityListItems { get; set; }

        public ObservableCollection<FormMSearchGridDTO> MyBaseFormMListViewItems { get; set; }
        public ObservableCollection<DDListItems> DDAssetTypeListItems { get; set; }
        public ObservableCollection<DDListItems> DDFromYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDToYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDBoundListItems { get; set; }
        public ObservableCollection<DDListItems> DDActivityListItems { get; set; }

        public DDListItems SelectedAssetType
        {
            get => _selectedAssetType;
            set
            {
                _selectedAssetType = value;
            }
        }


        public DDListItems SelectedActivity
        {
            get => _selectedActivity;
            set
            {
                _selectedActivity = value;
                if (_selectedActivity != null)
                    SelectedActivityName = _selectedActivity.Text.Split('-')[1].ToString(); if (_selectedActivity != null);
            }
        }

        public DDListItems SelectedBound
        {
            get => _selectedBound;
            set
            {
                _selectedBound = value;
            }
        }

        public AssetDDLResponseDTO.DropDown SelectedRMU
        {
            get => _selectedRMU;
            set
            {
                _selectedRMU = value;
                if (_selectedRMU != null)
                    SelectedRMUName = _selectedRMU.Text.Split('-')[1].ToString(); if (_selectedRMU != null)
                    //SelectedRMUName = _selectedRMU.Text.Split('-')[1].ToString();
                SelectedSectionCode = SelectedRoadCode = null;
                SelectedSectionName = SelectedRoadName = "";
                GetLandingDropDownList();
                RaisePropertyChanged();
            }
        }
        public AssetDDLResponseDTO.DropDown SelectedRoadCode
        {
            get => _selectedRoadCode;
            set
            {
                _selectedRoadCode = value;
                if (_selectedRoadCode != null)
                    SelectedRoadName = _selectedRoadCode.Text.Split('-')[1].ToString();
                RaisePropertyChanged();
            }
        }
        public AssetDDLResponseDTO.DropDown SelectedSectionCode
        {
            get => _selectedSectionCode;
            set
            {
                _selectedSectionCode = value;
                if (_selectedSectionCode != null)
                    SelectedSectionName = _selectedSectionCode.Text.Split('-')[1].ToString();

                SelectedRoadCode = null;
                SelectedRoadName = "";
                GetLandingDropDownList();
                RaisePropertyChanged();
            }
        }

        //public AssetDDLResponseDTO.DropDown SelectedActivity
        //{
        //    get => _selectedSectionCode;
        //    set
        //    {
        //        _selectedSectionCode = value;
        //        if (_selectedSectionCode != null)
        //            SelectedSectionName = _selectedSectionCode.Text.Split('-')[1].ToString();

        //        SelectedRoadCode = null;
        //        SelectedRoadName = "";
        //        GetLandingDropDownList();
        //        RaisePropertyChanged();
        //    }
        //}


        public DDListItems SelectedFromYear
        {
            get => _selectedFromYear;
            set
            {
                _selectedFromYear = value;
            }
        }

        public DDListItems SelectedToYear
        {
            get => _selectedToYear;
            set
            {
                _selectedToYear = value;
            }
        }

        public int SelectedPageSize
        {
            get => _selectedPageSize;
            set
            {
                _selectedPageSize = value;
                SetPageSize(value);
            }
        }

        #endregion

        #region Constructor
        public FormMPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }

        #endregion

        #region Init and Appearing
        public override void Init(object initData)
        {
            base.Init(initData);

            isView = Model.Security.IsView(ModuleNameList.Condition_Inspection);
            IsAdd = isModify = Model.Security.IsModify(ModuleNameList.Condition_Inspection);
            isDelete = Model.Security.IsDelete(ModuleNameList.Condition_Inspection);


            activitycode = CurrentPage.FindByName<ExtendedPicker>("ActCodePicker");

            DDRodeCodeListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            DDSectionListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            DDActivityListItems = new ObservableCollection<DDListItems>();

            MyBaseFormMListViewItems = new ObservableCollection<FormMSearchGridDTO>();
            ColumnList = new List<Column>();
            foreach (var item in ColumnName)
            {
                Column column = new Column() { data = item };
                ColumnList.Add(column);
            }
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            GetLandingDropDownList();
            GetDDListDetails("Asset type");
            GetActivityDetails("ACT-QA1");
            GetDDListDetails("Year");
            //GetDDListDetails("bound");
            GetGridData();
        }

        #endregion

        #region Methods
        private async void GetGridData()
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var values = (iValueRet - 1) * PageSize > 0 ? (iValueRet - 1) * PageSize : 0;

                    SearchCriteriaItems = new SearchRequestDTO()
                    {
                        start = values,
                        length = PageSize,
                        columns = ColumnList,
                        order = ColumnIndex >= 1 ? new List<Order>() { new Order() { column = ColumnIndex, dir = SortOrder == "1" ? "asc" : "" } } : null,
                        filter = new Dictionary<string, string>()
                        {
                            { "KeySearch", SmartSearch},
                            { "AssetType", SelectedAssetType?.Value },
                            { "RMUCode", SelectedRMU?.Value },
                            { "SecName", SelectedSectionCode?.Value },
                            { "RoadCode", SelectedRoadCode?.Value },
                            { "FromYear", SelectedFromYear?.Value },
                            { "ToYear", SelectedToYear?.Value },
                            { "ActivityCode" , SelectedActivity?.Value },
                            { "FromAuditDate" , SelectedFromDate?.ToString() },
                            { "ToAuditDate" , SelectedToDate?.ToString() },
                    
                        }
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(SearchCriteriaItems);

                    var response = await _restApi.GetFormMLandingGridData(SearchCriteriaItems);

                    var response1 = JsonConvert.DeserializeObject<List<FormMSearchGridDTO>>(response.data.data.ToString());

                    if (response.success)
                    {
                        TotalRecords = response.data.recordsTotal.ToString();
                        PageStart = response.data.recordsTotal == 0 ? "0" : (values + 1).ToString();
                        PageEnd = (values + PageSize) > response.data.recordsTotal ? TotalRecords : (values + PageSize).ToString();

                        var listItems = new ObservableCollection<FormMSearchGridDTO>(response1);

                        var sNo = 1;
                        foreach (var ivalue in listItems)
                        {
                            ivalue.SNo = sNo;
                            sNo++;
                        }

                        MyBaseFormMListViewItems = listItems;
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
                _userDialogs.HideLoading();
            }
        }

        private async void GetDDListDetails(string ddtype)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                        TypeCode = ddtype == "Asset type" ? "CV" : ddtype == "bound" ? "CV" : ""
                    };

                    ResponseBaseListObject<DDListItems> response;
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    if (ddtype == "Asset type")
                        response = await _restApi.GetDDLDescValueConcat(ddlist);
                    else if (ddtype == "bound")
                        response = await _restApi.GetDDLDescValueConcat(ddlist);
                    else
                        response = await _restApi.GetDDList(ddlist);

                    if (response.success)
                    {
                        if (ddtype == "Asset type")
                            DDAssetTypeListItems = new ObservableCollection<DDListItems>(response.data);
                        else if (ddtype == "Year")
                        {
                            DDFromYearListItems = new ObservableCollection<DDListItems>(response.data);
                            DDToYearListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                        else if (ddtype == "bound")
                            DDBoundListItems = new ObservableCollection<DDListItems>(response.data);
                        
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
        }


        private async void GetActivityDetails(string ddtype)
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
                        {
                            DDActivityListItems = new ObservableCollection<DDListItems>(response.data);
                            //activitycode.ItemsSource = DDActivityListItems.Select((DDListItems arg) => arg.Value + " - " + arg.Text).ToList();
                        }
                            
                      
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
            //return 1;
        }

        private void SetPageSize(int value)
        {
            iValueRet = 1;
            switch (value)
            {
                case 0:
                    PageSize = 10;
                    break;
                case 1:
                    PageSize = 25;
                    break;
                case 2:
                    PageSize = 50;
                    break;
                case 3:
                    PageSize = 100;
                    break;
                case 4:
                    PageSize = 500;
                    break;
                case 5:
                    PageSize = 1000;
                    break;
            }
            GetGridData();
        }

        private void GetSortOrder(int columnIndex)
        {
            if (columnIndex == 1)
                SortOrder = IsRefNoAssending ? "1" : "0";
            if (columnIndex == 2)
                SortOrder = IsDateOfInspectAssending ? "1" : "0";
            if (columnIndex == 3)
                SortOrder = IsRMUAssending ? "1" : "0";
            if (columnIndex == 4)
                SortOrder = IsRMUNameAssending ? "1" : "0";
            if (columnIndex == 5)
                SortOrder = IsRoadCodeAssending ? "1" : "0";
            if (columnIndex == 6)
                SortOrder = IsRoadNameAssending ? "1" : "0";
            if (columnIndex == 7)
                SortOrder = IsActivityCodeAssending ? "1" : "0";
            if (columnIndex == 8)
                SortOrder = IsTypeOfClosureAssending ? "1" : "0";
            if (columnIndex == 9)
                SortOrder = IsMethodAssending ? "1" : "0";
            if (columnIndex == 10)
                SortOrder = IsStatusAssending ? "1" : "0";
            if (columnIndex == 11)
                SortOrder = IsAuditedByAssending ? "1" : "0";
            if (columnIndex == 12)
                SortOrder = IsWitnessedByAssending ? "1" : "0";
            
        }
        private void SetSortOrder(int columnIndex)
        {
            if (columnIndex == 1)
            {
                IsRefNoAssending = !IsRefNoAssending;
                IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending  = false;
            }
            else if (columnIndex == 2)
            {
                IsDateOfInspectAssending = !IsDateOfInspectAssending;
                IsRefNoAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending = false;
            }
            else if (columnIndex == 3)
            {
                IsRMUAssending = !IsRMUAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending = false;
            }
            else if (columnIndex == 4)
            {
                IsRMUNameAssending = !IsRMUNameAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending = false;
            }
            else if (columnIndex == 5)
            {
                IsRoadCodeAssending = !IsRoadCodeAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending = false;
            }
            else if (columnIndex == 6)
            {
                IsRoadNameAssending = !IsRoadNameAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending = false;
            }
            else if (columnIndex == 7)
            {
                IsActivityCodeAssending = !IsActivityCodeAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending = false;

            }
            else if (columnIndex == 8)
            {
                IsTypeOfClosureAssending = !IsTypeOfClosureAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending = false;

            }
            else if (columnIndex == 9)
            {
                IsMethodAssending = !IsMethodAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending = false;

            }
            else if (columnIndex == 10)
            {
                IsStatusAssending = !IsStatusAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsAuditedByAssending = IsWitnessedByAssending = false;

            }
            else if (columnIndex == 11)
            {
                IsAuditedByAssending = !IsAuditedByAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsWitnessedByAssending = false;

            }
            else if (columnIndex == 12)
            {
                IsWitnessedByAssending = !IsWitnessedByAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = false;

            }
            else
            {
                IsRefNoAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsActivityCodeAssending = IsTypeOfClosureAssending = IsMethodAssending = IsStatusAssending = IsAuditedByAssending = IsWitnessedByAssending = false;
            }
        }

        public async void GetLandingDropDownList(string propName = null)
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
        }

        private int? ConvertToNullableInt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return Convert.ToInt32(value);
        }

        public async Task<int> DeleteRecord(int pkRefNo)
        {
            try
            {
                _userDialogs.ShowLoading("loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.DeleteFMHeader(pkRefNo);
                    if (response.success)
                    {
                        if (response.success)
                        {
                            await UserDialogs.Instance.AlertAsync("Data Deleted Successfully.", "RAMS", "0K");
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

        #endregion

        #region Commands

        public ICommand ClickMeAction
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedRowItem = (FormMSearchGridDTO)obj;

                    App.HeaderCode = SelectedRowItem.RefNo;
                    var actionResult = "";

                    string view = isView ? "View" : "";
                    string delete = isDelete ? "Delete" : "";
                    string edit = isModify && SelectedRowItem.Status == "Saved" ? "Edit" : "";

                    string[] permissions = new string[] { edit, view, delete };
                    permissions = permissions.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, permissions);

                    if (actionResult == "Delete")
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            await DeleteRecord(SelectedRowItem.RefNo);
                            GetGridData();
                            return;
                        }
                    }
                    if (actionResult == "Edit")
                    {
                        App.ReturnType = "Edit";
                        await CoreMethods.PushPageModel<FormMAddPageModel>();
                    }
                    else if (actionResult == "View")
                    {
                        App.ReturnType = "View";
                        await CoreMethods.PushPageModel<FormMAddPageModel>();
                    }
                });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    iValueRet = 1;
                    GetGridData();
                });
            }
        }

        public ICommand SearchExpandCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    DetailSearchVisible = !DetailSearchVisible;
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    App.ReturnType = "Add";
                    await CoreMethods.PushPageModel<FormMAddPageModel>();
                });
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    SelectedRMU = SelectedSectionCode = SelectedRoadCode = null;
                    SelectedAssetType = SelectedBound = SelectedFromYear = SelectedToYear = null;
                    SelectedSectionName = SelectedRoadName = SmartSearch = "";
                    SelectedActivity = SelectedActivity = null;
                    SelectedActivityName = "";
                    SelectedToDate = null;
                    SelectedFromDate = null;
                    SmartSearch = null;
                    FromChKM = ToChKM = null;
                    FromChM = ToChM = "";

                    GetGridData();
                    GetLandingDropDownList();
                });
            }
        }

        public ICommand btnPreviousCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (iValueRet > 1)
                        iValueRet = iValueRet - 1;

                    GetGridData();
                });
            }
        }

        public ICommand btnNextCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (iValueRet <= (Convert.ToInt32(TotalRecords) / PageSize))
                        iValueRet = iValueRet + 1;

                    GetGridData();
                });
            }
        }

        public ICommand SortingCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    ColumnIndex = Convert.ToInt32(obj);
                    GetSortOrder(ColumnIndex);
                    GetGridData();
                    SetSortOrder(ColumnIndex);
                    iValueRet = 1;
                });
            }
        }

        #endregion


    }
}
