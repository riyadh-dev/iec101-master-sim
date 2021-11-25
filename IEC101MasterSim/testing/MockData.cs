using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IEC101MasterSim.Model;
using lib60870.CS101;

namespace IEC101MasterSim.testing;

public static class MockData
{
    public static ObservableCollection<CustomInfoObj> CreateSignals(int loops)
    {
        ObservableCollection<CustomInfoObj> informationObjects = new();
        for (int i = 0; i < loops; i++)
        {
            SpInfoObj spInfo = new(
                new SinglePointInformation(100 + i, true, new()),
                lib60870.CS101.CauseOfTransmission.SPONTANEOUS,
                "testing Spi");

            SpTs24InfoObj spTs24 = new(
                new SinglePointWithCP24Time2a(100 + i, false, new(), new()),
                lib60870.CS101.CauseOfTransmission.SPONTANEOUS,
                "testing Spi");

            SpTs56InfoObj spTs56 = new(
                new SinglePointWithCP56Time2a(100 + i, true, new(), new(DateTime.Now)),
                lib60870.CS101.CauseOfTransmission.SPONTANEOUS,
                "testing Spi");

            DpTs56InfoObj dpTs56 = new(
                new DoublePointWithCP56Time2a(50 + i, DoublePointValue.INTERMEDIATE, new(), new()),
                CauseOfTransmission.ACTIVATION,
                "testing dpi");


            informationObjects.Add(spInfo);
            informationObjects.Add(spTs24);
            informationObjects.Add(spTs56);
            informationObjects.Add(dpTs56);
        }

        return informationObjects;
    }

    public static CustomInfoObj ReturnRandSignal()
    {
        List<CustomInfoObj> informationObjects = new()
        {
            new SpInfoObj(
                new SinglePointInformation(25, true, new()),
                lib60870.CS101.CauseOfTransmission.SPONTANEOUS,
                "testing Spi"),

            new SpTs24InfoObj(
                new SinglePointWithCP24Time2a(30, false, new(), new()),
                lib60870.CS101.CauseOfTransmission.SPONTANEOUS,
                "testing Spi"),

            new SpTs56InfoObj(
                new SinglePointWithCP56Time2a(50, true, new(), new(DateTime.Now)),
                lib60870.CS101.CauseOfTransmission.SPONTANEOUS,
                "testing Spi"),

            new DpTs56InfoObj(
                new DoublePointWithCP56Time2a(15, DoublePointValue.INTERMEDIATE, new(), new()),
                CauseOfTransmission.ACTIVATION,
                "testing dpi"),
        };

        Random rnd = new();

        return informationObjects[rnd.Next(0, informationObjects.Count)];
    }
}
