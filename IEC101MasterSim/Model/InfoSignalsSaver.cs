using System.Collections;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace IEC101MasterSim.Model
{
    public static class InfoSignalsSaver
    {
        #region SPI
        public sealed class SpInfoObjMap : ClassMap<SpInfoObj>
        {
            public SpInfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class SpTs24InfoObjMap : ClassMap<SpTs24InfoObj>
        {
            public SpTs24InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class SpTs56InfoObjMap : ClassMap<SpTs56InfoObj>
        {
            public SpTs56InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }
        #endregion

        #region DPI
        public sealed class DpInfoObjMap : ClassMap<DpInfoObj>
        {
            public DpInfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class DpTs24InfoObjMap : ClassMap<DpTs24InfoObj>
        {
            public DpTs24InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class DpTs56InfoObjMap : ClassMap<DpTs56InfoObj>
        {
            public DpTs56InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }
        #endregion

        #region MVN
        public sealed class MvnInfoObjMap : ClassMap<MvnInfoObj>
        {
            public MvnInfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class MvnTs24InfoObjMap : ClassMap<MvnTs24InfoObj>
        {
            public MvnTs24InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class MvnTs56InfoObjMap : ClassMap<MvnTs56InfoObj>
        {
            public MvnTs56InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }
        #endregion

        #region MVS
        public sealed class MvsInfoObjMap : ClassMap<MvsInfoObj>
        {
            public MvsInfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }
        public sealed class MvsTs24InfoObjMap : ClassMap<MvsTs24InfoObj>
        {
            public MvsTs24InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class MvsTs56InfoObjMap : ClassMap<MvsTs56InfoObj>
        {
            public MvsTs56InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }
        #endregion

        #region ITI
        public sealed class ItInfoObjMap : ClassMap<ItInfoObj>
        {
            public ItInfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class ItTs24InfoObjMap : ClassMap<ItTs24InfoObj>
        {
            public ItTs24InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class ItTs56InfoObjMap : ClassMap<ItTs56InfoObj>
        {
            public ItTs56InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }
        #endregion    

        #region SCO
        public sealed class ScoInfoObjMap : ClassMap<ScoInfoObj>
        {
            public ScoInfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class ScoTs56InfoObjMap : ClassMap<ScoTs56InfoObj>
        {
            public ScoTs56InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }
        #endregion

        #region DCO
        public sealed class DcoInfoObjMap : ClassMap<DcoInfoObj>
        {
            public DcoInfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }

        public sealed class DcoTs56InfoObjMap : ClassMap<DcoTs56InfoObj>
        {
            public DcoTs56InfoObjMap()
            {
                _ = Map(m => m.Type).Name("Type");
                _ = Map(m => m.Cot).Name("Cause of Transmission");
                _ = Map(m => m.Value).Name("Value");
                _ = Map(m => m.ObjectAddress).Name("Address");
                _ = Map(m => m.ObjectName).Name("Name");
                _ = Map(m => m.PCTime).Name("PC Time");
                _ = Map(m => m.TimeStamp).Name("Time Stamp");
            }
        }
        #endregion

        public static void SaveFile(string path, IList infoSignals)
        {
            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            writer.WriteLine("Type, Cause of Transmission, Value, Address, Name, PC Time, TimeStamp");

            #region Register Class Maps
            _ = csv.Context.RegisterClassMap<SpInfoObjMap>();
            _ = csv.Context.RegisterClassMap<SpTs24InfoObjMap>();
            _ = csv.Context.RegisterClassMap<SpTs56InfoObjMap>();

            _ = csv.Context.RegisterClassMap<DpInfoObjMap>();
            _ = csv.Context.RegisterClassMap<DpTs24InfoObjMap>();
            _ = csv.Context.RegisterClassMap<DpTs56InfoObjMap>();

            _ = csv.Context.RegisterClassMap<MvnInfoObjMap>();
            _ = csv.Context.RegisterClassMap<MvnTs24InfoObjMap>();
            _ = csv.Context.RegisterClassMap<MvnTs56InfoObjMap>();

            _ = csv.Context.RegisterClassMap<MvsInfoObjMap>();
            _ = csv.Context.RegisterClassMap<MvsTs24InfoObjMap>();
            _ = csv.Context.RegisterClassMap<MvsTs56InfoObjMap>();

            _ = csv.Context.RegisterClassMap<ItInfoObjMap>();
            _ = csv.Context.RegisterClassMap<ItTs24InfoObjMap>();
            _ = csv.Context.RegisterClassMap<ItTs56InfoObjMap>();

            _ = csv.Context.RegisterClassMap<DcoInfoObjMap>();
            _ = csv.Context.RegisterClassMap<DcoTs56InfoObjMap>();

            _ = csv.Context.RegisterClassMap<ScoInfoObjMap>();
            _ = csv.Context.RegisterClassMap<ScoTs56InfoObjMap>();
            #endregion

            foreach (CustomInfoObj infoSignal in infoSignals)
            {
                var type = infoSignal.GetType().Name;
                switch (type)
                {
                    #region SPI
                    case nameof(SpInfoObj):
                        {
                            csv.WriteRecord(infoSignal as SpInfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(SpTs24InfoObj):
                        {
                            csv.WriteRecord(infoSignal as SpTs24InfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(SpTs56InfoObj):
                        {
                            csv.WriteRecord(infoSignal as SpTs56InfoObj);
                            csv.NextRecord();
                        }; break;
                    #endregion

                    #region DPI
                    case nameof(DpInfoObj):
                        {
                            csv.WriteRecord(infoSignal as DpInfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(DpTs24InfoObj):
                        {
                            csv.WriteRecord(infoSignal as DpTs24InfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(DpTs56InfoObj):
                        {
                            csv.WriteRecord(infoSignal as DpTs56InfoObj);
                            csv.NextRecord();
                        }; break;
                    #endregion

                    #region MVN
                    case nameof(MvnInfoObj):
                        {
                            csv.WriteRecord(infoSignal as MvnInfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(MvnTs24InfoObj):
                        {
                            csv.WriteRecord(infoSignal as MvnTs24InfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(MvnTs56InfoObj):
                        {
                            csv.WriteRecord(infoSignal as MvnTs56InfoObj);
                            csv.NextRecord();
                        }; break;
                    #endregion

                    #region MVS
                    case nameof(MvsInfoObj):
                        {
                            csv.WriteRecord(infoSignal as MvsInfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(MvsTs24InfoObj):
                        {
                            csv.WriteRecord(infoSignal as MvsTs24InfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(MvsTs56InfoObj):
                        {
                            csv.WriteRecord(infoSignal as MvsTs56InfoObj);
                            csv.NextRecord();
                        }; break;
                    #endregion

                    #region ITI
                    case nameof(ItInfoObj):
                        {
                            csv.WriteRecord(infoSignal as ItInfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(ItTs24InfoObj):
                        {
                            csv.WriteRecord(infoSignal as ItTs24InfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(ItTs56InfoObj):
                        {
                            csv.WriteRecord(infoSignal as ItTs56InfoObj);
                            csv.NextRecord();
                        }; break;
                    #endregion

                    #region SCO
                    case nameof(ScoInfoObj):
                        {
                            csv.WriteRecord(infoSignal as ScoInfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(ScoTs56InfoObj):
                        {
                            csv.WriteRecord(infoSignal as ScoTs56InfoObj);
                            csv.NextRecord();
                        }; break;
                    #endregion

                    #region DCO
                    case nameof(DcoInfoObj):
                        {
                            csv.WriteRecord(infoSignal as DcoInfoObj);
                            csv.NextRecord();
                        }; break;
                    case nameof(DcoTs56InfoObj):
                        {
                            csv.WriteRecord(infoSignal as DcoTs56InfoObj);
                            csv.NextRecord();
                        }; break;
                    #endregion

                    default:
                        break;
                }
            }
        }
    }
}
