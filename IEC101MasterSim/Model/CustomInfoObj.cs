using System;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper.Configuration;
using lib60870.CS101;

namespace IEC101MasterSim.Model;

//Custom InfoObj
public abstract class CustomInfoObj
{
    public string Type { get; set; }
    public DateTime PCTime { get; set; }
    public int ObjectAddress { get; set; }
    public string ObjectName { get; set; }
    public string TimeStamp { get; set; }
    public bool Value { get; set; }
    public bool IsHighlighted { get; set; }
    public CauseOfTransmission Cot { get; set; }
    protected CustomInfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName)
    {
        ObjectName = objectName;
        ObjectAddress = informationObject.ObjectAddress;
        PCTime = DateTime.Now;
        TimeStamp = "Not Supported";
        IsHighlighted = false;
        Cot = cot;
    }

    public static string GetInfoObjName(InformationObject infoObject, Dictionary<int, string> addressNamesDictionary)
    {
        if (addressNamesDictionary == null)
            return "No ANs Loaded";
        else if (addressNamesDictionary.ContainsKey(infoObject.ObjectAddress))
            return addressNamesDictionary[infoObject.ObjectAddress];
        else
            return "Not in The List";
    }

    public static CustomInfoObj CustomInfoObjFactory(
        InformationObject infoObj,
        CauseOfTransmission cot,
        FiltredTypes FiltredTypes,
        string objName
        ) => infoObj.Type switch
        {
            TypeID.M_SP_NA_1 when !FiltredTypes.SPI => new SpInfoObj(infoObj, cot, objName),
            TypeID.M_SP_TA_1 when !FiltredTypes.SPI => new SpTs24InfoObj(infoObj, cot, objName),
            TypeID.M_SP_TB_1 when !FiltredTypes.SPI => new SpTs56InfoObj(infoObj, cot, objName),

            TypeID.M_DP_NA_1 when !FiltredTypes.DPI => new DpInfoObj(infoObj, cot, objName),
            TypeID.M_DP_TA_1 when !FiltredTypes.DPI => new DpTs24InfoObj(infoObj, cot, objName),
            TypeID.M_DP_TB_1 when !FiltredTypes.DPI => new DpTs56InfoObj(infoObj, cot, objName),

            TypeID.M_ME_NA_1 when !FiltredTypes.MVN => new MvnInfoObj(infoObj, cot, objName),
            TypeID.M_ME_TA_1 when !FiltredTypes.MVN => new MvnTs24InfoObj(infoObj, cot, objName),
            TypeID.M_ME_TD_1 when !FiltredTypes.MVN => new MvnTs56InfoObj(infoObj, cot, objName),

            TypeID.M_ME_NC_1 when !FiltredTypes.MVS => new MvsInfoObj(infoObj, cot, objName),
            TypeID.M_ME_TC_1 when !FiltredTypes.MVS => new MvsTs24InfoObj(infoObj, cot, objName),
            TypeID.M_ME_TF_1 when !FiltredTypes.MVS => new MvsTs56InfoObj(infoObj, cot, objName),

            TypeID.M_IT_NA_1 when !FiltredTypes.ITI => new ItInfoObj(infoObj, cot, objName),
            TypeID.M_IT_TA_1 when !FiltredTypes.ITI => new ItTs24InfoObj(infoObj, cot, objName),
            TypeID.M_IT_TB_1 when !FiltredTypes.ITI => new ItTs56InfoObj(infoObj, cot, objName),

            TypeID.C_SC_NA_1 when !FiltredTypes.SCO => new ScoInfoObj(infoObj, cot, objName),
            TypeID.C_SC_TA_1 when !FiltredTypes.SCO => new ScoTs56InfoObj(infoObj, cot, objName),

            TypeID.C_DC_NA_1 when !FiltredTypes.DCO => new DcoInfoObj(infoObj, cot, objName),
            TypeID.C_DC_TA_1 when !FiltredTypes.DCO => new DcoTs56InfoObj(infoObj, cot, objName),

            _ => null
        };
}

#region SPI
public class SpInfoObj : CustomInfoObj
{
    public SpInfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var spInfoObj = informationObject as SinglePointInformation;

        Type = "SPI";
        Value = spInfoObj.Value;
    }
}

public class SpTs24InfoObj : CustomInfoObj
{
    public SpTs24InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var spTs24InfoObj = informationObject as SinglePointWithCP24Time2a;
        var timeStamp = TimeSpan.FromMilliseconds(spTs24InfoObj.Timestamp.GetMilliseconds());

        Type = "SPI_TS_24";
        Value = spTs24InfoObj.Value;
        TimeStamp = timeStamp.ToString(@"hh\:mm\:ss\:fff", CultureInfo.CurrentCulture);
    }
}

public class SpTs56InfoObj : CustomInfoObj
{
    public new DateTime TimeStamp { get; set; }

    public SpTs56InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var spTs56InfoObj = informationObject as SinglePointWithCP56Time2a;

        Type = "SPI_TS_56";
        Value = spTs56InfoObj.Value;
        TimeStamp = spTs56InfoObj.Timestamp.GetDateTime();
    }
}
#endregion

#region DPI
public class DpInfoObj : CustomInfoObj
{
    public new DoublePointValue Value { get; set; }

    public DpInfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var dpInfoObj = informationObject as DoublePointInformation;

        Type = "DPI";
        Value = dpInfoObj.Value;
        TimeStamp = "Not Supported";
    }
}

public class DpTs24InfoObj : CustomInfoObj
{
    public new DoublePointValue Value { get; set; }

    public DpTs24InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var dpTs24InfoObj = informationObject as DoublePointWithCP24Time2a;
        var timeStamp = TimeSpan.FromMilliseconds(dpTs24InfoObj.Timestamp.GetMilliseconds());

        Type = "DPI_TS_24";
        Value = dpTs24InfoObj.Value;
        TimeStamp = timeStamp.ToString(@"hh\:mm\:ss\:fff", CultureInfo.CurrentCulture);
    }
}

public class DpTs56InfoObj : CustomInfoObj
{
    public new DoublePointValue Value { get; set; }
    public new DateTime TimeStamp { get; set; }

    public DpTs56InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var dpTs56InfoObj = informationObject as DoublePointWithCP56Time2a;

        Type = "DPI_TS_56";
        Value = dpTs56InfoObj.Value;
        TimeStamp = dpTs56InfoObj.Timestamp.GetDateTime();
    }
}
#endregion

#region MVS
public class MvsInfoObj : CustomInfoObj
{
    public new float Value { get; set; }

    public MvsInfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var mvsInfoObj = informationObject as MeasuredValueShort;

        Type = "MVS";
        Value = mvsInfoObj.Value;
    }
}

public class MvsTs24InfoObj : CustomInfoObj
{
    public new float Value { get; set; }

    public MvsTs24InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var mvsTs24InfoObj = informationObject as MeasuredValueShortWithCP24Time2a;
        var timeStamp = TimeSpan.FromMilliseconds(mvsTs24InfoObj.Timestamp.GetMilliseconds());

        Type = "MVS_TS_24";
        Value = mvsTs24InfoObj.Value;
        TimeStamp = timeStamp.ToString(@"hh\:mm\:ss\:fff", CultureInfo.CurrentCulture);
    }
}

public class MvsTs56InfoObj : CustomInfoObj
{
    public new float Value { get; set; }
    public new DateTime TimeStamp { get; set; }

    public MvsTs56InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var mvsTs56InfoObj = informationObject as MeasuredValueShortWithCP56Time2a;

        Type = "MVS_TS_56";
        Value = mvsTs56InfoObj.Value;
        TimeStamp = mvsTs56InfoObj.Timestamp.GetDateTime();
    }
}
#endregion

#region MVN
public class MvnInfoObj : CustomInfoObj
{
    public new float Value { get; set; }

    public MvnInfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var mvnInfoObj = informationObject as MeasuredValueNormalized;

        Type = "MVN";
        Value = mvnInfoObj.NormalizedValue;
    }
}

public class MvnTs24InfoObj : CustomInfoObj
{
    public new float Value { get; set; }

    public MvnTs24InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var mvnTs24InfoObj = informationObject as MeasuredValueNormalizedWithCP24Time2a;
        var timeStamp = TimeSpan.FromMilliseconds(mvnTs24InfoObj.Timestamp.GetMilliseconds());

        Type = "MVN_TS_24";
        Value = mvnTs24InfoObj.NormalizedValue;
        TimeStamp = timeStamp.ToString(@"hh\:mm\:ss\:fff", CultureInfo.CurrentCulture);
    }
}

public class MvnTs56InfoObj : CustomInfoObj
{
    public new float Value { get; set; }
    public new DateTime TimeStamp { get; set; }

    public MvnTs56InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var mvnTs56InfoObj = informationObject as MeasuredValueNormalizedWithCP56Time2a;

        Type = "MVN_TS_56";
        Value = mvnTs56InfoObj.NormalizedValue;
        TimeStamp = mvnTs56InfoObj.Timestamp.GetDateTime();
    }
}
#endregion

#region SCO
public class ScoInfoObj : CustomInfoObj
{
    public new bool Value { get; set; }

    public ScoInfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var scoInfoObj = informationObject as SingleCommand;

        Type = "SCO";
        Value = scoInfoObj.State;
    }
}

public class ScoTs56InfoObj : CustomInfoObj
{
    public new bool Value { get; set; }
    public new DateTime TimeStamp { get; set; }

    public ScoTs56InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var ScoTs56InfoObj = informationObject as SingleCommandWithCP56Time2a;

        Type = "SCO_TS_56";
        Value = ScoTs56InfoObj.State;
        TimeStamp = ScoTs56InfoObj.Timestamp.GetDateTime();
    }
}
#endregion

#region DCO
public class DcoInfoObj : CustomInfoObj
{
    public new int Value { get; set; }

    public DcoInfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var dcoInfoObj = informationObject as DoubleCommand;

        Type = "DCO";
        Value = dcoInfoObj.State;
    }
}

public class DcoTs56InfoObj : CustomInfoObj
{
    public new int Value { get; set; }
    public new DateTime TimeStamp { get; set; }

    public DcoTs56InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var dcoTs56InfoObj = informationObject as DoubleCommandWithCP56Time2a;

        Type = "DCO_TS_56";
        Value = dcoTs56InfoObj.State;
        TimeStamp = dcoTs56InfoObj.Timestamp.GetDateTime();
    }
}
#endregion

#region ITI
public class ItInfoObj : CustomInfoObj
{
    public new BinaryCounterReading Value { get; set; }
    public ItInfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var itInfoObj = informationObject as IntegratedTotals;

        Type = "ITI";
        Value = itInfoObj.BCR;
    }
}

public class ItTs24InfoObj : CustomInfoObj
{
    public new BinaryCounterReading Value { get; set; }

    public ItTs24InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var itTs24InfoObj = informationObject as IntegratedTotalsWithCP24Time2a;
        var timeStamp = TimeSpan.FromMilliseconds(itTs24InfoObj.Timestamp.GetMilliseconds());

        Type = "ITI_TS_24";
        Value = itTs24InfoObj.BCR;
        TimeStamp = timeStamp.ToString(@"hh\:mm\:ss\:fff", CultureInfo.CurrentCulture);
    }
}

public class ItTs56InfoObj : CustomInfoObj
{
    public new DateTime TimeStamp { get; set; }
    public new BinaryCounterReading Value { get; set; }

    public ItTs56InfoObj(InformationObject informationObject, CauseOfTransmission cot, string objectName) : base(informationObject, cot, objectName)
    {
        var itTs56InfoObj = informationObject as IntegratedTotalsWithCP56Time2a;

        Type = "ITI_TS_56";
        Value = itTs56InfoObj.BCR;
        TimeStamp = itTs56InfoObj.Timestamp.GetDateTime();
    }
}
