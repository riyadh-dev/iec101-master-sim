using lib60870.CS101;

namespace IEC101MasterSim.Model
{
    public class IEC101Command
    {
        public IEC101CommandValue Value { get; set; }
        public IEC101CommandType Type { get; set; }
        public int ObjectAddress { get; set; }

        private CS101Master _master;

        public void AddMaster(CS101Master master) => _master = master;

        public void SendSingleCommand()
        {
            var command = Value switch
            {
                IEC101CommandValue.ON => true,
                IEC101CommandValue.OFF => false,
                _ => throw new System.NotImplementedException(),
            };

            _master.SendControlCommand(
                CauseOfTransmission.ACTIVATION,
                _master.SlaveAddress,
                new SingleCommand(ObjectAddress, command, false, 0)
                );
        }

        public void SendDoubleCommand()
        {
            var command = Value switch
            {
                IEC101CommandValue.ON => DoubleCommand.ON,
                IEC101CommandValue.OFF => DoubleCommand.OFF,
                _ => throw new System.NotImplementedException(),
            };

            _master.SendControlCommand(
                CauseOfTransmission.ACTIVATION,
                _master.SlaveAddress,
                new DoubleCommand(ObjectAddress, command, false, 0)
                );
        }

        public void SendCommand(object _)
        {
            if (Type == IEC101CommandType.SCO)
                SendSingleCommand();
            else
                SendDoubleCommand();
        }
    }

    public enum IEC101CommandType
    {
        SCO,
        DCO
    }

    public enum IEC101CommandValue
    {
        ON,
        OFF
    }
}
