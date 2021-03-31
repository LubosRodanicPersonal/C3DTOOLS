//system
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//cad alias
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Civil.DatabaseServices;
//using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.Civil.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.Civil.Settings;

[assembly: ExtensionApplication(typeof(C3DTools.ExtApp))]
[assembly: CommandClass(typeof(C3DTools.Commands))]


namespace C3DTools
{
    public class ExtApp : IExtensionApplication {

        public void Initialize() {

        }
        public void Terminate() {

        }
    }

    public class Commands
    {
        static CivilDocument oDoc = null;
        static Database oDatabase = null;
        static Editor oEditor = null;
        static DlgAlignment dlgAli;

        [CommandMethod("Test1")]
        public static void Test1() {
            
            SetEnvironment();

            //get points
            Point3dCollection pcPoints = new Point3dCollection();

            bool bContinue = true;
            pcPoints.Clear();
            string message = "Vyberte počátečný bod:";
            try {
                while (bContinue) {

                    PromptPointOptions prPntOpt = new PromptPointOptions("\n " +  message + "\n");
                    prPntOpt.BasePoint = new Point3d(0, 0, 0);
                    prPntOpt.UseDashedLine = true;
                    prPntOpt.UseBasePoint = false;
                    
                    //_AcEd.PromptPointResult acSSPrompt = oEditor.GetPoint("\nSelect Point:\n");
                    PromptPointResult prPntRes = oEditor.GetPoint(prPntOpt);
                    if (prPntRes.Status == PromptStatus.OK) {
                        pcPoints.Add(prPntRes.Value);
                        message = "Vyberte další/koncový bod (Esc pro ukončení):";
                    }
                    else {
                        //necakam na Esc ale na vsetko okrem OK
                        bContinue = false;
                    }
                }
            }
            catch { }

            oEditor.WriteMessage("\nPočet vybraných bodů:" + pcPoints.Count + "\n");

            if (pcPoints.Count > 1) {
                //create alignment
                using (Transaction oTransaction = oDatabase.TransactionManager.StartTransaction()) {
                    try {
                        using (BlockTable acBlkTbl = oTransaction.GetObject(oDatabase.BlockTableId, OpenMode.ForRead) as BlockTable) {
                            using (BlockTableRecord acBlkTblRec = oTransaction.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord) {
                                Polyline acPoly = new Polyline();
                                acPoly.SetDatabaseDefaults();

                                foreach (Point3d pt in pcPoints) {
                                    acPoly.AddVertexAt(acPoly.NumberOfVertices, new Point2d(pt.X, pt.Y), 0, 1, 1);
                                }
                                acPoly.Closed = false;

                                //pridat krivku do vykresu
                                acBlkTblRec.AppendEntity(acPoly);
                                oTransaction.AddNewlyCreatedDBObject(acPoly, true);

                                ////vytvorenie trasy

                                //options
                                PolylineOptions plops = new PolylineOptions();
                                plops.AddCurvesBetweenTangents = true;
                                plops.EraseExistingEntities = true;
                                plops.PlineId = acPoly.ObjectId;

                                //get aliName
                                ObjectIdCollection oAlignments = oDoc.GetAlignmentIds();
                                string aliName = "Trasa_" + (oAlignments.Count + 1).ToString();

                                //get layerId
                                ObjectId layerId = ObjectId.Null;
                                try {
                                    LayerTable lt = oTransaction.GetObject(oDatabase.LayerTableId, OpenMode.ForRead) as LayerTable;

                                    LayerTableRecord layer;
                                    foreach (ObjectId layId in lt) {
                                        layer = oTransaction.GetObject(layId, OpenMode.ForRead) as LayerTableRecord;
                                        //lstlay.Add(layer.Name);

                                        if (layer.Name == "C-ROAD") {
                                            layerId = layId;
                                        }
                                    }
                                }
                                catch { }

                                //get aliStyle
                                ObjectId aliStyleId = ObjectId.Null;
                                try {
                                    aliStyleId = oDoc.Styles.AlignmentStyles[0];
                                }
                                catch { }

                                //get aliLSS
                                ObjectId aliLSSId = ObjectId.Null;
                                try {
                                    aliLSSId = oDoc.Styles.LabelSetStyles.AlignmentLabelSetStyles[0];
                                }
                                catch { }

                                //working
                                //SettingsAlignment alignmentSettings = oDoc.Settings.GetSettings<SettingsAlignment>();

                                //ali creation
                                try {
                                    ObjectId oAlignmentId = Alignment.Create(oDoc, plops, aliName, ObjectId.Null, layerId, aliStyleId, aliLSSId);
                                }
                                catch (System.Exception ex) {
                                    AddToLog("Chyba při vytvoření trasy:" + ex.Message, true);
                                }

                            }
                        }
                        oTransaction.Commit();
                    }
                    catch (System.Exception ex) {
                        oTransaction.Abort();
                        AddToLog("Chyba při vytvoření trasy:" + ex.Message, true);
                    }
                }
            }
            else {
                //nedostatok bodov, return !!!!
                AddToLog("K vytvoření trasy je třeba vybrat minimálně dva body!", true);
            }
        }

        [CommandMethod("Test2")]
        public static void Test2() {
            if (!SetEnvironment()) { return; }

            string aliHandle = "";
            string aliName = "";
            dlgAli = new DlgAlignment();

            using (Transaction oTransaction = oDatabase.TransactionManager.StartTransaction()) {
                try {
                    PromptEntityOptions opt = new PromptEntityOptions("\nVyberte trasu:");
                    opt.SetRejectMessage("\nVybraný objekt musí být trasa:\n");
                    opt.AddAllowedClass(typeof(Alignment), false);

                    PromptEntityResult ent = oEditor.GetEntity(opt);
                    if (ent.Status == PromptStatus.OK) {
                        var alignId = ent.ObjectId;
                        Alignment oAlignment = oTransaction.GetObject(alignId, OpenMode.ForRead) as Alignment;
                        aliHandle = alignId.Handle.ToString();
                        aliName = oAlignment.Name;
                        
                        GetProfilesToTable(oAlignment);
                    }

                    oTransaction.Commit();
                }
                catch (System.Exception ex) {
                    oTransaction.Abort();
                    AddToLog("Chyba při výběru trasy:" + ex.Message, true);
                }
            }

            //dlgAli.InitDataGrid(aliHandle, aliName);
            AcadApp.ShowModalDialog(dlgAli);
            //dlgAli.Visible = true;
        }

        public static void GetProfilesToTable(Alignment oAlignment) {

            dlgAli.dtProfiles.Rows.Clear();

            ObjectIdCollection cProfileIds = oAlignment.GetProfileIds();
            foreach (ObjectId profileId in cProfileIds) {
                //Profile oProfile = oTransaction.GetObject(profileId, OpenMode.ForWrite) as Profile;
                Profile oProfile = profileId.GetObject(OpenMode.ForRead) as Profile;
                if (!oProfile.IsErased) {
                    try {
                        System.Data.DataRow row = dlgAli.dtProfiles.NewRow();
                        row["ID"] = profileId.Handle.ToString();
                        row["NAME"] = oProfile.Name;

                        dlgAli.dtProfiles.Rows.Add(row);
                        
                    }
                    catch { }
                } 
            }

            dlgAli.txtAliName.Text = oAlignment.Name;
            dlgAli.txtAliHandle.Text = oAlignment.ObjectId.Handle.ToString();
            dlgAli.RefreshDataGrid();
        }

        public static bool SetEnvironment() {
            try {
                oDoc = Autodesk.Civil.ApplicationServices.CivilApplication.ActiveDocument;
                oEditor = AcadApp.DocumentManager.MdiActiveDocument.Editor;
                oDatabase = HostApplicationServices.WorkingDatabase;

                return true;
            }
            catch {
                //zrejme neotvoreny vykres
                return false;
            }
        }

        public static bool CreateProfile(string aliHandle, string profileName, double vyska) {
            if (!SetEnvironment()) { return false; }

            using (Transaction oTransaction = oDatabase.TransactionManager.StartTransaction()) {
                ObjectId alignId = ObjectId.Null;
                
                try {

                    long ln = Convert.ToInt64(aliHandle, 16);
                    Handle hn = new Handle(ln);
                    alignId = oDatabase.GetObjectId(false, hn, 0);
                    if (alignId != ObjectId.Null) {
                        Alignment oAlignment = oTransaction.GetObject(alignId, OpenMode.ForRead) as Alignment;

                        //profilename
                        //string profName = "Profile" + (oAlignment.GetProfileIds().Count + 1).ToString();
                        string profName = profileName;

                        // use the same layer as the alignment
                        ObjectId layerId = oAlignment.LayerId;
                        
                        //get styles
                        ObjectId styleId = ObjectId.Null;
                        try {
                            //styleId = oDoc.Styles.ProfileStyles["Standard"];
                            styleId = oDoc.Styles.ProfileStyles[0];
                        }
                        catch { }
                        ObjectId labelSetId = ObjectId.Null;
                        try {
                            //labelSetId = oDoc.Styles.LabelSetStyles.ProfileLabelSetStyles["Standard"];
                            labelSetId = oDoc.Styles.LabelSetStyles.ProfileLabelSetStyles[0];
                        }
                        catch { }

                        //profile creation
                        ObjectId oProfileId = ObjectId.Null;
                        try {
                            oProfileId = Profile.CreateByLayout(profName, alignId, layerId, styleId, labelSetId);
                        }
                        catch (System.Exception ex) {
                            AddToLog("Chyba při vytvoření profilu:" + ex.Message, true);
                        }
                        
                        // Now add the entities that define the profile.
                        if(oProfileId != null) {
                            Profile oProfile = oTransaction.GetObject(oProfileId, OpenMode.ForRead) as Profile;

                            Point2d startPoint = new Point2d(oAlignment.StartingStation, vyska);
                            Point2d endPoint = new Point2d(oAlignment.EndingStation, vyska);
                            ProfileTangent oTangent1 = oProfile.Entities.AddFixedTangent(startPoint, endPoint);

                            //Point3d startPoint = new Point3d(oAlignment.StartingStation, -40, 0);
                            //Point3d endPoint = new Point3d(758.2, -70, 0);
                            //ProfileTangent oTangent1 = oProfile.Entities.AddFixedTangent(startPoint, endPoint);

                            //startPoint = new Point3d(1508.2, -60.0, 0);
                            //endPoint = new Point3d(oAlignment.EndingStation, -4.0, 0);
                            //ProfileTangent oTangent2 = oProfile.Entities.AddFixedTangent(startPoint, endPoint);

                            //oProfile.Entities.AddFreeSymmetricParabolaByLength(oTangent1.EntityId, oTangent2.EntityId, VerticalCurveType.Sag, 900.1, true);
                        }
                        GetProfilesToTable(oAlignment);
                    }
                    oTransaction.Commit();
                }
                catch (System.Exception ex) {
                    AddToLog("Chyba při vytvoření profilu:" + ex.Message, true);
                    oTransaction.Abort();
                }
            }

            return true;
        }

        public static bool DeleteProfile(string aliHandle, string profileHandle) {
            if (!SetEnvironment()) { return false; }

            using (Transaction oTransaction = oDatabase.TransactionManager.StartTransaction()) {
                ObjectId alignId = ObjectId.Null;

                try {

                    long ln = Convert.ToInt64(aliHandle, 16);
                    Handle hn = new Handle(ln);
                    alignId = oDatabase.GetObjectId(false, hn, 0);
                    if (alignId != ObjectId.Null) {
                        Alignment oAlignment = oTransaction.GetObject(alignId, OpenMode.ForRead) as Alignment;

                        ObjectIdCollection cProfileIds = oAlignment.GetProfileIds();
                        foreach (ObjectId profileId in cProfileIds) {
                            
                            if(profileId.Handle.ToString() == profileHandle) {
                                Profile oProfile = oTransaction.GetObject(profileId, OpenMode.ForWrite) as Profile;

                                oAlignment.UpgradeOpen();
                                oProfile.Erase();
                                oAlignment.DowngradeOpen();
                            }
                        }
                        GetProfilesToTable(oAlignment);
                    }
                    oTransaction.Commit();
                }
                catch (System.Exception ex) {
                    AddToLog("Chyba při vymazání profilu:" + ex.Message, true);
                    oTransaction.Abort();
                }
            }

            return true;
        }

        public static void AddToLog(string text, bool alert) {
            string app = "C3DTools";

            try {
                if (oEditor != null) {
                    oEditor.WriteMessage("\n" + app + ": " + text + "\n");
                }
            }
            catch { }

            try {
                if (alert) {
                    AcadApp.ShowAlertDialog("\n" + app + ": " + text + "\n");
                }
            }
            catch { }

        }
    }
}
