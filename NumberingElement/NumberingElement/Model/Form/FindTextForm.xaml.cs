using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SingleData;
using Utility;

namespace Model.Form
{
    /// <summary>
    /// Interaction logic for TagSpunPileForm.xaml
    /// </summary>
    public partial class FindTextForm : Window
    {
       
        private static RevitData revitData
        {
            get
            {
                return RevitData.Instance;
            }
        }
        public static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }
        public FindTextForm()
        {
            InitializeComponent();
        }

        private void FindText_Click(object sender, RoutedEventArgs e)
        {
            var form = FormData.Instance.FindTextForm;
            //var uidoc = revitData.UIDocument;
            //var sel = revitData.Selection;
            var setting = modelData.Setting;
            //var tx = revitData.Transaction;
            string textFind = setting.TextFind;
            //Autodesk.Revit.DB.View viewOfTextNote = null;
            var tagInstances = revitData.IndependentTags.Where(x => x.TagText.Contains(textFind)).ToList();
            modelData.SelectedIndependentTag = tagInstances;
            var selectedIndependentTag = modelData.SelectedIndependentTag;

            var textNoteInstances = revitData.Textnotes.Where(x => x.Text.Contains(textFind)).ToList();
            modelData.SelectedTextNotes = textNoteInstances;
            var selectedTextNotes = modelData.SelectedTextNotes;
            //List<Autodesk.Revit.DB.ElementId> listTextNote = new List<Autodesk.Revit.DB.ElementId>();
            //foreach (var item in selectedTextNotes)
            //{
            //    viewOfTextNote = item.OwnerViewId.GetRevitElement() as Autodesk.Revit.DB.View;
            //    listTextNote.Add(item.Id);
            //    tx.Commit();
            //    uidoc.ActiveView = viewOfTextNote;
            //    sel.SetElementIds(new List<Autodesk.Revit.DB.ElementId> { item.Id });
            //    tx.Start();
            //    modelData.SelectedTextNotes.Remove(item);
            //    break;


            //}
            selectedTextNotes.ShowView();
            selectedIndependentTag.ShowView();

            //form.Show();


        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //var uidoc = revitData.UIDocument;
            //var sel = revitData.Selection;
            //var setting = modelData.Setting;
            //var tx = revitData.Transaction;
            //string textFind = setting.TextFind;
            //Autodesk.Revit.DB.View viewOfTextNote = null;
            //var selectedTextNotes1 = modelData.SelectedTextNotes;
            //List<Autodesk.Revit.DB.ElementId> listTextNote = new List<Autodesk.Revit.DB.ElementId>();
            //foreach (var item in selectedTextNotes1)
            //{
            //    viewOfTextNote = item.OwnerViewId.GetRevitElement() as Autodesk.Revit.DB.View;
            //    listTextNote.Add(item.Id);
            //    tx.Commit();
            //    uidoc.ActiveView = viewOfTextNote;
            //    sel.SetElementIds(new List<Autodesk.Revit.DB.ElementId> { item.Id });
            //    tx.Start();
            //    modelData.SelectedTextNotes.Remove(item);
            //    break;


            //}
            var selectedTextNotes1 = modelData.SelectedTextNotes;
            selectedTextNotes1.ShowView();

            var selectedIndependentTag1 = modelData.SelectedIndependentTag;
            selectedIndependentTag1.ShowView();
        }
    }
}
