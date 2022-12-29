using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;
using Autodesk.Revit.DB;


namespace Utility
{
    public static class ViewUtil
    {
        private static RevitData revitData = RevitData.Instance;
        public static string GetIdentifyName (this View view)
        {
            
            var doc = revitData.Document;
            var viewFamilyType = view.GetTypeId().GetRevitElement() as ViewFamilyType;
            var viewFamily = viewFamilyType?.ViewFamily;
            return $"{viewFamily.ToString()}__{view.Name}";
       
        }
        public static View GetView (string name, ViewFamily viewFamily)
        {
            var instanceView = revitData.InstanceViews.SingleOrDefault(x => x.Name == name 
            && (x.GetTypeId().GetRevitElement() as ViewFamilyType).ViewFamily == viewFamily);
            if(instanceView == null)
            {
                throw new Model.Exception.ElementNotFoundException();
            }
            return instanceView;
        }
        public static Autodesk.Revit.DB.Solid CreateCutPlaneSolid(this Autodesk.Revit.DB.View planView)
        {
            Autodesk.Revit.DB.BoundingBoxXYZ bbActiveView = planView.get_BoundingBox(null);
            Autodesk.Revit.DB.Plane planePlanView = planView.SketchPlane.GetPlane();
            Autodesk.Revit.DB.PlanViewRange viewRange = (planView as Autodesk.Revit.DB.ViewPlan).GetViewRange();
            double cutPlaneHeight = viewRange.GetOffset(Autodesk.Revit.DB.PlanViewPlane.CutPlane);

            Autodesk.Revit.DB.XYZ pt0 = new Autodesk.Revit.DB.XYZ(bbActiveView.Min.X, bbActiveView.Min.Y, bbActiveView.Min.Z);
            Autodesk.Revit.DB.XYZ pt1 = new Autodesk.Revit.DB.XYZ(bbActiveView.Max.X, bbActiveView.Min.Y, bbActiveView.Min.Z);
            Autodesk.Revit.DB.XYZ pt2 = new Autodesk.Revit.DB.XYZ(bbActiveView.Max.X, bbActiveView.Max.Y, bbActiveView.Min.Z);
            Autodesk.Revit.DB.XYZ pt3 = new Autodesk.Revit.DB.XYZ(bbActiveView.Min.X, bbActiveView.Max.Y, bbActiveView.Min.Z);

            Autodesk.Revit.DB.XYZ pt00 = PlaneUtil.ProjectOnto(planePlanView, pt0);
            Autodesk.Revit.DB.XYZ pt11 = PlaneUtil.ProjectOnto(planePlanView, pt1);
            Autodesk.Revit.DB.XYZ pt22 = PlaneUtil.ProjectOnto(planePlanView, pt2);
            Autodesk.Revit.DB.XYZ pt33 = PlaneUtil.ProjectOnto(planePlanView, pt3);

            Autodesk.Revit.DB.Line edge00 = Autodesk.Revit.DB.Line.CreateBound(pt00, pt11);
            Autodesk.Revit.DB.Line edge11 = Autodesk.Revit.DB.Line.CreateBound(pt11, pt22);
            Autodesk.Revit.DB.Line edge22 = Autodesk.Revit.DB.Line.CreateBound(pt22, pt33);
            Autodesk.Revit.DB.Line edge33 = Autodesk.Revit.DB.Line.CreateBound(pt33, pt00);

            List<Autodesk.Revit.DB.Curve> edges0 = new List<Autodesk.Revit.DB.Curve>();
            edges0.Add(edge00);
            edges0.Add(edge11);
            edges0.Add(edge22);
            edges0.Add(edge33);

            Autodesk.Revit.DB.CurveLoop baseLoop0 = Autodesk.Revit.DB.CurveLoop.Create(edges0);
            List<Autodesk.Revit.DB.CurveLoop> loopList0 = new List<Autodesk.Revit.DB.CurveLoop>();
            loopList0.Add(baseLoop0);
            Autodesk.Revit.DB.Solid preTransformSolid = Autodesk.Revit.DB.GeometryCreationUtilities.CreateExtrusionGeometry(loopList0, Autodesk.Revit.DB.XYZ.BasisZ, cutPlaneHeight);
            Autodesk.Revit.DB.Solid transformSolid = Autodesk.Revit.DB.SolidUtils.CreateTransformed(preTransformSolid, bbActiveView.Transform);

            return transformSolid;
            //return preTransformSolid;
            //Autodesk.Revit.DB.BoundingBoxXYZ inputBb = planView.get_BoundingBox(null);
            //Autodesk.Revit.DB.BoundingBoxXYZ bbActiveView = planView.CropBox;
            //Autodesk.Revit.DB.Plane planePlanView = planView.SketchPlane.GetPlane();
            //Autodesk.Revit.DB.PlanViewRange viewRange = (planView as Autodesk.Revit.DB.ViewPlan).GetViewRange();

            ////edges in BBox coords

            ////create loop, still in BBox coords
            ////List<Curve> edges = new List<Curve>();

            ////Double height = viewRange.GetOffset(Autodesk.Revit.DB.PlanViewPlane.CutPlane); 
            ////CurveLoop baseLoop1 = planView.GetCropRegionShapeManager().GetAnnotationCropShape();
            ////List<CurveLoop> loopList = planView.GetCropRegionShapeManager().GetCropShape().ToList();
            //////loopList.Add(baseLoop1);
            ////Solid preTransformBox = GeometryCreationUtilities.CreateExtrusionGeometry(loopList, XYZ.BasisZ, height);
            ////Solid transformBox = SolidUtils.CreateTransformed(preTransformBox, inputBb.Transform);

            ////return preTransformBox;
            //return transformBox;


        }
        public static void ShowView (this List<TextNote> selectedTextNotes, View viewOfTextNote = null)
        {
            if(selectedTextNotes != null)
            {
                foreach (var item in selectedTextNotes)
                {
                    viewOfTextNote = item.OwnerViewId.GetRevitElement() as Autodesk.Revit.DB.View;
                    RevitData.Instance.Transaction.Commit();
                    RevitData.Instance.UIDocument.ActiveView = viewOfTextNote;
                    RevitData.Instance.Selection.SetElementIds(new List<Autodesk.Revit.DB.ElementId> { item.Id });
                    //RevitData.Instance.UIDocument.ShowElements(item);
                    RevitData.Instance.Transaction.Start();
                    ModelData.Instance.SelectedTextNotes.Remove(item);
                    break;
                }
            }
            
        }
        public static void ShowView(this List<IndependentTag> selectedIndependentTag, View viewOfTextNote = null)
        {
            if (selectedIndependentTag != null)
            {
                foreach (var item in selectedIndependentTag)
                {
                    viewOfTextNote = item.OwnerViewId.GetRevitElement() as Autodesk.Revit.DB.View;
                    RevitData.Instance.Transaction.Commit();
                    RevitData.Instance.UIDocument.ActiveView = viewOfTextNote;
                    RevitData.Instance.Selection.SetElementIds(new List<Autodesk.Revit.DB.ElementId> { item.Id });
                    RevitData.Instance.UIDocument.ShowElements(item);
                    RevitData.Instance.Transaction.Start();
                    ModelData.Instance.SelectedIndependentTag.Remove(item);
                    break;               
                }
            }            
        }
    }
}
