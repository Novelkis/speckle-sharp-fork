﻿//----------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by https://github.com/StefH/ProxyInterfaceSourceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//----------------------------------------------------------------------------------------

#nullable enable
using System;

namespace Speckle.Revit2023.Api
{
    public partial interface IRevitDocument : global::System.IDisposable
    {
        global::Autodesk.Revit.DB.Document _Instance { get; }

        event global::System.EventHandler<global::Autodesk.Revit.DB.Events.DocumentSavingEventArgs> DocumentSaving;

        event global::System.EventHandler<global::Autodesk.Revit.DB.Events.DocumentSavedEventArgs> DocumentSaved;

        event global::System.EventHandler<global::Autodesk.Revit.DB.Events.DocumentSavingAsEventArgs> DocumentSavingAs;

        event global::System.EventHandler<global::Autodesk.Revit.DB.Events.DocumentSavedAsEventArgs> DocumentSavedAs;

        event global::System.EventHandler<global::Autodesk.Revit.DB.Events.DocumentPrintingEventArgs> DocumentPrinting;

        event global::System.EventHandler<global::Autodesk.Revit.DB.Events.DocumentPrintedEventArgs> DocumentPrinted;

        event global::System.EventHandler<global::Autodesk.Revit.DB.Events.ViewPrintingEventArgs> ViewPrinting;

        event global::System.EventHandler<global::Autodesk.Revit.DB.Events.ViewPrintedEventArgs> ViewPrinted;

        event global::System.EventHandler<global::Autodesk.Revit.DB.Events.DocumentClosingEventArgs> DocumentClosing;

        bool IsValidObject { get; }

        global::System.Guid WorksharingCentralGUID { get; }

        bool IsModelInCloud { get; }

        bool IsDetached { get; }

        bool IsWorkshared { get; }

        bool IsLinked { get; }

        bool IsReadOnlyFile { get; }

        bool IsReadOnly { get; }

        bool IsModified { get; }

        bool IsModifiable { get; }

        string Title { get; }

        string PathName { get; }

        global::Autodesk.Revit.ApplicationServices.Application Application { get; }

        bool IsFamilyDocument { get; }

        global::Autodesk.Revit.Creation.FamilyItemFactory FamilyCreate { get; }

        global::Autodesk.Revit.Creation.Document Create { get; }

        bool ReactionsAreUpToDate { get; }

        bool LoadFamily(string filename);

        bool LoadFamilySymbol(string filename, string name);

        bool Close();

        bool Close(bool saveModified);

        global::System.Collections.Generic.ICollection<global::Autodesk.Revit.DB.ElementId> GetPrintSettingIds();

        void Regenerate();

        void AutoJoinElements();

        bool CanEnableCloudWorksharing();

        bool CanEnableWorksharing();

        void EnableWorksharing(string worksetNameGridLevel, string worksetName);

        void Save();

        void SaveAs(string filepath);

        void SaveCloudModel();

        void SaveAsCloudModel(global::System.Guid accountId, global::System.Guid projectId, string folderId, string modelName);

        void EnableCloudWorksharing();

        global::System.Collections.Generic.IList<global::Autodesk.Revit.DB.FailureMessage> GetWarnings();

        string GetCloudModelUrn();

        string GetHubId();

        string GetProjectId();

        string GetCloudFolderId(bool forceRefresh);

        bool HasAllChangesFromCentral();

        global::System.Collections.Generic.ICollection<global::Autodesk.Revit.DB.ElementId> Delete(global::System.Collections.Generic.ICollection<global::Autodesk.Revit.DB.ElementId> elementIds);

        void ResetSharedCoordinates();

        bool IsBackgroundCalculationInProgress();
    }
}
#nullable restore