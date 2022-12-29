using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SingleData;

namespace Utility
{
    public static class FoundationTypeUtil
    {
        private static RevitData revitData
        {
            get
            {
                return RevitData.Instance;
            }
        }
        private static ModelData modelData
        {
            get
            {
                return ModelData.Instance;
            }
        }
        public static List<Model.Entity.FoundationType> GetFoundationTypes()
        {
            var foundationTypes = new List<Model.Entity.FoundationType>();

            foreach (var element in revitData.FoundationInstances)
            {
                var elementType = element.Symbol;
                var foundationType = foundationTypes.SingleOrDefault(x => x.RevitElement.Id == elementType.Id);
                if (foundationType == null)
                {
                    foundationType = new Model.Entity.FoundationType
                    {
                        RevitElement = elementType
                    };
                    foundationTypes.Add(foundationType);
                }
                var foundation = new Model.Entity.Element
                {
                    RevitElement = element
                };
                foundationType.Foundations.Add(foundation);

            }

            return foundationTypes;
        }
        public static List<Model.Entity.FoundationFamily> GetFoundationFamilies()
        {
            //var foundationTypes = new List<Model.Entity.FoundationType>();
            var foundationFamilies = new List<Model.Entity.FoundationFamily>();

            foreach (var element in revitData.FoundationInstancesInView)
            {
                var elementType = element.Symbol;
                var elementFam = elementType.Family;
                //var foundationType = foundationTypes.SingleOrDefault(x => x.RevitElement.Id == elementType.Id);
                var foundationFamily = foundationFamilies.SingleOrDefault(x => (x.RevitElement.Name == elementFam.Name));
                //if (foundationType == null)
                //{
                //    foundationType = new Model.Entity.FoundationType
                //    {
                //        RevitElement = elementType
                //    };
                //    foundationTypes.Add(foundationType);
                //}
                //var foundation = new Model.Entity.Foundation
                //{
                //    RevitElement = element
                //};
                //foundationType.Foundations.Add(foundation);
                if(foundationFamily == null)
                {
                    foundationFamily = new Model.Entity.FoundationFamily
                    {
                        RevitElement = elementFam
                    };
                    foundationFamilies.Add(foundationFamily);
                }
                var foundation = new Model.Entity.Element
                {
                    RevitElement = element
                };
                foundationFamily.Foundations.Add(foundation);
                
            }

            return foundationFamilies;
        }
    }
}
