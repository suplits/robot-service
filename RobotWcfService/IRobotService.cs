﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Slb.InversionOptimization.RobotLibary;

namespace Slb.InversionOptimization.RobotWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRobotService" in both code and config file together.
    [ServiceContract]
    public interface IRobotService
    {

        [OperationContract(Action = "UploadFile", IsOneWay = true)]
        void UploadFile(FileUploadMessage request);

        /// <summary>
        /// Initialize inversion, send settings to Robot
        /// </summary>
        /// <param name="wellID"></param>
        /// <param name="OwnerID"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        [OperationContract]
        bool InitInversion(Guid wellID, Guid inversionID, Guid ownerID);

        /// <summary>
        /// Start an inversion
        /// </summary>
        /// <param name="inversionID"></param>
        /// <returns></returns>
        [OperationContract]
        bool StartInversion(Guid ownerID, Guid inversionID);

        /// <summary>
        /// Stop  an inversion
        /// </summary>
        /// <param name="inversionID"></param>
        /// <returns></returns>
        [OperationContract]
        bool StopInversion(Guid ownerID, Guid inversionID);

        /// <summary>
        /// Query and get all inversions for a well
        /// </summary>
        /// <param name="wellID"></param>
        /// <returns>Dictionary with OwnerID, Inversion pair</returns>
        [OperationContract]
        IDictionary<Guid, IInversion> QueryInversion(Guid wellID);

        /// <summary>
        /// Retrieve an inversion result which includes both Input and Output files
        /// </summary>
        /// <param name="inversionID"></param>
        /// <param name="accessCode"></param>
        /// <returns></returns>
        [OperationContract]
        bool RetrieveInversion(Guid inversionID, string accessCode);

    }


    [MessageContract]
    public class FileUploadMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public string SavePath;

        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageBodyMember(Order = 1)]
        public Stream FileData;

    }


}
