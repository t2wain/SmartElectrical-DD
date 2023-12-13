using Microsoft.Extensions.DependencyInjection;
using SELDictionary;
using SELDictionary.Model;
using System.IO;

namespace SELApp
{
    public class DictTest
    {
        private readonly ISPELDRepository? _repo;
        private readonly IPLANTDRepository? _repo2;
        private readonly ISITEDRepository? _repo3;
        private readonly ItemSQL? _itemSQL;
        private readonly ISELDictContextReadOnly _dbCtx;
        private Dictionary<int, string> _itemIds;
        private Dictionary<int, string> _plantItemIds;
        private Dictionary<int, string> _siteItemIds;
        private List<int> _enumIds;
        private IServiceProvider _provider;

        public DictTest(IServiceProvider provider)
        {
            _provider = provider;
            _repo = provider.GetService<ISPELDRepository>();
            _repo2 = provider.GetService<IPLANTDRepository>();
            _repo3 = provider.GetService<ISITEDRepository>();
            _itemSQL = provider.GetService<ItemSQL>();
            _dbCtx = provider.GetService<SELDictContext>();

            _itemIds = new Dictionary<int, string>
            {
                { 299, "Ammeter" },
                { 348, "AuxiliaryContact" },
                { 141, "BatteryBank" },
                { 134, "BatteryCharger" },
                { 160, "Bus" },
                { 291, "Busway" },
                { 326, "CBDDrawing" },
                { 280, "Cabinet" },
                { 161, "Cable" },
                { 339, "CableAssembly" },
                { 282, "CableDrum" },
                { 314, "CableRouting" },
                { 315, "CableRoutingSegPartition" },
                { 287, "CableSegmentPartition" },
                { 278, "CableSet" },
                { 335, "CableSpliceDrum" },
                { 284, "CableWay" },
                { 285, "CableWaySegment" },
                { 140, "Capacitor" },
                { 165, "Cell" },
                { 293, "ChangesLog" },
                { 147, "Circuit" },
                { 163, "CircuitBreaker" },
                { 166, "Conductor" },
                { 334, "ConductorTerminal" },
                { 357, "ConnectionDrawingConfig" },
                { 330, "Connector" },
                { 331, "ConnectorVertex" },
                { 131, "Contactor" },
                { 151, "ControlStation" },
                { 145, "ConvertingElectEquip" },
                { 175, "ConvertingEquipComponent" },
                { 332, "CurrentLimitingReactor" },
                { 295, "CurrentTransformer" },
                { 156, "Dimension" },
                { 144, "DisconnectElectEquip" },
                { 154, "DisconnectSwitch" },
                { 246, "Document" },
                { 320, "DocumentClaim" },
                { 99, "Drawing" },
                { 264, "DrawingComponent" },
                { 168, "ElectricalConductor" },
                { 167, "ElectricalEquipment" },
                { 312, "EquipDisconnectElectEquip" },
                { 15, "Equipment" },
                { 355, "EquipmentOperatingCase" },
                { 250, "ExternalDocument" },
                { 162, "Fuse" },
                { 152, "Generator" },
                { 351, "Group" },
                { 139, "HarmonicFilter" },
                { 171, "HeatTrace" },
                { 132, "Heater" },
                { 37, "Instrument" },
                { 322, "ItemJoin" },
                { 269, "ItemRegistry" },
                { 149, "Junctionbox" },
                { 44, "Label" },
                { 336, "LabelPersist" },
                { 353, "LabelProperty" },
                { 125, "Load" },
                { 148, "LocalPanel" },
                { 297, "MeteringEquipment" },
                { 294, "MiscellaneousDrawing" },
                { 283, "MiscellaneousElement" },
                { 21, "ModelItem" },
                { 290, "ModelItemLookup" },
                { 159, "Motor" },
                { 300, "Multimeter" },
                { 258, "NamingConvention" },
                { 257, "NamingConventionComp" },
                { 276, "OffSitePower" },
                { 354, "OperatingCase" },
                { 359, "OptionDispSet" },
                { 358, "OptionDispSetFolder" },
                { 263, "OptionFormat" },
                { 95, "OptionSetting" },
                { 172, "OtherConvertingEquipment" },
                { 251, "OtherDisconnectEquip" },
                { 169, "OtherElectEquipment" },
                { 133, "OverloadRelay" },
                { 261, "PDBLayoutDrawing" },
                { 338, "PDBRow" },
                { 337, "PDBSection" },
                { 150, "Panel" },
                { 346, "ParallelEquipment" },
                { 347, "ParallelPlantItemJoin" },
                { 268, "PlantGroupParameters" },
                { 26, "PlantItem" },
                { 319, "PlantItemClaim" },
                { 124, "PlantItemDocJoin" },
                { 265, "PlantItemGroup" },
                { 127, "PlantItemJoin" },
                { 323, "PlantItemSymbol" },
                { 271, "PlantUser" },
                { 272, "PlantUserPreferences" },
                { 296, "PotentialTransformer" },
                { 158, "PowerDistributionBoard" },
                { 126, "PowerDistributionEquip" },
                { 321, "ProcessEquipment" },
                { 155, "ProtectionRelay" },
                { 305, "RefAmmeter" },
                { 349, "RefAuxiliaryContact" },
                { 201, "RefBatteryBank" },
                { 194, "RefBatteryCharger" },
                { 216, "RefBus" },
                { 292, "RefBusway" },
                { 345, "RefCBDDrawing" },
                { 281, "RefCabinet" },
                { 179, "RefCable" },
                { 340, "RefCableAssembly" },
                { 342, "RefCableDrumType" },
                { 279, "RefCableSet" },
                { 288, "RefCableWayComponent" },
                { 200, "RefCapacitor" },
                { 220, "RefCell" },
                { 223, "RefCircuit" },
                { 218, "RefCircuitBreaker" },
                { 178, "RefColorInstance" },
                { 177, "RefColorPattern" },
                { 180, "RefConductor" },
                { 307, "RefConduit" },
                { 227, "RefConfiguration" },
                { 229, "RefConnectionType" },
                { 228, "RefConnectionTypeWire" },
                { 191, "RefContactor" },
                { 208, "RefControlStation" },
                { 203, "RefConvertingElectEquip" },
                { 252, "RefConvertingEquipCompt" },
                { 333, "RefCurrentLimitingReactor" },
                { 301, "RefCurrentTransformer" },
                { 221, "RefDisconnectElectEquip" },
                { 211, "RefDisconnectSwitch" },
                { 344, "RefDocument" },
                { 181, "RefElectricalConductor" },
                { 222, "RefElectricalEquipment" },
                { 256, "RefEquipment" },
                { 217, "RefFuse" },
                { 209, "RefGenerator" },
                { 313, "RefGland" },
                { 199, "RefHarmonicFilter" },
                { 233, "RefHeatTrace" },
                { 192, "RefHeater" },
                { 274, "RefInstrument" },
                { 240, "RefItemTypeStorage" },
                { 206, "RefJunctionbox" },
                { 185, "RefLoad" },
                { 205, "RefLocalPanel" },
                { 235, "RefLookUpTable" },
                { 238, "RefLookUpTableAttributes" },
                { 239, "RefLookUpTableValue" },
                { 303, "RefMeteringEquipment" },
                { 289, "RefMiscellaneousElement" },
                { 184, "RefModelItem" },
                { 215, "RefMotor" },
                { 306, "RefMultimeter" },
                { 317, "RefNamingConvention" },
                { 318, "RefNamingConventionComp" },
                { 277, "RefOffSitePower" },
                { 224, "RefOtherConvertingEquip" },
                { 253, "RefOtherDisconnectEquip" },
                { 234, "RefOtherElectEquipment" },
                { 193, "RefOverloadRelay" },
                { 207, "RefPanel" },
                { 316, "RefPlantGroup" },
                { 183, "RefPlantItem" },
                { 232, "RefPlantItemGroup" },
                { 254, "RefPlantItemProfileJoin" },
                { 343, "RefPlantItemSymbol" },
                { 302, "RefPotentialTransformer" },
                { 214, "RefPowerDistributionBoard" },
                { 186, "RefPowerDistributionEquip" },
                { 270, "RefPreferences" },
                { 231, "RefProfile" },
                { 212, "RefProtectionRelay" },
                { 308, "RefRaceWayMiscellaneous" },
                { 310, "RefRelayFunction" },
                { 309, "RefRelayFunctionLib" },
                { 198, "RefResistor" },
                { 273, "RefSignalGroup" },
                { 267, "RefSignalRun" },
                { 213, "RefStarter" },
                { 219, "RefStrip" },
                { 230, "RefStripCableConnection" },
                { 226, "RefStripConfig" },
                { 225, "RefStripConfigTerm" },
                { 325, "RefSymbology" },
                { 352, "RefSynchPropertyList" },
                { 202, "RefTerminal" },
                { 195, "RefTransformer" },
                { 245, "RefTransformerComponent" },
                { 189, "RefTray" },
                { 236, "RefTypicalSchematic" },
                { 243, "RefTypicalSchematicBlock" },
                { 237, "RefTypicalSchematicItem" },
                { 197, "RefUPS" },
                { 196, "RefVariableFrequencyDrive" },
                { 304, "RefVoltmeter" },
                { 176, "RefWideParameters" },
                { 182, "RefWiringEquipment" },
                { 311, "RelayFunction" },
                { 328, "Representation" },
                { 138, "Resistor" },
                { 247, "Revision" },
                { 248, "SLDDrawing" },
                { 259, "SLDLabel" },
                { 128, "SchematicBlock" },
                { 249, "SchematicDrawing" },
                { 255, "SchematicMacro" },
                { 286, "SegmentPartition" },
                { 350, "Shape" },
                { 266, "SignalRun" },
                { 356, "SmartText" },
                { 157, "Starter" },
                { 39, "Status" },
                { 164, "Strip" },
                { 329, "Symbol" },
                { 275, "SystemReport" },
                { 122, "Task" },
                { 341, "TaskDependency" },
                { 123, "TaskItemProperty" },
                { 143, "Terminal" },
                { 135, "Transformer" },
                { 174, "TransformerComponent" },
                { 129, "Tray" },
                { 137, "UPS" },
                { 136, "VariableFrequencyDrive" },
                { 298, "Voltmeter" },
                { 327, "WiringDrawing" },
                { 142, "WiringEquipment" },
            };

            _plantItemIds = new Dictionary<int, string>
            {
                { 29, "ActiveWSSite" },
                { 119, "AppProject" },
                { 72, "Area" },
                { 40, "DrawingSite" },
                { 25, "DrawingSubscriber" },
                { 130, "FormatTypes" },
                { 146, "Formats" },
                { 54, "HierarchyLevels" },
                { 113, "InterSiteOPC" },
                { 97, "Plant" },
                { 100, "PlantGroup" },
                { 18, "PlantSetting" },
                { 114, "Project" },
                { 115, "ProjectPlantGroup" },
                { 120, "RoleAccess" },
                { 76, "ShowFieldTypeAttributes" },
                { 75, "ShowFieldTypes" },
                { 77, "ShowFieldUids" },
                { 94, "Unit" },
                { 22, "WSSite" },
            };

            _siteItemIds = new Dictionary<int, string>
            {
                { 72, "Area" },
                { 63, "BusinessSector" },
                { 102, "DB_Data" },
                { 130, "FormatTypes" },
                { 146, "Formats" },
                { 66, "Level" },
                { 97, "Plant" },
                { 100, "PlantGroup" },
                { 67, "PlantSystem" },
                { 121, "RoleRights" },
                { 122, "RoleUsers" },
                { 120, "Roles" },
                { 103, "RootItem" },
                { 98, "Site" },
                { 119, "SiteSetting" },
                { 123, "SlotUids" },
                { 68, "SubSystem" },
                { 94, "Unit" },
                { 105, "ValidHierarchy" },
                { 118, "ValidHierarchyLevel" },
            };

            _enumIds = new List<int>
            {
                147, // Conductor Size
                194, // Motor Rated Power
            };
        }

        public void Run()
        {
            PrintEntitySQL();
            //PrintItemPaths();
            //TestItemSQL();
            //TestItemSQL(true);
            //PrintItemRelation();
            //PrintItemRelation(true);
            //PrintAllItems();
            //PrintAllEnumerations();
            //PrintEnumerationsAndCodeLists();
            //PrintTableView();
            //TestPlantDict();
            //TestPlantItemSQL();
            //TestSiteItemSQL();
            //OutputAll();
            //OutputAll(true);


            //RunImportTest();
        }

        protected void RunImportTest()
        {
            var i = new ImportTest();
            i.Run();
        }

        protected void PrintEntitySQL() 
        {
            var e = _repo!.FindEntity(16).Result;
            var sql = SQLGen.GetSQL(e);
            Console.WriteLine(sql);
        }

        protected void PrintItemPaths()
        {
            var item = _repo!.FindItem(15).Result;
            var q = from ia in item!.ItemAtts
                    group ia by ia.Path;

            Console.WriteLine(string.Format("Source table : {0}", item?.SourceEntity?.EntityName));
            foreach (var p in q)
            {
                Console.WriteLine(String.Format("Path {0} : Table {1}", p.Key, p.FirstOrDefault()?.Attribution?.Entity?.EntityName));
                foreach (var ia in p.OrderBy(i => i.Name))
                    Console.WriteLine(string.Format("   {0} - {1}", ia.Name, ia.Attribution?.Attribute?.AttributeName));
                Console.WriteLine();
            }
        }

        protected void TestItemSQL(bool attrOnly = false, TextWriter ? writer = null)
        {
            foreach (var itemId in _itemIds.Keys)
            {
                var item = _repo.FindItem(itemId).Result;
                if (attrOnly)
                    ItemJoin.PrintItemAttributions(item, writer);
                else
                {
                    var res = _itemSQL.GetItemJoin(item, _repo).Result;
                    res.PrintAllSQL(writer);
                }
            }
        }

        protected void PrintItemRelation(bool relOnly = false, TextWriter? writer = null)
        {
            if (relOnly)
            {
                ObjectJoin.PrintRelations(_itemIds.Keys, _repo, writer);
            }
            else
            {
                foreach (var item in _itemIds)
                {
                    var res = _itemSQL.GetItemObjRelJoin(item.Key, _repo);
                    if (res != null)
                        res.PrintAllSQL(writer);
                }
            }
        }

        protected void PrintAllItems()
        {
            foreach (var item in _dbCtx.Items.OrderBy(i => i.Name))
            {
                Console.WriteLine(string.Format("{0}  -  {1}", item.Name, item.ID));
            }
        }

        protected void PrintAllEnumerations()
        {
            foreach (var e in _dbCtx.Enumerations.OrderBy(e => e.Name))
                Console.WriteLine(string.Format("{0} - {1}", e.Name, e.ID));
        }

        protected void PrintEnumerationsAndCodeLists()
        {
            foreach (var i in _enumIds)
            {
                var e = _repo.FindEnumerations(i).Result;
                if (e == null) continue;
                Console.WriteLine(string.Format("{0} - {1}", e.Name, e.ID));
                Console.WriteLine();
                var lst = e.CodeItems
                    .Where(e => (e.CodeListEntryDisabled ?? 0) == 0)
                    .OrderBy(e => e.CodeListSortValue);
                foreach (var c in lst)
                    Console.WriteLine(string.Format("   {0} - {1}", c.CodeListShortText, c.CodeListIndex));
                Console.WriteLine();
            }
        }

        protected void PrintTableView()
        {
            var q = _repo.FindTableView(_itemIds.Keys).Result;
            var lstView = q.GroupBy(e => e.Item).ToList();
            foreach (var g in lstView)
            {
                Console.WriteLine(string.Format("Item : {0}", g.Key?.Name));
                foreach (var v in g)
                {
                    var parentView = v.ParentView != null ? string.Format("  - ( {0} )", v.ParentView?.Name) : "";
                    Console.WriteLine("   {0,3}{1}", v.Name, parentView);
                    foreach (var l in v.ViewLayouts)
                    {
                        Console.WriteLine("      {0}", l.Name.Trim());
                        var atts = l.ViewAttributes
                            .OrderBy(j => j.ItemAttribute.Path.Length)
                            .ThenBy(j => j.ItemAttribute.Path)
                            .ThenBy(j => j.ItemAttribute.Name);
                        foreach (var a in atts)
                            Console.WriteLine("         {0}  -  ( {1} )", a.ColumnName, a.ItemAttribute?.Path);
                    }
                }
                Console.WriteLine();
            }
        }

        protected void TestPlantDict()
        {
            var db = _provider.GetService<IPLANTDRepository>();
            var itemId = 94; // Unit
            var lstItems = db.FindItem(itemId).Result;
        }

        protected void TestPlantItemSQL(bool attrOnly = false, TextWriter ? writer = null)
        {
            foreach (var itemId in _plantItemIds.Keys)
            {
                var item = _repo2.FindItem(itemId).Result;
                if (attrOnly)
                    ItemJoin.PrintItemAttributions(item, writer);
                else
                {
                    var res = _itemSQL.GetItemJoin(item, _repo2).Result;
                    res.PrintAllSQL(writer);
                }
            }
        }

        protected void TestSiteItemSQL(bool attrOnly = false, TextWriter? writer = null)
        {
            foreach (var itemId in _siteItemIds.Keys)
            {
                var item = _repo3.FindItem(itemId).Result;
                if (attrOnly)
                    ItemJoin.PrintItemAttributions(item, writer);
                else
                {
                    var res = _itemSQL.GetItemJoin(item, _repo3).Result;
                    res.PrintAllSQL(writer);
                }
            }
        }
        
        protected void OutputAll(bool attrOnly = false)
        {
            Console.WriteLine("Starting ....");

            var fileName = attrOnly ? "Site ItemAttributions.txt" : "Site ItemAttributions SQL.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                TestSiteItemSQL(attrOnly, writer);
            }
            Console.WriteLine(fileName);

            fileName = attrOnly ? "Plant ItemAttributions.txt" : "Plant ItemAttributions SQL.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                TestPlantItemSQL(attrOnly, writer);
            }
            Console.WriteLine(fileName);

            fileName = attrOnly ? "SEL ItemAttributions.txt" : "SEL ItemAttributions SQL.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                TestItemSQL(attrOnly, writer);
            }
            Console.WriteLine(fileName);

            fileName = attrOnly ? "SEL SourceDestObjectRels.txt" : "SEL SourceDestObjectRels SQL.txt";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                PrintItemRelation(attrOnly, writer);
            }
            Console.WriteLine(fileName);

            Console.WriteLine("Finish!");

        }
    }
}
