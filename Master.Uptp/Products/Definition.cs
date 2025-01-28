using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OneDriver.Master.Uptp.Products
{
    public class Definition
    {

        #region e_activeprot enum

        public enum e_activeprot
        {
            /// <summary>active protocol 1</summary>
            ACTIVE_PROTOCOL_1 = 0,

            /// <summary>active protocol 2</summary>
            ACTIVE_PROTOCOL_2 = 1,

            /// <summary>active protocol 3</summary>
            ACTIVE_PROTOCOL_3 = 2,

            /// <summary>active protocol 4</summary>
            ACTIVE_PROTOCOL_4 = 3
        }

        #endregion

        #region e_baudrates enum

        public enum e_baudrates
        {
            ///<summary>baudrate for communication with UPT is 1200Baud</summary>
            BAUDRATE_1200 = 1,

            ///<summary>baudrate for communication with UPT is 2400Baud</summary>
            BAUDRATE_2400 = 2,

            ///<summary>baudrate for communication with UPT is 4800Baud</summary>
            BAUDRATE_4800 = 3,

            ///<summary>baudrate for communication with UPT is 9600Baud</summary>
            BAUDRATE_9600 = 4,

            ///<summary>baudrate for communication with UPT is 19200Baud</summary>
            BAUDRATE_19200 = 5,

            ///<summary>baudrate for communication with UPT is 38400Baud</summary>
            BAUDRATE_38400 = 6,

            ///<summary>baudrate for communication with UPT is 57600Baud</summary>
            BAUDRATE_57600 = 7,

            ///<summary>baudrate for communication with UPT is 115200Baud</summary>
            BAUDRATE_115200 = 8,

            ///<summary>baudrate for communication with UPT is 230400Baud</summary>
            BAUDRATE_230400 = 9,

            ///<summary>baudrate for communication with UPT is 256000Baud</summary>
            BAUDRATE_256000 = 10
        }

        #endregion

        #region e_com enum

        //! state of connection
        public enum e_com
        {
            /// <summary>
            ///     no communication
            /// </summary>
            COM_OFFLINE = 0,

            /// <summary>
            ///     auto connect, if sensor is connected
            /// </summary>
            COM_AUTOCONNECT = 1,

            /// <summary>
            ///     communication ready, slave connection sequence ok
            /// </summary>
            COM_ONLINE = 2,

            /// <summary>
            ///     upt tries to connect to slave
            /// </summary>
            COM_CONNECT = 3,

            /// <summary>
            ///     communication error
            /// </summary>
            COM_ERROR = 4,

            /// <summary>
            ///     discard connection with slave without resetting slave
            /// </summary>
            COM_DISCARD = 5,

            /// <summary>
            ///     COM_CONNECT with additional wait time
            /// </summary>
            COM_CONNECT_WITH_WAIT = 6
        }

        #endregion

        #region e_com_state enum

        public enum e_com_state
        {
            /// <summary>master connects to upt</summary>
            CONNECT = 0,

            /// <summary>master disconnects from upt</summary>
            DISCONNECT = 1
        }

        #endregion

        #region e_connection_type enum

        /// <summary>
        ///     connection behaviour, if slave is plugged in
        /// </summary>
        public enum e_connection_type
        {
            ///<summary>do not connect, if slave is plugged in</summary>
            CONNECT_NOT = 0,

            ///<summary>connect immediately, even if no sensor detected (e.g. debug purpose)</summary>
            CONNECT_NOW = 1,

            ///<summary>try to connect to newly plugged in slave</summary>
            CONNECT_AUTO = 2,

            ///<summary>discard connection to sensor without resetting the sensor (do not switch power supply off)</summary>
            CONNECT_DISCARD = 3
        }

        #endregion

        #region e_error_codes enum

        public enum e_error_codes
        {
            ERR_NONE = ERR_CATEGORY_GENERAL | 0x00, //!< no error
            ERR_WRONG_CRC = ERR_CATEGORY_GENERAL | 0x04, //!< message with wrong crc received
            ERR_WRONG_LENGTH = ERR_CATEGORY_TX | 0x01, //!< message with wrong length received
            ERR_ID_NOT_AVAIL = ERR_CATEGORY_GENERAL | 0x01, //!< ID is not available (command RdID)
            ERR_STAT_NOT_AVAIL = ERR_CATEGORY_GENERAL | 0x05, //!< status not available (command RdStat)
            ERR_VAL_NOT_AVAIL = ERR_CATEGORY_GENERAL | 0x06, //!< value not available
            ERR_CFG_NOT_AVAIL = ERR_CATEGORY_GENERAL | 0x07, //!< config parameter not available (RdCfg)
            ERR_WRONG_CHKSUM = ERR_CATEGORY_TX | 0x02, //!< message with wrong checksum received (sspp)
            ERR_PAR_RD_ONLY = ERR_CATEGORY_PAR | 0x02, //!< parameter is read only
            ERR_PAR_NOT_EXIST = ERR_CATEGORY_PAR | 0x01, //!< parameter does not exist
            ERR_PAR_NOT_RDY = ERR_CATEGORY_PAR | 0x05, //!< parameter is not ready
            ERR_CMD_NOT_RDY = ERR_CATEGORY_CMD | 0x02, //!< command is not ready
            ERR_CMD_NOT_EXIST = ERR_CATEGORY_CMD | 0x01, //!< command does not exist
            ERR_PAR_VAL_ERR = ERR_CATEGORY_PAR | 0x03, //!< parameter value error
            ERR_PAR_FORMAT_ERR = ERR_CATEGORY_PAR | 0x04, //!< parameter value error
            ERR_BUF_NOT_EXIST = ERR_CATEGORY_GENERAL | 0x08, //!< buffer does not exist
            ERR_BUF_EMPTY = ERR_CATEGORY_GENERAL | 0x09, //!< buffer is empty
            ERR_BUF_NOT_RDY = ERR_CATEGORY_GENERAL | 0x0A, //!< buffer is not ready
            ERR_FWUPD_ERROR = ERR_CATEGORY_FWUPD | 0x03, //!< firmware update error
            ERR_UART_TIMEOUT = ERR_CATEGORY_TX | 0x03, //!< UART timeout
            ERR_UART_FRAME = ERR_CATEGORY_TX | 0x04, //!< UART frame error
            ERR_UART_PARITY = ERR_CATEGORY_TX | 0x05, //!< UART parity error

            ERR_INVALID_FRAME =
                ERR_CATEGORY_TX | 0x06, //!< invalid frame (UART error / wrong length / wrong checksum or crc)
            ERR_REQ_NOT_EXIST = ERR_CATEGORY_GENERAL | 0x02, //!< request index is invalid

            ERR_SLAVE_NOT_CONNECTED =
                ERR_CATEGORY_GENERAL | 0x0B, //!< sending to slave is not possible, because slave is not connected

            ERR_UPT_NOT_CONNECTED =
                ERR_CATEGORY_GENERAL | 0x0C, //!< no response to UPTP_Idle command from upt while calling UPTP_Link()
            ERR_UNDEF = ERR_CATEGORY_GENERAL | 0x03, //!< undefined error
            ERR_INDEX_NAME_NOT_FOUND = ERR_CATEGORY_USER | 0x01,

            RET_OK = 0x00000000, // OK
            RET_TOGGLE_MISMATCH = 0x05030000, // Toggle bit not alternated
            RET_SDO_TIMEOUT = 0x05040000, // SDO Protocol timed out
            RET_CS_NOT_VALID = 0x05040001, // command specifier not valid or unknown
            RET_SDO_WRONG_BLOCKSIZE = 0x05040002, // Wrong Blocksize
            RET_SDO_WRONG_SEQ_NR = 0x05040003, // Wrong Sequence Number
            RET_SDO_CRC_ERROR = 0x05040004, // CRC Error
            RET_UNSUPPORTED_ACCESS = 0x05040005, // Unsupported access to an object
            RET_UNSUPPORTED_ACCESS2 = 0x06010000, // Unsupported access to an object
            RET_READ_NOT_ALLOWED = 0x06010001, // Attempt to read a write only object
            RET_WRITE_NOT_ALLOWED = 0x06010002, // Attempt to write a read only object
            RET_OBJECT_DOESNT_EXIST = 0x06020000, // Object does not exist
            RET_OBJECT_CANT_MAPPED_PDO = 0x06040041, // Object cannot be mapped to the PDO

            RET_PDO_LENGTH_EXCEEDED =
                0x06040042, // The number and length of the objects to be mapped would exceed PDO length
            RET_PARAM_INCOMPATIBILITY = 0x06040043, // General parameter incompatibility reason
            RET_HARDWARE_ERROR = 0x06060000, // Access failed due to an hardware error
            RET_TYPE_MISMATCH_HIGH = 0x06070012, // Data type does not match: length too high
            RET_TYPE_MISMATCH_LOW = 0x06070013, // Data type does not match: length too low
            RET_DATA_TYPE_MISMATCH = 0x06070010, // Data type does not match
            RET_SUBINDEX_DOESNT_EXIST = 0x06090011, // sub index does not exist
            RET_SDO_INVALID_VALUE = 0x06090030, // Invalid value for parameter (download only)
            RET_PARAM_VALUE_HIGH = 0x06090031, // value of parameter too high
            RET_PARAM_VALUE_LOW = 0x06090033, // value of parameter too low
            RET_MAX_VALUE_LESS_MIN = 0x06090036, // maximum value is less than minimum value
            RET_EDO_RESOURCE_NOT_AVAILABLE = 0x060A0023, // resource not available: SDO connection
            RET_GENERAL_ERROR = 0x08000000, // General error
            RET_DATA_TRANSFER_ERROR = 0x08000020, // data cannot be transferred or stored to the application

            RET_DATA_TRANSFER_ERROR_LOCAL =
                0x08000021, // Data cannot be transferred or stored to the application because of local control

            RET_DATA_TRANSFER_ERROR_STATE =
                0x08000022, // Data cannot be transferred or stored to the application because of present device state

            RET_DICTIONARY_GENERATION =
                0x08000023, // Object dictionary dynamic generation fails or no object dictionary is present
            RET_NO_DATA = 0x08000024, // No data available

            // The following errors are generated by P+F
            RET_TRANSFER_NOT_FINISHED = 0x0f0f0f00, // Shouldn't happen - last transfer wasn't finished
            RET_TRANSFER_NOT_CONNECTED = 0x0f0f0f01, // Shouldn't happen - Device is not connected

            RET_TRANSFER_CONVERSION_ERROR =
                0x0f0f0f02 // This will happen if a double value cant be converted to a byte array
        }

        #endregion

        #region e_intf_sply_on_state enum

        public enum e_intf_sply_on_state
        {
            INTF_SUPPLY_OFF = 0,
            INTF_SUPPLY_ON = 1
        }

        #endregion

        #region e_slave_com_port enum

        /// <summary>
        ///     port of upt, where sensor is connected
        /// </summary>
        public enum e_slave_com_port
        {
            /// <summary>
            ///     sensor is connected to port 1 of upt
            /// </summary>
            COMMUNICATION_PORT_1 = 0,

            /// <summary>
            ///     sensor is connected to port 2 of upt
            /// </summary>
            COMMUNICATION_PORT_2 = 1,

            /// <summary>
            ///     use out1 and in2 for communication
            /// </summary>
            COMMUNICATION_PORT_OUT_1_IN_2 = 2,

            /// <summary>
            ///     use out2 and in1 for communication
            /// </summary>
            COMMUNICATION_PORT_OUT_2_IN_1 = 3,

            /// <summary>
            ///     use out3 and in1 for communication
            /// </summary>
            COMMUNICATION_PORT_OUT_3_IN_1 = 4,

            /// <summary>
            ///     use out3 and in2 for communication
            /// </summary>
            COMMUNICATION_PORT_OUT_3_IN_2 = 5
        }

        #endregion

        #region e_slave_output_type enum

        /// <summary>
        ///     output type of sensor
        /// </summary>
        public enum e_slave_output_type
        {
            /// <summary>output type of sensor is push pull</summary>
            OUTPUT_TYPE_PUSH_PULL = 0,

            /// <summary>output type of sensor is pnp</summary>
            OUTPUT_TYPE_PNP = 1,

            /// <summary>output type of sensor is npn</summary>
            OUTPUT_TYPE_NPN = 2,

            /// <summary>output type of sensor is pnp without pull-down</summary>
            OUTPUT_TYPE_PNP_NO_PD = 3,

            /// <summary>output type of sensor is npn without pull-up</summary>
            OUTPUT_TYPE_NPN_NO_PU = 4
        }

        #endregion

        #region e_sspp_cmds enum

        //! sspp commands
        public enum e_sspp_cmds
        {
            /// <summary>SSPP Idle command code</summary>
            SSPP_IDLE = 0x0,

            /// <summary>Reset sensor</summary>
            SSPP_RESET = 0x1,

            /// <summary>SSPP RdPar command code</summary>
            SSPP_RDPAR = 0x4,

            /// <summary>SSPP WrCmd command code</summary>
            SSPP_WRCMD = 0xA,

            /// <summary>SSPP WrPar command code</summary>
            SSPP_WRPAR = 0xC,

            /// <summary>SSPP WrFwUpd command code</summary>
            SSPP_WRFWUPD = 0xE,

            /// <summary>SSPP STARTFW command code</summary>
            SSPP_STARTFW = 0xF
        }

        #endregion

        #region e_sspp_flags enum

        /// <summary>
        ///     response flag
        /// </summary>
        public enum e_sspp_flags
        {
            /// <summary>positive response</summary>
            SSPP_POSITIVE = 0x0,

            /// <summary>negative response</summary>
            SSPP_NEGATIVE = 0x1
        }

        #endregion

        #region e_sspp_par_access enum

        /// <summary>
        ///     access method
        /// </summary>
        public enum e_sspp_par_access
        {
            /// <summary>parameter access via address</summary>
            SSPP_PAR_ADDRESS_ACCESS = 0x0,

            /// <summary>parameter access via index</summary>
            SSPP_PAR_INDEX_ACCESS = 0x1
        }

        #endregion

        #region e_uptp_req enum

        /// <summary>
        ///     uptp request codes
        /// </summary>
        public enum e_uptp_req : byte
        {
            /// <summary>request code for parameter "Hardware Revision" (command: RdID)</summary>
            UPTP_REQ_HARDWARE_REV = 0x01,

            /// <summary>request code for parameter "Bootloader Revision" (command: RdID)</summary>
            UPTP_REQ_BOOTLOADER_REV = 0x11,

            /// <summary>request code for parameter "Firmware Revision" (command: RdID)</summary>
            UPTP_REQ_FIRMWARE_REV = 0x12,

            /// <summary>request code for parameter "Number of Protocols" (command: RdID)</summary>
            UPTP_REQ_NUM_OF_PROTS = 0x20,

            /// <summary>request code for parameter "Protocol 1 Code" (command: RdID)</summary>
            UPTP_REQ_PROT1_CODE = 0x21,

            /// <summary>request code for parameter "Protocol 1 Revision" (command: RdID)</summary>
            UPTP_REQ_PROT1_REV = 0x22,

            /// <summary>request code for parameter "Protocol 2 Code" (command: RdID)</summary>
            UPTP_REQ_PROT2_CODE = 0x23,

            /// <summary>request code for parameter "Protocol 2 Revision" (command: RdID)</summary>
            UPTP_REQ_PROT2_REV = 0x24,

            /// <summary>request code for parameter "Protocol 3 Code" (command: RdID)</summary>
            UPTP_REQ_PROT3_CODE = 0x25,

            /// <summary>request code for parameter "Protocol 3 Revision" (command: RdID)</summary>
            UPTP_REQ_PROT3_REV = 0x26,

            /// <summary>request code for parameter "Protocol 4 Code" (command: RdID)</summary>
            UPTP_REQ_PROT4_CODE = 0x27,

            /// <summary>request code for parameter "Protocol 4 Revision" (command: RdID)</summary>
            UPTP_REQ_PROT4_REV = 0x28,

            /// <summary>request code for parameter "Serial Number" (command: RdID)</summary>
            UPTP_REQ_SERIAL_NUMBER = 0x30,

            /// <summary>request code for parameter "Service Req. Code" (command: RdStat)</summary>
            UPTP_REQ_SERVICE = 0x01,

            /// <summary>request code for parameter "Application State" (command: RdStat)</summary>
            UPTP_REQ_APPLICATION = 0x02,

            /// <summary>request code for parameter "Power State" (command: RdStat)</summary>
            UPTP_REQ_POWER = 0x11,

            /// <summary>request code for parameter "Indicator State" (command: RdStat)</summary>
            UPTP_REQ_INDICATOR = 0x12,

            /// <summary>
            ///     request code for parameter "Interface Supply State" (command: RdStat; values: on, off, no load, overload,
            ///     undervoltage)
            /// </summary>
            UPTP_REQ_INTF_SPLY = 0x21,

            /// <summary>request code for parameter "Interface State" (command: RdStat; values: in/out level, overload)</summary>
            UPTP_REQ_INTF = 0x22,

            /// <summary>request code for parameter "Interface Supply Voltage" (command: RdStat)</summary>
            UPTP_REQ_INTF_SPLYVOLTAGE = 0x23,

            /// <summary>request code for parameter "Interface Supply Voltage ON and connect with time delay" (command: WrCtrl)</summary>
            UPTP_REQ_SPLY_ON_COM = 0x24,

            /// <summary>request code for parameter "Active Interface protocol" (command: RdStat, WrCtrl)</summary>
            UPTP_REQ_ACTIVE_PROT = 0x31,

            /// <summary>
            ///     request code for parameter "Interface Com. State" (command: RdStat, WrCtrl; values: offline, connect, online,
            ///     error)
            /// </summary>
            UPTP_REQ_COM = 0x32,

            /// <summary>request code for parameter "Interface Com. Error code" (command: RdStat)</summary>
            UPTP_REQ_ERR = 0x33,

            /// <summary>request code for parameter "UPTP_Baudrate " (command: RdStat, WrCtrl)</summary>
            UPTP_REQ_BAUDRATE = 0x41,

            /// <summary>request code for parameter "UPTP Timing" (command: RdStat, WrCtrl)</summary>
            UPTP_REQ_TIMING = 0x42,

            /// <summary>request code for parameter "Reset UPT" (command: WrCtrl)</summary>
            UPTP_REQ_RESET_UPT = 0x00,

            /// <summary>request code for parameter "Reset Service Req. " (command: WrCtrl)</summary>
            UPTP_REQ_RESET_SERVICE_REQ = 0x01,

            /// <summary>
            ///     request code for parameter "Port1 Config." (command: RdCfg; values: enable, direction, polarity, config,
            ///     pullup, io)
            /// </summary>
            UPTP_REQ_PORT1_CFG = 0x01,

            /// <summary>
            ///     request code for parameter "Port2 Config." (command: RdCfg; values: enable, direction, polarity, config,
            ///     pullup, io)
            /// </summary>
            UPTP_REQ_PORT2_CFG = 0x02,

            /// <summary>
            ///     request code for parameter "Port3 Config." (command: RdCfg; values: enable, direction, polarity, config,
            ///     pullup, io)
            /// </summary>
            UPTP_REQ_PORT3_CFG = 0x03,

            /// <summary>request code for parameter "Sensor Config Detection" (command: RdCfg; values: none, n, p, pp)</summary>
            UPTP_REQ_SENSOR_CFG_DET = 0x11,

            /// <summary>request code for parameter "Output channel" (command: RdCfg; values: all, port 1,2,3)</summary>
            UPTP_REQ_OUT_CHANNEL = 0x12,

            /// <summary>request code for parameter "Input channel" (command: RdCfg; values: all, port 1,2,3)</summary>
            UPTP_REQ_IN_CHANNEL = 0x13,

            /// <summary>request code for parameter "Interface Baudrate Detection" (command: RdCfg; values: auto, value)</summary>
            UPTP_REQ_INTF_BAUDRATE_DET = 0x14,

            /// <summary>request code for parameter "Interface Baudrate" (command: RdCfg)</summary>
            UPTP_REQ_INTF_BAUDRATE = 0x15,

            /// <summary>request code for parameter "Input Threshold Voltage" (command: WrCfg, RdCfg)</summary>
            UPTP_REQ_INPUT_THRESHOLD = 0x16,

            /// <summary>request code for write buffer (command: WrData)</summary>
            UPTP_REQ_WRITE_BUFFER = 0x01,

            /// <summary>
            ///     request code for write buffer in queued mode (command: WrData) (UPT-CR-140225-17: added for queued
            ///     communication)
            /// </summary>
            UPTP_REQ_WRITE_BUFFER_QUEUED = 0x02,

            /// <summary>request code for read buffer (command: RdData)</summary>
            UPTP_REQ_READ_BUFFER = 0x01,

            /// <summary>request code for read raw buffer (command: RdData)</summary>
            UPTP_REQ_READ_RAW_BUFFER = 0x02,

            /// <summary>request code for read buffer in queued mode (command: RdData)</summary>
            UPTP_REQ_READ_BUFFER_QUEUED = 0x03,

            /// <summary>request code for firmware update (command: WrFwUpd)</summary>
            UPTP_REQ_ACTIVATION_CODE = 0xFF
        }

        #endregion

        private const int FWUPD_LAST_BLOCK = 0x3FFF; //!< the highest block number for a firmware update
        public const int ERR_CATEGORY_GENERAL = 0x0000; //!< general error category
        public const int ERR_CATEGORY_TX = 0x0100; //!< tx error category
        public const int ERR_CATEGORY_PAR = 0x0200; //!< parameter error category
        public const int ERR_CATEGORY_CMD = 0x0300; //!< command error category
        public const int ERR_CATEGORY_FWUPD = 0x0400; //!< firmware update error category
        public const int ERR_CATEGORY_USER = 0x0500; //!< user error category

        #region Nested type: BitfieldLengthAttribute

        [AttributeUsage(AttributeTargets.Field)]
        private sealed class BitfieldLengthAttribute : Attribute
        {
            public BitfieldLengthAttribute(uint length)
            {
                Length = length;
            }

            public uint Length { get; }
        }

        #endregion

        #region Nested type: PrimitiveConversion

        public static class PrimitiveConversion
        {
            public static byte ToByte<T>(T t) where T : struct
            {
                long r = 0;
                var offset = 0;

                // For every field suitably attributed with a BitfieldLength
                foreach (var f in t.GetType().GetFields())
                {
                    var attrs = f.GetCustomAttributes(typeof(BitfieldLengthAttribute), false);
                    if (attrs.Length == 1)
                    {
                        var fieldLength = ((BitfieldLengthAttribute)attrs[0]).Length;

                        // Calculate a bitmask of the desired length
                        long mask = 0;
                        for (var i = 0; i < fieldLength; i++)
                            mask |= 1 << i;

                        r |= ((byte)f.GetValue(t) & mask) << offset;

                        offset += (int)fieldLength;
                    }
                }

                return Convert.ToByte(r);
            }
        }

        #endregion

        #region Nested type: s_comSet

        public struct s_comSet
        {
            /// <summary>
            ///     used COM-Port, integer-representation
            /// </summary>
            public int comPortNr;

            /// <summary>
            ///     used COM-Port for UPT
            /// </summary>
            public string comPort;

            /// <summary>
            ///     last used COM-Port
            /// </summary>
            public string oldComPort;

            /// <summary>
            ///     type of output (PP, P, N)
            /// </summary>
            public e_slave_output_type type;

            /// <summary>
            ///     PORT1 = V15-4, PORT2 = V15-2
            /// </summary>
            public e_slave_com_port port;

            /// <summary>
            ///     input threshold of UPT-receiver in 1/10V
            /// </summary>
            public byte threshold;

            /// <summary>
            ///     delay between power-on and connect
            /// </summary>
            public double delay;

            /// <summary>
            ///     force a re-connect
            /// </summary>
            public bool forceConnect;

            /// <summary>
            ///     com-configuration of client changed
            /// </summary>
            public bool ClientConfChanged;

            /// <summary>
            ///     com-configuration of UPT changed
            /// </summary>
            public bool UPTConfChanged;

            /// <summary>
            ///     cycle power at connect or not
            /// </summary>
            public int cycle_power;
        }

        #endregion

        #region Nested type: s_information

        public struct s_information
        {
            [BitfieldLength(8)]
            /// <summary>bit 0-3: command code \see e_uptp_cmds</summary>
            public byte response_info;
        }

        #endregion

        #region Nested type: s_sspp_req

        /// <summary>
        ///     sspp request structure
        /// </summary>
        public struct s_sspp_req
        {
            public s_information info;

            /// <summary>
            ///     index
            /// </summary>
            private byte idx;

            /// <summary>
            ///     SSPP telegram length
            /// </summary>
            private byte obj_len;

            /// <summary>
            ///     pointer to data
            /// </summary>
            private byte[] p_obj_data;

            /// <summary>
            ///     error code of request
            /// </summary>
            private e_error_codes error_code;
        }

        #endregion

        #region Nested type: s_sspp_resp

        /// <summary>
        ///     sspp response public structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct s_sspp_resp
        {
            public s_information info;

            /// <summary>
            ///     index
            /// </summary>
            public byte idx;

            /// <summary>
            ///     UPTP telegram length
            /// </summary>
            public byte obj_len;

            /// <summary>
            ///     pointer to data
            /// </summary>
            public IntPtr p_obj_data;

            /// <summary>
            ///     error code of response (e.g. ID_NOT_AVAIL etc.)
            /// </summary>
            public e_error_codes error_code;
        }

        #endregion

        #region Nested type: s_uptp_resp

        /// <summary>
        ///     sspp request public structure
        /// </summary>
        public struct s_uptp_resp
        {
            public s_information info;

            /// <summary>
            ///     UPTP telegram length
            /// </summary>
            public byte obj_len;

            /// <summary>
            ///     pointer to data
            /// </summary>
            public IntPtr p_obj_data;

            /// <summary>
            ///     error code of response (e.g. ID_NOT_AVAIL etc.)
            /// </summary>
            public e_error_codes error_code;
        }

        #endregion
    }
}


