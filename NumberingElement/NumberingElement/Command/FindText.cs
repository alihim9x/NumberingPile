using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Structure;
using SingleData;
using Utility;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace FindText
{
    [Transaction(TransactionMode.Manual)]
    public class FindText : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region Initial
            var singleTon = Singleton.Instance = new Singleton();
            var modelData = singleTon.ModelData;
            var revitData = singleTon.RevitData;
            revitData.UIApplication = commandData.Application;
            var sel = revitData.Selection;
            var doc = revitData.Document;
            var activeView = revitData.ActiveView;
            var tx = revitData.Transaction;
            var uidoc = revitData.UIDocument;
            var app = revitData.Application;
            tx.Start();
            #endregion

            var form = FormData.Instance.FindTextForm;

            form.ShowDialog();


            //tx.Commit();
            //var form = FormData.Instance.TagPileForm;
            //form.ShowDialog();

            //string text = "HRB1";
            //var textNoteInstances = revitData.Textnotes.Where(x => x.Text.Contains(text)).ToList();
            //var a = textNoteInstances.FirstOrDefault().OwnerViewId.GetRevitElement() as View;
            //View viewOfTextNote = null;
            //List < ElementId > listTextNote = new List<ElementId>();
            //foreach (var item in textNoteInstances)
            //{
            //    viewOfTextNote = item.OwnerViewId.GetRevitElement() as View;
            //    listTextNote.Add(item.Id);
            //}
            ////TaskDialog.Show("Revit", $"{textNoteInstances.Count}");

            //uidoc.ActiveView = a;
            //sel.SetElementIds(new List<ElementId> {textNoteInstances.FirstOrDefault().Id });

            //var a = sel.PickObject(ObjectType.Element).GetRevitElement() as TextNote;
            //uidoc.ShowElements(a);

            tx.Commit();
            return Result.Succeeded;
        }
    }
}
