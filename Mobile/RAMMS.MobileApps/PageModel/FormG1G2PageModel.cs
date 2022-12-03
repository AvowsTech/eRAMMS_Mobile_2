using Acr.UserDialogs;
using FreshMvvm;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Model.RequestModel;
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
    class FormG1G2PageModel : FreshBasePageModel
    {
        #region Private fields

        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private AssetDDLResponseDTO.DropDown _selectedRMU;
        private AssetDDLResponseDTO.DropDown _selectedRoadCode;
        private AssetDDLResponseDTO.DropDown _selectedSectionCode;
        private DDListItems _selectedAssetType;
        private DDListItems _selectedBound;
        private DDListItems _selectedFromYear;
        private DDListItems _selectedToYear;
        private int iValueRet = 1;
        private int _selectedPageSize = 0;

        private bool isModify;
        private bool isDelete;
        private bool isView;
        #endregion

        #region Public Properties
        public string[] ColumnName = { null, "RefID", "Year", "InsDate", "AssetRefId", "RMUCode", "RMUDesc", "SecCode", "SecName", "RoadCode", "RoadName", "LocationCH", "InspectedBy", "AuditedBy", "RoadId" };
        public List<Column> ColumnList { get; set; }
        public bool IsAdd { get; set; }
        public bool DetailSearchVisible { get; set; } = false;
        public string SelectedRMUName { get; set; }
        public string SelectedRoadName { get; set; }
        public string SelectedSectionName { get; set; }
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
        public bool IsYearOfInspectAssending { get; set; } = false;
        public bool IsDateOfInspectAssending { get; set; } = false;
        public bool IsAssetIdByAssending { get; set; } = false;
        public bool IsRmuAssending { get; set; } = false;
        public bool IsRmuNameAssending { get; set; } = false;
        public bool IsSectionCodeAssending { get; set; } = false;
        public bool IsSectionNameAssending { get; set; } = false;
        public bool IsRoadCodeAssending { get; set; } = false;
        public bool IsRoadNameAssending { get; set; } = false;
        public bool IsLocationChAssending { get; set; } = false;
        public bool IsInspectedByAssending { get; set; } = false;
        public bool IsAuditedByAssending { get; set; } = false;
        public bool IsStatusAssending { get; set; } = false;
        public int ColumnIndex { get; set; }
        public SearchRequestDTO SearchCriteriaItems { get; set; }
        //public FilteredPagingDefinition<FormG1G2SearchGridDTO> SearchCriteriaItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }
        public ObservableCollection<FormG1G2SearchGridDTO> MyBaseFormF2ListViewItems { get; set; }
        public ObservableCollection<DDListItems> DDAssetTypeListItems { get; set; }
        public ObservableCollection<DDListItems> DDFromYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDToYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDBoundListItems { get; set; }

        public DDListItems SelectedAssetType
        {
            get => _selectedAssetType;
            set
            {
                _selectedAssetType = value;
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
                    SelectedRMUName = _selectedRMU.Text.Split('-')[1].ToString();
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
        public FormG1G2PageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
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

            DDRodeCodeListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            DDSectionListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            MyBaseFormF2ListViewItems = new ObservableCollection<FormG1G2SearchGridDTO>();

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
            GetDDListDetails("Year");
            GetDDListDetails("bound");
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
                    //string rmuSearchcode = string.Empty;
                    //if (SelectedRMU != null && SelectedRMU.Value == "MIRI")
                    //    rmuSearchcode = "MRI";
                    //if (SelectedRMU != null && SelectedRMU.Value == "Batu Niah")
                    //    rmuSearchcode = "BTN";

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
                            { "RMUCode", SelectedRMU?.Value },
                            { "SecName", SelectedSectionCode?.Value },
                            { "RoadCode", SelectedRoadCode?.Value },
                            { "FromYear", SelectedFromYear?.Value },
                            { "ToYear",SelectedToYear?.Value },
                            { "chFromKM", string.IsNullOrEmpty(FromChKM) ? null : FromChKM },
                            { "chFromM", string.IsNullOrEmpty(FromChM) ? null : FromChM },
                            { "chToKm", string.IsNullOrEmpty(ToChKM) ? null : ToChKM },
                            { "chToM", string.IsNullOrEmpty(ToChM) ? null : ToChM },
                        }
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(SearchCriteriaItems);
                    var response = await _restApi.GetFormG1G2LandingGridData(SearchCriteriaItems);
                    var response1 = JsonConvert.DeserializeObject<List<FormG1G2SearchGridDTO>>(response.data.data.ToString());

                    if (response.success)
                    {
                        TotalRecords = response.data.recordsTotal.ToString();
                        PageStart = response.data.recordsTotal == 0 ? "0" : (values + 1).ToString();
                        PageEnd = (values + PageSize) > response.data.recordsTotal ? TotalRecords : (values + PageSize).ToString();

                        var listItems = new ObservableCollection<FormG1G2SearchGridDTO>(response1);

                        var sNo = 1;
                        foreach (var ivalue in listItems)
                        {
                            //ivalue.Status = ivalue.SubmitSts == true ? "Submitted" : "Saved";
                            ivalue.SNo = sNo;
                            sNo++;
                        }

                        MyBaseFormF2ListViewItems = listItems;
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
                        TypeCode = ddtype == "Asset type" ? "GR" : ddtype == "bound" ? "GR" : null
                    };

                    ResponseBaseListObject<DDListItems> response;
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    if (ddtype == "Asset type")
                        response = await _restApi.GetDDLDescValue(ddlist);
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
                SortOrder = IsYearOfInspectAssending ? "1" : "0";
            if (columnIndex == 3)
                SortOrder = IsDateOfInspectAssending ? "1" : "0";
            if (columnIndex == 4)
                SortOrder = IsAssetIdByAssending ? "1" : "0";
            if (columnIndex == 5)
                SortOrder = IsRmuAssending ? "1" : "0";
            if (columnIndex == 6)
                SortOrder = IsRmuNameAssending ? "1" : "0";
            if (columnIndex == 7)
                SortOrder = IsSectionCodeAssending ? "1" : "0";
            if (columnIndex == 8)
                SortOrder = IsSectionNameAssending ? "1" : "0";
            if (columnIndex == 9)
                SortOrder = IsRoadCodeAssending ? "1" : "0";
            if (columnIndex == 10)
                SortOrder = IsRoadNameAssending ? "1" : "0";
            if (columnIndex == 11)
                SortOrder = IsLocationChAssending ? "1" : "0";
            if (columnIndex == 12)
                SortOrder = IsInspectedByAssending ? "1" : "0";
            if (columnIndex == 13)
                SortOrder = IsAuditedByAssending ? "1" : "0";
            if (columnIndex == 14)
                SortOrder = IsStatusAssending ? "1" : "0";
        }
        private void SetSortOrder(int columnIndex)
        {
            if (columnIndex == 1)
            {
                IsRefNoAssending = !IsRefNoAssending;
                IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 2)
            {
                IsYearOfInspectAssending = !IsYearOfInspectAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 3)
            {
                IsDateOfInspectAssending = !IsDateOfInspectAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 4)
            {
                IsAssetIdByAssending = !IsAssetIdByAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 5)
            {
                IsRmuAssending = !IsRmuAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 6)
            {
                IsRmuNameAssending = !IsRmuNameAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 7)
            {
                IsSectionCodeAssending = !IsSectionCodeAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 8)
            {
                IsSectionNameAssending = !IsSectionNameAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 9)
            {
                IsRoadCodeAssending = !IsRoadCodeAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 10)
            {
                IsRoadNameAssending = !IsRoadNameAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 11)
            {
                IsLocationChAssending = !IsLocationChAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 12)
            {
                IsInspectedByAssending = !IsInspectedByAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsAuditedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 13)
            {
                IsAuditedByAssending = !IsAuditedByAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsStatusAssending = false;
            }
            else if (columnIndex == 14)
            {
                IsStatusAssending = !IsStatusAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = false;
            }
            else
            {
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsAssetIdByAssending = IsRmuAssending = IsRmuNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsLocationChAssending = IsInspectedByAssending = IsAuditedByAssending = IsStatusAssending = false;
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
                    var response = await _restApi.DeleteG1G2Header(pkRefNo);
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
                    var SelectedRowItem = (FormG1G2SearchGridDTO)obj;

                    App.HeaderCode = SelectedRowItem.RefNo;
                    var actionResult = "";

                    string view = isView ? "View" : "";
                    string delete = isDelete ? "Delete" : "";
                    string edit = isModify && SelectedRowItem.Status.ToLower() != "submitted" ? "Edit" : "";

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
                        await CoreMethods.PushPageModel<FormG1G2AddPageModel>();
                    }
                    else if (actionResult == "View")
                    {
                        App.ReturnType = "View";
                        await CoreMethods.PushPageModel<FormG1G2AddPageModel>();
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
                    await CoreMethods.PushPageModel<FormG1G2AddPageModel>();
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
