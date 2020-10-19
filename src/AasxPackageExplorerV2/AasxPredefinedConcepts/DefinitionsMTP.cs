﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminShellNS;

namespace AasxPredefinedConcepts
{
    /// <summary>
    /// Definitions for MTP. Somehow preliminary, to be replaced by "full" JSON definitions
    /// </summary>
    public class DefinitionsMTP : AasxDefinitionBase
    {
        /// <summary>
        /// Definitions for MTP. Somehow preliminary, to be replaced by "full" JSON definitions
        /// </summary>
        public class ModuleTypePackage
        {
            public AdminShell.SemanticId
                SEM_MtpSubmodel,
                SEM_MtpInstanceSubmodel;

            public AdminShell.ConceptDescription
                CD_MtpTypeSubmodel,
                CD_SourceList,
                CD_SourceOpcUaServer,
                CD_Endpoint,
                CD_MtpFile;

            public ModuleTypePackage(AasxDefinitionBase bs)
            {
                SEM_MtpSubmodel = new AdminShell.SemanticId(
                    AdminShell.Key.CreateNew(
                        type: "Submodel",
                        local: false,
                        idType: "IRI",
                        value: "http://www.admin-shell.io/mtp/v1/submodel"));

                SEM_MtpInstanceSubmodel = new AdminShell.SemanticId(
                    AdminShell.Key.CreateNew(
                        type: "Submodel",
                        local: false,
                        idType: "IRI",
                        value: "http://www.admin-shell.io/mtp/v1/mtp-instance-submodel"));

                CD_MtpTypeSubmodel = CreateSparseConceptDescription("en", "IRI",
                    "MtpTypeSubmodel",
                    "http://www.admin-shell.io/mtp/v1/New/MtpTypeSubmodel",
                    @"Direct Reference to MTP Type Submodel.");

                CD_SourceList = CreateSparseConceptDescription("en", "IRI",
                    "SourceList",
                    "http://www.admin-shell.io/mtp/v1/MTPSUCLib/CommunicationSet/SourceList",
                    @"Collects source of process data for MTP.");

                CD_SourceOpcUaServer = CreateSparseConceptDescription("en", "IRI",
                    "SourceOpcUaServer",
                    "http://www.admin-shell.io/mtp/v1/MTPCommunicationSUCLib/ServerAssembly/OPCUAServer",
                    "Holds infomation on a single data source, which is an OPC UA server.");

                CD_Endpoint = CreateSparseConceptDescription("en", "IRI",
                    "Endpoint",
                    "http://www.admin-shell.io/mtp/v1/MTPCommunicationSUCLib/ServerAssembly/OPCUAServer/Endpoint",
                    "URL of OPC UA server for data source.");

                CD_MtpFile = CreateSparseConceptDescription("en", "IRI",
                    "MtpFile",
                    "http://www.admin-shell.io/mtp/v1/MTPSUCLib/ModuleTypePackage",
                    "Specifies a File, which contains MTP information in MTP/ AutmationML format." +
                    "File may be zipped.");

            }
        }
    }
}
