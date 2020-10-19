using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminShellNS;
using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.AssetAdministrationShell.Identification;
using BaSyx.Models.Core.AssetAdministrationShell.References;
using BaSyx.Models.Core.AssetAdministrationShell.Semantics;
using BaSyx.Models.Core.Common;
using static AdminShellNS.AdminShellV20;

namespace AasxPackageExplorer.Utils
{
    public record BasyxHelper
    {
        /// <summary>
        /// BasyX 결과를 패키지 익스플로러에서 동작할수 있도록 구조를 맞춰줌
        /// </summary>
        /// <param name="assetAdministrationShell"></param>
        /// <returns></returns>
        public AdminShellPackageEnv BasyxToAdminShellPackageEnv(IAssetAdministrationShell assetAdministrationShell, List<Submodel> submodels, List<SubmodelRef> submodelRefs)
        {
            var adminShellPackageEnv = new AdminShellPackageEnv();
            try
            {
                //asset
                adminShellPackageEnv.AasEnv.Assets.Add(ConvertFromBasyxAsset(assetAdministrationShell.Administration, assetAdministrationShell.Asset));

                //submodel
                adminShellPackageEnv.AasEnv.Submodels.AddRange(ConvertFromBasyxSubmodel(submodels));
                
                //concept description
                adminShellPackageEnv.AasEnv.ConceptDescriptions.Add(ConvertFromBasyxConceptDescriptions(assetAdministrationShell.ConceptDescription)); //처리


                //aas
                var administrationShell = new AdministrationShell();
                administrationShell = ConvertFromBasyxAdministrationShell(assetAdministrationShell);

                //assetRef for aas
                administrationShell.assetRef = BuildAssetRefFromBasyx(assetAdministrationShell.Asset);

                //submodelRefs for aas
                administrationShell.submodelRefs.AddRange(BuildSubmodelRefFromSubmodels(submodels));

                adminShellPackageEnv.AasEnv.AdministrationShells.Add(administrationShell);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
            return adminShellPackageEnv;
        }

        private List<Submodel> ConvertFromBasyxSubmodel(List<Submodel> submodels)
        {
            //var submodelList = new List<Submodel>();

            foreach (var submodel in submodels)
            {
                if (int.TryParse(submodel.identification.idType, out var idTypeNumber))
                {
                    submodel.identification.idType = ConvertIdType(idTypeNumber);
                }
            }

            return submodels;
        }

        private string ConvertIdType(int idTypeNumber)
        {
            return ((KeyType)idTypeNumber).ToString();
        }

        private Submodel ccc(IElementContainer<ISubmodel> submodels)
        {
            var sss = new Submodel();
            
            return sss;
        }
        private List<SubmodelRef> BuildSubmodelRefFromBasyxSubmodels(IElementContainer<ISubmodel> submodels)
        {
            var submodelRef = new List<SubmodelRef>();

            foreach (var submodel in submodels)
            {
                var submodelReference = SubmodelRef.CreateNew(submodel.ModelType.ToString(), true, submodel.Identification.IdType.ToString(), submodel.Identification.Id);
                submodelRef.Add(submodelReference);
            }
            return submodelRef;
        }

        private List<SubmodelRef> BuildSubmodelRefFromSubmodels(List<Submodel> submodels)
        {
            var submodelRef = new List<SubmodelRef>();
            submodels = ConvertFromBasyxSubmodel(submodels);

            foreach (var submodel in submodels)
            {
                var submodelReference = SubmodelRef.CreateNew("Submodel", true, submodel.identification.idType, submodel.identification.id);
                submodelRef.Add(submodelReference);
            }
            return submodelRef;
        }

        private List<SubmodelRef> BuildSubmodelRefsFromBasyx(IElementContainer<ISubmodel> submodels)
        {
            var submodelRef = new List<SubmodelRef>();

            return submodelRef;
        }

        private AssetRef BuildAssetRefFromBasyx(IAsset asset)
        {
            var assetRef = new AssetRef();

            return assetRef;
        }

        private AdministrationShell ConvertFromBasyxAdministrationShell(IAssetAdministrationShell basyxAssetAdministrationShell)
        {
            var administrationShell = new AdministrationShell();
            administrationShell.idShort = basyxAssetAdministrationShell.IdShort;
            
            return administrationShell;
        }

        /// <summary>
        /// Basyx의 ConceptDescription을 변환 
        /// </summary>
        /// <param name="basyxConceptDescription"></param>
        /// <returns></returns>
        private AdminShellV20.ConceptDescription ConvertFromBasyxConceptDescriptions(IConceptDescription basyxConceptDescription)
        {
            var conceptDescriptions = new AdminShellV20.ConceptDescription();

            return conceptDescriptions;
        }

        /// <summary>
        /// Basyx의 Asset을 변환
        /// </summary>
        /// <param name="basyxAsset"></param>
        /// <returns></returns>
        private Asset ConvertFromBasyxAsset(AdministrativeInformation basyxAdministrativeInformation,  IAsset basyxAsset)
        {
            try
            {
                var asset = new Asset();
                asset.idShort = basyxAsset.IdShort;
                asset.kind = basyxAsset.Kind == BaSyx.Models.Core.AssetAdministrationShell.Enums.AssetKind.Instance ? AssetKind.CreateAsInstance() : AssetKind.CreateAsType();
                asset.administration = new Administration
                {
                    revision = basyxAdministrativeInformation?.Revision,
                    version = basyxAdministrativeInformation?.Version,
                };
                if (basyxAsset.Description != null)
                {
                    foreach (var langString in basyxAsset.Description)
                    {
                        asset.AddDescription(langString.Language, langString.Text);
                    }
                }
                if (basyxAsset.Identification != null)
                {
                    asset.identification.id = basyxAsset.Identification?.Id;
                    asset.identification.idType = basyxAsset.Identification?.IdType.ToString();
                }

                return asset;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new Asset();
            }
        }
    }
}
