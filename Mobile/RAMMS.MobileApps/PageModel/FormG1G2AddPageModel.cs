using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Model.RequestModel;
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
        public class FormG1G2AddPageModel : FreshBasePageModel
        {
            
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
            private int? _selectedInspBarrier = -1;
            private int? _selectInspGBeam = -1;
            private int? _selectedInspVms = -1;
       



            private int _selectedVerIndex = -1;
            private int _selectedInspIndex = -1;
            private int? _selectedInspGColumn = -1;
            private int? _selectedInspFootings = -1;
            private int? _selectedInspGPads = -1;
            private int? _selectedInspMaintenance = -1;
            private int? _selectedInspStaticSigns = -1;
            private int? _selectedGantrySignConRat = -1;
       

            private bool isModify;
            private bool isDelete;
            private bool isView;
            SignaturePadView InspPadView;
            SignaturePadView VerPadView;

            Grid MainGrid;
            public ObservableCollection<FormG1G2ImgRequestDTO> DetailImageList { get; set; }
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
            public string InspectedSignature { get; set; }
            public ImageSource InspSign { get; set; }
            public ImageSource VerSign { get; set; }
            public string AuditedSignature { get; set; }
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
            private int _selectedWaterwayDistress = -1;
            private int _selectedEmbankmentDistress = -1;
            private int _selectedHeadwallInletDistress = -1;
            private int _selectedWingwallInletDistress = -1;
            private int _selectedApronInletDistress = -1;
            private int _selectedRiprapInletDistress = -1;
            private int _selectedHeadwallOutletDistress = -1;
            private int _selectedWingwallOutletDistress = -1;
            private int _selectedApronOutletDistress = -1;
            private int _selectedRiprapOutletDistress = -1;
            private int _selectedCulvertApproachDistress = -1;

            private int _selectedBarrel1DistressDistress = -1;
            private int _selectedBarrel2DistressDistress = -1;
            private int _selectedBarrel3DistressDistress = -1;
            private int _selectedBarrel4DistressDistress = -1;
            private int _selectedBarrel5DistressDistress = -1;
            private int _selectedBarrel6DistressDistress = -1;
            private int _selectedBarrel7DistressDistress = -1;
            private int _selectedBarrel8DistressDistress = -1;
            private int _selectedBarrel9DistressDistress = -1;
            private int _selectedBarrel10DistressDistress = -1;






            public int SelectedCulvertMarkerDistress
            {
                get { return _selectedCulvertMarkerDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedCulvertMarkerDistress = value;

                        var userprp = DDCulvertMarkerDistressListItems[_selectedCulvertMarkerDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsCulvertMarkerOthersVisible = true;
                        }
                        else
                        {
                            IsCulvertMarkerOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedWaterwayDistress
            {
                get { return _selectedWaterwayDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedWaterwayDistress = value;

                        var userprp = DDWaterwayDistressListItems[_selectedWaterwayDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsWaterwayOthersVisible = true;
                        }
                        else
                        {
                            IsWaterwayOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedEmbankmentDistress
            {
                get { return _selectedEmbankmentDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedEmbankmentDistress = value;

                        var userprp = DDEmbankmentDistressListItems[_selectedEmbankmentDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsEmbankmentOthersVisible = true;
                        }
                        else
                        {
                            IsEmbankmentOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedHeadwallInletDistress
            {
                get { return _selectedHeadwallInletDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedHeadwallInletDistress = value;

                        var userprp = DDHeadwallInletDistressListItems[_selectedHeadwallInletDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsHeadwallInletOthersVisible = true;
                        }
                        else
                        {
                            IsHeadwallInletOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedWingwallInletDistress
            {
                get { return _selectedWingwallInletDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedWingwallInletDistress = value;

                        var userprp = DDWingwallInletDistressListItems[_selectedWingwallInletDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsWingwallInletOthersVisible = true;
                        }
                        else
                        {
                            IsWingwallInletOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedApronInletDistress
            {
                get { return _selectedApronInletDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedApronInletDistress = value;

                        var userprp = DDApronInletDistressListItems[_selectedApronInletDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsApronInletOthersVisible = true;
                        }
                        else
                        {
                            IsApronInletOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedRiprapInletDistress
            {
                get { return _selectedRiprapInletDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedRiprapInletDistress = value;

                        var userprp = DDRiprapInletDistressListItems[_selectedRiprapInletDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsRiprapInletOthersVisible = true;
                        }
                        else
                        {
                            IsRiprapInletOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedHeadwallOutletDistress
            {
                get { return _selectedHeadwallOutletDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedHeadwallOutletDistress = value;

                        var userprp = DDHeadwallOutletDistressListItems[_selectedHeadwallOutletDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsHeadwallOutletOthersVisible = true;
                        }
                        else
                        {
                            IsHeadwallOutletOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedWingwallOutletDistress
            {
                get { return _selectedWingwallOutletDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedWingwallOutletDistress = value;

                        var userprp = DDWingwallOutletDistressListItems[_selectedWingwallOutletDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsWingwallOutletOthersVisible = true;
                        }
                        else
                        {
                            IsWingwallOutletOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedApronOutletDistress
            {
                get { return _selectedApronOutletDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedApronOutletDistress = value;

                        var userprp = DDApronOutletDistressListItems[_selectedApronOutletDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsApronOutletOthersVisible = true;
                        }
                        else
                        {
                            IsApronOutletOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedRiprapOutletDistress
            {
                get { return _selectedRiprapOutletDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedRiprapOutletDistress = value;

                        var userprp = DDRiprapOutletDistressListItems[_selectedRiprapOutletDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsRiprapOutletOthersVisible = true;
                        }
                        else
                        {
                            IsRiprapOutletOthersVisible = false;
                        }

                    }
                }
            }
            public int SelectedCulvertApproachDistress
            {
                get { return _selectedCulvertApproachDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedCulvertApproachDistress = value;

                        var userprp = DDCulvertApproachDistressListItems[_selectedCulvertApproachDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsCulvertApproachOthersVisible = true;
                        }
                        else
                        {
                            IsCulvertApproachOthersVisible = false;
                        }

                    }
                }
            }




            public int SelectedBarrel1DistressDistress
            {
                get { return _selectedBarrel1DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel1DistressDistress = value;

                        var userprp = DDBarrel1DistressListItems[_selectedBarrel1DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel1DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel1DistressOthersVisible = false;
                        }

                    }
                }
            }





            public int SelectedBarrel2DistressDistress
            {
                get { return _selectedBarrel2DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel2DistressDistress = value;

                        var userprp = DDBarrel2DistressListItems[_selectedBarrel2DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel2DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel2DistressOthersVisible = false;
                        }

                    }
                }
            }




            public int SelectedBarrel3DistressDistress
            {
                get { return _selectedBarrel3DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel3DistressDistress = value;

                        var userprp = DDBarrel3DistressListItems[_selectedBarrel3DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel3DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel3DistressOthersVisible = false;
                        }

                    }
                }
            }



            public int SelectedBarrel4DistressDistress
            {
                get { return _selectedBarrel4DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel4DistressDistress = value;

                        var userprp = DDBarrel4DistressListItems[_selectedBarrel4DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel4DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel4DistressOthersVisible = false;
                        }

                    }
                }
            }






            public int SelectedBarrel5DistressDistress
            {
                get { return _selectedBarrel5DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel5DistressDistress = value;

                        var userprp = DDBarrel5DistressListItems[_selectedBarrel5DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel5DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel5DistressOthersVisible = false;
                        }

                    }
                }
            }




            public int SelectedBarrel6DistressDistress
            {
                get { return _selectedBarrel6DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel6DistressDistress = value;

                        var userprp = DDBarrel6DistressListItems[_selectedBarrel6DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel6DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel6DistressOthersVisible = false;
                        }

                    }
                }
            }


            public int SelectedBarrel7DistressDistress
            {
                get { return _selectedBarrel7DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel7DistressDistress = value;

                        var userprp = DDBarrel7DistressListItems[_selectedBarrel7DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel7DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel7DistressOthersVisible = false;
                        }

                    }
                }
            }


            public int SelectedBarrel8DistressDistress
            {
                get { return _selectedBarrel8DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel8DistressDistress = value;

                        var userprp = DDBarrel8DistressListItems[_selectedBarrel8DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel8DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel8DistressOthersVisible = false;
                        }

                    }
                }
            }





            public int SelectedBarrel9DistressDistress
            {
                get { return _selectedBarrel9DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel9DistressDistress = value;

                        var userprp = DDBarrel9DistressListItems[_selectedBarrel9DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel9DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel9DistressOthersVisible = false;
                        }

                    }
                }
            }



            public int SelectedBarrel10DistressDistress
            {
                get { return _selectedBarrel10DistressDistress; }
                set
                {
                    if (value != -1)
                    {
                        _selectedBarrel10DistressDistress = value;

                        var userprp = DDBarrel10DistressListItems[_selectedBarrel10DistressDistress].Text.Split('-')[1];
                        if (userprp.ToLower() == "others")
                        {
                            IsBarrel10DistressOthersVisible = true;
                        }
                        else
                        {
                            IsBarrel10DistressOthersVisible = false;
                        }

                    }
                }
            }


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
                    SelectedRefNo = "CI/Form G1/G2/" + DDAssetListItems[SelectedAsset].Text + "/" + DDYearListItems[SelectedYear].Value;
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
        public int? SelectedInspBarrier
        {
            get => _selectedInspBarrier;
            set
            {
                _selectedInspBarrier = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedGantrySignConRat
        {
            get => _selectedGantrySignConRat;
            set
            {
                _selectedGantrySignConRat = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedInspVms
        {
            get => _selectedInspVms;
            set
            {
                _selectedInspVms = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedInspStaticSigns
        {
            get => _selectedInspStaticSigns;
            set
            {
                _selectedInspStaticSigns = value;
                RaisePropertyChanged();
            }
        }

        public int? SelectedInspMaintenance
        {
            get => _selectedInspMaintenance;
            set
            {
                _selectedInspMaintenance = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedInspGPads
        {
            get => _selectedInspGPads;
            set
            {
                _selectedInspGPads = value;
                RaisePropertyChanged();
            }
        }

        public int? SelectedInspFootings
        {
            get => _selectedInspFootings;
            set
            {
                _selectedInspFootings = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectInspGBeam
        {
            get => _selectInspGBeam;
            set
            {
                _selectInspGBeam = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedInspGColumn
        {
            get => _selectedInspGColumn;
            set
            {
                _selectedInspGColumn = value;
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
        //public int? AiLocChM { get; set; }
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
        //public bool IsView { get; set; } = false;
        //public string RmuCode { get; set; }
        //public string SectionCode { get; set; }
        //public string SectionName { get; set; }
        //public string Status { get; set; }
        //public string DisplayAssetId { get; set; }
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
        public int? RecordNo { get; set; }
        public bool? PrkPosition { get; set; }
        public bool? Accessibility { get; set; }
        public bool? PotentialHazards { get; set; }
        public string InspBarrier { get; set; }
        public string InspGBeam { get; set; }
        public string InspGColumn { get; set; }
        public string InspFootings { get; set; }
        public string InspGPads { get; set; }
        public string InspMaintenance { get; set; }
        public string InspStaticSigns { get; set; }
        public string InspVms { get; set; }
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
        public FormG2DTO FormG2 { get; set; }
        public bool IsView { get; set; } = false;


        #endregion

        public FormG1G2AddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
            {
                _userDialogs = userDialogs;
                _restApi = restApi;
                _localDatabase = localDatabase;
            }
            public override void Init(object initData)
            {
                base.Init(initData);

                DDCulvertUnitsDistressListIems = new ObservableCollection<DDListItems>();

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
                await GetAssetList();
                await GetUserList();

            if (DDInspUserListListItems != null) { 
  
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
                    await GetG1G2ById(App.HeaderCode);
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
                            var hdrresponse = await _restApi.GetG1G2Images(App.HeaderCode);

                            if (hdrresponse.success == true)
                            {
                                var list = hdrresponse.data;
                                DetailImageList = new ObservableCollection<FormG1G2ImgRequestDTO>(hdrresponse.data);

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
                                    var imageID = (obj as FormG1G2ImgRequestDTO).PkRefNo;
                                    var response = await _restApi.DeleteG1G2Image(App.HeaderCode, imageID);

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
                        await CoreMethods.PushPageModel<FormG1G2CameraPopupPageModel>();
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

                            if (SelectedDivision == null)
                            {
                                await UserDialogs.Instance.AlertAsync("Please select Division", "RAMS", "OK");
                                return;
                            }
                            if (SelectedRMU == null)
                            {
                                await UserDialogs.Instance.AlertAsync("Please select RMU", "RAMS", "OK");
                                return;
                            }
                            if (SelectedSectionCode == null)
                            {
                                await UserDialogs.Instance.AlertAsync("Please select Section Code", "RAMS", "OK");
                                return;
                            }
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

                            var response = SaveFormG1G2Header();

                            FromAdd = false;
                        }
                        catch
                        {
                        }


                    });
                }
            }


        //private async void ClearDropdownData()
        //{
        //    SelectedParkingPosition = -1;
        //    SelectedAccesibility = -1;
        //    SelectedPotentialHazard = -1;
        //    SelectedInspBarrier = -1;
        //    SelectInspGBeam = -1;
        //    SelectedInspGColumn = -1;
        //    SelectedInspGPads = -1;
        //    SelectedInspMaintenance = -1;
        //    SelectedInspStaticSigns = -1;
        //    SelectedInspVms = -1;
        //    SelectedGantrySignConRat = -1;
        //    SelectedFutureInvestigation = -1;
            
        //}


        private async Task<ObservableCollection<FormG1DTO>> SaveFormG1G2Header()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    FormG1DTO GridItems = new FormG1DTO()
                    {
                        PkRefNo = App.HeaderCode,
                        AiPkRefNo = Convert.ToInt32(DDAssetListItems[SelectedAsset]?.Value),
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

                    var response = await _restApi.SaveG1G2Hdr(GridItems);

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

        private async Task<int> GetG1G2ById(int headerCode)
            {
                try
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        var response = await _restApi.GetG1G2ById(headerCode);

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

        private void SetViewData(ResponseBaseObject<FormG1DTO> response)
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
            YearOfInsp = response.data.YearOfInsp;
            DtOfInsp = response.data.DtOfInsp;
            RecordNo = response.data.RecordNo;
            

           
            
            SelectedInspBarrier = response.data.InspBarrier != null ? Convert.ToInt32(response.data.InspBarrier) : (int?)null; //Convert.ToInt32(InspBarrier);
            
            SelectInspGBeam = response.data.InspGBeam != null ? Convert.ToInt32(response.data.InspGBeam) : (int?)null; //Convert.ToInt32(InspGBeam);
            
            SelectedInspGColumn = response.data.InspGColumn != null ? Convert.ToInt32(response.data.InspGColumn) : (int?)null;//Convert.ToInt32(InspGColumn);
           
            SelectedInspFootings = response.data.InspFootings != null ? Convert.ToInt32(response.data.InspFootings) : (int?)null;//Convert.ToInt32(InspFootings);
            
            SelectedInspGPads = response.data.InspGPads != null ? Convert.ToInt32(response.data.InspGPads) : (int?)null;//Convert.ToInt32(InspGPads);
            
            SelectedInspMaintenance = response.data.InspMaintenance != null ? Convert.ToInt32(response.data.InspMaintenance) : (int?)null; //Convert.ToInt32(InspMaintenance);
           
            SelectedInspStaticSigns = response.data.InspStaticSigns != null ? Convert.ToInt32(response.data.InspStaticSigns) : (int?)null;
            
            SelectedInspVms = response.data.InspStaticSigns != null ? Convert.ToInt32(response.data.InspStaticSigns) : (int?)null;

            InspectedBy = response.data.InspectedBy;
            InspectedName = response.data.InspectedName;
            InspectedDesig = response.data.InspStaticSigns;
            InspectedDt = response.data.InspectedDt;
            InspectedSign = true;//response.data.InspectedSign;
            //CondRating = response.data.CondRating;
            SelectedGantrySignConRat = response.data.CondRating != null ? Convert.ToInt32(response.data.CondRating) : (int?)null;//CondRating;

            //IssuesFound = response.data.IssuesFound;
            SelectedFutureInvestigation = response.data.IssuesFound != null ? Convert.ToInt32(response.data.IssuesFound) : (int?)null;//Convert.ToInt32(IssuesFound);

            AuditedBy = response.data.AuditedBy;
            AuditedName = response.data.AuditedName;
            AuditedDesig = response.data.AuditedDesig;
            AuditedDt = response.data.AuditedDt;

            AuditedSign = true; //response.data.AuditedSign;
            PartBServiceProvider = response.data.FormG2.DistressSp;
            PartBConsultant = response.data.FormG2.DistressEc;
            PartCServiceProvider = response.data.FormG2.GeneralSp;
            PartCConsultant = response.data.FormG2.GeneralEc;
            PartDServiceProvider = response.data.FormG2.FeedbackSp;
            PartDConsultant = response.data.FormG2.FeedbackEc;

            ModBy = response.data.ModBy;
            ModDt = response.data.ModDt;
            CrBy = response.data.CrBy;
            CrDt = response.data.CrDt;
            SubmitSts = response.data.SubmitSts;
            ActiveYn = response.data.ActiveYn;
            Status = response.data.Status;
            AuditLog = response.data.AuditLog;

            SelectedParkingPosition = response.data.PrkPosition != null ? (response.data.PrkPosition == true ? 0 : 1) : (int?)null;
            SelectedAccesibility = response.data.Accessibility != null ? (response.data.Accessibility == true ? 0 : 1) : (int?)null;
            SelectedPotentialHazard = response.data.PotentialHazards != null ? (response.data.PotentialHazards == true ? 0 : 1) : (int?)null;
            
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
                        var response = await _restApi.GetGGAssetList(ddlist);

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
                    //var ddlist = new AssetDDLRequestDTO()
                    //{
                    //    RMU = SelectedRMU?.Value,
                    //    SectionCode = Convert.ToInt32(SelectedSectionCode?.Code),
                    //    RdCode = SelectedRoadCode?.Value
                    //};


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
                            var HeaderItemResponse = await _restApi.GetG1G2ById(App.HeaderCode);

                            if (HeaderItemResponse.success)
                            {
                                HeaderItemResponse.data.PkRefNo = App.HeaderCode;
                                HeaderItemResponse.data.DtOfInsp = SelectedDate;
          
                                HeaderItemResponse.data.AuditedBy = SelectedVerIndex != -1 ? Convert.ToInt32(DDVerUserListListItems[SelectedVerIndex]?.Value) : (int?)null;
                                HeaderItemResponse.data.AuditedName = VerName;
                                HeaderItemResponse.data.AuditedDesig = VerDesignation;
                                HeaderItemResponse.data.AuditedDt = VerDate;
                                HeaderItemResponse.data.AuditedSign = true;    //inspSign ?? HeaderItemResponse.data.AuditedSign ?? null;
                                HeaderItemResponse.data.AuditedSignature = verSign ?? HeaderItemResponse.data.AuditedSignature ?? null;

                            HeaderItemResponse.data.InspectedBy = SelectedInspIndex != -1 ? Convert.ToInt32(DDInspUserListListItems[SelectedInspIndex].Value) : (int?)null;
                                HeaderItemResponse.data.InspectedName = InspName;
                                HeaderItemResponse.data.InspectedDesig = InspDesignation;
                                HeaderItemResponse.data.InspectedDt = InspDate;
                            HeaderItemResponse.data.InspectedSign = true;  //verSign ?? HeaderItemResponse.data.InspectedSign ?? null;
                            HeaderItemResponse.data.InspectedSignature = inspSign ?? HeaderItemResponse.data.InspectedSignature ?? null;

                            HeaderItemResponse.data.SubmitSts = type == "save" ? false : true;
                                if (SelectedGantrySignConRat != null && SelectedGantrySignConRat != -1)
                                    HeaderItemResponse.data.CondRating = SelectedGantrySignConRat + 1;
                                
                                if (SelectedFutureInvestigation != null && SelectedFutureInvestigation != -1)
                                    HeaderItemResponse.data.IssuesFound = SelectedFutureInvestigation == 0 ? true : false;
                                
                                if (SelectedParkingPosition != null && SelectedParkingPosition != -1)
                                    HeaderItemResponse.data.PrkPosition = SelectedParkingPosition == 0 ? true : false;

                                if (SelectedAccesibility != null && SelectedAccesibility != -1)
                                    HeaderItemResponse.data.Accessibility = SelectedAccesibility == 0 ? true : false;

                                if (SelectedPotentialHazard != null && SelectedPotentialHazard != -1)
                                    HeaderItemResponse.data.PotentialHazards = SelectedPotentialHazard == 0 ? true : false;


                            if (SelectInspGBeam != null && SelectInspGBeam != -1)
                                HeaderItemResponse.data.InspGBeam = Convert.ToString(SelectInspGBeam);  //!= -1 ? Convert.ToInt32(DDInspUserListListItems[SelectInspGBeam]?.Value) : "";


                            if (SelectedInspMaintenance != null && SelectedInspMaintenance != -1)
                                HeaderItemResponse.data.InspMaintenance = Convert.ToString(SelectedInspMaintenance); // == 0 ? true : false;


                            if (SelectedInspBarrier != null && SelectedInspBarrier != -1)
                                HeaderItemResponse.data.InspBarrier = Convert.ToString(SelectedInspBarrier); // == 0 ? true : false;


                            if (SelectedInspGColumn != null && SelectedInspGColumn != -1)
                                HeaderItemResponse.data.InspGColumn = Convert.ToString(SelectedInspGColumn); //== 0 ? true : false;

                            if (SelectedInspFootings != null && SelectedInspFootings != -1)
                                HeaderItemResponse.data.InspFootings = Convert.ToString(SelectedInspFootings); // == 0 ? true : false;

                            if (SelectedInspGPads != null && SelectedInspGPads != -1)
                                HeaderItemResponse.data.InspGPads = Convert.ToString(SelectedInspGPads); // == 0 ? true : false;

                            if (SelectedInspStaticSigns != null && SelectedInspStaticSigns != -1)
                                HeaderItemResponse.data.InspStaticSigns = Convert.ToString(SelectedInspStaticSigns); // == 0 ? true : false;

                            if (SelectedInspVms != null && SelectedInspVms != -1)
                                HeaderItemResponse.data.InspVms = Convert.ToString(SelectedInspVms); // == 0 ? true : false;

                            if (SelectedInspFootings != null && SelectedInspFootings != -1)
                                HeaderItemResponse.data.InspFootings = Convert.ToString(SelectedInspFootings); // == 0 ? true : false;

                            HeaderItemResponse.data.FormG2.Fg1hPkRefNo = App.HeaderCode;
                            HeaderItemResponse.data.FormG2.DistressSp = PartBServiceProvider;
                            HeaderItemResponse.data.FormG2.DistressEc = PartBConsultant;
                            HeaderItemResponse.data.FormG2.GeneralSp = PartCServiceProvider;
                            HeaderItemResponse.data.FormG2.GeneralEc = PartCConsultant;
                            HeaderItemResponse.data.FormG2.FeedbackSp = PartDServiceProvider;
                            HeaderItemResponse.data.FormG2.FeedbackEc = PartDConsultant;

                            if (type == "submit") { HeaderItemResponse.data.SubmitSts = true; HeaderItemResponse.data.Status = "Submitted"; }
                            var response = await _restApi.UpdateG1G2(HeaderItemResponse.data);
                               
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
                        _userDialogs.Alert(ex.Message + "details:"+ ex.StackTrace);
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

                        if (SelectedGantrySignConRat == -1 || SelectedGantrySignConRat == null)
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

                        if (SelectedParkingPosition == -1 || SelectedParkingPosition == null)
                        {
                            _userDialogs.Alert("Parking Position field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedAccesibility == -1 || SelectedAccesibility == null)
                        {
                            _userDialogs.Alert("Accesibility field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedPotentialHazard == -1 || SelectedPotentialHazard == null)
                        {
                            _userDialogs.Alert("PotentialHazard field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedInspBarrier == -1 || SelectedInspBarrier == null)
                        {
                            _userDialogs.Alert("Barrier field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectInspGBeam == -1 || SelectInspGBeam == null)
                        {
                            _userDialogs.Alert("GantryBeams field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedInspGColumn == -1 || SelectedInspGColumn == null)
                        {
                            _userDialogs.Alert("GantryColumns field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedInspFootings == -1 || SelectedInspFootings == null)
                        {
                            _userDialogs.Alert("Footings field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedInspGPads == -1 || SelectedInspGPads == null)
                        {
                            _userDialogs.Alert("Anchor Bolts and Mortar Grout Pads field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedInspMaintenance == -1 || SelectedInspMaintenance == null)
                        {
                            _userDialogs.Alert("Maintenance Access field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }
                        if (SelectedInspStaticSigns == -1 || SelectedInspStaticSigns == null)
                        {
                            _userDialogs.Alert("Static Signs field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }

                        if (SelectedInspVms == -1 || SelectedInspVms == null)
                        {
                            _userDialogs.Alert("Variable Message Signs field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                            return;
                        }

                        //if (SelectedGantrySignConRat == -1 || SelectedGantrySignConRat == null)
                        //{
                        //    _userDialogs.Alert("Gantry Sign Con Rating field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        //    return;
                        //}

                        if (SelectedDate == null)
                        {
                            _userDialogs.Alert("Date of Inspection is required.", "RAMS", "Ok");
                            return;
                        }


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

                        if (SelectedGantrySignConRat == -1 || SelectedGantrySignConRat == null)
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
        //    //public int SelectedCulvertUnitDistress
        //    //{
        //    //    get { return _selectedCulvertUnitDistress; }
        //    //    set
        //    //    {
        //    //        if (value != -1)
        //    //        {
        //    //            _selectedCulvertUnitDistress = value;

        //    //            var userprp = CulvertUnitDistressList[_selectedCulvertUnitDistress].Text.Split('-')[1];
        //    //            if (userprp.ToLower() == "others")
        //    //            {
        //    //                IsCulvertUnitOthersVisible = true;
        //    //            }
        //    //            else
        //    //            {
        //    //                IsCulvertUnitOthersVisible = false;
        //    //            }

        //    //        }
        //    //    }
        //    //}

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


