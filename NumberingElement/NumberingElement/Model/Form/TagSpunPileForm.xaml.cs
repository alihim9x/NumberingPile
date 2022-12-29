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
    public partial class TagSpunPileForm : Window
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
        public TagSpunPileForm()
        {
            InitializeComponent();
        }

        private void TagPile_Click(object sender, RoutedEventArgs e)
        {
            var piles = modelData.CurrentFoundationFamily.Foundations.Select(x => x.RevitElement as Autodesk.Revit.DB.FamilyInstance).ToList();
            var tags = modelData.FoundationTags;
            var tag = ModelData.Instance.CurrentFoundationTag;
            foreach (Autodesk.Revit.DB.FamilyInstance pile in piles)
            {
                Autodesk.Revit.DB.IndependentTag.Create(revitData.Document, tag.Id, revitData.ActiveView.Id, new Autodesk.Revit.DB.Reference(pile), false,
                    Autodesk.Revit.DB.TagOrientation.Horizontal,(pile.Location as Autodesk.Revit.DB.LocationPoint).Point);
            }
            var form = FormData.Instance.TagPileForm;
            form.Close();
        }
    }
}
