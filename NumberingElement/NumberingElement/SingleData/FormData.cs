using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace SingleData
{
    public class FormData : NotifyClass
    {
        public static FormData Instance { get { return Singleton.Instance.FormData; } }
        private Model.Form.InputForm inputForm;
        public Model.Form.InputForm InputForm
        {
            get
            {
                if (inputForm == null)
                {
                    inputForm = new Model.Form.InputForm { DataContext = this };
                }
                return inputForm;
            }
        }
        private Model.Form.TagSpunPileForm tagPileForm;
        public Model.Form.TagSpunPileForm TagPileForm
        {
            get
            {
                if(tagPileForm == null)
                {
                    tagPileForm = new Model.Form.TagSpunPileForm { DataContext = this };
                }
                return tagPileForm;
            }
        }
        private Model.Form.FindTextForm findTextForm;
        public Model.Form.FindTextForm FindTextForm
        {
            get
            {
                if (findTextForm == null)
                {
                    findTextForm = new Model.Form.FindTextForm { DataContext = this };
                }
                return findTextForm;
            }
        }
        private List<Autodesk.Revit.DB.TextNote> selectedTextNotes;
        public List<Autodesk.Revit.DB.TextNote> SelectedTextNotes
        {
            get
            {
                return selectedTextNotes;
            }
            set
            {
                selectedTextNotes = value;
                OnPropertyChanged();
                ModelData.Instance.SelectedTextNotes = value;
            }
        }
        private List<Autodesk.Revit.DB.IndependentTag> selectedIndependentTag;
        public List<Autodesk.Revit.DB.IndependentTag> SelectedIndependentTag
        {
            get
            {
                return selectedIndependentTag;
            }
            set
            {
                selectedIndependentTag = value;
                OnPropertyChanged();
                ModelData.Instance.SelectedIndependentTag = value;
            }
        }
        //private Model.ViewModel.ElementView elementView;
        //public Model.ViewModel.ElementView ElementView
        //{
        //    get
        //    {
        //        return elementView;
        //    }
        //    set
        //    {
        //        elementView = value;
        //        OnPropertyChanged();
        //    }
        //}
        private Model.ViewModel.SettingView settingView;
        public Model.ViewModel.SettingView SettingView
        {
            get
            {
                if (settingView == null)
                {
                    settingView = new Model.ViewModel.SettingView()
                    {
                        Setting = ModelData.Instance.Setting
                    };
                }
                return settingView;
            }
        }
        public List<Model.Entity.FoundationFamily> FoundationFamilies
        {
            get
            {
                return ModelData.Instance.FoundationFamilies;
            }
        }
        public List<Model.Entity.FoundationType> FoundationTypes
        {
            get
            {
                return ModelData.Instance.FoundationTypes;
            }
        }
        private Model.Entity.FoundationType currentFoundationType;
        public Model.Entity.FoundationType CurrentFoundationType
        {
            get
            {
                return currentFoundationType;
            }
            set
            {
                currentFoundationType = value;
                OnPropertyChanged();
                ModelData.Instance.CurrentFoundationType = value;
            }
        }
        private Model.Entity.FoundationFamily currentFoundationFamily;
        public Model.Entity.FoundationFamily CurrentFoundationFamily
        {
            get
            {
                return currentFoundationFamily;
            }
            set
            {
                currentFoundationFamily = value;
                OnPropertyChanged();
                ModelData.Instance.CurrentFoundationFamily = value;
            }
        }
        public List<Autodesk.Revit.DB.FamilySymbol> FoundationTags
        {
            get
            {
                return ModelData.Instance.FoundationTags;
            }
        }
        private Autodesk.Revit.DB.FamilySymbol currentFoundationTag;
        public Autodesk.Revit.DB.FamilySymbol CurrentFoundationTag
        {
            get
            {
                return currentFoundationTag;
            }
            set
            {
                currentFoundationTag = value;
                OnPropertyChanged();
                ModelData.Instance.CurrentFoundationTag = value;
            }
        }
        //private ObservableCollection<Model.ViewModel.RebarView> rebarViews;
        //public ObservableCollection<Model.ViewModel.RebarView> RebarViews
        //{
        //    get
        //    {
        //        if(rebarViews == null)
        //        {
        //            rebarViews = new ObservableCollection<Model.ViewModel.RebarView>();
        //        }
        //        return rebarViews;
        //    }
        //    set
        //    {
        //        rebarViews = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private ObservableCollection<Model.ViewModel.StirrupView> stirrupViews;
        //public ObservableCollection<Model.ViewModel.StirrupView> StirrupViews
        //{
        //    get
        //    {
        //        if(stirrupViews == null)
        //        {
        //            stirrupViews = new ObservableCollection<Model.ViewModel.StirrupView>();
        //        }
        //        return stirrupViews;
        //    }
        //    set
        //    {
        //        stirrupViews = value;
        //        OnPropertyChanged();
        //    }
        //}

    }
}
